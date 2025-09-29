using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(ThoughtHandler), nameof(ThoughtHandler.MoodOffsetOfGroup))]
    public static class ThoughtHandler_MoodOffsetOfGroup_Patch
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            var moodOffsetMethod = AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            var moodMultiplierMethod = AccessTools.Method(typeof(ThoughtHandler_MoodOffsetOfGroup_Patch), nameof(MoodMultiplier));
            var pawnField = AccessTools.Field(typeof(ThoughtHandler), nameof(ThoughtHandler.pawn));
            int thoughtLocalIndex = -1;
            int insertionPoint = -1;
            for (int t = 0; t < codes.Count; t++)
            {
                var ci = codes[t];
                if (ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi == moodOffsetMethod)
                {
                    thoughtLocalIndex = codes[t-1].LocalIndex();
                    
                    insertionPoint = t;
                    break;
                }
            }
            if (thoughtLocalIndex == -1 || insertionPoint == -1)
            {
                Log.Error("[Rimpsyche] Mood thought transpiler failed to patch");
            }
            else
            {
                for (int i = 0; i < codes.Count; i++)
                {
                    yield return codes[i];

                    if (i == insertionPoint)
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_0); // load this
                        yield return new CodeInstruction(OpCodes.Ldfld, pawnField); // load pawn
                        yield return new CodeInstruction(OpCodes.Ldloc_S, thoughtLocalIndex); // load thought
                        yield return new CodeInstruction(OpCodes.Call, moodMultiplierMethod); // call MoodMultiplier
                    }
                }
            }
        }

        public static float MoodMultiplier(float originalOffset, Pawn pawn, Thought thought)
        {
            if (originalOffset == 0f || pawn?.compPsyche() is not { } compPsyche)
                return originalOffset;
            if (compPsyche.Enabled != true)
                return originalOffset;

            float result = originalOffset;
            if (originalOffset < 0f)
            {
                //General Modifier
                result *= compPsyche.Evaluate(FormulaDB.NegativeMoodOffsetMultiplier);
                if (Find.TickManager.TicksGame < compPsyche.lastResilientSpiritTick)
                {
                    result *= 0.5f;
                }
            }
            else
            {
                result *= compPsyche.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }
            //Individual Thoughts
            if (useIndividualThoughtsSetting)
            {
                var hashval = thought.def.shortHash;
                //Thoughts
                if (compPsyche.ThoughtEvaluationCache.TryGetValue(hashval, out float value))
                {
                    if (value >= 0f) result *= value;
                }
                else
                {
                    if (StageThoughtUtil.StageMoodThoughtTagDB.TryGetValue(thought.def.shortHash, out var stageFormulas))
                    {
                        int stageIndex = thought.CurStageIndex;
                        if ((uint)stageIndex < (uint)stageFormulas.Length)
                        {
                            if (stageFormulas[thought.CurStageIndex] is { } stageFormula)
                            {
                                result *= compPsyche.Evaluate(stageFormula);
                            }
                        }
                    }
                    else if (ThoughtUtil.MoodThoughtTagDB.TryGetValue(thought.def.shortHash, out RimpsycheFormula indivFormula))
                    {
                        value = compPsyche.Evaluate(indivFormula);
                        compPsyche.ThoughtEvaluationCache[hashval] = value;
                        result *= value;
                    }
                    else
                    {
                        compPsyche.ThoughtEvaluationCache[hashval] = -1f;
                    }
                }
            }
            //Log.Message($"{pawn.Name} thought with defname {thought.def.defName} | original {num} became {result}");
            return result;
        }
    }
}