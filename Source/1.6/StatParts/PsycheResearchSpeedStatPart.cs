using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheResearchSpeedStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(ReflectivenessResearchSpeedMult);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ReflectivenessResearchSpeed".Translate() + ": x" + compPsyche.Evaluate(ReflectivenessResearchSpeedMult).ToStringPercent()+"\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula ReflectivenessResearchSpeedMult = new(
            "ReflectivenessResearchSpeedMult",
            (tracker) =>
            {
                float reflectiveness = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness) * 0.1f;
                return reflectiveness;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
