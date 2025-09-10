using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    [HarmonyPatch(typeof(SlaveRebellionUtility), "IncrementSuppression")]
    public static class SlaveRebellionUtility_IncrementSuppression
    {
        static void Prefix(ref float suppressionIncrementPct, Pawn recipient)
        {
            if (recipient?.compPsyche() is not { } compPsyche || suppressionIncrementPct == 0f) return;
            if (compPsyche?.Enabled != true) return;
            suppressionIncrementPct *= compPsyche.Evaluate(ResistSuppressionMult);
        }


        public static RimpsycheFormula ResistSuppressionMult = new(
            "ResistSuppressionMult",
            (tracker) =>
            {
                float mult = 1f;
                float resilience = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Resilience, 10f / 12f);
                float bravery = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Bravery, 10f / 12f);
                return mult * resilience * bravery;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
