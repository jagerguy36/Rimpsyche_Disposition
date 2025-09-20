using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class BiotechDB
    {
        public static void AddDefs_Biotech(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Biotech_Tag_Judgemental) MoodThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in moodList_Biotech_Tag_Empathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in moodList_Biotech_Tag_Empathy_Kin) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Kin;
            foreach (var defName in moodList_Biotech_Tag_Worry_Kin) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Kin;
            foreach (var defName in moodList_Biotech_Tag_Affluence) MoodThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
            foreach (var defName in moodList_Biotech_Tag_Harmed) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in moodList_Biotech_Tag_Gathering) MoodThoughtTagDB[defName] = FormulaDB.Tag_Gathering;
            foreach (var defName in moodList_Biotech_Tag_Sympathy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy;
            foreach (var defName in moodList_Biotech_Tag_Empathy_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Loved;
            foreach (var defName in moodList_Biotech_Tag_Needy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Needy;
            foreach (var defName in moodList_Biotech_Tag_Recluse) MoodThoughtTagDB[defName] = FormulaDB.Tag_Recluse;
            foreach (var defName in opinionList_Biotech_Tag_Judgemental) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in opinionList_Biotech_Tag_Empathy_M) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in opinionList_Biotech_Tag_Empathy_Kin) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Kin;
            foreach (var defName in opinionList_Biotech_Tag_Harmed) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in opinionList_Biotech_Tag_Gathering) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Gathering;
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

        private static readonly List<string> moodList_Biotech_Tag_Gathering = new(
            ["WasTaught",
            "GaveLesson",
            "PlayedWithMe"]
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
