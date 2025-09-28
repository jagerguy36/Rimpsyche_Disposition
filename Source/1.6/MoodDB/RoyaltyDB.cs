using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class RoyaltyDB : BaseThoughtDB
    {
        public static void AddDefs_Royalty(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            RegisterThoughts(moodList_Royalty_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_Royalty_Tag_Empathy, MoodThoughtTagDB, FormulaDB.Tag_Empathy);
            RegisterThoughts(moodList_Royalty_Tag_Harmed, MoodThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(moodList_Royalty_Tag_Art, MoodThoughtTagDB, FormulaDB.Tag_Art);
            RegisterThoughts(opinionList_Royalty_Tag_Loved, OpinionThoughtTagDB, FormulaDB.Tag_Loved);
        }

        private static readonly List<string> moodList_Royalty_Tag_Affluence = new(
            ["GainedTitle",
            "LostTitle",
            "TitleApparelRequirementNotMet",
            "TitleApparelMinQualityNotMet",
            "TitleThroneroomRequirementsNotMet",
            "TitleBedroomRequirementsNotMet",
            "TitleNoThroneRoom",
            "TitleNoPersonalBedroom",
            "AteFoodInappropriateForTitle"]
        );

        private static readonly List<string> moodList_Royalty_Tag_Empathy = new(
            ["OtherTravelerDied"]
        );

        private static readonly List<string> moodList_Royalty_Tag_Harmed = new(
            ["OtherTravelerArrested",
            "OtherTravelerSurgicallyViolated"]
        );

        private static readonly List<string> moodList_Royalty_Tag_Art = new(
            ["ListeningToHarp",
            "ListeningToHarpsichord",
            "ListeningToPiano"]
        );

        private static readonly List<string> opinionList_Royalty_Tag_Loved = new(
            ["PsychicLove"]
        );
    }
}
