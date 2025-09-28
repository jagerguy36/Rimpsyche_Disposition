using Verse;
using System.Collections.Generic;
using RimWorld;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class ThoughtUtil
    {
        //Base Function
        private static readonly float MoodCurveC = RimpsycheDispositionSettings.moodIndividualC;
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