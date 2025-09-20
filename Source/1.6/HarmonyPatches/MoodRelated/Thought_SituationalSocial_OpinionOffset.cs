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
                //Thoughts
                if (StageThoughtUtil.StageOpinionThoughtTagDB.TryGetValue(__instance.def.defName, out var stageFormulas))
                {
                    int stageIndex = __instance.CurStageIndex;
                    if ((uint)stageIndex < (uint)stageFormulas.Length)
                    {
                        var formula = stageFormulas[stageIndex];
                        if (formula != null)
                        {
                            __result *= compPsyche.Evaluate(formula);
                        }
                    }
                }
                else if (ThoughtUtil.OpinionThoughtTagDB.TryGetValue(__instance.def.defName, out RimpsycheFormula indivFormula))
                {
                    if (indivFormula != null)
                    {
                        __result *= compPsyche.Evaluate(indivFormula);
                    }
                }
            }
        }
    }

}
