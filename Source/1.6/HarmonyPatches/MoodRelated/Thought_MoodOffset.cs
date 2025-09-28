using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch]
    public static class Thought_MoodOffset
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            yield return AccessTools.Method(typeof(Thought_Situational_Precept_SlavesInColony), nameof(Thought_Situational_Precept_SlavesInColony.MoodOffset));
        }

        static void Postfix(ref float __result, Pawn ___pawn, Thought __instance)
        {
            if (__result == 0f || ___pawn?.compPsyche() is not { } compPsyche)
                return;
            if (compPsyche.Enabled != true)
                return;
            //Mood multiplier
            if (__result < 0f)
            {
                //General Modifier
                __result *= compPsyche.Evaluate(FormulaDB.NegativeMoodOffsetMultiplier);
                if (Find.TickManager.TicksGame < compPsyche.lastResilientSpiritTick)
                {
                    __result *= 0.5f;
                }
            }            
            else
            {
                __result *= compPsyche.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }

            //Individual Thoughts
            if (useIndividualThoughtsSetting)
            {
                var hashval = __instance.def.shortHash;
                //Thoughts
                if (compPsyche.ThoughtEvaluationCache.TryGetValue(hashval, out float value))
                {
                    if (value >= 0f) __result *= value;
                }
                else
                {
                    if (StageThoughtUtil.StageMoodThoughtTagDB.TryGetValue(__instance.def.shortHash, out var stageFormulas))
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
                    else if (ThoughtUtil.MoodThoughtTagDB.TryGetValue(__instance.def.shortHash, out RimpsycheFormula indivFormula))
                    {
                        value = compPsyche.Evaluate(indivFormula);
                        compPsyche.ThoughtEvaluationCache[hashval] = value;
                        __result *= value;
                    }
                    else
                    {
                        compPsyche.ThoughtEvaluationCache[hashval] = -1f;
                    }
                }
            }
        }
    }
}
