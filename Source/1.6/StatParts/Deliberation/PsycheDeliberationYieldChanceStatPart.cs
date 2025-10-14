using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationYieldChanceStatPart : StatPart
    {
        public SkillDef skill;
        public const float levelC = 6f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche?.Enabled == true)
                {
                    int level = Mathf.Clamp(pawn.skills.GetSkill(skill).Level, 0, 20);
                    val *= 1 + compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) / (levelC + level);
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
                    int level = Mathf.Clamp(pawn.skills.GetSkill(skill).Level, 0, 20);
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_DeliberationYield".Translate() + ": x" + (1 + compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) / (levelC + level)).ToStringPercent() + "\n";
                }
            }
            return null;
        }
    }
}
