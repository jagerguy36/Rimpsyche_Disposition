using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheIdeoSpreadStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(RP_IdeoSpread_Mult);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_IdeoSpreadMult".Translate() + ": " + compPsyche.Evaluate(RP_IdeoSpread_Mult).ToStringPercentSigned() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula RP_IdeoSpread_Mult = new(
            "RP_IdeoSpread_Mult",
            (tracker) =>
            {
                float confidence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float conviction = (confidence - openness) * 0.5f;
                float zealousy = 0.25f * ((conviction * (3f - passion) + (1 + passion))); //-1~1
                float talkativeness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Talkativeness); //-1~1
                float spread = 0.5f * (talkativeness + zealousy);

                if (spread >= 0f)
                {
                    return 0.5f * spread + 1f;
                }
                else
                {
                    return spread / 3f + 1f;
                }
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}