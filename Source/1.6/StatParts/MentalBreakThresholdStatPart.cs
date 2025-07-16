using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheVolatilityMentalBreakThreasholdOffset : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val += compPsyche.Personality.Evaluate(TensionMentalBreakThresholdOffset);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    return "RP_Stat_MentalBreakThreashold".Translate() + ": " + compPsyche.Personality.Evaluate(TensionMentalBreakThresholdOffset).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula TensionMentalBreakThresholdOffset = new(
            "TensionMentalBreakThresholdOffset",
            (tracker) =>
            {
                float resilience = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension) * 0.2f;
                return resilience;
            }
        );
    }
}
