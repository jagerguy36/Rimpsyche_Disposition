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
            if (___pawn?.compPsyche() is not { } compPsyche || __result == 0f)
                return;
            if (!compPsyche.Enabled)
                return;

            //Individual Thoughts
            if (useIndividualThoughtsSetting)
            {
                var hashval = __instance.def.shortHash;
                //Thoughts
                if (compPsyche.OpinionEvaluationCache.TryGetValue(hashval, out float value))
                {
                    if (value >= 0f) __result *= value;
                }
                else
                {
                    if (StageThoughtUtil.StageOpinionThoughtTagDB.TryGetValue(__instance.def.shortHash, out var stageFormulas))
                    {
                        int stageIndex = __instance.CurStageIndex;
                        if ((uint)stageIndex < (uint)stageFormulas.Length)
                        {
                            if (stageFormulas[__instance.CurStageIndex] is { } stageFormula)
                            {
                                __result *= compPsyche.Evaluate(stageFormula);
                            }
                        }
                    }
                    else if (ThoughtUtil.OpinionThoughtTagDB.TryGetValue(__instance.def.shortHash, out RimpsycheFormula indivFormula))
                    {
                        value = compPsyche.Evaluate(indivFormula);
                        compPsyche.OpinionEvaluationCache[hashval] = value;
                        __result *= value;
                    }
                    else
                    {
                        compPsyche.OpinionEvaluationCache[hashval] = -1f;
                    }
                }
            }
        }
    }

}
