using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public class StageThoughtUtil
    {
        public static Dictionary<int, RimpsycheFormula[]> StageMoodThoughtTagDB = [];
        public static Dictionary<int, RimpsycheFormula[]> StageOpinionThoughtTagDB = [];

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
            Log.Message("[Rimpsyche - Disposition] Using individual thoughts. StageThoughtUtil initialized.");
            AddBaseThoughts();
        }

        public static void ModCompat()
        {
            Log.Message("[Rimpsyche - Disposition] Using individual thoughts. Compatibility Thoughts added.");
        }

        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla_Stage(StageMoodThoughtTagDB, StageOpinionThoughtTagDB);
            if (ModsConfig.AnomalyActive) AnomalyDB.AddDefs_Anomaly_Stage(StageMoodThoughtTagDB, StageOpinionThoughtTagDB);
        }
    }
}