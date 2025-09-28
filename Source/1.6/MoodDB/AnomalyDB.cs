using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class AnomalyDB : BaseThoughtDB
    {
        public static void AddDefs_Anomaly_Stage(Dictionary<int, RimpsycheFormula[]> StageThoughtTagDB, Dictionary<int, RimpsycheFormula[]> StageOpinionThoughtTagDB)
        {
            RegisterSingleThought("UnnaturalDarkness", StageThoughtTagDB,
                [FormulaDB.Tag_Fear,
                null]
            );
        }
        public static void AddDefs_Anomaly(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            RegisterThoughts(moodList_Anomaly_Tag_Judgemental, MoodThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(moodList_Anomaly_Tag_Morality, MoodThoughtTagDB, FormulaDB.Tag_Morality);
            RegisterThoughts(moodList_Anomaly_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_Anomaly_Tag_Harmed, MoodThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(moodList_Anomaly_Tag_Fear, MoodThoughtTagDB, FormulaDB.Tag_Fear);
            RegisterThoughts(opinionList_Anomaly_Tag_Judgemental, OpinionThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(opinionList_Anomaly_Tag_Morality, OpinionThoughtTagDB, FormulaDB.Tag_Morality);
            RegisterThoughts(opinionList_Anomaly_Tag_Harmed, OpinionThoughtTagDB, FormulaDB.Tag_Harmed);
        }

        private static readonly List<string> moodList_Anomaly_Tag_Judgemental = new(
            ["Inhumanizing_Required_Human",
            "PsychicRitualPerformed_Disapproved",
            "InvolvedInPsychicRitual_Disapproved",
            "InvolvedInPsychicRitual_Social_Disapproved",
            "InvolvedInPsychicRitual_Exalted",
            "InvolvedInPsychicRitual_Social_Exalted",
            "NoPsychicRituals"]
        );

        private static readonly List<string> moodList_Anomaly_Tag_Morality = new(
            ["PsychicRitualPerformed_Abhorrent",
            "InvolvedInPsychicRitual_Abhorrent",
            "InvolvedInPsychicRitual_Social_Abhorrent",
            "PsychicRitualGuilt"]
        );

        private static readonly List<string> moodList_Anomaly_Tag_Affluence = new(
            ["AteTwistedMeat"]
        );

        private static readonly List<string> moodList_Anomaly_Tag_Harmed = new(
            ["PsychicRitualVictim",
            "MutatedMyArm",
            "DrainedMySkills",
            "UsedMeForPsychicRitual"]
        );

        private static readonly List<string> moodList_Anomaly_Tag_Fear = new(
            ["DarknessLifted",
            "UnnaturalCorpseDestroyed",
            "SwallowedByDarkness"]
        );

        private static readonly List<string> opinionList_Anomaly_Tag_Judgemental = new(
            ["InvolvedInPsychicRitual_Social_Disapproved",
            "InvolvedInPsychicRitual_Social_Exalted"]
        );

        private static readonly List<string> opinionList_Anomaly_Tag_Morality = new(
            ["InvolvedInPsychicRitual_Social_Abhorrent"]
        );

        private static readonly List<string> opinionList_Anomaly_Tag_Harmed = new(
            ["MutatedMyArm",
            "DrainedMySkills",
            "UsedMeForPsychicRitual"]
        );
    }
}
