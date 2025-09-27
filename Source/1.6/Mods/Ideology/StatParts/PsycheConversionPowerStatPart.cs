using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheConversionPowerStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(RP_ConversionP_Mult);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ConversionPowerMult".Translate() + ": x" + compPsyche.Evaluate(RP_ConversionP_Mult).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula RP_ConversionP_Mult = new(
            "RP_ConversionP_Mult",
            (tracker) =>
            {
                float confidence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float authenticity = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Authenticity);
                float conviction = (confidence - openness) * 0.5f;
                float sincerity = 0.25f * ((conviction * (3f + authenticity) + (1 - authenticity))); //-1~1
                float tact = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tact); //-1~1
                float result = 1f + 0.2f * (tact + sincerity);
                return result;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}