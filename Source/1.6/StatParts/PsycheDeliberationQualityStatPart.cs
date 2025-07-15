using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationQualityStatPart : StatPart
    {
        public const float levelC = 35f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche != null)
                {
                    val += compPsyche.Personality.Evaluate(DeliberationQualityMultiplier);
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
                    return "RP_Stat_DeliberationYield".Translate() + ": " + compPsyche.Personality.Evaluate(DeliberationQualityMultiplier).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationQualityMultiplier = new(
            "DeliberationQualityMultiplier",
            (tracker) =>
            {
                float diligence = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.2f;
                return diligence;
            }
        );
    }
}
