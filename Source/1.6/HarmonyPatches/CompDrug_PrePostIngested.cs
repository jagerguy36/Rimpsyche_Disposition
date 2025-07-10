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
                    // We are looking for the call to GetAddictivenessAtTolerance
                    if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo method && method == getAddictivenessMethod)
                    {
                        // The original code stores the result of the call in local variable 4.
                        // We want to intercept this value, pass it to our custom method,
                        // and then store the *new* result back into the same local variable.

                        // In the CIL, the call instruction will be followed by an instruction to store the result.
                        // In this case, it is `stloc.s 4`.
                        if (i + 1 < codes.Count && codes[i + 1].IsStloc())
                        {
                            var localBuilder = codes[i + 1].operand; // This captures the correct local variable index.

                            // We will insert our new instructions right after the original `stloc.s 4`.
                            int insertionIndex = i + 2;

                            var newInstructions = new List<CodeInstruction>
                    {
                        // Load the addiction chance that was just calculated and stored.
                        new CodeInstruction(OpCodes.Ldloc_S, localBuilder),
                        // Load the 'ingester' (pawn), which is the first argument of the original method.
                        new CodeInstruction(OpCodes.Ldarg_1),
                        // Call our custom method to get the adjusted addiction chance.
                        new CodeInstruction(OpCodes.Call, getAdjustedAddictionChanceMethod),
                        // Store the new, adjusted value back into the same local variable.
                        new CodeInstruction(OpCodes.Stloc_S, localBuilder)
                    };

                            codes.InsertRange(insertionIndex, newInstructions);

                            // Since we've found our target and patched the code, we can exit the loop.
                            break;
                        }
                    }
                }

                return codes;
            }

            // Note the change in the method signature to match our transpiler logic.
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
                return originalChance * psyche.Personality.GetMultiplier(AddictionChancedMultiplier);
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
