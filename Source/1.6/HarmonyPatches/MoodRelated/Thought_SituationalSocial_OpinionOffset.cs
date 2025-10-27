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

            int stageIndex = __instance.CurStageIndex;
            int hashKey = (stageIndex << 16) | __instance.def.shortHash;
            var cache = compPsyche.OpinionEvaluationCache;
            //cache hit
            if (cache.TryGetValue(hashKey, out float value))
            {
                if (value >= 0f) __result *= value;
                return;
            }
            //cache miss
            //First try OpinionThoughtTagDB
            float eval = -1f;
            if (ThoughtUtil.OpinionThoughtTagDB.TryGetValue(__instance.def.shortHash, out RimpsycheFormula indivFormula))
            {
                eval = compPsyche.Evaluate(indivFormula);
                __result *= value;
            }
            else if (StageThoughtUtil.StageOpinionThoughtTagDB.TryGetValue(__instance.def.shortHash, out var stageFormulas))
            {
                if ((uint)stageIndex < (uint)stageFormulas.Length)
                {
                    var stageFormula = stageFormulas[stageIndex];
                    if (stageFormula != null)
                    {
                        eval = compPsyche.Evaluate(stageFormula);
                        __result *= eval;
                    }
                }
            }
            cache[hashKey] = eval;
        }
    }
}
