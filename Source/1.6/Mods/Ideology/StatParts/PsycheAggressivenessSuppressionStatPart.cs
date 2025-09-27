using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheAggressivenessSuppressionStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(AggressivenessSuppression);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_AggressivenessSuppression".Translate() + ": " + compPsyche.Evaluate(AggressivenessSuppression).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula AggressivenessSuppression = new(
            "AggressivenessSuppression",
            (tracker) =>
            {
                float aggressiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness) * 0.1f;
                return aggressiveness;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
