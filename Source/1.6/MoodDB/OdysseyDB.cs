using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class OdysseyDB
    {
        public static void AddDefs_Odyssey(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Odyssey_Tag_Empathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in moodList_Odyssey_Tag_Empathy_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_J;
            foreach (var defName in moodList_Odyssey_Tag_Judgemental) MoodThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in opinionList_Odyssey_Tag_Empathy_M) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in opinionList_Odyssey_Tag_Empathy_J) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_J;
            foreach (var defName in opinionList_Odyssey_Tag_Affluence) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
        }

        private static readonly List<string> moodList_Odyssey_Tag_Empathy_M = new(
            ["SlaughteredFish_Know_Prohibited",
            "SlaughteredFish_Know_Prohibited_Mood"]
        );

        private static readonly List<string> moodList_Odyssey_Tag_Empathy_J = new(
            ["SlaughteredFish_Disapproved",
            "SlaughteredFish_Know_Disapproved",
            "SlaughteredFish_Know_Disapproved_Mood"]
        );

        private static readonly List<string> moodList_Odyssey_Tag_Judgemental = new(
            ["SlaughteredFish_Sacred",
            "SlaughteredFish_Sacred_NoFish",
            "ResettledRecently",
            "SpaceHabitat_Mood"]
        );

        private static readonly List<string> opinionList_Odyssey_Tag_Empathy_M = new(
            ["SlaughteredFish_Know_Prohibited"]
        );

        private static readonly List<string> opinionList_Odyssey_Tag_Empathy_J = new(
            ["SlaughteredFish_Know_Disapproved"]
        );

        private static readonly List<string> opinionList_Odyssey_Tag_Affluence = new(
            ["MadeStatueOfMe"]
        );
    }
}
