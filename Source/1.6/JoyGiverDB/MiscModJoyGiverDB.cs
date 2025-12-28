using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class MiscModJoyGiverDB
    {
        public static void AddDefs_Mods(Dictionary<int, RimpsycheFormula> JoyChanceDB)
        {
            //Dubs Bad Hygiene
            if (ModsConfig.IsActive("dubwise.dubsbadhygiene"))
            {
                RegisterJoyChanceMultiplier("UseHotTub", JoyChanceDB, UseHotTubChanceMultiplier);
                RegisterJoyChanceMultiplier("WatchWashingMachine", JoyChanceDB, WatchWashingMachineChanceMultiplier);
            }

            //Vanilla Furniture Expanded
            if (ModsConfig.IsActive("vanillaexpanded.vfecore"))
            {
                RegisterJoyChanceMultiplier("Play_Roulette", JoyChanceDB, Play_RouletteChanceMultiplier);
                RegisterJoyChanceMultiplier("Play_Arcade", JoyChanceDB, Play_ArcadeChanceMultiplier);
                RegisterJoyChanceMultiplier("Play_Piano", JoyChanceDB, JoyGiverDB.Play_MusicalInstrumentChanceMultiplier);
                RegisterJoyChanceMultiplier("Play_DartsBoard", JoyChanceDB, Play_DartsBoardChanceMultiplier);
                RegisterJoyChanceMultiplier("Play_PunchingBag", JoyChanceDB, Play_PunchingBagChanceMultiplier);
                RegisterJoyChanceMultiplier("VFE_Play_Computer", JoyChanceDB, JoyGiverDB.WatchTelevisionChanceMultiplier);
                RegisterJoyChanceMultiplier("VFE_Play_Sunbathing", JoyChanceDB, VFE_Play_SunbathingChanceMultiplier);
                RegisterJoyChanceMultiplier("VFE_ListenToMusic", JoyChanceDB, JoyGiverDB.ViewArtChanceMultiplier);
            }

            //Misc. Training
            if (ModsConfig.IsActive("haplo.miscellaneous.training"))
            {
                RegisterJoyChanceMultiplier("ShootSomeArrows", JoyChanceDB, TrainShootingChanceMultiplier);
                RegisterJoyChanceMultiplier("PracticeShooting", JoyChanceDB, TrainShootingChanceMultiplier);
                RegisterJoyChanceMultiplier("PracticeMartialArts", JoyChanceDB, TrainMeleeChanceMultiplier);
            }

            //Vanilla Factions Expanded - Medieval 2
            if (ModsConfig.IsActive("oskarpotocki.vfe.medieval2"))
            {
                RegisterJoyChanceMultiplier("VFEM2_Play_Archery", JoyChanceDB, TrainShootingChanceMultiplier);
                RegisterJoyChanceMultiplier("VFEM2_Play_TrainingDummy", JoyChanceDB, TrainMeleeChanceMultiplier);
            }

            //Medieval Overhaul
            if (ModsConfig.IsActive("dankpyon.medieval.overhaul"))
            {
                RegisterJoyChanceMultiplier("DankPyon_Play_CupAndDice", JoyChanceDB, JoyGiverDB.Play_PokerChanceMultiplier);
                RegisterJoyChanceMultiplier("DankPyon_Play_Tarocco", JoyChanceDB, JoyGiverDB.Play_ChessChanceMultiplier);
                RegisterJoyChanceMultiplier("DankPyon_Play_RimWar", JoyChanceDB, JoyGiverDB.Play_ChessChanceMultiplier);
            }

            //Rimbody
            if (ModsConfig.IsActive("maux36.rimbody"))
            {
                RegisterJoyChanceMultiplier("Rimbody_WorkoutJoy", JoyChanceDB, WorkoutChanceMultiplier);
            }
        }

        public static RimpsycheFormula UseHotTubChanceMultiplier = new(
            "UseHotTubChanceMultiplier",
            (tracker) =>
            {
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula WatchWashingMachineChanceMultiplier = new(
            "WatchWashingMachineChanceMultiplier",
            (tracker) =>
            {
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float imagination = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float reflectiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
                float x = imagination + reflectiveness + laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula Play_RouletteChanceMultiplier = new(
            "Play_RouletteChanceMultiplier",
            (tracker) =>
            {
                float playfulness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float discipline = -1f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = playfulness + discipline + laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula Play_ArcadeChanceMultiplier = new(
            "Play_ArcadeChanceMultiplier",
            (tracker) =>
            {
                float playfulness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float laziness = -0.5f * Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = playfulness + laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula Play_DartsBoardChanceMultiplier = new(
            "Play_DartsBoardChanceMultiplier",
            (tracker) =>
            {
                float playfulness = 0.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                float competitiveness = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Competitiveness);
                float deliberation = 1.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                float x = playfulness + competitiveness + deliberation;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula Play_PunchingBagChanceMultiplier = new(
            "Play_PunchingBagChanceMultiplier",
            (tracker) =>
            {
                float aggressiveness = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                float ambition = 0.75f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                float x = aggressiveness + ambition;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula VFE_Play_SunbathingChanceMultiplier = new(
            "VFE_Play_SunbathingChanceMultiplier",
            (tracker) =>
            {
                float laziness = -Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float x = laziness;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula TrainShootingChanceMultiplier = new(
            "TrainShootingChanceMultiplier",
            (tracker) =>
            {
                float aggressiveness = 0.75f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                float ambition = 0.75f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                float diligence = 0.75f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float discipline = 0.75f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float x = aggressiveness + ambition + diligence + discipline;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula TrainMeleeChanceMultiplier = new(
            "TrainMeleeChanceMultiplier",
            (tracker) =>
            {
                float aggressiveness = 1.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                float ambition = 0.75f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                float diligence = 0.5f * Mathf.Max(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence));
                float discipline = 0.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float x = aggressiveness + ambition + diligence + discipline;
                return JoyGiverUtil.JoyMult(x);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula WorkoutChanceMultiplier = new(
            "WorkoutChanceMultiplier",
            (tracker) =>
            {
                float ambition = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
                float diligence = 0.75f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence);
                float discipline = 0.25f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                float x = ambition + diligence + discipline;
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
