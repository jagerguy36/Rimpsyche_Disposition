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
            if (compPsyche.Enabled != true)
                return;
            if (__instance.sourcePrecept != null)
            {
                __result *= compPsyche.Personality.Evaluate(FormulaDB.PreceptMoodOffsetMultiplier);
            }
            if (__result < 0f)
            {
                __result *= compPsyche.Personality.Evaluate(FormulaDB.NegativeMoodOffsetMultiplier);
            }            
            else
            {
                __result *= compPsyche.Personality.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }
            if (ThoughtUtil.MoodMultiplierDB.TryGetValue(__instance.def.defName, out RimpsycheFormula multiplierMethod))
            {
                if (multiplierMethod != null)
                {
                    __result *= compPsyche.Personality.Evaluate(multiplierMethod);
                }
            }
        }


    }
}
