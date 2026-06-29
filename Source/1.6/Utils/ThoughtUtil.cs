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
            if (originalOffset == 0f || pawn == null)
                return originalOffset;
            var compPsyche = pawn.compPsyche();
            if (compPsyche == null || !compPsyche.Enabled)
                return originalOffset;

            float result = originalOffset;
            //General Modifier
            if (originalOffset < 0f)
            {
                result *= compPsyche.Evaluate(FormulaDB.NegativeMoodOffsetMultiplier);
                if (Find.TickManager.TicksGame < compPsyche.lastResilientSpiritTick)
                    result *= 0.5f;
            }
            else
            {
                result *= compPsyche.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }
            //Individual Thoughts
            if (!useIndividualThoughtsSetting)
                return result;

            int stageIndex = thought.CurStageIndex;
            int hashKey = (stageIndex << 16) | thought.def.shortHash;
            var cache = compPsyche.ThoughtEvaluationCache;
            //cache hit
            if (cache.TryGetValue(hashKey, out float value))
            {
                if (value >= 0f) result *= value;
                return result;
            }
            //cache miss
            float eval = -1f;
            if (ThoughtUtil.MoodThoughtTagDB.TryGetValue(hashKey, out RimpsycheFormula thoughtFormula))
            {
                //Log.Message($"{pawn.Name} registered {thought.def.defName} with stage: {thought.CurStageIndex}. Key: {hashKey}");
                if (thoughtFormula != null)
                {
                    eval = compPsyche.Evaluate(thoughtFormula);
                    result *= eval;
                }
            }
            //if (eval <0) Log.Message($"{pawn.Name} blacklisted {thought.def.defName} with stage: {thought.CurStageIndex}");
            cache[hashKey] = eval;
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
                Log.Message("[Rimpsyche - Disposition] Using individual thoughts. ThoughtUtil initialized.");
                Initialize();
            }
        }

        public static void Initialize()
        {
            LoadModCompatibility();
            // AddBaseThoughts();
            // ModCompat();
        }
        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla(MoodThoughtTagDB, OpinionThoughtTagDB);
        }
        public static void ModCompat()
        {
            if (ModsConfig.RoyaltyActive) RoyaltyDB.AddDefs_Royalty(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.IdeologyActive) IdeologyDB.AddDefs_Ideology(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.BiotechActive) BiotechDB.AddDefs_Biotech(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.AnomalyActive) AnomalyDB.AddDefs_Anomaly(MoodThoughtTagDB, OpinionThoughtTagDB);
            if (ModsConfig.OdysseyActive) OdysseyDB.AddDefs_Odyssey(MoodThoughtTagDB, OpinionThoughtTagDB);
            MiscModDB.AddDefs_MiscMods(MoodThoughtTagDB, OpinionThoughtTagDB);
        }
        public static void LoadModCompatibility()
        {
            List<string> integratedMods = new List<string>();
            foreach (var compDef in DefDatabase<RimpsycheCompatibilityDef>.AllDefs)
            {
                var modId = compDef.modContentPack.packageIdInt;
                foreach (var mapping in compDef.moodThoughtMaps)
                {
                    RimpsycheFormula formula = GetFormulaFromTag(mapping.tag);
                    if (formula != null)
                    {
                        RegisterThoughts(mapping.defNames, MoodThoughtTagDB, formula);
                    }
                }
                foreach (var mapping in compDef.opinionThoughtMaps)
                {
                    RimpsycheFormula formula = GetFormulaFromTag(mapping.tag);
                    if (formula != null)
                    {
                        RegisterThoughts(mapping.defNames, OpinionThoughtTagDB, formula);
                    }
                }

                foreach (var mapping in compDef.stageMoodThoughtMaps)
                {
                    if (thought != null)
                    {
                        RimpsycheFormula[] formulaStages = new RimpsycheFormula[mapping.stages.Count];
                        for (int i = 0; i < mapping.stageTags.Count; i++)
                        {
                            formulaStages[i] = GetFormulaFromTag(mapping.stages[i]);
                        }
                        RegisterStageThought(mapping.defName, MoodThoughtTagDB, formulaStages);
                    }
                }

                foreach (var mapping in compDef.stageOpinionThoughtMaps)
                {
                    if (thought != null)
                    {
                        RimpsycheFormula[] formulaStages = new RimpsycheFormula[mapping.stages.Count];
                        for (int i = 0; i < mapping.stageTags.Count; i++)
                        {
                            formulaStages[i] = GetFormulaFromTag(mapping.stages[i]);
                        }
                        RegisterStageThought(mapping.defName, OpinionThoughtTagDB, formulaStages);
                    }
                }

                integratedMods.Add(modId);
            }

            if (integratedMods.Count > 0)
            {
                Log.Message("[Rimpsyche - Disposition] tagged thoughts from: " + string.Join(", ", integratedMods));
            }
        }
        private static RimpsycheFormula GetFormulaFromTag(ThoughtTag tag)
        {
            switch (tag)
            {
                case ThoughtTag.None: return null;
                case ThoughtTag.Tag_Affluence: return FormulaDB.Tag_Affluence;
                case ThoughtTag.Tag_Morality: return FormulaDB.Tag_Morality;
                case ThoughtTag.Tag_Judgemental: return FormulaDB.Tag_Judgemental;
                default:
                    Log.Warning($"[Rimpsyche - Disposition] Unhandled or invalid ThoughtTag discovered: {tag}");
                    return null;
            }
        }
    }

    //Harmony
}