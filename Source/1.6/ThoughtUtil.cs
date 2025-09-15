using Verse;
using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class ThoughtUtil
    {
        //Base Function
        private static readonly float MoodCurveC = RimpsycheDispositionSettings.moodPreceptC;
        public static float MoodMultCurve(float mood)
        {
            if (mood >= 0)
            {
                return 1f + (MoodCurveC - (MoodCurveC / ((2f * mood) + 1f)));
            }
            else
            {
                return 1f + ((MoodCurveC / (1f - (2f * mood))) - MoodCurveC);
            }
        }

        public static Dictionary<string, RimpsycheFormula> IssueMultiplierDB = [];
        public static Dictionary<string, RimpsycheFormula> MoodMultiplierDB = [];

        static ThoughtUtil()
        {
            if (RimpsycheDispositionSettings.useIndividualThoughts)
            {
                Initialize();
                ModCompat();
            }
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] ThoughtUtil initialized.");
            AddBaseIssues();
            AddBaseThoughts();
        }

        public static void ModCompat()
        {
            Log.Message("[Rimpsyche - Disposition] Compatibility Thoughts added.");
        }

        private static void AddBaseIssues()
        {
            //IssueMultiplierDB["defname"] = FormulaDB.Issue_Morality;
            //IssueMultiplierDB["defname"] = FormulaDB.Issue_Preference;
            //IssueMultiplierDB["defname"] = FormulaDB.Issue_Propriety;
        }

        private static void AddBaseThoughts()
        {
            //Died
            MoodMultiplierDB["KnowColonistDied"] = FormulaDB.Mood_Died;
            //MoodMultiplierDB["KnowPrisonerDiedInnocent"] = FormulaDB.Mood_Died_Innocent;
            MoodMultiplierDB["BondedAnimalDied"] = FormulaDB.Mood_Died_Bond;
            MoodMultiplierDB["PawnWithGoodOpinionDied"] = FormulaDB.Mood_Died_Social;
            MoodMultiplierDB["PawnWithBadOpinionDied"] = FormulaDB.Mood_Died_Glad_Social;
            foreach (var defName in moodList_Mood_Died_Kin) MoodMultiplierDB[defName] = FormulaDB.Mood_Died_Kin;
            foreach (var defName in moodList_Mood_Died_Loved) MoodMultiplierDB[defName] = FormulaDB.Mood_Died_Loved;

            //Lost
            MoodMultiplierDB["ColonistLost"] = FormulaDB.Mood_Lost;
            MoodMultiplierDB["ColonistBanished"] = FormulaDB.Mood_Lost;
            MoodMultiplierDB["ColonistBanishedToDie"] = FormulaDB.Mood_Lost;
            MoodMultiplierDB["BondedAnimalReleased"] = FormulaDB.Mood_Bond;
            MoodMultiplierDB["BondedAnimalLost"] = FormulaDB.Mood_Lost_Bond;
            MoodMultiplierDB["BondedAnimalBanished"] = FormulaDB.Mood_Lost_Bond;
            MoodMultiplierDB["PawnWithGoodOpinionLost"] = FormulaDB.Mood_Lost_Social;
            MoodMultiplierDB["PawnWithBadOpinionLost"] = FormulaDB.Mood_Lost_Glad_Social;
            MoodMultiplierDB["DeniedJoining"] = FormulaDB.Mood_Lost_Trust;
            MoodMultiplierDB["PrisonerBanishedToDie"] = FormulaDB.Mood_Lost_Trust;
            MoodMultiplierDB["FailedToRescueRelative"] = FormulaDB.Mood_Lost_Kin;
            foreach (var defName in moodList_Mood_Lost_Kin) MoodMultiplierDB[defName] = FormulaDB.Mood_Lost_Kin;
            foreach (var defName in moodList_Mood_Lost_Loved) MoodMultiplierDB[defName] = FormulaDB.Mood_Lost_Loved;

            //Expect
            MoodMultiplierDB["NeedJoy"] = FormulaDB.Mood_Expect_Joy;
            MoodMultiplierDB["NeedBeauty"] = FormulaDB.Mood_Expect_Art;
            MoodMultiplierDB["AteWithoutTable"] = FormulaDB.Mood_Expect_Organize;
            MoodMultiplierDB["EnvironmentDark"] = FormulaDB.Mood_Expect_Need_Fear;
            MoodMultiplierDB["DeadMansApparel"] = FormulaDB.Mood_Expect_Need_Fear;
            MoodMultiplierDB["HumanLeatherApparelSad"] = FormulaDB.Mood_Expect_Human;
            MoodMultiplierDB["HumanLeatherApparelHappy"] = FormulaDB.Mood_Expect_Glad_Human;
            foreach (var defName in moodList_Mood_Expect) MoodMultiplierDB[defName] = FormulaDB.Mood_Expect;
            foreach (var defName in moodList_Mood_Expect_Human) MoodMultiplierDB[defName] = FormulaDB.Mood_Expect_Human;
            foreach (var defName in moodList_Mood_Expect_Need) MoodMultiplierDB[defName] = FormulaDB.Mood_Expect_Need;
            foreach (var defName in moodList_Mood_Expect_Drug) MoodMultiplierDB[defName] = FormulaDB.Mood_Expect_Drug;

            //Prisoners
            MoodMultiplierDB["KnowPrisonerSold"] = FormulaDB.Mood_Prisoner_Sold;
            MoodMultiplierDB["ReleasedHealthyPrisoner"] = FormulaDB.Mood_Prisoner_Released;

            //Gathering
            MoodMultiplierDB["GotMarried"] = FormulaDB.Mood_Bond;
            MoodMultiplierDB["AttendedWedding"] = FormulaDB.Mood_Social;
            MoodMultiplierDB["AttendedParty"] = FormulaDB.Mood_Social_Play;
            MoodMultiplierDB["AttendedConcert"] = FormulaDB.Mood_Social_Art;
            MoodMultiplierDB["HeldConcert"] = FormulaDB.Mood_Social_Art;

            //Ambition
            MoodMultiplierDB["NewColonyOptimism"] = FormulaDB.Mood_Ambition_New;
            MoodMultiplierDB["NewColonyHope"] = FormulaDB.Mood_Ambition_New;
            MoodMultiplierDB["DefeatedHostileFactionLeader"] = FormulaDB.Mood_Ambition;
            MoodMultiplierDB["DefeatedMechCluster"] = FormulaDB.Mood_Ambition;
            MoodMultiplierDB["DefeatedInsectHive"] = FormulaDB.Mood_Ambition;

            //Misc Situation

            //Special Thoughts
            MoodMultiplierDB["Naked"] = FormulaDB.PrudishNakedMultiplier;
            MoodMultiplierDB["DoingPassionateWork"] = FormulaDB.PassionWorkMultiplier;
        }
        private static readonly List<string> moodList_Mood_Died_Kin = new(
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
        private static readonly List<string> moodList_Mood_Died_Loved = new(
            ["MyHusbandDied",
            "MyWifeDied",
            "MyFianceDied",
            "MyFianceeDied",
            "MyLoverDied"]
        );
        private static readonly List<string> moodList_Mood_Lost_Kin = new(
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
            "MyKinLost"]
        );
        private static readonly List<string> moodList_Mood_Lost_Loved = new(
            ["MyHusbandLost",
            "MyWifeLost",
            "MyFianceLost",
            "MyFianceeLost",
            "MyLoverLost"]
        );
        private static readonly List<string> moodList_Mood_Expect = new(
            ["AteLavishMeal",
            "AteFineMeal",
            "AteRawFood",
            "AteKibble",
            "AteCorpse",
            "AteInsectMeatDirect",
            "AteInsectMeatAsIngredient",
            "AteRottenFood",
            "AteInImpressiveDiningRoom",
            "JoyActivityInImpressiveRecRoom",
            "SleptInBedroom",
            "SleptInBarracks",
            "PrisonCell",
            "PrisonBarracks",
            "HospitalPatientRoomStats",
            "NeedRoomSize"]
        );
        private static readonly List<string> moodList_Mood_Expect_Human = new(
            ["AteHumanlikeMeatDirect",
            "AteHumanlikeMeatDirectCannibal",
            "AteHumanlikeMeatAsIngredient",
            "AteHumanlikeMeatAsIngredientCannibal"]
        );
        private static readonly List<string> moodList_Mood_Expect_Need = new(
            ["SleepDisturbed",
            "SleptOutside",
            "SleptOnGround",
            "SleptInCold",
            "SleptInHeat",
            "MyOrganHarvested",
            "SoakingWet",
            "ApparelDamaged",
            "EnvironmentCold",
            "EnvironmentHot",
            "NeedFood",
            "NeedRest",
            "NeedComfort",
            "NeedOutdoors",
            "NeedIndoors"]
        );
        private static readonly List<string> moodList_Mood_Expect_Drug = new(
            ["DrugDesireFascination",
            "DrugDesireFascinationSatisfied",
            "DrugDesireInterest",
            "DrugDesireInterestSatisfied",]
        );
    }
}