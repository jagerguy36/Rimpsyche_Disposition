using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("PrecisionModeThought")]
    [HarmonyPatch]
    public static class Thought_MoodOffset
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            yield return AccessTools.Method(typeof(Thought_Situational_Precept_SlavesInColony), nameof(Thought_Situational_Precept_SlavesInColony.MoodOffset));
            yield return AccessTools.Method(typeof(Thought_Situational_Recluse), nameof(Thought_Situational_Recluse.MoodOffset));
        }

        static void Postfix(ref float __result, Pawn ___pawn, Thought __instance)
        {
            __result = ThoughtUtil.MoodMultiplier(__result, ___pawn, __instance);
        }
    }
}
