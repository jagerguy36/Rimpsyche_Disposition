using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheResilienceSuppressionFallStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(ResilienceSuppressionFall);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ResilienceSuppressionFall".Translate() + ": " + compPsyche.Evaluate(ResilienceSuppressionFall).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula ResilienceSuppressionFall = new(
            "ResilienceSuppressionFall",
            (tracker) =>
            {
                float resilience = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience) * 0.3f;
                return resilience;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
