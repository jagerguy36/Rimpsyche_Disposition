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
                    int level = Mathf.Clamp(pawn.skills.GetSkill(SkillDefOf.Cooking).Level, 0, 20);
                    float deliberation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                    val *= FoodPoisonMultiplier(level, deliberation);
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
                    int level = Mathf.Clamp(pawn.skills.GetSkill(SkillDefOf.Cooking).Level, 0, 20);
                    float deliberation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_DeliberationFoodpoison".Translate() + ": x" + FoodPoisonMultiplier(level, deliberation).ToStringPercent()+"\n";
                }
            }
            return null;
        }

        public float FoodPoisonMultiplier(int level, float deliberation)
        {
            float mult;
            if (deliberation >= 0f)
            {
                mult = 1 - deliberation * (0.2f + 0.02f * level);
            }
            else
            {
                mult = 1 - deliberation * (0.5f - 0.01f * level);
            }
            return mult;

        }
    }
}
