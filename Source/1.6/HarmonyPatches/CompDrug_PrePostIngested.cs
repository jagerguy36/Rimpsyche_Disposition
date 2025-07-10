using HarmonyLib;
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

                var getAdjustedAddictionChanceMethod = AccessTools.Method(typeof(CompDrug_PrePostIngested_Patch), nameof(GetAdjustedAddictionChance));

                List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo method &&
                        method.DeclaringType == typeof(DrugStatsUtility) && method.Name == "GetAddictivenessAtTolerance")
                    {
                        if (i + 1 < codes.Count && (codes[i + 1].opcode == OpCodes.Stloc_S || codes[i + 1].opcode == OpCodes.Stloc_2))
                        {
                            int insertionIndex = i + 2;

                            List<CodeInstruction> newInstructions = new List<CodeInstruction>();
                            newInstructions.Add(new CodeInstruction(codes[i + 1].opcode == OpCodes.Stloc_S ? OpCodes.Ldloc_S : OpCodes.Ldloc_2, codes[i + 1].operand));
                            newInstructions.Add(new CodeInstruction(OpCodes.Ldarg_1));
                            newInstructions.Add(new CodeInstruction(OpCodes.Call, getAdjustedAddictionChanceMethod));
                            newInstructions.Add(new CodeInstruction(OpCodes.Mul));
                            newInstructions.Add(new CodeInstruction(codes[i + 1].opcode, codes[i + 1].operand));
                            codes.InsertRange(insertionIndex, newInstructions);
                            break;
                        }
                    }
                }
                return codes;
            }

            public static float GetAdjustedAddictionChance(Pawn pawn, float original)
            {
                if (original >=1f || pawn == null)
                {
                    return original;
                }
                var psyche = pawn.compPsyche();
                if (psyche == null)
                    return original;

                return original * psyche.Personality.GetMultiplier(AddictionChancedMultiplier);
            }


            public static RimpsycheMultiplier AddictionChancedMultiplier = new(
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
