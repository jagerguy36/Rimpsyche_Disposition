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
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Thought_SituationalSocial), nameof(Thought_SituationalSocial.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_Tale), nameof(Thought_Tale.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_MemorySocial), nameof(Thought_MemorySocial.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_MemorySocialCumulative), nameof(Thought_MemorySocialCumulative.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_ChemicalInterestVsTeetotaler), nameof(Thought_ChemicalInterestVsTeetotaler.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_TeetotalerVsChemicalInterest), nameof(Thought_TeetotalerVsChemicalInterest.OpinionOffset));
            yield return AccessTools.Method(typeof(Thought_HardWorkerVsLazy), nameof(Thought_HardWorkerVsLazy.OpinionOffset));
        }
        static void Postfix(ref float __result, Pawn ___pawn, Thought __instance)
        {
            if (__result == 0f || ___pawn == null)
                return;
            var compPsyche = ___pawn.compPsyche();
            if (compPsyche == null || !compPsyche.Enabled)
                return;

            //Individual Thoughts
            if (!useIndividualThoughtsSetting) return;
            //var originalOffset = __result;
            int stageIndex = __instance.CurStageIndex;
            int hashKey = (stageIndex << 16) | __instance.def.shortHash;
            var cache = compPsyche.ThoughtEvaluationCache;
            //cache hit
            if (cache.TryGetValue(hashKey, out float value))
            {
                if (value >= 0f) __result *= value;
                return;
            }
            //cache miss
            //First try OpinionThoughtTagDB
            float eval = -1f;
            if (ThoughtUtil.ThoughtTagDB.TryGetValue(hashKey, out RimpsycheFormula opinionFormula))
            {
                //Log.Message($"{___pawn.Name} registered {__instance.def.defName} with stage: {__instance.CurStageIndex}. Key: {hashKey}");
                if (opinionFormula != null)
                {
                    eval = compPsyche.Evaluate(opinionFormula);
                    __result *= eval;
                }
            }
            //if (eval < 0) Log.Message($"{___pawn.Name} blacklisted {__instance.def.defName} with stage: {__instance.CurStageIndex}");
            cache[hashKey] = eval;
            //Log.Message($"{___pawn.Name} opinion with defname {__instance.def.defName} | originalOffset {originalOffset} became {__result}");
        }
    }
}
