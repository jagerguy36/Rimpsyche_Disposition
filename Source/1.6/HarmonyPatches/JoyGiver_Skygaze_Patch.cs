using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(JoyGiver_Skygaze), "GetChance")]
    public static class JoyGiver_Skygaze_Patch
    {
        static void Postfix(ref float __result, Pawn pawn)
        {
            if (pawn?.compPsyche() is not { } compPsyche || __result == 0f) return;
            if (compPsyche?.Enabled != true) return;
            __result *= compPsyche.Evaluate(SkyGazeChanceMultiplier);
        }


        public static RimpsycheFormula SkyGazeChanceMultiplier = new(
            "SkyGazeChanceMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float imaginationMult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 1.5f);
                return mult * imaginationMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}