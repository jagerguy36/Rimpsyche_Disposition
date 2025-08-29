using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Verse.AI;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(MentalBreaker), "TestMoodMentalBreak")]
    public static class MentalBreaker_TestMoodMentalBreak_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo mtbEventOccursMethod = AccessTools.Method(typeof(Rand), nameof(Rand.MTBEventOccurs));
            FieldInfo pawnField = AccessTools.Field(typeof(MentalBreaker), "pawn");
            MethodInfo getMultiplierMethod = AccessTools.Method(typeof(MentalBreaker_TestMoodMentalBreak_Patch), nameof(GetMultiplier));
            var code = new List<CodeInstruction>(instructions);
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].Calls(mtbEventOccursMethod))
                {
                    var instructionsToInsert = new List<CodeInstruction>
                    {
                        new CodeInstruction(OpCodes.Ldarg_0),
                        new CodeInstruction(OpCodes.Ldfld, pawnField),
                        new CodeInstruction(OpCodes.Call, getMultiplierMethod),
                        new CodeInstruction(OpCodes.Mul)
                    };
                    code.InsertRange(i - 2, instructionsToInsert);
                    i += instructionsToInsert.Count;
                }
            }
            return code;
        }
        //Small -> Frequent
        public static float GetMultiplier(Pawn pawn)
        {
            if (pawn == null)
            {
                return 1f;
            }
            var psyche = pawn.compPsyche();
            if (psyche?.Enabled != true)
            {
                return 1f;
            }
            return psyche.Evaluate(MentalBreakIntervalMultiplier);
        }

        public static RimpsycheFormula MentalBreakIntervalMultiplier = new(
            "MentalBreakIntervalMultiplier",
            (tracker) =>
            {
                float discipline = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Resilience, 2f);
                return discipline;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}