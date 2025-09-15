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
            if (___pawn?.compPsyche() is not { } compPsyche || __result == 0f)
                return;
            if (compPsyche.Enabled != true)
                return;
            //General Mood multiplier
            if (__result < 0f)
            {
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
            float indiv_val = 0f;
            //Ideo thought multiplier
            if (__instance.sourcePrecept?.def.issue != null)
            {
                if (ThoughtUtil.IssueMultiplierDB.TryGetValue(__instance.sourcePrecept.def.issue.defName, out RimpsycheFormula issueFormula))
                {
                    if (issueFormula != null)
                    {
                        indiv_val += compPsyche.Evaluate(issueFormula);
                    }
                }
            }

            //Individual thought multiplier
            if (useIndividualThoughtsSetting)
            {
                //Thought specific multiplier
                if (ThoughtUtil.MoodMultiplierDB.TryGetValue(__instance.def.defName, out RimpsycheFormula indivFormula))
                {
                    if (indivFormula != null)
                    {
                        indiv_val += compPsyche.Evaluate(indivFormula);
                    }
                }

                //Thought-stage-specific multiplier
                else if (StageThoughtUtil.StageMoodMultiplierDB.TryGetValue(__instance.def.defName, out var stageFormulas))
                {
                    int stageIndex = __instance.CurStageIndex;
                    if ((uint)stageIndex < (uint)stageFormulas.Length)
                    {
                        var formula = stageFormulas[stageIndex];
                        if (formula != null)
                        {
                            indiv_val += compPsyche.Evaluate(formula);
                        }
                    }
                }
            }
            __result *= ThoughtUtil.MoodMultCurve(indiv_val);
        }
    }
}
