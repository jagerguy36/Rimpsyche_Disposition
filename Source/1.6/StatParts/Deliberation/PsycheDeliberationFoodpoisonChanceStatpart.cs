using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationFoodpoisonChanceStatpart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche?.Enabled == true)
                {
                    int level = pawn.skills.GetSkill(SkillDefOf.Cooking).Level;
                    var deliberation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                    val = val * (1 - deliberation * 0.2f);
                    val += additionalValue(level, deliberation);
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
                    int level = pawn.skills.GetSkill(SkillDefOf.Cooking).Level;
                    var deliberation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                    return "RP_Stat_DeliberationFoodpoison".Translate() + ": x" + (1 - deliberation * 0.2f).ToStringPercent();
                }
            }
            return null;
        }

        public float additionalValue(int level, float deliberation)
        {
            float addition;
            if (deliberation >= 0f)
            {
                addition = -level * deliberation * 0.00005f;
            }
            else
            {
                addition = deliberation * (level * 0.0002f - 0.005f);
            }
            return addition;

        }
    }
}
