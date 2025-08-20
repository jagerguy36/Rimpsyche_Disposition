using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheTradePriceImprovementStatPart : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Personality.Evaluate(TradePriceImprovementOffset);
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
                    return "RP_Stat_TradePriceImprovementOffset".Translate() + ": " + compPsyche.Personality.Evaluate(TradePriceImprovementOffset).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula TradePriceImprovementOffset = new(
            "TradePriceImprovementOffset",
            (tracker) =>
            {
                float diligence = 0.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tact) * tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 2f);
                return diligence;
            }
        );
    }
}
