using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheTenacitySuppressionFallStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(TenacitySuppressionFall);
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
                    return "RP_Stat_TenacitySuppressionFall".Translate() + ": " + compPsyche.Evaluate(TenacitySuppressionFall).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula TenacitySuppressionFall = new(
            "TenacitySuppressionFall",
            (tracker) =>
            {
                float tenacity = -tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity) * 0.15f;
                return tenacity;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
