using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDiligenceWorkspeedOffset : StatPart// M 0.85 ~ 1.15
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val += compPsyche.Personality.Evaluate(DiligenceWorkspeedOffset);
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
                    return "RP_Stat_DiligenceWorkspeed".Translate() + ": " + compPsyche.Personality.Evaluate(DiligenceWorkspeedOffset).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula DiligenceWorkspeedOffset = new(
            "DiligenceWorkspeedOffset",
            (tracker) =>
            {
                float diligence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence) * 0.2f;
                return diligence;
            }
        );
    }
}
