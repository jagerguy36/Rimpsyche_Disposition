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
                if (Find.TickManager.TicksGame < compPsyche.lastResilientSpiritTick) result *= 0.5f;
            }
            else
            {
                result *= compPsyche.Evaluate(FormulaDB.PositiveMoodOffsetMultiplier);
            }
            //Individual Thoughts
            if (!useIndividualThoughtsSetting) return result;

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
            AddBaseThoughts();
            ModCompat();
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
    }

    //TODO: move to proper place, modularize formula registry
    //JoyGiverUtil.cs
    [StaticConstructorOnStartup]
    public static class JoyGiverUtil
    {
        private static readonly bool useIndividualJoychanceSetting = RimpsycheDispositionSettings.useIndividualJoychance;
        private static readonly float JoyChanceCurveC = RimpsycheDispositionSettings.joychanceIndividualC;
        public static Dictionary<int, RimpsycheFormula> JoyChanceDB = [];

        static JoyGiverUtil()
        {
            Initialize();
        }

        public static void Initialize()
        {
            AddBaseJoyGivers();
            ModCompat();
        }
        private static void AddBaseJoyGivers()
        {
            JoyGiverDB.AddDefs_Vanilla(JoyChanceDB);
        }
        public static void ModCompat()
        {
            JoyGiverDB.AddDefs_Mods(JoyChanceDB);
        }

        public static float JoychanceCurve(float mood)
        {
            if (mood >= 0)
            {
                return 1f + (JoyChanceCurveC - (JoyChanceCurveC / ((2f * mood) + 1f)));
            }
            else
            {
                //Needs tweak
                return 1f + ((JoyChanceCurveC / (1f - (2f * mood))) - JoyChanceCurveC);
            }
        }
    }

    //JoyGiverDB.cs

    public class JoyGiverDB
    {
        public static void AddDefs_Vanilla(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            // RegisterJoyChanceMultiplier("JoyGiver_Meditate", JoyChanceDB, MeditateChanceMultiplier)
        }
        public static void AddDefs_Mods(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            if (ModsConfig.IsActive("dubwise.dubsbadhygiene"))
            {
                // RegisterJoyChanceMultiplier("WatchWashingMachine", JoyChanceDB, MeditateChanceMultiplier)
            }
        }


        public static void RegisterJoyChanceMultiplier(string defName, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            var thoughtDef = DefDatabase<JoyGiverDef>.GetNamed(defName, false);
            if (thoughtDef != null)
            {
                targetDb[thoughtDef.shortHash] = value;
            }
            else
            {
                Log.Warning($"[Rimpsyche] Could not find JoyGiverDef named '{defName}'.");
            }
        }
    }

    //Harmony
    //TODO: move to proper folder
    [HarmonyPatch(typeof(JoyGiver), "GetChance")]
    public static class JoyGiver_GetChance_Patch
    {
        public static void Postfix(ref float __result, JoyGiver __instance, Pawn pawn)
        {
            if (__result == 0f || pawn == null)
                return;
            var compPsyche = pawn.compPsyche();
            if (compPsyche == null || !compPsyche.Enabled)
                return;

            //TODO: implement joychance cache
            if (JoyChanceDB.TryGetValue(__instance.def.shorthash,  out RimpsycheFormula joychanceFormula))
            {
                if (joychanceFormula != null)
                {
                    __result *= compPsyche.Evaluate(thoughtFormula);
                }
            }
        }
    }
}