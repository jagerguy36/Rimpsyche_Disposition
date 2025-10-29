using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class CoreDB: BaseThoughtDB
    {
        public static void AddDefs_Vanilla(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            RegisterThoughts(moodList_Vanilla_Tag_Preference, MoodThoughtTagDB, FormulaDB.Tag_Preference);
            RegisterThoughts(moodList_Vanilla_Tag_Empathy_M, MoodThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(moodList_Vanilla_Tag_Empathy, MoodThoughtTagDB, FormulaDB.Tag_Empathy);
            RegisterThoughts(moodList_Vanilla_Tag_Empathy_Bond, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Bond);
            RegisterThoughts(moodList_Vanilla_Tag_Sympathy, MoodThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(moodList_Vanilla_Tag_Empathy_Kin, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Kin);
            RegisterThoughts(moodList_Vanilla_Tag_Empathy_Loved, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Loved);
            RegisterThoughts(moodList_Vanilla_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_Vanilla_Tag_Loved, MoodThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(moodList_Vanilla_Tag_Gathering, MoodThoughtTagDB, FormulaDB.Tag_Gathering);
            RegisterThoughts(moodList_Vanilla_Tag_Concert, MoodThoughtTagDB, FormulaDB.Tag_Concert);
            RegisterThoughts(moodList_Vanilla_Tag_Worry, MoodThoughtTagDB, FormulaDB.Tag_Worry);
            RegisterThoughts(moodList_Vanilla_Tag_Worry_Bond, MoodThoughtTagDB, FormulaDB.Tag_Worry_Bond);
            RegisterThoughts(moodList_Vanilla_Tag_Disquiet, MoodThoughtTagDB, FormulaDB.Tag_Disquiet);
            RegisterThoughts(moodList_Vanilla_Tag_Worry_Kin, MoodThoughtTagDB, FormulaDB.Tag_Worry_Kin);
            RegisterThoughts(moodList_Vanilla_Tag_Worry_Loved, MoodThoughtTagDB, FormulaDB.Tag_Worry_Loved);
            RegisterThoughts(moodList_Vanilla_Tag_Needy, MoodThoughtTagDB, FormulaDB.Tag_Needy);
            RegisterThoughts(moodList_Vanilla_Tag_Worry_Outsider_M, MoodThoughtTagDB, FormulaDB.Tag_Worry_Outsider_M);
            RegisterThoughts(moodList_Vanilla_Tag_Outsider, MoodThoughtTagDB, FormulaDB.Tag_Outsider);
            RegisterThoughts(moodList_Vanilla_Tag_Bloodlust, MoodThoughtTagDB, FormulaDB.Tag_Bloodlust);
            RegisterThoughts(moodList_Vanilla_Tag_Harmed, MoodThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(moodList_Vanilla_Tag_Fear, MoodThoughtTagDB, FormulaDB.Tag_Fear);
            RegisterThoughts(moodList_Vanilla_Tag_SawDeath, MoodThoughtTagDB, FormulaDB.Tag_SawDeath);
            RegisterThoughts(moodList_Vanilla_Tag_SawDeath_Outsider, MoodThoughtTagDB, FormulaDB.Tag_SawDeath_Outsider);
            RegisterThoughts(moodList_Vanilla_Tag_SawDeath_Kin, MoodThoughtTagDB, FormulaDB.Tag_SawDeath_Kin);
            RegisterThoughts(moodList_Vanilla_Tag_Worry_Outsider, MoodThoughtTagDB, FormulaDB.Tag_Worry_Outsider);
            RegisterThoughts(moodList_Vanilla_Tag_Harmed_Loved, MoodThoughtTagDB, FormulaDB.Tag_Harmed_Loved);
            RegisterThoughts(moodList_Vanilla_Tag_Harmed_Bond, MoodThoughtTagDB, FormulaDB.Tag_Harmed_Bond);
            RegisterThoughts(moodList_Vanilla_Tag_Decency, MoodThoughtTagDB, FormulaDB.Tag_Decency);
            RegisterThoughts(moodList_Vanilla_Tag_Sympathy_M, MoodThoughtTagDB, FormulaDB.Tag_Sympathy_M);
            RegisterThoughts(moodList_Vanilla_Tag_Sympathy_P, MoodThoughtTagDB, FormulaDB.Tag_Sympathy_P);
            RegisterThoughts(moodList_Vanilla_Tag_Needy_Art, MoodThoughtTagDB, FormulaDB.Tag_Needy_Art);
            RegisterThoughts(moodList_Vanilla_Tag_Bond, MoodThoughtTagDB, FormulaDB.Tag_Bond);
            RegisterThoughts(moodList_Vanilla_Tag_Art, MoodThoughtTagDB, FormulaDB.Tag_Art);
            RegisterThoughts(opinionList_Vanilla_Tag_Empathy_M, OpinionThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(opinionList_Vanilla_Tag_Loved, OpinionThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(opinionList_Vanilla_Tag_Harmed, OpinionThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(opinionList_Vanilla_Tag_Harmed_Loved, OpinionThoughtTagDB, FormulaDB.Tag_Harmed_Loved);
            RegisterThoughts(opinionList_Vanilla_Tag_Harmed_Bond, OpinionThoughtTagDB, FormulaDB.Tag_Harmed_Bond);
            RegisterThoughts(opinionList_Vanilla_Tag_Harmed_Kin, OpinionThoughtTagDB, FormulaDB.Tag_Harmed_Kin);
            RegisterThoughts(opinionList_Vanilla_Tag_Decency, OpinionThoughtTagDB, FormulaDB.Tag_Decency);
            RegisterThoughts(opinionList_Vanilla_Tag_Judgemental, OpinionThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(opinionList_Vanilla_Tag_Morality, OpinionThoughtTagDB, FormulaDB.Tag_Morality);

            RegisterStageThought("KnowGuestExecuted", MoodThoughtTagDB,
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M] //someone was ripscanned
            );
            RegisterStageThought("KnowColonistExecuted", MoodThoughtTagDB,
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M, //someone was organ-murdered
                FormulaDB.Tag_Empathy_M] //someone was ripscanned
            );
            RegisterStageThought("NeedJoy", MoodThoughtTagDB,
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence]
            );
            RegisterStageThought("NeedComfort", MoodThoughtTagDB,
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence,
                FormulaDB.Tag_Affluence]
            );
            RegisterStageThought("NeedRoomSize", MoodThoughtTagDB,
                [FormulaDB.Tag_Needy,
                FormulaDB.Tag_Needy,
                FormulaDB.Tag_Affluence]
            );
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

        private static readonly List<string> moodList_Vanilla_Tag_Bloodlust = new(
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
