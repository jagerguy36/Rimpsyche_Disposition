using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class JoyGiverDB
    {
        private static readonly float joyIndivC = RimpsycheDispositionSettings.joyIndividualC;
        public static void AddDefs_Vanilla(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            RegisterJoyChanceMultiplier("Meditate", JoyChanceDB, MeditateChanceMultiplier);
            RegisterJoyChanceMultiplier("Pray", JoyChanceDB, PrayChanceMultiplier);
            RegisterJoyChanceMultiplier("Skygaze", JoyChanceDB, SkyGazeChanceMultiplier);
            RegisterJoyChanceMultiplier("VisitGrave", JoyChanceDB, VisitGraveChanceMultiplier);
            RegisterJoyChanceMultiplier("ViewArt", JoyChanceDB, ViewArtChanceMultiplier);
            RegisterJoyChanceMultiplier("BuildSnowman", JoyChanceDB, BuildSnowmanChanceMultiplier);
            RegisterJoyChanceMultiplier("SocialRelax", JoyChanceDB, SocialRelaxChanceMultiplier);
            RegisterJoyChanceMultiplier("VisitSickPawn", JoyChanceDB, VisitSickPawnChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Horseshoes", JoyChanceDB, Play_HorseshoesChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Hoopstone", JoyChanceDB, Play_HorseshoesChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Billiards", JoyChanceDB, Play_BilliardsChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Chess", JoyChanceDB, Play_ChessChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_GameOfUr", JoyChanceDB, Play_ChessChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Poker", JoyChanceDB, Play_PokerChanceMultiplier);
            RegisterJoyChanceMultiplier("WatchTelevision", JoyChanceDB, WatchTelevisionChanceMultiplier);
            RegisterJoyChanceMultiplier("UseTelescope", JoyChanceDB, UseTelescopeChanceMultiplier);
            RegisterJoyChanceMultiplier("TakeDrug", JoyChanceDB, TakeDrugChanceMultiplier);
            RegisterJoyChanceMultiplier("Reading", JoyChanceDB, ReadingChanceMultiplier);
        }
        public static void AddDefs_Mods(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            if (ModsConfig.IsActive("dubwise.dubsbadhygiene"))
            {
                // RegisterJoyChanceMultiplier("WatchWashingMachine", JoyChanceDB, MeditateChanceMultiplier)
            }
        }
        public static float Mult(float x, float mult)
        {
            x = Mathf.Clamp(x, -1f, 1f);
            x = Rimpsyche_Utility.Boost(x);
            if (x >= 0f) return (mult - 1f) * x + 1f;
            else return (1f - (1f / mult)) * x + 1f;
        }

        public static RimpsycheFormula MeditateChanceMultiplier = new(
            "MeditateChanceMultiplier",
            (tracker) =>
            {
                float r = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float c = joyIndivC * r * (2f - r);
                if (c > 0) return c;
                return 0f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula PrayChanceMultiplier = new(
            "PrayChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                return Mult(reflectiveness, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SkyGazeChanceMultiplier = new(
            "SkyGazeChanceMultiplier",
            (tracker) =>
            {
                float imagination = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = (imagination + reflectiveness) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula VisitGraveChanceMultiplier = new(
            "VisitGraveChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = 0.75f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float loyalty = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float playfulness = 2f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness));
                float fearfulness = -0.75f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery));
                float x = (reflectiveness + loyalty - playfulness - fearfulness) / 5f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula ViewArtChanceMultiplier = new(
            "ViewArtChanceMultiplier",
            (tracker) =>
            {
                float imagination = 3f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = (imagination + reflectiveness) / 4f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula BuildSnowmanChanceMultiplier = new(
            "BuildSnowmanChanceMultiplier",
            (tracker) =>
            {
                float playfulness = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float imagination = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float x = (playfulness + imagination) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SocialRelaxChanceMultiplier = new(
            "SocialRelaxChanceMultiplier",
            (tracker) =>
            {
                float sociability = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float talkativeness = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Talkativeness);
                float x = (sociability + talkativeness) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula VisitSickPawnChanceMultiplier = new(
            "VisitSickPawnChanceMultiplier",
            (tracker) =>
            {
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float altruism = -Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest));
                float x = (sociability + compassion + altruism) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_HorseshoesChanceMultiplier = new(
            "Play_HorseshoesChanceMultiplier",
            (tracker) =>
            {
                float competitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float playfulness = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float reflectiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = (competitiveness + playfulness - reflectiveness) / 4f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_BilliardsChanceMultiplier = new(
            "Play_BilliardsChanceMultiplier",
            (tracker) =>
            {
                float competitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float deliberation = 1.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                float x = (competitiveness + deliberation) / 2.25f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_ChessChanceMultiplier = new(
            "Play_ChessChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float competitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float x = (reflectiveness + competitiveness) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_PokerChanceMultiplier = new(
            "Play_PokerChanceMultiplier",
            (tracker) =>
            {
                float confidence = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float competitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float x = (confidence + sociability + competitiveness) / 3f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula WatchTelevisionChanceMultiplier = new(
            "WatchTelevisionChanceMultiplier",
            (tracker) =>
            {
                float laziness = -Mathf.Min(0f,tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = laziness;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula UseTelescopeChanceMultiplier = new(
            "UseTelescopeChanceMultiplier",
            (tracker) =>
            {
                float inquisitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Inquisitiveness);
                float x = inquisitiveness;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula TakeDrugChanceMultiplier = new(
            "TakeDrugChanceMultiplier",
            (tracker) =>
            {
                float expreimentation = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                float discipline = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float x = (expreimentation + discipline) / 2.5f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula ReadingChanceMultiplier = new(
            "ReadingChanceMultiplier",
            (tracker) =>
            {
                float inquisitiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Inquisitiveness);
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = (inquisitiveness + imagination + reflectiveness) / 3.5f;
                return Mult(x, joyIndivC);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );


        public static void RegisterJoyChanceMultiplier(string defName, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            var def = DefDatabase<JoyGiverDef>.GetNamed(defName, false);
            if (def != null)
                targetDb[def.shortHash] = value;
            else
                Log.Warning($"[Rimpsyche - Disposition] Could not find JoyGiverDef named '{defName}'.");
        }
    }
}
