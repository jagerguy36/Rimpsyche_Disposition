using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class NeedSeeker_NeedInterval
    {
        [HarmonyPatch(typeof(Need_Seeker), nameof(Need_Seeker.NeedInterval))]
        public static class Patch_NeedSeeker_NeedInterval
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);

                var pawnField = AccessTools.Field(typeof(Need), "pawn");
                var getRisingMultiplierMethod = AccessTools.Method(typeof(Patch_NeedSeeker_NeedInterval), nameof(GetRisingMultiplier));
                var getFallingMultiplierMethod = AccessTools.Method(typeof(Patch_NeedSeeker_NeedInterval), nameof(GetFallingMultiplier));

                for (int i = 0; i < codes.Count - 2; i++)
                {
                    // seekerRisePerHour * 0.06
                    if (codes[i].opcode == OpCodes.Ldfld && codes[i].operand.ToString().Contains("seekerRisePerHour") &&
                        codes[i + 1].opcode == OpCodes.Ldc_R4 && (float)codes[i + 1].operand == 0.06f)
                    {
                        codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldarg_0));               // this
                        codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldfld, pawnField));      // this.pawn
                        codes.Insert(i + 4, new CodeInstruction(OpCodes.Call, getRisingMultiplierMethod)); // call method
                        codes.Insert(i + 5, new CodeInstruction(OpCodes.Mul));
                        i += 5;
                    }

                    // seekerFallPerHour * 0.06
                    if (codes[i].opcode == OpCodes.Ldfld && codes[i].operand.ToString().Contains("seekerFallPerHour") &&
                        codes[i + 1].opcode == OpCodes.Ldc_R4 && (float)codes[i + 1].operand == 0.06f)
                    {
                        codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldarg_0));
                        codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldfld, pawnField));
                        codes.Insert(i + 4, new CodeInstruction(OpCodes.Call, getFallingMultiplierMethod));
                        codes.Insert(i + 5, new CodeInstruction(OpCodes.Mul));
                        i += 5;
                    }
                }

                return codes;
            }

            public static float GetRisingMultiplier(Pawn pawn)
            {
                if (pawn == null)
                    return 1f;

                var psyche = pawn.compPsyche();
                if (psyche?.Enabled != true)
                    return 1f;

                return psyche.Evaluate(MoodRisingSpeedMultiplier);
            }

            public static float GetFallingMultiplier(Pawn pawn)
            {
                if (pawn == null)
                    return 1f;

                var psyche = pawn.compPsyche();
                if (psyche?.Enabled != true)
                    return 1f;

                return psyche.Evaluate(MoodFallingSpeedMultiplier);
            }


            public static RimpsycheFormula MoodRisingSpeedMultiplier = new(
                "MoodRisingSpeedMultiplier",
                (tracker) =>
                {
                    float mult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Stability, 0.75f); //-1: 1.3333 || 1: 0.75
                    return mult;
                },
                RimpsycheFormulaManager.FormulaIdDict
            );

            public static RimpsycheFormula MoodFallingSpeedMultiplier = new(
                "MoodFallingSpeedMultiplier",
                (tracker) =>
                {
                    float mult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Stability, 0.2f); //-1: 5 || 1: 0.2
                    return mult;
                },
                RimpsycheFormulaManager.FormulaIdDict
            );
        }
    }
}
