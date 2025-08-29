using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheShootingAccuracyPawnStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(ShootingAccuracyPawnOffset);
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
                    float val = compPsyche.Evaluate(ShootingAccuracyPawnOffset);
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ShootingAccuracyPawnOffset".Translate() + ": " + val.ToStringSign() + val.ToString("F1")+"\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula ShootingAccuracyPawnOffset = new(
            "ShootingAccuracyPawnOffset",
            (tracker) =>
            {
                float diligence = 4f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                return diligence;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
