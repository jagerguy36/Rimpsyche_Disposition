using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationYieldChanceStatPart : StatPart
    {
        public const float levelC = 35f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche != null)
                {

                    int level = pawn.skills.GetSkill(SkillDefOf.Construction).Level;
                    val += (levelC - level) * compPsyche.Personality.Evaluate(DeliberationYieldMultiplier);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    return "RP_Stat_DeliberationYield".Translate() + ": " + compPsyche.Personality.Evaluate(DeliberationYieldMultiplier).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationYieldMultiplier = new(
            "DeliberationYieldMultiplier",
            (tracker) =>
            {
                float diligence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.003f;
                return diligence;
            }
        );
    }
}
