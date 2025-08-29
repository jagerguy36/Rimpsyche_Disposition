using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheAimingDelayFactorPawnStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(AimingDelayFactorOffset);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_AimingDelayFactorOffset".Translate() + ": " + compPsyche.Evaluate(AimingDelayFactorOffset).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula AimingDelayFactorOffset = new(
            "AimingDelayFactorOffset",
            (tracker) =>
            {
                float diligence = 0.2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                return diligence;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
