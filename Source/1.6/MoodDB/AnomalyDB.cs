using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class AnomalyDB
    {
        public static void AddDefs_Anomaly(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Anomaly_Tag_Judgemental) MoodThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in moodList_Anomaly_Tag_Morality) MoodThoughtTagDB[defName] = FormulaDB.Tag_Morality;
            foreach (var defName in moodList_Anomaly_Tag_Affluence) MoodThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
            foreach (var defName in moodList_Anomaly_Tag_Harmed) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in moodList_Anomaly_Tag_Fear) MoodThoughtTagDB[defName] = FormulaDB.Tag_Fear;
            foreach (var defName in opinionList_Anomaly_Tag_Judgemental) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in opinionList_Anomaly_Tag_Morality) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Morality;
            foreach (var defName in opinionList_Anomaly_Tag_Harmed) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
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
