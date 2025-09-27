using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheCertaintyLossStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(RP_CertaintyLossMult);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_CertaintyLossMult".Translate() + ": x" + compPsyche.Evaluate(RP_CertaintyLossMult).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula RP_CertaintyLossMult = new(
            "RP_CertaintyLossMult",
            (tracker) =>
            {
                float confidence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                float conviction = (confidence - openness) * 0.5f;
                float shake = trust - conviction;
                if (shake >= 0f)
                {
                    return 0.5f * shake + 1f;
                }
                else
                {
                    return shake / 3f + 1f;
                }
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}