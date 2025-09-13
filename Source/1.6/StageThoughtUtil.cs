using Verse;
using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public class StageThoughtUtil
    {
        static StageThoughtUtil()
        {
            Initialize();
            ModCompat();
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] StageThoughtUtil initialized.");
        }

        public static void ModCompat()
        {
            if (ModsConfig.IdeologyActive)
            {
            }
        }
        public static readonly Dictionary<string, RimpsycheFormula[]> StageMoodMultiplierDB = new()
        {
            { "KnowGuestExecuted", [
                FormulaDB.Mood_Died, //justified execution
                FormulaDB.Mood_Died, //someone was euthanized
                FormulaDB.Mood_Died, //someone was executed
                FormulaDB.Mood_Died, //someone was organ-murdered
                FormulaDB.Mood_Died
                ]
            }, //someone was ripscanned
        };
    }
}