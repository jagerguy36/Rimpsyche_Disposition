using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(SkillRecord), "Learn")]
    public static class SkillRecord_Learn
    {
        private static void Prefix(Pawn ___pawn, float ___xpSinceMidnight, int ___levelInt, ref float xp)
        {
            if (xp > 0f)
            {
                var compPsyche = ___pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    float multiplier = compPsyche.Personality.Evaluate(SkillLearningMultiplier) * (float)(___levelInt - 10) + 1f;
                    xp = multiplier * xp;
                    if (___xpSinceMidnight > compPsyche.Personality.Evaluate(SkillMoodBuffxp))
                    {
                        compPsyche.ProgressMade(1f);
                    }
                    else if (___xpSinceMidnight > compPsyche.Personality.Evaluate(SkillNeutralxp))
                    {
                        compPsyche.ProgressMade(0f);
                    }
                }
            }
        }

        public static RimpsycheFormula SkillLearningMultiplier = new(
            "SkillLearningMultiplier",
            (tracker) =>
            {
                return tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Focus) * 0.05f;
            }
        );
        public static RimpsycheFormula SkillMoodBuffxp = new(
            "SkillMoodBuffxp",
            (tracker) =>
            {
                return 200f + 300f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
            }
        );
        public static RimpsycheFormula SkillNeutralxp = new(
            "SkillNeutralxp",
            (tracker) =>
            {
                return 100f + 100f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
            }
        );
    }
}
