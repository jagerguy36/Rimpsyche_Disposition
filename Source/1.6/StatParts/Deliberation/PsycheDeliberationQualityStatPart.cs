using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationQualityStatPart : StatPart //Tend Quality
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val *= compPsyche.Personality.Evaluate(DeliberationQualityMultiplier);
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
                    return "RP_Stat_DeliberationTendQuality".Translate() + ": x" + compPsyche.Personality.Evaluate(DeliberationQualityMultiplier).ToStringPercent();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationQualityMultiplier = new(
            "DeliberationQualityMultiplier",
            (tracker) =>
            {
                float diligence = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.2f; //Self-tend quality penalty is *0.7, so worst case is almost as bad as selftend
                return diligence;
            }
        );
    }
}
