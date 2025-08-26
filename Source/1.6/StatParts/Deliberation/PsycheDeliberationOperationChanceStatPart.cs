using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheDeliberationOperationChanceStatPart : StatPart
    {
        public const float levelC = 0.9f;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche?.Enabled == true)
                {
                    int level = pawn.skills.GetSkill(SkillDefOf.Medicine).Level;
                    val *= 1 + compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) / (levelC + level);
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
                    int level = pawn.skills.GetSkill(SkillDefOf.Medicine).Level;
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_DeliberationOperationChance".Translate() + ": x" + (1 + compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) / (levelC + level)).ToStringPercent() + "\n";
                }
            }
            return null;
        }
    }
}
