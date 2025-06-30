using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheVolatilityMentalBreakThreasholdOffset : StatPart// M 0.85 ~ 1.15
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val += VolatilityMentalBreakThresholdOffset(compPsyche);
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
                    return "RP_Stat_MentalBreakThreashold".Translate() + VolatilityMentalBreakThresholdOffset(compPsyche).ToStringPercent();
                }
            }
            return null;
        }

        private float VolatilityMentalBreakThresholdOffset(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Volatility)*0.2f;
        }
    }
}
