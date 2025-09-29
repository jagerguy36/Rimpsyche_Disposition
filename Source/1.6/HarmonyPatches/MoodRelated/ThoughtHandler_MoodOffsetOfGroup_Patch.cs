using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
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
            var defField = AccessTools.Field(typeof(Thought), nameof(Thought.def));
            var shortHashField = AccessTools.Field(typeof(ThoughtDef), nameof(ThoughtDef.shortHash));

            for (int i = 0; i < codes.Count; i++)
            {
                yield return codes[i];

                if (codes[i].opcode == OpCodes.Stloc_0 && i > 0 && codes[i - 1].opcode == OpCodes.Add)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0); // load num
                    yield return new CodeInstruction(OpCodes.Ldloc_0); // load num (for first arg)
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // load this
                    yield return new CodeInstruction(OpCodes.Ldfld, pawnField); // load pawn
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 4); // load thought
                    yield return new CodeInstruction(OpCodes.Call, moodMultiplierMethod); // call MoodMultiplier
                    yield return new CodeInstruction(OpCodes.Mul); // num * MoodMultiplier(...)
                    yield return new CodeInstruction(OpCodes.Stloc_0); // store back to num
                }
            }
        }

        public static float MoodMultiplier(float num, Pawn pawn, Thought thought)
        {
            float mult = 1f;
            if (num == 0f || pawn?.compPsyche() is not { } compPsyche)
                return mult;
            if (compPsyche.Enabled != true)
                return mult;
            if (num < 0f)
            {
                //General Modifier
                mult = compPsyche.Evaluate(FormulaDB.NegativeMoodOffsetMultiplier);
                if (Find.TickManager.TicksGame < compPsyche.lastResilientSpiritTick)
                {
                    mult *= 0.5f;
                }
            }
            else
            {
                mult = compPsyche.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }
            //Individual Thoughts
            if (useIndividualThoughtsSetting)
            {
                var hashval = thought.def.shortHash;
                //Thoughts
                if (compPsyche.ThoughtEvaluationCache.TryGetValue(hashval, out float value))
                {
                    if (value >= 0f) mult *= value;
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
                                mult *= compPsyche.Evaluate(stageFormula);
                            }
                        }
                    }
                    else if (ThoughtUtil.MoodThoughtTagDB.TryGetValue(thought.def.shortHash, out RimpsycheFormula indivFormula))
                    {
                        value = compPsyche.Evaluate(indivFormula);
                        compPsyche.ThoughtEvaluationCache[hashval] = value;
                        mult *= value;
                    }
                    else
                    {
                        compPsyche.ThoughtEvaluationCache[hashval] = -1f;
                    }
                }
            }
            //Log.Message($"{pawn.Name} caled thought with defname {thought.def.defName} and num {num} got mult {mult}");
            return mult;
        }
    }
}