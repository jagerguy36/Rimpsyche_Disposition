using Verse;
using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public class StageThoughtUtil
    {
        public static Dictionary<string, RimpsycheFormula[]> StageMoodThoughtTagDB = [];
        public static Dictionary<string, RimpsycheFormula[]> StageOpinionThoughtTagDB = [];

        static StageThoughtUtil()
        {
            if (RimpsycheDispositionSettings.useIndividualThoughts)
            {
                Initialize();
                ModCompat();
            }
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] StageThoughtUtil initialized.");
            AddBaseThoughts();
        }

        public static void ModCompat()
        {
            Log.Message("[Rimpsyche - Disposition] Compatibility Thoughts added.");
        }

        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla_Stage(StageMoodThoughtTagDB, StageOpinionThoughtTagDB);
        }
    }
}