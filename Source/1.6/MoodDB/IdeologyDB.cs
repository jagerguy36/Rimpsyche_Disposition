using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class IdeologyDB
    {
        public static void AddDefs_Ideology(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Ideology_Tag_Empathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in moodList_Ideology_Tag_Empathy_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_J;
            foreach (var defName in moodList_Ideology_Tag_Judgemental) MoodThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in moodList_Ideology_Tag_Morality) MoodThoughtTagDB[defName] = FormulaDB.Tag_Morality;
            foreach (var defName in moodList_Ideology_Tag_Preference) MoodThoughtTagDB[defName] = FormulaDB.Tag_Preference;
            foreach (var defName in moodList_Ideology_Tag_Sympathy_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_J;
            foreach (var defName in moodList_Ideology_Tag_Sympathy_P) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_P;
            foreach (var defName in moodList_Ideology_Tag_Sympathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_M;
            foreach (var defName in moodList_Ideology_Tag_Charity_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Charity_M;
            foreach (var defName in moodList_Ideology_Tag_Charity_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Charity_J;
            foreach (var defName in moodList_Ideology_Tag_Worry_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_M;
            foreach (var defName in moodList_Ideology_Tag_Worry_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_J;
            foreach (var defName in moodList_Ideology_Tag_Openmindedness) MoodThoughtTagDB[defName] = FormulaDB.Tag_Openmindedness;
            foreach (var defName in moodList_Ideology_Tag_Decency_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Decency_M;
            foreach (var defName in moodList_Ideology_Tag_Decency_J) MoodThoughtTagDB[defName] = FormulaDB.Tag_Decency_J;
            foreach (var defName in moodList_Ideology_Tag_Affluence) MoodThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
            foreach (var defName in moodList_Ideology_Tag_Decency) MoodThoughtTagDB[defName] = FormulaDB.Tag_Decency;
            foreach (var defName in moodList_Ideology_Tag_Gathering) MoodThoughtTagDB[defName] = FormulaDB.Tag_Gathering;
            foreach (var defName in opinionList_Ideology_Tag_Empathy_M) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in opinionList_Ideology_Tag_Empathy_J) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_J;
            foreach (var defName in opinionList_Ideology_Tag_Judgemental) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in opinionList_Ideology_Tag_Morality) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Morality;
            foreach (var defName in opinionList_Ideology_Tag_Sympathy_J) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_J;
            foreach (var defName in opinionList_Ideology_Tag_Decency_M) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Decency_M;
            foreach (var defName in opinionList_Ideology_Tag_Decency_J) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Decency_J;
        }

        private static readonly List<string> moodList_Ideology_Tag_Empathy_M = new(
            ["AteVeneratedAnimalMeat",
            "SlaughteredAnimal_Know_Prohibited",
            "SlaughteredAnimal_Know_Prohibited_Mood",
            "SlaughteredAnimal_Horrible",
            "SlaughteredAnimal_Know_Horrible",
            "SlaughteredAnimal_Know_Horrible_Mood",
            "AteHumanMeat_Abhorrent",
            "ButcheredHuman_Abhorrent",
            "ButcheredHuman_Know_Abhorrent",
            "ButcheredHuman_Know_Abhorrent_Opinion",
            "AteHumanMeat_Know_Abhorrent",
            "HumanLeatherApparel_Abhorrent",
            "AteHumanMeat_Horrible",
            "ButcheredHuman_Horrible",
            "ButcheredHuman_Know_Horrible",
            "ButcheredHuman_Know_Horrible_Opinion",
            "AteHumanMeat_Know_Horrible",
            "HumanLeatherApparel_Horrible",
            "AteNonCannibalFood_Horrible",
            "AteNonCannibalFood_Know_Horrible",
            "AteNonCannibalFood_Abhorrent",
            "AteNonCannibalFood_Know_Abhorrent",
            "NoRecentHumanMeat_RequiredRavenous",
            "CharityRefused_Essential_Beggars_Betrayed",
            "CharityRefused_Important_Beggars_Betrayed",
            "CharityRefused_Essential_Pilgrims_Betrayed",
            "CharityRefused_Important_Pilgrims_Betrayed",
            "CharityRefused_Essential_ShuttleCrashRescue",
            "CharityRefused_Important_ShuttleCrashRescue",
            "CharityRefused_Essential_RefugeePodCrash",
            "CharityRefused_Important_RefugeePodCrash",
            "CharityRefused_Essential_ThreatReward_Joiner",
            "CharityRefused_Important_ThreatReward_Joiner",
            "CharityFulfilled_Essential_WandererJoins",
            "CharityFulfilled_Essential_ShuttleCrashRescue",
            "CharityFulfilled_Essential_RefugeePodCrash",
            "CharityFulfilled_Essential_ThreatReward_Joiner",
            "KilledInnocentAnimal_Know_Abhorrent",
            "KilledInnocentAnimal_Know_Abhorrent_Mood",
            "KilledInnocentAnimal_Horrible",
            "KilledInnocentAnimal_Know_Horrible",
            "KilledInnocentAnimal_Know_Horrible_Mood",
            "HarvestedOrgan_Abhorrent",
            "HarvestedOrgan_Know_Abhorrent",
            "HarvestedOrgan_Know_Abhorrent_Mood",
            "HarvestedOrgan_Horrible",
            "HarvestedOrgan_Know_Horrible",
            "HarvestedOrgan_Know_Horrible_Mood",
            "ExecutedGuest_Know_Abhorrent_Mood",
            "ExecutedColonist_Know_Abhorrent_Mood",
            "ExecutedPrisoner_Know_Abhorrent",
            "InnocentPrisonerDied_Know_Abhorrent",
            "InnocentPrisonerDied_Know_Abhorrent_Mood",
            "InnocentPrisonerDied_Abhorrent",
            "ExecutedPrisoner_Horrible",
            "InnocentPrisonerDied_Horrible",
            "ExecutedPrisoner_Know_Horrible",
            "InnocentPrisonerDied_Know_Horrible",
            "ExecutedPrisonerInnocent_Horrible",
            "ExecutedPrisonerInnocent_Know_Horrible",
            "Slavery_Abhorrent_SlavesInColony",
            "SoldSlave_Know_Abhorrent",
            "SoldSlave_Know_Abhorrent_Mood",
            "EnslavedPrisoner_Know_Abhorrent",
            "Slavery_Horrible_SlavesInColony",
            "SoldSlave_Horrible",
            "EnslavedPrisoner_Horrible",
            "SoldSlave_Know_Horrible",
            "SoldSlave_Know_Horrible_Mood",
            "EnslavedPrisoner_Know_Horrible",
            "EnslavedPrisoner_Know_Horrible_Mood"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Empathy_J = new(
            ["TameVeneratedAnimalDied",
            "SlaughteredAnimal_Disapproved",
            "SlaughteredAnimal_Know_Disapproved",
            "SlaughteredAnimal_Know_Disapproved_Mood",
            "AteHumanMeat_Disapproved",
            "ButcheredHuman_Disapproved",
            "ButcheredHuman_Know_Disapproved",
            "ButcheredHuman_Know_Disapproved_Opinion",
            "AteHumanMeat_Know_Disapproved",
            "HumanLeatherApparel_Disapproved",
            "NoRecentHumanMeat_RequiredStrong",
            "CharityRefused_Worthwhile_Beggars_Betrayed",
            "CharityRefused_Worthwhile_Pilgrims_Betrayed",
            "CharityRefused_Worthwhile_ShuttleCrashRescue",
            "CharityRefused_Worthwhile_RefugeePodCrash",
            "CharityRefused_Worthwhile_ThreatReward_Joiner",
            "CharityFulfilled_Important_WandererJoins",
            "CharityFulfilled_Worthwhile_WandererJoins",
            "CharityFulfilled_Important_ShuttleCrashRescue",
            "CharityFulfilled_Worthwhile_ShuttleCrashRescue",
            "CharityFulfilled_Important_RefugeePodCrash",
            "CharityFulfilled_Worthwhile_RefugeePodCrash",
            "CharityFulfilled_Important_ThreatReward_Joiner",
            "CharityFulfilled_Worthwhile_ThreatReward_Joiner",
            "KilledInnocentAnimal_Disapproved",
            "KilledInnocentAnimal_Know_Disapproved",
            "KilledInnocentAnimal_Know_Disapproved_Mood",
            "Slavery_Disapproved_SlavesInColony",
            "SoldSlave_Disapproved",
            "EnslavedPrisoner_Disapproved",
            "SoldSlave_Know_Disapproved",
            "SoldSlave_Know_Disapproved_Mood",
            "EnslavedPrisoner_Know_Disapproved",
            "EnslavedPrisoner_Know_Disapproved_Mood"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Judgemental = new(
            ["VeneratedAnimalsOnMapOrCaravan",
            "ChangedIdeo_Know_Disapproved",
            "IsApostate_Disapproved_Social",
            "HasAutomatedTurrets_Disapproved",
            "BlindingCeremony_Self_Respected",
            "BlindingCeremony_Self_Elevated",
            "BlindingCeremony_Self_Sublime",
            "Blindness_Respected_Blind",
            "Blindness_Elevated_Blind",
            "Blindness_Sublime_Blind",
            "Blindness_Respected_Blind_Social",
            "Blindness_Elevated_Blind_Social",
            "Blindness_Sublime_Blind_Social",
            "Blindness_ArtificialBlind",
            "Blindness_Elevated_HalfBlind",
            "Blindness_Sublime_HalfBlind",
            "Blindness_Elevated_HalfBlind_Social",
            "Blindness_Sublime_HalfBlind_Social",
            "Blindness_Elevated_NonBlind",
            "Blindness_Sublime_NonBlind",
            "Blindness_Respected_NonBlind_Social",
            "Blindness_Elevated_NonBlind_Social",
            "Blindness_Sublime_NonBlind_Social",
            "InstalledProsthetic_Disapproved",
            "InstalledProsthetic_Know_Disapproved",
            "HasProsthetic_Disapproved",
            "HasProsthetic_Disapproved_Social",
            "HasNoProsthetic_Disapproved",
            "HasNoProsthetic_Disapproved_Social",
            "HasProsthetic_Approved",
            "CharityRefused_Worthwhile_Pilgrims",
            "HighLife",
            "IdeoDiversity_Horrible_Uniform",
            "IdeoDiversity_Horrible_StyleDominance",
            "IdeoDiversity_Disapproved",
            "IdeoDiversity_Disapproved_Uniform",
            "IdeoDiversity_Disapproved_Social",
            "IdeoDiverity_Disapproved_AltarSharing",
            "IdeoDiversity_Disapproved_StyleDominance",
            "IdeoDiversity_Disapproved_ParticipatedInOthersRitual",
            "AteMeat_Disapproved",
            "AteMeat_Know_Disapproved",
            "AteNonMeat_Disapproved",
            "AteNonMeat_Know_Disapproved",
            "Mined_Disapproved",
            "Mined_Know_Disapproved",
            "Mined_Know_Disapproved_Mood",
            "MineableDestroyed_Disapproved",
            "Ranching_Central_AnimalMassPerCapita",
            "Ranching_SowedPlant",
            "ScarificationCeremony_Self_Minor",
            "ScarificationCeremony_Self_Heavy",
            "ScarificationCeremony_Self_Extreme",
            "Scarification_Extreme_Opinion",
            "Scarification_Extreme",
            "Scarification_Heavy_Opinion",
            "Scarification_Heavy",
            "Scarification_Minor_Opinion",
            "Scarification_Minor",
            "Skullspike_Desired",
            "Skullspike_Disapproved",
            "SleptUsingSleepAccelerator",
            "NeedNeuralSupercharge",
            "AgeReversalDemanded",
            "AgeReversalReceived",
            "BioSculpterDespised",
            "MinifiedTreeDied_Know_Disapproved_Mood",
            "CutTree_Disapproved",
            "CutTree_Know_Disapproved",
            "CutTree_Know_Disapproved_Mood",
            "IdeoRoleEmpty",
            "IdeoLeaderResentmentStandard",
            "IdeoLeaderResentmentDisapproved",
            "IdeoLeaderResentmentHorrible",
            "IdeoLeaderResentmentAbhorrent",
            "FailedConvertAbilityRecipient",
            "FailedConvertIdeoAttemptResentment"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Morality = new(
            ["ChangedIdeo_Know_Abhorrent",
            "ChangedIdeo_Know_Horrible",
            "IsApostate_Abhorrent_Social",
            "IsApostate_Horrible_Social",
            "HasAutomatedTurrets_Prohibited",
            "HasAutomatedTurrets_Horrible",
            "BlindingCeremony_Know_Horrible",
            "InstalledProsthetic_Abhorrent",
            "InstalledProsthetic_Know_Abhorrent",
            "HasProsthetic_Abhorrent",
            "HasProsthetic_Abhorrent_Social",
            "CharityRefused_Essential_Pilgrims",
            "CharityRefused_Important_Pilgrims",
            "IngestedDrug_Horrible",
            "IngestedRecreationalDrug_Horrible",
            "IngestedHardDrug_Horrible",
            "IngestedDrug_Know_Horrible",
            "IngestedRecreationalDrug_Know_Horrible",
            "IngestedHardDrug_Know_Horrible",
            "AdministeredDrug_Horrible",
            "AdministeredRecreationalDrug_Horrible",
            "AdministeredHardDrug_Horrible",
            "AdministeredDrug_Know_Horrible",
            "AdministeredRecreationalDrug_Know_Horrible",
            "AdministeredHardDrug_Know_Horrible",
            "IdeoDiversity_Abhorrent",
            "IdeoDiversity_Abhorrent_Uniform",
            "IdeoDiversity_Abhorrent_Social",
            "IdeoDiverity_Abhorrent_AltarSharing",
            "IdeoDiversity_Abhorrent_StyleDominance",
            "IdeoDiversity_Abhorrent_ParticipatedInOthersRitual",
            "IdeoDiversity_Horrible",
            "IdeoDiversity_Horrible_Social",
            "IdeoDiverity_Horrible_AltarSharing",
            "IdeoDiversity_Horrible_ParticipatedInOthersRitual",
            "AteMeat_Abhorrent",
            "AteMeat_Know_Abhorrent",
            "AteMeat_Horrible",
            "AteMeat_Know_Horrible",
            "AteNonMeat_Abhorrent",
            "AteNonMeat_Know_Abhorrent",
            "AteNonMeat_Horrible",
            "AteNonMeat_Know_Horrible",
            "Mined_Know_Prohibited",
            "Mined_Know_Prohibited_Mood",
            "MineableDestroyed_Prohibited",
            "Mined_Horrible",
            "Mined_Know_Horrible",
            "Mined_Know_Horrible_Mood",
            "MineableDestroyed_Horrible",
            "TradedOrgan_Abhorrent",
            "TradedOrgan_Know_Abhorrent",
            "TradedOrgan_Know_Abhorrent_Mood",
            "SoldOrgan_Disapproved",
            "SoldOrgan_Know_Disapproved",
            "SoldOrgan_Know_Horrible_Mood",
            "InstalledOrgan_Abhorrent",
            "InstalledOrgan_Know_Abhorrent",
            "InstalledOrgan_Know_Abhorrent_Mood",
            "Pain_Idealized",
            "ScarificationCeremony_Know_Horrible",
            "MinifiedTreeDied_Know_Prohibited_Mood",
            "MinifiedTreeDied_Know_Horrible_Mood",
            "CutTree_Know_Prohibited",
            "CutTree_Horrible",
            "CutTree_Know_Prohibited_Mood",
            "CutTree_Know_Horrible",
            "CutTree_Know_Horrible_Mood"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Preference = new(
            ["WearingDesiredApparel_Soft",
            "WearingDesiredApparel_Strong",
            "SlabBed_Preferred",
            "AteNonFungusPlant_Despised",
            "AteNonFungusMealWithPlants_Despised",
            "AteFungus_Preferred",
            "AteFungusAsIngredient_Preferred",
            "AteFungus_Despised",
            "AteFungusAsIngredient_Despised",
            "AteInsectMeat_Loved",
            "AteInsectMeatAsIngredient_Loved",
            "Darklight_Preferred_BlindingLight",
            "Darklight_Preferred_IndoorLight",
            "Darklight_Preferred_Darklight"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Sympathy_J = new(
            ["AteHumanMeat_Preferred",
            "NoRecentHumanMeat_Preferred",
            "AteHumanMeat_RequiredStrong",
            "ExecutedPrisonerGuilty_Respected",
            "GuiltyPrisonerDied_Respected",
            "ExecutedPrisonerGuilty_Know_Respected",
            "ExecutedPrisonerGuilty_Know_Respected_Mood",
            "GuiltyPrisonerDied_Know_Respected",
            "ExecutedPrisoner_Respected",
            "PrisonerDied_Respected",
            "ExecutedPrisoner_Know_Respected",
            "PrisonerDied_Know_Respected",
            "Execution_Know_Respected_Mood",
            "NoRecentExecution",
            "Slavery_Honorable_SlavesInColony",
            "Slavery_Honorable_NoSlavesInColony",
            "SoldSlave_Honorable",
            "EnslavedPrisoner_Honorable",
            "SoldSlave_Know_Honorable",
            "SoldSlave_Know_Honorable_Mood",
            "EnslavedPrisoner_Know_Honorable",
            "EnslavedPrisoner_Know_Honorable_Mood",
            "ParticipatedInRaid_Respected",
            "ParticipatedInRaid_Required",
            "RecentConquest_Respected",
            "RecentConquest_Required"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Sympathy_P = new(
            ["HumanLeatherApparel_Preferred",
            "HumanLeatherApparel_RequiredStrong",
            "HumanLeatherApparel_RequiredRavenous"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Sympathy_M = new(
            ["AteHumanMeat_RequiredRavenous"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Charity_M = new(
            ["CharityRefused_Essential_Beggars",
            "CharityRefused_Important_Beggars",
            "CharityRefused_Essential_HospitalityRefugees",
            "CharityRefused_Important_HospitalityRefugees",
            "CharityFulfilled_Essential_Beggars",
            "CharityFulfilled_Essential_HospitalityRefugees"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Charity_J = new(
            ["CharityRefused_Worthwhile_Beggars",
            "CharityRefused_Worthwhile_HospitalityRefugees",
            "CharityFulfilled_Important_Beggars",
            "CharityFulfilled_Worthwhile_Beggars",
            "CharityFulfilled_Important_HospitalityRefugees",
            "CharityFulfilled_Worthwhile_HospitalityRefugees"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Worry_M = new(
            ["CharityRefused_Essential_WandererJoins",
            "CharityRefused_Important_WandererJoins",
            "CharityRefused_Essential_IntroWimp",
            "CharityRefused_Important_IntroWimp",
            "CharityFulfilled_Essential_IntroWimp"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Worry_J = new(
            ["CharityRefused_Worthwhile_WandererJoins",
            "CharityRefused_Worthwhile_IntroWimp",
            "CharityFulfilled_Important_IntroWimp",
            "CharityFulfilled_Worthwhile_IntroWimp"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Openmindedness = new(
            ["IdeoDiversity_Standard_StyleDominance",
            "IdeoDiversity_Approved",
            "IdeoDiversity_Approved_StyleDominance",
            "IdeoDiversity_Respected",
            "IdeoDiversity_Respected_StyleDominance",
            "IdeoDiversity_Exalted",
            "IdeoDiversity_Exalted_StyleDominance"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Decency_M = new(
            ["GotLovin_Abhorrent",
            "Lovin_Know_Abhorrent",
            "GotLovin_Horrible",
            "Lovin_Know_Horrible"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Decency_J = new(
            ["GotLovin_Disapproved",
            "Lovin_Know_Disapproved",
            "AnyBodyPartCovered_Disapproved_Male",
            "AnyBodyPartCovered_Disapproved_Female",
            "AnyBodyPartCovered_Disapproved_Memory",
            "AnyBodyPartCovered_Disapproved_Social_Male",
            "AnyBodyPartCovered_Disapproved_Social_Female",
            "AnyBodyPartButGroinCovered_Disapproved_Male",
            "AnyBodyPartButGroinCovered_Disapproved_Female",
            "AnyBodyPartButGroinCovered_Disapproved_Memory",
            "AnyBodyPartButGroinCovered_Disapproved_Social_Male",
            "AnyBodyPartButGroinCovered_Disapproved_Social_Female",
            "GroinUncovered_Disapproved_Male",
            "GroinUncovered_Disapproved_Female",
            "GroinUncovered_Disapproved_Social_Male",
            "GroinUncovered_Disapproved_Social_Female",
            "GroinOrChestUncovered_Disapproved_Male",
            "GroinOrChestUncovered_Disapproved_Female",
            "GroinOrChestUncovered_Disapproved_Social_Male",
            "GroinOrChestUncovered_Disapproved_Social_Female",
            "GroinChestOrHairUncovered_Disapproved_Male",
            "GroinChestOrHairUncovered_Disapproved_Female",
            "GroinChestOrHairUncovered_Disapproved_Social_Male",
            "GroinChestOrHairUncovered_Disapproved_Social_Female",
            "GroinChestHairOrFaceUncovered_Disapproved_Male",
            "GroinChestHairOrFaceUncovered_Disapproved_Female",
            "GroinChestHairOrFaceUncovered_Disapproved_Social_Male",
            "GroinChestHairOrFaceUncovered_Disapproved_Social_Female",
            "KillWithNobleWeapon",
            "UsedDespisedWeapon",
            "WieldingNobleOrDespisedWeapon"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Affluence = new(
            ["IdeoRoleLost"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Decency = new(
            ["IdeoRoleApparelRequirementNotMet"]
        );

        private static readonly List<string> moodList_Ideology_Tag_Gathering = new(
            ["FunParty",
            "UnforgettableParty",
            "TerribleFestival",
            "BoringFestival",
            "FunFestival",
            "UnforgettableFestival",
            "TerribleSkyLanterns",
            "UnimpressiveSkyLanterns",
            "BeautifulSkyLanterns",
            "UnforgettableSkyLanterns"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Empathy_M = new(
            ["SlaughteredAnimal_Know_Prohibited",
            "SlaughteredAnimal_Know_Horrible",
            "ButcheredHuman_Know_Abhorrent_Opinion",
            "AteHumanMeat_Know_Abhorrent",
            "ButcheredHuman_Know_Horrible_Opinion",
            "AteHumanMeat_Know_Horrible",
            "AteNonCannibalFood_Know_Horrible",
            "AteNonCannibalFood_Know_Abhorrent",
            "KilledInnocentAnimal_Know_Abhorrent",
            "KilledInnocentAnimal_Know_Horrible",
            "HarvestedOrgan_Know_Abhorrent",
            "HarvestedOrgan_Know_Horrible",
            "ExecutedPrisoner_Know_Abhorrent",
            "InnocentPrisonerDied_Know_Abhorrent",
            "ExecutedPrisoner_Know_Horrible",
            "InnocentPrisonerDied_Know_Horrible",
            "ExecutedPrisonerInnocent_Know_Horrible",
            "SoldSlave_Know_Abhorrent",
            "EnslavedPrisoner_Know_Abhorrent",
            "SoldSlave_Know_Horrible",
            "EnslavedPrisoner_Know_Horrible"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Empathy_J = new(
            ["SlaughteredAnimal_Know_Disapproved",
            "ButcheredHuman_Know_Disapproved_Opinion",
            "AteHumanMeat_Know_Disapproved",
            "KilledInnocentAnimal_Know_Disapproved",
            "SoldSlave_Know_Disapproved",
            "EnslavedPrisoner_Know_Disapproved"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Judgemental = new(
            ["ChangedIdeo_Know_Disapproved",
            "IsApostate_Disapproved_Social",
            "Blindness_Respected_Blind_Social",
            "Blindness_Elevated_Blind_Social",
            "Blindness_Sublime_Blind_Social",
            "Blindness_Elevated_HalfBlind_Social",
            "Blindness_Sublime_HalfBlind_Social",
            "Blindness_Respected_NonBlind_Social",
            "Blindness_Elevated_NonBlind_Social",
            "Blindness_Sublime_NonBlind_Social",
            "InstalledProsthetic_Know_Disapproved",
            "HasProsthetic_Disapproved_Social",
            "HasNoProsthetic_Disapproved_Social",
            "IdeoDiversity_Disapproved_Social",
            "AteMeat_Know_Disapproved",
            "AteNonMeat_Know_Disapproved",
            "Mined_Know_Disapproved",
            "Scarification_Extreme_Opinion",
            "Scarification_Heavy_Opinion",
            "Scarification_Minor_Opinion",
            "CutTree_Know_Disapproved",
            "FailedConvertIdeoAttemptResentment"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Morality = new(
            ["ChangedIdeo_Know_Abhorrent",
            "ChangedIdeo_Know_Horrible",
            "IsApostate_Abhorrent_Social",
            "IsApostate_Horrible_Social",
            "InstalledProsthetic_Know_Abhorrent",
            "HasProsthetic_Abhorrent_Social",
            "IngestedDrug_Know_Horrible",
            "IngestedRecreationalDrug_Know_Horrible",
            "IngestedHardDrug_Know_Horrible",
            "AdministeredDrug_Know_Horrible",
            "AdministeredRecreationalDrug_Know_Horrible",
            "AdministeredHardDrug_Know_Horrible",
            "IdeoDiversity_Abhorrent_Social",
            "IdeoDiversity_Horrible_Social",
            "AteMeat_Know_Abhorrent",
            "AteMeat_Know_Horrible",
            "AteNonMeat_Know_Abhorrent",
            "AteNonMeat_Know_Horrible",
            "Mined_Know_Prohibited",
            "Mined_Know_Horrible",
            "TradedOrgan_Know_Abhorrent",
            "SoldOrgan_Know_Disapproved",
            "InstalledOrgan_Know_Abhorrent",
            "CutTree_Know_Prohibited",
            "CutTree_Know_Horrible"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Sympathy_J = new(
            ["ExecutedPrisonerGuilty_Know_Respected",
            "GuiltyPrisonerDied_Know_Respected",
            "ExecutedPrisoner_Know_Respected",
            "PrisonerDied_Know_Respected",
            "SoldSlave_Know_Honorable",
            "EnslavedPrisoner_Know_Honorable"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Decency_M = new(
            ["Lovin_Know_Abhorrent",
            "Lovin_Know_Horrible"]
        );

        private static readonly List<string> opinionList_Ideology_Tag_Decency_J = new(
            ["Lovin_Know_Disapproved",
            "AnyBodyPartCovered_Disapproved_Social_Male",
            "AnyBodyPartCovered_Disapproved_Social_Female",
            "AnyBodyPartButGroinCovered_Disapproved_Female",
            "AnyBodyPartButGroinCovered_Disapproved_Social_Male",
            "AnyBodyPartButGroinCovered_Disapproved_Social_Female",
            "GroinUncovered_Disapproved_Social_Male",
            "GroinUncovered_Disapproved_Social_Female",
            "GroinOrChestUncovered_Disapproved_Social_Male",
            "GroinOrChestUncovered_Disapproved_Social_Female",
            "GroinChestOrHairUncovered_Disapproved_Social_Male",
            "GroinChestOrHairUncovered_Disapproved_Social_Female",
            "GroinChestHairOrFaceUncovered_Disapproved_Social_Male",
            "GroinChestHairOrFaceUncovered_Disapproved_Social_Female"]
        );
    }
}
