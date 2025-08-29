using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsychePainShockThresholdStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(PainShockThresholdOffset);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_PainShockThresholdOffset".Translate() + ": " + compPsyche.Evaluate(PainShockThresholdOffset).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula PainShockThresholdOffset = new(
            "PainShockThresholdOffset",
            (tracker) =>
            {
                float diligence = 0.2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery) + 0.15f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience);
                return diligence;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
