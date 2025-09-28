using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class OdysseyDB : BaseThoughtDB
    {
        public static void AddDefs_Odyssey(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            RegisterThoughts(moodList_Odyssey_Tag_Empathy_M, MoodThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(moodList_Odyssey_Tag_Empathy_J, MoodThoughtTagDB, FormulaDB.Tag_Empathy_J);
            RegisterThoughts(moodList_Odyssey_Tag_Judgemental, MoodThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(opinionList_Odyssey_Tag_Empathy_M, OpinionThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(opinionList_Odyssey_Tag_Empathy_J, OpinionThoughtTagDB, FormulaDB.Tag_Empathy_J);
            RegisterThoughts(opinionList_Odyssey_Tag_Affluence, OpinionThoughtTagDB, FormulaDB.Tag_Affluence);
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
