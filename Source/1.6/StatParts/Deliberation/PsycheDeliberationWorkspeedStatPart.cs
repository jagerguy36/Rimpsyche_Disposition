using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationWorkspeedStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(DeliberationWorkspeedOffset);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_DeliberationWorkspeed".Translate() + ": x" + compPsyche.Evaluate(DeliberationWorkspeedOffset).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationWorkspeedOffset = new(
            "DeliberationWorkspeedOffset",
            (tracker) =>
            {
                float diligence = 1f-tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.2f;
                return diligence;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
