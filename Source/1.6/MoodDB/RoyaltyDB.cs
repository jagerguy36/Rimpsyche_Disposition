using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class RoyaltyDB
    {
        public static void AddDefs_Royalty(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Royalty_Tag_Affluence) MoodThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
            foreach (var defName in moodList_Royalty_Tag_Empathy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy;
            foreach (var defName in moodList_Royalty_Tag_Harmed) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in moodList_Royalty_Tag_Art) MoodThoughtTagDB[defName] = FormulaDB.Tag_Art;
            foreach (var defName in moodList_Royalty_Tag_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Loved;
            foreach (var defName in opinionList_Royalty_Tag_Loved) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Loved;
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

        private static readonly List<string> moodList_Royalty_Tag_Loved = new(
            ["PsychicLove"]
        );

        private static readonly List<string> opinionList_Royalty_Tag_Loved = new(
            ["PsychicLove"]
        );
    }
}
