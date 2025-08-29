using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationButcherEfficiency : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(DeliberationButcherEfficiencyyMultiplier);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "DeliberationButcherEfficiency".Translate() + ": x" + compPsyche.Evaluate(DeliberationButcherEfficiencyyMultiplier).ToStringPercent()+"\n";
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
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
