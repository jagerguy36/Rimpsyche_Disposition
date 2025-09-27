using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class PsycheBraveryTerrorStatPart: StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(BraveryTerrorMultiplier);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_BraveryTerrorMultiplier".Translate() + ": x" + compPsyche.Evaluate(BraveryTerrorMultiplier).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula BraveryTerrorMultiplier = new(
            "BraveryTerrorMultiplier",
            (tracker) =>
            {
                float bravery = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery) * 0.5f;
                return bravery;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
