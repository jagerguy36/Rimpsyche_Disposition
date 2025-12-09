using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class JoyGiverUtil
    {
        private static readonly bool useIndividualJoychanceSetting = RimpsycheDispositionSettings.useIndividualJoychance;
        public static Dictionary<int, RimpsycheFormula> JoyChanceDB = [];

        static JoyGiverUtil()
        {
            if (useIndividualJoychanceSetting)
            {
                Log.Message("[Rimpsyche - Disposition] Using individual joy chance. JoyGiverUtil initialized.");
                Initialize();
            }
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
    }
}
