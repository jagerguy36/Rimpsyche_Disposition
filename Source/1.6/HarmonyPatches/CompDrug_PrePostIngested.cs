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
                    if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo method && method == getAddictivenessMethod)
                    {
                        if (i + 1 < codes.Count && codes[i + 1].IsStloc())
                        {
                            var localBuilder = codes[i + 1].operand;
                            int insertionIndex = i + 2;

                            var newInstructions = new List<CodeInstruction>
                    {
                        new CodeInstruction(OpCodes.Ldloc_S, localBuilder),
                        new CodeInstruction(OpCodes.Ldarg_1),
                        new CodeInstruction(OpCodes.Call, getAdjustedAddictionChanceMethod),
                        new CodeInstruction(OpCodes.Stloc_S, localBuilder)
                    };

                            codes.InsertRange(insertionIndex, newInstructions);
                            break;
                        }
                    }
                }

                return codes;
            }

            public static float GetAdjustedAddictionChance(float originalChance, Pawn pawn)
            {
                if (originalChance >= 1f || pawn == null)
                {
                    return originalChance;
                }
                var psyche = pawn.compPsyche();
                if (psyche == null)
                {
                    return originalChance;
                }
                return originalChance * psyche.Personality.Evaluate(AddictionChancedMultiplier);
            }


            public static RimpsycheFormula AddictionChancedMultiplier = new(
                "AddictionChancedMultiplier",
                (tracker) =>
                {
                    float mult = 1f;
                    float discipline = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 0.5f);
                    return mult * discipline;
                }
            );
        }
    }
}
