using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationOperationChanceStatPart : StatPart
    {
        public const float levelC = 30f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche?.Enabled == true)
                {
                    int level = pawn.skills.GetSkill(SkillDefOf.Construction).Level;
                    val += (levelC - level) * compPsyche.Personality.Evaluate(DeliberationOperationMultiplier);
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
                    return "RP_Stat_DeliberationOperationChance".Translate() + ": " + compPsyche.Personality.Evaluate(DeliberationOperationMultiplier).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula DeliberationOperationMultiplier = new(
            "DeliberationOperationMultiplier",
            (tracker) =>
            {
                float diligence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.0035f;
                return diligence;
            }
        );
    }
}
