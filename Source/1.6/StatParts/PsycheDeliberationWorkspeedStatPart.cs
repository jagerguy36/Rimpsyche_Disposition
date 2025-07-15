using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationWorkspeedStatPart : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val += compPsyche.Personality.Evaluate(DeliberationWorkspeedOffset);
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
                    return "RP_Stat_DeliberationWorkspeed".Translate() + ": " + compPsyche.Personality.Evaluate(DeliberationWorkspeedOffset).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationWorkspeedOffset = new(
            "DeliberationWorkspeedOffset",
            (tracker) =>
            {
                float diligence = -tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.2f;
                return diligence;
            }
        );
    }
}
