using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class JoyGiverUtil
    {
        private static readonly float joyIndivC = RimpsycheDispositionSettings.joyIndividualC;
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

        public static float JoyMult(float p)
        {
            float x;
            //Compress personality factor into -1~1 range.
            if (p >= 0)
                x = p / (p + 1f);
            else
                x = p / (1f - p);

            //Boost the factor
            //x = Rimpsyche_Utility.Boost(x);
            if (x >= 0f)
                return 1f + (joyIndivC - 1f) * x;
            else
                return 1f / (1f - (joyIndivC - 1f) * x);
        }
        public static float JoyBaseTranslator(float original)
        {
            //Translate base. 0~2~unlim -> 0~5~10
            return 10f * (original / (original + 2f));
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
            MiscModJoyGiverDB.AddDefs_Mods(JoyChanceDB);
        }
    }
}
