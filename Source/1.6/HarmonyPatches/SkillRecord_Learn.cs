using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(SkillRecord), "Learn")]
    public static class SkillRecord_Learn
    {
        private static void Prefix(Pawn ___pawn, int ___levelInt, ref float xp)
        {
            if (xp > 0f)
            {
                var compPsyche = ___pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    float multiplier = compPsyche.Personality.Evaluate(SkillLearningMultiplier) * (float)(___levelInt - 10) + 1f;
                    xp = multiplier * xp;
                    compPsyche.lastProgressTick = Find.TickManager.TicksGame;
                }
            }
        }

        public static RimpsycheFormula SkillLearningMultiplier = new(
            "SkillLearningMultiplier",
            (tracker) =>
            {
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Focus) * 0.05f;
                return optimism;
            }
        );
    }
}
