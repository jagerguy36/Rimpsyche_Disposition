using HarmonyLib;
using System.Reflection;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class CompDrug_PrePostIngested
    {
        [HarmonyPatch(typeof(CompDrug), nameof(CompDrug.PrePostIngested))]
        public static class CompDrug_PrePostIngested_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var getAddictivenessMethod = AccessTools.Method(typeof(DrugStatsUtility), nameof(DrugStatsUtility.GetAddictivenessAtTolerance));
                var getAdjustedAddictionChanceMethod = AccessTools.Method(typeof(CompDrug_PrePostIngested_Patch), nameof(GetAdjustedAddictionChance));
                var codes = new List<CodeInstruction>(instructions);

                for (int i = 0; i < codes.Count; i++)
                {
                    yield return codes[i];
                    if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo method && method == getAddictivenessMethod)
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Call, getAdjustedAddictionChanceMethod);
                    }
                }
            }

            public static float GetAdjustedAddictionChance(float originalChance, Pawn pawn)
            {
                if (originalChance >= 1f || pawn == null)
                {
                    return originalChance;
                }
                var psyche = pawn.compPsyche();
                if (psyche?.Enabled != true)
                {
                    return originalChance;
                }
                return originalChance * psyche.Evaluate(AddictionChancedMultiplier);
            }


            public static RimpsycheFormula AddictionChancedMultiplier = new(
                "AddictionChancedMultiplier",
                (tracker) =>
                {
                    float discipline = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 0.5f);
                    return discipline;
                },
            RimpsycheFormulaManager.FormulaIdDict
            );
        }
    }
}
