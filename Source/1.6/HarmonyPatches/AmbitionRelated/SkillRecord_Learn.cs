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
                    float multiplier = compPsyche.Evaluate(SkillLearningMultiplier) * (float)(Mathf.Clamp(___levelInt,0,20) - 10) + 1f;
                    xp = multiplier * xp;
                    if (!RimpsycheDispositionSettings.useSenseOfProgress) return;
                    if (___xpSinceMidnight > compPsyche.Evaluate(SkillMoodBuffxp))
                    {
                        compPsyche.ProgressMade(1f);
                    }
                    else if (___xpSinceMidnight > compPsyche.Evaluate(SkillNeutralxp))
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
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SkillMoodBuffxp = new(
            "SkillMoodBuffxp",
            (tracker) =>
            {
                return 400f + 200f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SkillNeutralxp = new(
            "SkillNeutralxp",
            (tracker) =>
            {
                return 150f + 100f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
