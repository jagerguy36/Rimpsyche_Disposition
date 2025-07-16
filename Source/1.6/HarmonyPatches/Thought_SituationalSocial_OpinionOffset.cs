using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Thought_SituationalSocial), "OpinionOffset")]
    public static class Thought_SituationalSocial_OpinionOffset
    {
        static void Postfix(ref float __result, Pawn ___pawn, ThoughtDef ___def)
        {
            if (___pawn?.compPsyche() is not { } compPsyche || __result == 0f)
                return;
            if(___def.sourcePrecept != null)
            {
                __result *= compPsyche.Personality.Evaluate(Thought_MoodOffset.MoralityMoodOffsetMultiplier);
            }
        }
    }
}
