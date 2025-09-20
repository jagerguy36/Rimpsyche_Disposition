using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.CombatExtended
{
    public class PsycheSuppressabilityStatPartCE : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(SuppressabilityPartCE);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_SuppressabilityPartCE".Translate() + ": " + compPsyche.Evaluate(SuppressabilityPartCE).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula SuppressabilityPartCE = new(
            "SuppressabilityPartCE",
            (tracker) =>
            {
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                return - 0.2f*bravery;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
