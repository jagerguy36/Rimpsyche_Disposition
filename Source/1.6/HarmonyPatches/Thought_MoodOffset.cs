using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Thought), "MoodOffset")]
    public static class Thought_MoodOffset
    {
        static void Postfix(ref float __result, Pawn ___pawn, Thought __instance)
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
            if(__instance.sourcePrecept != null)
            {
                __result *= compPsyche.Personality.Evaluate(PreceptMoodOffsetMultiplier);
            }
            if (ThoughtUtil.MoodMultiplierDB.TryGetValue(__instance.def.defName, out RimpsycheFormula multiplierMethod))
            {
                if(multiplierMethod != null)
                {
                    __result *= compPsyche.Personality.Evaluate(multiplierMethod);
                }
                
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

        public static RimpsycheFormula PreceptMoodOffsetMultiplier = new(
            "PreceptMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                //Openness concerns what could be right/wrong. Morality adhering to the right/wrong.
                //High open + High moral pawns will be open to question their right and wrong, but will still feel strongly about adhering to what is considered right.
                //So openness's effect here is very little, just enough to reflect their 'doubt' about the moral standard.
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * 0.45f;
                float opennessMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness) * 0.1f;
                return mult * moralityMult * opennessMult;
            }
        );


    }
}
