using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationCookingSpeedStatPart : StatPart
    {
        private const float mult = 10f / 3f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(DeliberationCookspeedOffset);
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
                    float offsetvalue = compPsyche.Evaluate(DeliberationCookspeedOffset);
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_DeliberationWorkspeed".Translate() + ": " + offsetvalue.ToStringSign() + offsetvalue.ToString("F1") + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationCookspeedOffset = new(
            "DeliberationCookspeedOffset",
            (tracker) =>
            {
                return -tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
