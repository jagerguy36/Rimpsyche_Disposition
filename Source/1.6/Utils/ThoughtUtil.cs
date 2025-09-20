using Verse;
using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class ThoughtUtil
    {
        //Base Function
        private static readonly float MoodCurveC = RimpsycheDispositionSettings.moodPreceptC;
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

        public static Dictionary<string, RimpsycheFormula> MoodThoughtTagDB = [];
        public static Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB = [];

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
            Log.Message("[Rimpsyche - Disposition] ThoughtUtil initialized.");
            AddBaseThoughts();
        }

        public static void ModCompat()
        {
            Log.Message("[Rimpsyche - Disposition] Compatibility Thoughts added.");
        }

        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla(MoodThoughtTagDB, OpinionThoughtTagDB);
        }
        
    }
}