using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheTradePriceImprovementStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(TradePriceImprovementOffset);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_TradePriceImprovementOffset".Translate() + ": x" + compPsyche.Evaluate(TradePriceImprovementOffset).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula TradePriceImprovementOffset = new(
            "TradePriceImprovementOffset",
            (tracker) =>
            {
                float tradepriceOffset = 1f + 0.2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tact) * tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 2f);
                return tradepriceOffset;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
