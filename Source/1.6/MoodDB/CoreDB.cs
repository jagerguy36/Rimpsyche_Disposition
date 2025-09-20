using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public class CoreDB
    {
        public static void AddDefs_Vanilla_Stage(Dictionary<string, RimpsycheFormula[]> StageThoughtTagDB, Dictionary<string, RimpsycheFormula[]> StageOpinionThoughtTagDB)
        {
            StageThoughtTagDB["KnowGuestExecuted"] =
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M]; //someone was ripscanned
            StageThoughtTagDB["KnowColonistExecuted"] =
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M]; //someone was ripscanned
            StageThoughtTagDB["NeedJoy"] =
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence];
            StageThoughtTagDB["NeedComfort"] =
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence];
            StageThoughtTagDB["NeedRoomSize"] =
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence];
        }
        public static void AddDefs_Vanilla(Dictionary<string, RimpsycheFormula> MoodThoughtTagDB, Dictionary<string, RimpsycheFormula> OpinionThoughtTagDB)
        {
            foreach (var defName in moodList_Vanilla_Tag_Preference) MoodThoughtTagDB[defName] = FormulaDB.Tag_Preference;
            foreach (var defName in moodList_Vanilla_Tag_Empathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in moodList_Vanilla_Tag_Empathy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy;
            foreach (var defName in moodList_Vanilla_Tag_Empathy_Bond) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Bond;
            foreach (var defName in moodList_Vanilla_Tag_Sympathy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy;
            foreach (var defName in moodList_Vanilla_Tag_Empathy_Kin) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Kin;
            foreach (var defName in moodList_Vanilla_Tag_Empathy_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Empathy_Loved;
            foreach (var defName in moodList_Vanilla_Tag_Affluence) MoodThoughtTagDB[defName] = FormulaDB.Tag_Affluence;
            foreach (var defName in moodList_Vanilla_Tag_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Loved;
            foreach (var defName in moodList_Vanilla_Tag_Gathering) MoodThoughtTagDB[defName] = FormulaDB.Tag_Gathering;
            foreach (var defName in moodList_Vanilla_Tag_Concert) MoodThoughtTagDB[defName] = FormulaDB.Tag_Concert;
            foreach (var defName in moodList_Vanilla_Tag_Worry) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry;
            foreach (var defName in moodList_Vanilla_Tag_Worry_Bond) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Bond;
            foreach (var defName in moodList_Vanilla_Tag_Disquiet) MoodThoughtTagDB[defName] = FormulaDB.Tag_Disquiet;
            foreach (var defName in moodList_Vanilla_Tag_Worry_Kin) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Kin;
            foreach (var defName in moodList_Vanilla_Tag_Worry_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Loved;
            foreach (var defName in moodList_Vanilla_Tag_Needy) MoodThoughtTagDB[defName] = FormulaDB.Tag_Needy;
            foreach (var defName in moodList_Vanilla_Tag_Worry_Outsider_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Outsider_M;
            foreach (var defName in moodList_Vanilla_Tag_Outsider) MoodThoughtTagDB[defName] = FormulaDB.Tag_Outsider;
            foreach (var defName in moodList_Vanilla_Tag_Bloodlost) MoodThoughtTagDB[defName] = FormulaDB.Tag_Bloodlost;
            foreach (var defName in moodList_Vanilla_Tag_Harmed) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in moodList_Vanilla_Tag_Fear) MoodThoughtTagDB[defName] = FormulaDB.Tag_Fear;
            foreach (var defName in moodList_Vanilla_Tag_SawDeath) MoodThoughtTagDB[defName] = FormulaDB.Tag_SawDeath;
            foreach (var defName in moodList_Vanilla_Tag_SawDeath_Outsider) MoodThoughtTagDB[defName] = FormulaDB.Tag_SawDeath_Outsider;
            foreach (var defName in moodList_Vanilla_Tag_SawDeath_Kin) MoodThoughtTagDB[defName] = FormulaDB.Tag_SawDeath_Kin;
            foreach (var defName in moodList_Vanilla_Tag_Worry_Outsider) MoodThoughtTagDB[defName] = FormulaDB.Tag_Worry_Outsider;
            foreach (var defName in moodList_Vanilla_Tag_Harmed_Loved) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed_Loved;
            foreach (var defName in moodList_Vanilla_Tag_Harmed_Bond) MoodThoughtTagDB[defName] = FormulaDB.Tag_Harmed_Bond;
            foreach (var defName in moodList_Vanilla_Tag_Decency) MoodThoughtTagDB[defName] = FormulaDB.Tag_Decency;
            foreach (var defName in moodList_Vanilla_Tag_Sympathy_M) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_M;
            foreach (var defName in moodList_Vanilla_Tag_Sympathy_P) MoodThoughtTagDB[defName] = FormulaDB.Tag_Sympathy_P;
            foreach (var defName in moodList_Vanilla_Tag_Needy_Art) MoodThoughtTagDB[defName] = FormulaDB.Tag_Needy_Art;
            foreach (var defName in moodList_Vanilla_Tag_Bond) MoodThoughtTagDB[defName] = FormulaDB.Tag_Bond;
            foreach (var defName in moodList_Vanilla_Tag_Art) MoodThoughtTagDB[defName] = FormulaDB.Tag_Art;
            foreach (var defName in opinionList_Vanilla_Tag_Empathy_M) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Empathy_M;
            foreach (var defName in opinionList_Vanilla_Tag_Loved) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Loved;
            foreach (var defName in opinionList_Vanilla_Tag_Harmed) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed;
            foreach (var defName in opinionList_Vanilla_Tag_Harmed_Loved) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed_Loved;
            foreach (var defName in opinionList_Vanilla_Tag_Harmed_Bond) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed_Bond;
            foreach (var defName in opinionList_Vanilla_Tag_Harmed_Kin) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Harmed_Kin;
            foreach (var defName in opinionList_Vanilla_Tag_Decency) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Decency;
            foreach (var defName in opinionList_Vanilla_Tag_Judgemental) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Judgemental;
            foreach (var defName in opinionList_Vanilla_Tag_Morality) OpinionThoughtTagDB[defName] = FormulaDB.Tag_Morality;
        }

        private static readonly List<string> moodList_Vanilla_Tag_Preference = new(
            ["AteNutrientPasteMeal",
            "AteInsectMeatDirect",
            "AteInsectMeatAsIngredient"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Empathy_M = new(
            ["KnowPrisonerDiedInnocent",
            "AteHumanlikeMeatDirect",
            "AteHumanlikeMeatAsIngredient",
            "KnowGuestOrganHarvested",
            "KnowColonistOrganHarvested",
            "ButcheredHumanlikeCorpse",
            "KnowButcheredHumanlikeCorpse",
            "ButcheredHumanlikeCorpseOpinion"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Empathy = new(
            ["KnowColonistDied",
            "PawnWithGoodOpinionDied"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Empathy_Bond = new(
            ["BondedAnimalDied"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Sympathy = new(
            ["PawnWithBadOpinionDied",
            "AteHumanlikeMeatDirectCannibal",
            "AteHumanlikeMeatAsIngredientCannibal"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Empathy_Kin = new(
            ["MySonDied",
            "MyDaughterDied",
            "MyBrotherDied",
            "MySisterDied",
            "MyGrandchildDied",
            "MyFatherDied",
            "MyMotherDied",
            "MyNieceDied",
            "MyNephewDied",
            "MyHalfSiblingDied",
            "MyAuntDied",
            "MyUncleDied",
            "MyGrandparentDied",
            "MyCousinDied",
            "MyKinDied"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Empathy_Loved = new(
            ["MyHusbandDied",
            "MyWifeDied",
            "MyFianceDied",
            "MyFianceeDied",
            "MyLoverDied"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Affluence = new(
            ["AteLavishMeal",
            "AteFineMeal",
            "AteRawFood",
            "AteKibble",
            "AteCorpse",
            "AteRottenFood",
            "AteWithoutTable",
            "AteInImpressiveDiningRoom",
            "JoyActivityInImpressiveRecRoom",
            "SleptInBedroom",
            "SleptInBarracks",
            "ApparelDamaged",
            "PrisonCell",
            "PrisonBarracks",
            "HospitalPatientRoomStats",
            "Greedy",
            "Jealous"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Loved = new(
            ["GotMarried",
            "BrokeUpWithMe",
            "BrokeUpWithMeMood",
            "CheatedOnMe",
            "CheatedOnMeMood",
            "DivorcedMe",
            "DivorcedMeMood",
            "RejectedMyProposal",
            "RejectedMyProposalMood",
            "HoneymoonPhase",
            "GotSomeLovin",
            "Affair",
            "OpinionOfMyLover",
            "WantToSleepWithSpouseOrLover"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Gathering = new(
            ["AttendedWedding",
            "AttendedParty"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Concert = new(
            ["AttendedConcert",
            "HeldConcert"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry = new(
            ["ColonistLost",
            "PawnWithGoodOpinionLost",
            "ColonistBanished",
            "ColonistBanishedToDie"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry_Bond = new(
            ["BondedAnimalReleased",
            "BondedAnimalLost",
            "BondedAnimalBanished"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Disquiet = new(
            ["PawnWithBadOpinionLost"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry_Kin = new(
            ["MySonLost",
            "MyDaughterLost",
            "MyBrotherLost",
            "MySisterLost",
            "MyGrandchildLost",
            "MyFatherLost",
            "MyMotherLost",
            "MyNieceLost",
            "MyNephewLost",
            "MyHalfSiblingLost",
            "MyAuntLost",
            "MyUncleLost",
            "MyGrandparentLost",
            "MyCousinLost",
            "MyKinLost",
            "FailedToRescueRelative"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry_Loved = new(
            ["MyHusbandLost",
            "MyWifeLost",
            "MyFianceLost",
            "MyFianceeLost",
            "MyLoverLost"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Needy = new(
            ["SleptOutside",
            "SleptOnGround",
            "SleptInCold",
            "SleptInHeat",
            "SoakingWet",
            "EnvironmentCold",
            "EnvironmentHot",
            "NeedFood",
            "NeedRest",
            "NeedOutdoors",
            "NeedIndoors",
            "DrugDesireFascination",
            "DrugDesireInterest",
            "Pain"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry_Outsider_M = new(
            ["KnowPrisonerSold"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Outsider = new(
            ["ReleasedHealthyPrisoner"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Bloodlost = new(
            ["HarvestedOrgan_Bloodlust",
            "WitnessedDeathBloodlust",
            "KilledHumanlikeBloodlust"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Harmed = new(
            ["MyOrganHarvested",
            "WasImprisoned",
            "HarmedMe",
            "BotchedMySurgery",
            "ForcedMeToTakeDrugs",
            "ForcedMeToTakeDrugsMood",
            "ForcedMeToTakeLuciferium",
            "ForcedMeToTakeLuciferiumMood",
            "KilledMyFriend",
            "HoldingMePrisoner"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Fear = new(
            ["ObservedLayingCorpse",
            "ObservedLayingRottingCorpse",
            "EnvironmentDark"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_SawDeath = new(
            ["WitnessedDeathAlly"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_SawDeath_Outsider = new(
            ["WitnessedDeathNonAlly"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_SawDeath_Kin = new(
            ["WitnessedDeathFamily"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Worry_Outsider = new(
            ["DeniedJoining",
            "PrisonerBanishedToDie"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Harmed_Loved = new(
            ["SoldMyLovedOne",
            "KilledMyLover",
            "KilledMyFiance",
            "KilledMySpouse"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Harmed_Bond = new(
            ["SoldMyBondedAnimal",
            "SoldMyBondedAnimalMood",
            "KilledMyBondedAnimal"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Decency = new(
            ["WrongApparelGender",
            "DeadMansApparel",
            "SharedBed",
            "Naked"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Sympathy_M = new(
            ["HumanLeatherApparelSad"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Sympathy_P = new(
            ["HumanLeatherApparelHappy"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Needy_Art = new(
            ["NeedBeauty"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Bond = new(
            ["BondedAnimalMaster",
            "NotBondedAnimalMaster"]
        );

        private static readonly List<string> moodList_Vanilla_Tag_Art = new(
            ["Aurora"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Empathy_M = new(
            ["ButcheredHumanlikeCorpseOpinion"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Loved = new(
            ["BrokeUpWithMe",
            "CheatedOnMe",
            "DivorcedMe",
            "RejectedMyProposal",
            "HoneymoonPhase",
            "GotSomeLovin",
            "Affair",
            "OpinionOfMyLover"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Harmed = new(
            ["HarmedMe",
            "BotchedMySurgery",
            "ForcedMeToTakeDrugs",
            "ForcedMeToTakeLuciferium",
            "KilledMyFriend",
            "HoldingMePrisoner"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Harmed_Loved = new(
            ["SoldMyLovedOne",
            "KilledMyLover",
            "KilledMyFiance",
            "KilledMySpouse"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Harmed_Bond = new(
            ["SoldMyBondedAnimal",
            "KilledMyBondedAnimal"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Harmed_Kin = new(
            ["KilledMyFather",
            "KilledMyMother",
            "KilledMySon",
            "KilledMyDaughter",
            "KilledMyBrother",
            "KilledMySister",
            "KilledMyKin"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Decency = new(
            ["SharedBed"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Judgemental = new(
            ["Incestuous",
            "HardWorkerVsLazy",
            "TeetotalerVsChemicalInterest",
            "TeetotalerVsAddict",
            "ChemicalInterestVsTeetotaler",
            "Man",
            "Woman",
            "TranshumanistAppreciation",
            "BodyPuristDisgust"]
        );

        private static readonly List<string> opinionList_Vanilla_Tag_Morality = new(
            ["AteRawHumanlikeMeat",
            "SoldPrisoner",
            "ExecutedPrisoner"]
        );
    }
}
