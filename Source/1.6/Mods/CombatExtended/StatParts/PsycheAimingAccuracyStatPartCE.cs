using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.CombatExtended
{
    public class PsycheAimingAccuracyStatPartCE : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(AimingAccuracyPartCE);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_AimingAccuracyPartCE".Translate() + ": x" + compPsyche.Evaluate(AimingAccuracyPartCE).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula AimingAccuracyPartCE = new(
            "AimingAccuracyPartCE",
            (tracker) =>
            {
                float deliberation = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Deliberation, 1.1f);
                return deliberation;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
