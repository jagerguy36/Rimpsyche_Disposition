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
                var getMultiplierMethod = AccessTools.Method(typeof(Patch_NeedSeeker_NeedInterval), nameof(GetMultiplier));

                for (int i = 0; i < codes.Count - 2; i++)
                {
                    // seekerRisePerHour * 0.06
                    if (codes[i].opcode == OpCodes.Ldfld && codes[i].operand.ToString().Contains("seekerRisePerHour") &&
                        codes[i + 1].opcode == OpCodes.Ldc_R4 && (float)codes[i + 1].operand == 0.06f)
                    {
                        codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldarg_0));               // this
                        codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldfld, pawnField));      // this.pawn
                        codes.Insert(i + 4, new CodeInstruction(OpCodes.Ldc_I4_1));              // true = rising
                        codes.Insert(i + 5, new CodeInstruction(OpCodes.Call, getMultiplierMethod)); // call method
                        codes.Insert(i + 6, new CodeInstruction(OpCodes.Mul));
                        i += 6;
                    }

                    // seekerFallPerHour * 0.06
                    if (codes[i].opcode == OpCodes.Ldfld && codes[i].operand.ToString().Contains("seekerFallPerHour") &&
                        codes[i + 1].opcode == OpCodes.Ldc_R4 && (float)codes[i + 1].operand == 0.06f)
                    {
                        codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldarg_0));
                        codes.Insert(i + 3, new CodeInstruction(OpCodes.Ldfld, pawnField));
                        codes.Insert(i + 4, new CodeInstruction(OpCodes.Ldc_I4_0)); // false = falling
                        codes.Insert(i + 5, new CodeInstruction(OpCodes.Call, getMultiplierMethod));
                        codes.Insert(i + 6, new CodeInstruction(OpCodes.Mul));
                        i += 6;
                    }
                }

                return codes;
            }

            public static float GetMultiplier(Pawn pawn, bool isRising)
            {
                if (pawn == null)
                    return 1f;

                var psyche = pawn.compPsyche();
                if (psyche == null)
                    return 1f;

                return isRising ? psyche.Personality.GetMultiplier(MoodRisingSpeedMultiplier) : psyche.Personality.GetMultiplier(MoodFallingSpeedMultiplier);
            }


            public static RimpsycheMultiplier MoodRisingSpeedMultiplier = new(
                "MoodRisingSpeedMultiplier",
                (tracker) =>
                {
                    float mult = 1f;
                    float volatility = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Volatility, 1.5f);
                    return mult * volatility;
                }
            );

            public static RimpsycheMultiplier MoodFallingSpeedMultiplier = new(
                "MoodFallingSpeedMultiplier",
                (tracker) =>
                {
                    float mult = 1f;
                    float volatility = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Volatility, 3f);
                    return mult * volatility;
                }
            );
        }
    }
}
