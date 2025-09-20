using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.CombatExtended
{
    public class PsycheShootingAccuracyPawnStatPartCE : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(ShootingAccuracyPartCE);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ShootingAccuracyPartCE".Translate() + ": x" + compPsyche.Evaluate(ShootingAccuracyPartCE).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula ShootingAccuracyPartCE = new(
            "ShootingAccuracyPartCE",
            (tracker) =>
            {
                float deliberation = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Deliberation, 1.2f);
                return deliberation;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
