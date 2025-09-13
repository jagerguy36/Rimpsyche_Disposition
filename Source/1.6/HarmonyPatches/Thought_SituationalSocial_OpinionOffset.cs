using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch]
    public static class Thought_SituationalSocial_OpinionOffset
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Thought_SituationalSocial), nameof(Thought_SituationalSocial.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_Tale), nameof(Thought_Tale.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_MemorySocial), nameof(Thought_MemorySocial.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_MemorySocialCumulative), nameof(Thought_MemorySocialCumulative.OpinionOffset));
            //Thought_ChemicalInterestVsTeetotaler
            //Thought_TeetotalerVsChemicalInterest
            //Thought_HardWorkerVsLazy
        }
        static void Postfix(ref float __result, Pawn ___pawn, Thought __instance)
        {
            if (___pawn?.compPsyche() is not { } compPsyche || __result == 0f)
                return;
            if (!compPsyche.Enabled)
                return;

            if (__instance.sourcePrecept != null)
            {
                __result *= compPsyche.Evaluate(FormulaDB.PreceptMoodOffsetMultiplier);
            }
        }
    }

}
