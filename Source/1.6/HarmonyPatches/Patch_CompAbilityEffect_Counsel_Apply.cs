using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;


namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("PerformanceModeThought")]
    [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "Apply")]
    public static class Patch_CompAbilityEffect_Counsel_Apply
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var moodOffsetMethod = AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            var moodMultiplierMethod = AccessTools.Method(typeof(Patch_CompAbilityEffect_Counsel_Apply), nameof(MoodMultiplier));
            var parentField = AccessTools.Field(typeof(AbilityComp), nameof(AbilityComp.parent));
            var pawnField = AccessTools.Field(typeof(Ability), nameof(Ability.pawn));

            for (int i = 0; i < codes.Count; i++)
            {
                var ci = codes[i];
                yield return ci;
                if (ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi == moodOffsetMethod)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // load this
                    yield return new CodeInstruction(OpCodes.Ldfld, parentField); // load pawn
                    yield return new CodeInstruction(OpCodes.Ldfld, pawnField); // load pawn
                    yield return new CodeInstruction(codes[i - 1]); // load thought
                    yield return new CodeInstruction(OpCodes.Call, moodMultiplierMethod); // call MoodMultiplier
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
            //Log.Message($"{pawn.Name} thought with defname {thought.def.defName} | originalOffset {originalOffset} became {result}");
            return result;
        }
    }
}