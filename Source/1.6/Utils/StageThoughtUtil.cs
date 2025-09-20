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
                FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M //someone was ripscanned
                ]
            },
            { "KnowGuestExecuted", [
                FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M //someone was ripscanned
                ]
            },
        };
    }
}