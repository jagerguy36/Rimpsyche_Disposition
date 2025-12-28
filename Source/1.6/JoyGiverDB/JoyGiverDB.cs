using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class JoyGiverDB
    {
        public static void AddDefs_Vanilla(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            //Core
            //<!-- meditative -->
            RegisterJoyChanceMultiplier("Meditate", JoyChanceDB, MeditateChanceMultiplier);
            RegisterJoyChanceMultiplier("Pray", JoyChanceDB, PrayChanceMultiplier);
            RegisterJoyChanceMultiplier("Skygaze", JoyChanceDB, SkyGazeChanceMultiplier);
            RegisterJoyChanceMultiplier("GoForWalk", JoyChanceDB, GoForWalkChanceMultiplier);
            RegisterJoyChanceMultiplier("VisitGrave", JoyChanceDB, VisitGraveChanceMultiplier);
            RegisterJoyChanceMultiplier("ViewArt", JoyChanceDB, ViewArtChanceMultiplier);
            RegisterJoyChanceMultiplier("BuildSnowman", JoyChanceDB, BuildSnowmanChanceMultiplier);
            //<!-- social -->
            RegisterJoyChanceMultiplier("SocialRelax", JoyChanceDB, SocialRelaxChanceMultiplier);
            RegisterJoyChanceMultiplier("VisitSickPawn", JoyChanceDB, VisitSickPawnChanceMultiplier);
            //<!-- dexterity play -->
            RegisterJoyChanceMultiplier("Play_Horseshoes", JoyChanceDB, Play_HorseshoesChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Hoopstone", JoyChanceDB, Play_HorseshoesChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Billiards", JoyChanceDB, Play_BilliardsChanceMultiplier);
            //<!-- cerebral play -->
            RegisterJoyChanceMultiplier("Play_GameOfUr", JoyChanceDB, Play_ChessChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Chess", JoyChanceDB, Play_ChessChanceMultiplier);
            RegisterJoyChanceMultiplier("Play_Poker", JoyChanceDB, Play_PokerChanceMultiplier);
            //<!-- television -->
            RegisterJoyChanceMultiplier("WatchTelevision", JoyChanceDB, WatchTelevisionChanceMultiplier);
            //<!-- telescope -->
            RegisterJoyChanceMultiplier("UseTelescope", JoyChanceDB, UseTelescopeChanceMultiplier);
            //<!-- chemical consumption -->
            RegisterJoyChanceMultiplier("TakeDrug", JoyChanceDB, TakeDrugChanceMultiplier);
            //<!-- food consumption -->
            //EatChocolate
            //<!-- reading -->
            RegisterJoyChanceMultiplier("Reading", JoyChanceDB, ReadingChanceMultiplier);

            //Royalty
            if (ModsConfig.RoyaltyActive)
            {
                RegisterJoyChanceMultiplier("Play_MusicalInstrument", JoyChanceDB, Play_MusicalInstrumentChanceMultiplier);
            }

            //Odyssey
            if (ModsConfig.OdysseyActive)
            {
                RegisterJoyChanceMultiplier("GoSwimming", JoyChanceDB, GoSwimmingChanceMultiplier);
            }


        }

        public static RimpsycheFormula MeditateChanceMultiplier = new(
            "MeditateChanceMultiplier",
            (tracker) =>
            {
                float r = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float c = 2.5f * r * (2f - r);
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
                return JoyGiverUtil.JoyMult(reflectiveness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SkyGazeChanceMultiplier = new(
            "SkyGazeChanceMultiplier",
            (tracker) =>
            {
                float imagination = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = imagination + reflectiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula GoForWalkChanceMultiplier = new(
            "GoForWalkChanceMultiplier",
            (tracker) =>
            {
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = reflectiveness - laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula VisitGraveChanceMultiplier = new(
            "VisitGraveChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float playfulness = 2f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness));
                float fearfulness = -0.15f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery));
                float x = reflectiveness + loyalty - playfulness - fearfulness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula ViewArtChanceMultiplier = new(
            "ViewArtChanceMultiplier",
            (tracker) =>
            {
                float imagination = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = imagination + reflectiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula BuildSnowmanChanceMultiplier = new(
            "BuildSnowmanChanceMultiplier",
            (tracker) =>
            {
                float playfulness = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float imagination = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float x = playfulness + imagination;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula SocialRelaxChanceMultiplier = new(
            "SocialRelaxChanceMultiplier",
            (tracker) =>
            {
                float sociability = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float talkativeness = 1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Talkativeness);
                float x = sociability + talkativeness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula VisitSickPawnChanceMultiplier = new(
            "VisitSickPawnChanceMultiplier",
            (tracker) =>
            {
                float sociability = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float altruism = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest));
                float x = sociability + compassion + altruism;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_HorseshoesChanceMultiplier = new(
            "Play_HorseshoesChanceMultiplier",
            (tracker) =>
            {
                float competitiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float playfulness = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = competitiveness + playfulness - reflectiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_BilliardsChanceMultiplier = new(
            "Play_BilliardsChanceMultiplier",
            (tracker) =>
            {
                float competitiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float deliberation = 1.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                float x = competitiveness + deliberation;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_ChessChanceMultiplier = new(
            "Play_ChessChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float competitiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float x = reflectiveness + competitiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_PokerChanceMultiplier = new(
            "Play_PokerChanceMultiplier",
            (tracker) =>
            {
                float reflectiveness = 0.75f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float competitiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float confidence = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float sociability = 0.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float x = reflectiveness + confidence + sociability + competitiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula WatchTelevisionChanceMultiplier = new(
            "WatchTelevisionChanceMultiplier",
            (tracker) =>
            {
                float laziness = -Mathf.Min(0f,tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula UseTelescopeChanceMultiplier = new(
            "UseTelescopeChanceMultiplier",
            (tracker) =>
            {
                float inquisitiveness = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Inquisitiveness);
                float x = inquisitiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula TakeDrugChanceMultiplier = new(
            "TakeDrugChanceMultiplier",
            (tracker) =>
            {
                float expreimentation = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                float discipline = -1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float x = expreimentation + discipline;
                return JoyGiverUtil.JoyMult(x);
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
                float x = inquisitiveness + imagination + reflectiveness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Play_MusicalInstrumentChanceMultiplier = new(
            "Play_MusicalInstrumentChanceMultiplier",
            (tracker) =>
            {
                float imagination = 2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float confidence = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
                float x = imagination + confidence;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula GoSwimmingChanceMultiplier = new(
            "GoSwimmingChanceMultiplier",
            (tracker) =>
            {
                float fearfulness = -0.15f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery));
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = -fearfulness - laziness;
                return JoyGiverUtil.JoyMult(x);
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
