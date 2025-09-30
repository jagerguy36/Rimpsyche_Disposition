using Verse;
using System.Collections.Generic;
using RimWorld;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class ThoughtUtil
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;
        private static readonly float MoodCurveC = RimpsycheDispositionSettings.moodIndividualC;
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

        //Base Function
        public static float MoodMultCurve(float mood)
        {
            if (mood >= 0)
            {
                return 1f + (MoodCurveC - (MoodCurveC / ((2f * mood) + 1f)));
            }
            else
            {
                return 1f + ((MoodCurveC / (1f - (2f * mood))) - MoodCurveC);
            }
        }

        public static Dictionary<int, RimpsycheFormula> MoodThoughtTagDB = [];
        public static Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB = [];

        static ThoughtUtil()
        {
            if (RimpsycheDispositionSettings.useIndividualThoughts)
            {
                Initialize();
                ModCompat();
            }
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] Using individual thoughts. ThoughtUtil initialized.");
            AddBaseThoughts();
        }

        public static void ModCompat()
        {
            Log.Message("[Rimpsyche - Disposition] Using individual thoughts. Compatibility Thoughts added.");
        }

        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.RoyaltyActive) RoyaltyDB.AddDefs_Royalty(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.IdeologyActive) IdeologyDB.AddDefs_Ideology(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.BiotechActive) BiotechDB.AddDefs_Biotech(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.AnomalyActive) AnomalyDB.AddDefs_Anomaly(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.OdysseyActive) OdysseyDB.AddDefs_Odyssey(MoodThoughtTagDB, OpinionThoughtTagDB);
        }
        
    }
}