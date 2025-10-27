using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheMentalBreakThresholdStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(TensionMentalBreakThresholdOffset);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_MentalBreakThreashold".Translate() + ": " + compPsyche.Evaluate(TensionMentalBreakThresholdOffset).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula TensionMentalBreakThresholdOffset = new(
            "TensionMentalBreakThresholdOffset",
            (tracker) =>
            {
                float Tension = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension) * 0.1f;
                return Tension;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
