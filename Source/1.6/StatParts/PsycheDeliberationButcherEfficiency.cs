using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationButcherEfficiency : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val *= compPsyche.Personality.Evaluate(DeliberationButcherEfficiencyyMultiplier);
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
                    return "DeliberationButcherEfficiencyy".Translate() + ": x" + compPsyche.Personality.Evaluate(DeliberationButcherEfficiencyyMultiplier).ToStringPercent();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationButcherEfficiencyyMultiplier = new(
            "DeliberationButcherEfficiencyyMultiplier",
            (tracker) =>
            {
                float deliberation = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.2f;
                return deliberation;
            }
        );
    }
}
