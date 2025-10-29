using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public class StageThoughtUtil
    {
        static StageThoughtUtil()
        {
            if (RimpsycheDispositionSettings.useIndividualThoughts)
            {
                Initialize();
            }
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] Using individual thoughts. StageThoughtUtil initialized.");
            AddBaseThoughts();
            ModCompat();
        }
        private static void AddBaseThoughts()
        {
            CoreDB.AddDefs_Vanilla_Stage(MoodThoughtTagDB, OpinionThoughtTagDB);
        }
        public static void ModCompat()
        {
            if (ModsConfig.AnomalyActive) AnomalyDB.AddDefs_Anomaly_Stage(MoodThoughtTagDB, OpinionThoughtTagDB);
            MiscModDB.AddStageDefs_MiscMods(MoodThoughtTagDB, OpinionThoughtTagDB);
        }

    }
}