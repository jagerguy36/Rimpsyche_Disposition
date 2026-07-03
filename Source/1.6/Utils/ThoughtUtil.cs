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
            if (ThoughtTagDB.TryGetValue(hashKey, out RimpsycheFormula tagFormula))
            {
                //Log.Message($"{pawn.Name} registered {thought.def.defName} with stage: {thought.CurStageIndex}. Key: {hashKey}");
                if (tagFormula != null)
                {
                    eval = compPsyche.Evaluate(tagFormula);
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

        public static Dictionary<int, RimpsycheFormula> ThoughtTagDB = [];

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
            TagThoughts();
        }
        public static void TagThoughts()
        {
            HashSet<string> integratedMapDefs = new HashSet<string>();
            foreach (var mapDef in DefDatabase<ThoughtTagMappingDef>.AllDefs)
            {
                foreach (var mapping in mapDef.thoughtMaps)
                {
                    RimpsycheFormula formula = GetFormulaFromTag(mapping.tag);
                    if (formula != null)
                    {
                        RegisterThoughts(mapping.defNames, ThoughtTagDB, formula);
                    }
                }

                foreach (var mapping in mapDef.stageThoughtMaps)
                {
                    List<RimpsycheFormula> formulaStages = new List<RimpsycheFormula>(mapping.stageTags.Count);
                    for (int i = 0; i < mapping.stageTags.Count; i++)
                    {
                        formulaStages.Add(GetFormulaFromTag(mapping.stageTags[i]));
                    }
                    RegisterStageThought(mapping.defName, ThoughtTagDB, formulaStages);
                }
                integratedMapDefs.Add(mapDef.label??mapDef.defName);
            }

            if (integratedMapDefs.Count > 0)
            {
                Log.Message("[Rimpsyche - Disposition] tagged thoughts from: " + string.Join(", ", integratedMapDefs));
            }
        }
        public static void RegisterThoughts(IEnumerable<string> defNames, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            foreach (var defName in defNames)
            {
                var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
                if (thoughtDef != null)
                {
                    //Log.Message($"Tagging ThoughDef: {thoughtDef.defName}");
                    targetDb[(0 << 16) | thoughtDef.shortHash] = value;
                }
                else
                {
                    Log.Warning($"[Rimpsyche - Disposition] Could not find ThoughtDef named '{defName}' to tag.");
                }
            }
        }
        public static void RegisterStageThought(string defName, Dictionary<int, RimpsycheFormula> targetDb, List<RimpsycheFormula> values)
        {
            var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
            if (thoughtDef != null)
            {
                //Log.Message($"Tagging ThoughDef: {thoughtDef.defName}");
                int stageCount = thoughtDef.stages?.Count ?? 0;
                if (values.Count != stageCount)
                {
                    Log.Error($"[Rimpsyche - Disposition] Mismatch in '{defName}': Provided stage tag formulas count ({values.Count}) does not match ThoughtDef stages count ({stageCount}).");
                    return;
                }
                for (int i = 0; i < values.Count; i++)
                {
                    targetDb[(i << 16) | thoughtDef.shortHash] = values[i];
                }
            }
            else
            {
                Log.Warning($"[Rimpsyche - Disposition] Could not find ThoughtDef named '{defName}' to tag.");
            }
        }
        private static RimpsycheFormula GetFormulaFromTag(ThoughtTag tag)
        {
            switch (tag)
            {
                case ThoughtTag.None: return null;
                case ThoughtTag.Tag_Empathy: return FormulaDB.Tag_Empathy;
                case ThoughtTag.Tag_Empathy_Bond: return FormulaDB.Tag_Empathy_Bond;
                case ThoughtTag.Tag_Empathy_Kin: return FormulaDB.Tag_Empathy_Kin;
                case ThoughtTag.Tag_Empathy_Loved: return FormulaDB.Tag_Empathy_Loved;
                case ThoughtTag.Tag_Empathy_J: return FormulaDB.Tag_Empathy_J;
                case ThoughtTag.Tag_Empathy_M: return FormulaDB.Tag_Empathy_M;
                case ThoughtTag.Tag_Sympathy: return FormulaDB.Tag_Sympathy;
                case ThoughtTag.Tag_Sympathy_J: return FormulaDB.Tag_Sympathy_J;
                case ThoughtTag.Tag_Sympathy_M: return FormulaDB.Tag_Sympathy_M;
                case ThoughtTag.Tag_Sympathy_P: return FormulaDB.Tag_Sympathy_P;
                case ThoughtTag.Tag_Worry: return FormulaDB.Tag_Worry;
                case ThoughtTag.Tag_Worry_Bond: return FormulaDB.Tag_Worry_Bond;
                case ThoughtTag.Tag_Worry_Kin: return FormulaDB.Tag_Worry_Kin;
                case ThoughtTag.Tag_Worry_Loved: return FormulaDB.Tag_Worry_Loved;
                case ThoughtTag.Tag_Worry_Outsider: return FormulaDB.Tag_Worry_Outsider;
                case ThoughtTag.Tag_Worry_Outsider_M: return FormulaDB.Tag_Worry_Outsider_M;
                case ThoughtTag.Tag_Worry_M: return FormulaDB.Tag_Worry_M;
                case ThoughtTag.Tag_Worry_J: return FormulaDB.Tag_Worry_J;
                case ThoughtTag.Tag_Disquiet: return FormulaDB.Tag_Disquiet;
                case ThoughtTag.Tag_Harmed: return FormulaDB.Tag_Harmed;
                case ThoughtTag.Tag_Harmed_Bond: return FormulaDB.Tag_Harmed_Bond;
                case ThoughtTag.Tag_Harmed_Kin: return FormulaDB.Tag_Harmed_Kin;
                case ThoughtTag.Tag_Harmed_Loved: return FormulaDB.Tag_Harmed_Loved;
                case ThoughtTag.Tag_Affluence: return FormulaDB.Tag_Affluence;
                case ThoughtTag.Tag_Needy: return FormulaDB.Tag_Needy;
                case ThoughtTag.Tag_Needy_Art: return FormulaDB.Tag_Needy_Art;
                case ThoughtTag.Tag_Fear: return FormulaDB.Tag_Fear;
                case ThoughtTag.Tag_SawDeath: return FormulaDB.Tag_SawDeath;
                case ThoughtTag.Tag_SawDeath_Kin: return FormulaDB.Tag_SawDeath_Kin;
                case ThoughtTag.Tag_SawDeath_Outsider: return FormulaDB.Tag_SawDeath_Outsider;
                case ThoughtTag.Tag_Charity_J: return FormulaDB.Tag_Charity_J;
                case ThoughtTag.Tag_Charity_M: return FormulaDB.Tag_Charity_M;
                case ThoughtTag.Tag_Decency: return FormulaDB.Tag_Decency;
                case ThoughtTag.Tag_Decency_J: return FormulaDB.Tag_Decency_J;
                case ThoughtTag.Tag_Decency_M: return FormulaDB.Tag_Decency_M;
                case ThoughtTag.Tag_Art: return FormulaDB.Tag_Art;
                case ThoughtTag.Tag_Gathering: return FormulaDB.Tag_Gathering;
                case ThoughtTag.Tag_Recluse: return FormulaDB.Tag_Recluse;
                case ThoughtTag.Tag_Concert: return FormulaDB.Tag_Concert;
                case ThoughtTag.Tag_Bloodlust: return FormulaDB.Tag_Bloodlust;
                case ThoughtTag.Tag_Outsider: return FormulaDB.Tag_Outsider;
                case ThoughtTag.Tag_Loved: return FormulaDB.Tag_Loved;
                case ThoughtTag.Tag_Bond: return FormulaDB.Tag_Bond;
                case ThoughtTag.Tag_Morality: return FormulaDB.Tag_Morality;
                case ThoughtTag.Tag_Judgemental: return FormulaDB.Tag_Judgemental;
                case ThoughtTag.Tag_Preference: return FormulaDB.Tag_Preference;
                case ThoughtTag.Tag_Openmindedness: return FormulaDB.Tag_Openmindedness;
                case ThoughtTag.Tag_JustifiedGuilt: return FormulaDB.Tag_JustifiedGuilt;
                default:
                    Log.Warning($"[Rimpsyche - Disposition] Unhandled or invalid ThoughtTag discovered: {tag}");
                    return null;
            }
        }
    }
}