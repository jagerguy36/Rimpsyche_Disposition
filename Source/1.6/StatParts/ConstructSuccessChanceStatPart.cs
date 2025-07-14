using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ConstructSuccessChanceStatPart : StatPart// M 0.85 ~ 1.15
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (pawn.skills != null && compPsyche != null)
                {

                    int level = pawn.skills.GetSkill(SkillDefOf.Construction).Level;
                    val += (30 - level) * compPsyche.Personality.Evaluate(ConstructSuccessChanceDeliberationMultiplier);
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
                    return "RP_Stat_DeliberationWorkspeed".Translate() + ": " + compPsyche.Personality.Evaluate(ConstructSuccessChanceDeliberationMultiplier).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula ConstructSuccessChanceDeliberationMultiplier = new(
            "ConstructSuccessChanceDeliberationMultiplier",
            (tracker) =>
            {
                float diligence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation) * 0.0035f;
                return diligence;
            }
        );
    }
}
