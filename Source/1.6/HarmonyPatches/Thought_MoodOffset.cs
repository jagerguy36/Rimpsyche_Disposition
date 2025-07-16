using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Thought), "MoodOffset")]
    public static class Thought_MoodOffset
    {
        static void Postfix(ref float __result, Pawn ___pawn, ThoughtDef ___def)
        {
            if (___pawn?.compPsyche() is not { } compPsyche || __result == 0f)
                return;
            if (__result < 0f)
            {
                __result *= compPsyche.Personality.Evaluate(NegativeMoodOffsetMultiplier);
            }            
            else
            {
                __result *= compPsyche.Personality.Evaluate(PositiveMoodOffsetMultiplier);
            }
            if(___def.sourcePrecept != null)
            {
                __result *= compPsyche.Personality.Evaluate(MoralityMoodOffsetMultiplier);
            }
        }


        public static RimpsycheFormula PositiveMoodOffsetMultiplier = new(
            "PositiveMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            }
        );

        public static RimpsycheFormula NegativeMoodOffsetMultiplier = new(
            "NegativeMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            }
        );

        public static RimpsycheFormula MoralityMoodOffsetMultiplier = new(
            "MoralityMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * 0.45f;
                return mult * moralityMult;
            }
        );

        
    }
}
