using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class BiotechDB : BaseThoughtDB
    {
        public static void AddDefs_Biotech(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            RegisterThoughts(moodList_Biotech_Tag_Judgemental, MoodThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(moodList_Biotech_Tag_Empathy_M, MoodThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(moodList_Biotech_Tag_Empathy_Kin, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Kin);
            RegisterThoughts(moodList_Biotech_Tag_Worry_Kin, MoodThoughtTagDB, FormulaDB.Tag_Worry_Kin);
            RegisterThoughts(moodList_Biotech_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_Biotech_Tag_Harmed, MoodThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(moodList_Biotech_Tag_Sympathy, MoodThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(moodList_Biotech_Tag_Empathy_Loved, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Loved);
            RegisterThoughts(moodList_Biotech_Tag_Needy, MoodThoughtTagDB, FormulaDB.Tag_Needy);
            RegisterThoughts(moodList_Biotech_Tag_Recluse, MoodThoughtTagDB, FormulaDB.Tag_Recluse);
            RegisterThoughts(opinionList_Biotech_Tag_Judgemental, OpinionThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(opinionList_Biotech_Tag_Empathy_M, OpinionThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(opinionList_Biotech_Tag_Empathy_Kin, OpinionThoughtTagDB, FormulaDB.Tag_Empathy_Kin);
            RegisterThoughts(opinionList_Biotech_Tag_Harmed, OpinionThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(opinionList_Biotech_Tag_Gathering, OpinionThoughtTagDB, FormulaDB.Tag_Gathering);
        }

        private static readonly List<string> moodList_Biotech_Tag_Judgemental = new(
            ["Bloodfeeders_Revered_Opinion_Bloodfeeder",
            "Bloodfeeders_Reviled_Opinion_Bloodfeeder",
            "BloodfeederDied_Revered",
            "BloodfeederDied_Reviled",
            "Bloodfeeder_ReveredBloodfeeder",
            "Bloodfeeder_ReviledBloodfeeder",
            "BloodfeederColonist_Revered",
            "BloodfeederColonist_Reviled",
            "ChildLabor_Encouraged_ChildAssignedRecreation",
            "ChildLabor_Encouraged_ChildAssignedWork",
            "GrowthVat_Essential_Pregnant",
            "GrowthVat_Essential_ChildNotInGrowthVat",
            "GrowthVat_Prohibited_ChildNotInGrowthVat",
            "GrowthVat_Prohibited_GrowthVatInColony",
            "GrowthVat_Prohibited_ChildInGrowthVat",
            "PreferredXenotype",
            "PreferredXenotypeMakeup",
            "SelfDislikedXenotype"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Empathy_M = new(
            ["KilledChild",
            "EnslavedChild",
            "KilledChild_Opinion"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Empathy_Kin = new(
            ["MyBirthMotherDied",
            "KilledMyBirthMother",
            "PregnancyTerminated",
            "Stillbirth",
            "Miscarried",
            "BabyBorn"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Worry_Kin = new(
            ["MyBirthMotherLost"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Affluence = new(
            ["AteBabyFood",
            "IngestedHemogenPack",
            "DeathrestChamber"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Harmed = new(
            ["FedOn_Social",
            "FedOn",
            "XenogermHarvested_Prisoner"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Sympathy = new(
            ["PregnancyEnded"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Empathy_Loved = new(
            ["PartnerMiscarried"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Needy = new(
            ["NeedLearning",
            "NeedPlay",
            "HemogenCraving"]
        );

        private static readonly List<string> moodList_Biotech_Tag_Recluse = new(
            ["Recluse"]
        );

        private static readonly List<string> opinionList_Biotech_Tag_Judgemental = new(
            ["Bloodfeeders_Revered_Opinion_Bloodfeeder",
            "Bloodfeeders_Reviled_Opinion_Bloodfeeder",
            "PreferredXenotype"]
        );

        private static readonly List<string> opinionList_Biotech_Tag_Empathy_M = new(
            ["KilledChild_Opinion"]
        );

        private static readonly List<string> opinionList_Biotech_Tag_Empathy_Kin = new(
            ["KilledMyBirthMother"]
        );

        private static readonly List<string> opinionList_Biotech_Tag_Harmed = new(
            ["FedOn_Social"]
        );

        private static readonly List<string> opinionList_Biotech_Tag_Gathering = new(
            ["WasTaught",
            "GaveLesson",
            "PlayedWithMe"]
        );
    }
}
