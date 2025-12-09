using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class JoyGiverDB
    {
        public static void AddDefs_Vanilla(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            RegisterJoyChanceMultiplier("Meditate", JoyChanceDB, MeditateChanceMultiplier);
            RegisterJoyChanceMultiplier("Skygaze", JoyChanceDB, SkyGazeChanceMultiplier);
        }
        public static void AddDefs_Mods(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            if (ModsConfig.IsActive("dubwise.dubsbadhygiene"))
            {
                // RegisterJoyChanceMultiplier("WatchWashingMachine", JoyChanceDB, MeditateChanceMultiplier)
            }
        }

        public static RimpsycheFormula MeditateChanceMultiplier = new(
            "MeditateChanceMultiplier",
            (tracker) =>
            {
                float r = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float c = -2.5f * r * (r - 2f);
                if (c > 0) return c;
                return 0f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SkyGazeChanceMultiplier = new(
            "SkyGazeChanceMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float imaginationMult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 1.5f);
                return mult * imaginationMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );


        public static void RegisterJoyChanceMultiplier(string defName, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            var thoughtDef = DefDatabase<JoyGiverDef>.GetNamed(defName, false);
            if (thoughtDef != null)
                targetDb[thoughtDef.shortHash] = value;
            else
                Log.Warning($"[Rimpsyche - Disposition] Could not find JoyGiverDef named '{defName}'.");
        }
    }
}
