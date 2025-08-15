using Verse;
using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public class ThoughtUtil
    {
        static ThoughtUtil()
        {
            Initialize();
            ModCompat();
        }

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] ThoughtUtil initialized.");
        }

        public static void ModCompat()
        {
            if (ModsConfig.IdeologyActive)
            {
                Log.Message("[Rimpsyche - Disposition] Ideology thoughts added.");
                //SelfInterest
                //Charity_Essential Charity_Important Charity_Worthwhile
                
                //Compassion
                //Precepts_AnimalSlaughter

                //Openmindedness
                //Precepts_Apostasy.
            }
        }

        public static RimpsycheFormula CompassionMoodMultiplier = new(
            "CompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula LoyaltyCompassionMoodMultiplier = new(
            "LoyaltyCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = (1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f) + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty)*0.5f;
                return mult;//0.0 ~ 2.0
            }
        );

        public static RimpsycheFormula SociabilityCompassionMoodMultiplier = new(
            "SociabilityCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability)*0.5f;
                return mult;//0.0 ~ 2.0
            }
        );

        public static RimpsycheFormula CompassionPositiveMoodMultiplier = new(
            "CompassionPositiveMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula LoyaltyMoodMultiplier = new(
            "LoyaltyMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty)*0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula ExpectationMoodMultiplier = new(
            "ExpectationMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation) * 0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula SociabilityMoodMultiplier = new(
            "SociabilityMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability) * 0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula ImaginationMoodMultiplier = new(
            "ImaginationMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination) * 0.5f;
                return mult;//0.5 ~ 1.5
            }
        );


        public static Dictionary<string, RimpsycheFormula> MoodMultiplierDB = new()
        {
            //Special Thoughts
            { "Naked", FormulaDB.PrudishNakedMultiplier},
            { "DoingPassionateWork", FormulaDB.PassionWorkMultiplier},


            // CompassionMoodMultiplier
            // Thoughts_Memory_Death
            { "KnowGuestExecuted", CompassionMoodMultiplier},
            { "KnowColonistExecuted", CompassionMoodMultiplier},
            { "KnowPrisonerDiedInnocent", CompassionMoodMultiplier},
            { "KnowColonistDied", CompassionMoodMultiplier},
            { "MySonDied", CompassionMoodMultiplier},
            { "MyDaughterDied", CompassionMoodMultiplier},
            { "MyHusbandDied", CompassionMoodMultiplier},
            { "MyWifeDied", CompassionMoodMultiplier},
            { "MyFianceDied", CompassionMoodMultiplier},
            { "MyFianceeDied", CompassionMoodMultiplier},
            { "MyLoverDied", CompassionMoodMultiplier},
            { "MyBrotherDied", CompassionMoodMultiplier},
            { "MySisterDied", CompassionMoodMultiplier},
            { "MyGrandchildDied", CompassionMoodMultiplier},
            { "MyFatherDied", CompassionMoodMultiplier},
            { "MyMotherDied", CompassionMoodMultiplier},
            { "MyNieceDied", CompassionMoodMultiplier},
            { "MyNephewDied", CompassionMoodMultiplier},
            { "MyHalfSiblingDied", CompassionMoodMultiplier},
            { "MyAuntDied", CompassionMoodMultiplier},
            { "MyUncleDied", CompassionMoodMultiplier},
            { "MyGrandparentDied", CompassionMoodMultiplier},
            { "MyCousinDied", CompassionMoodMultiplier},
            { "MyKinDied", CompassionMoodMultiplier},
            // Thoughts_Memory_Lost
            { "ColonistLost", CompassionMoodMultiplier},
            { "MySonLost", CompassionMoodMultiplier},
            { "MyDaughterLost", CompassionMoodMultiplier},
            { "MyHusbandLost", CompassionMoodMultiplier},
            { "MyWifeLost", CompassionMoodMultiplier},
            { "MyFianceLost", CompassionMoodMultiplier},
            { "MyFianceeLost", CompassionMoodMultiplier},
            { "MyLoverLost", CompassionMoodMultiplier},
            { "MyBrotherLost", CompassionMoodMultiplier},
            { "MySisterLost", CompassionMoodMultiplier},
            { "MyGrandchildLost", CompassionMoodMultiplier},
            { "MyFatherLost", CompassionMoodMultiplier},
            { "MyMotherLost", CompassionMoodMultiplier},
            { "MyNieceLost", CompassionMoodMultiplier},
            { "MyNephewLost", CompassionMoodMultiplier},
            { "MyHalfSiblingLost", CompassionMoodMultiplier},
            { "MyAuntLost", CompassionMoodMultiplier},
            { "MyUncleLost", CompassionMoodMultiplier},
            { "MyGrandparentLost", CompassionMoodMultiplier},
            { "MyCousinLost", CompassionMoodMultiplier},
            { "MyKinLost", CompassionMoodMultiplier},
            // Thoughts_Memory_Misc
            { "KnowGuestOrganHarvested", CompassionMoodMultiplier},
            { "KnowColonistOrganHarvested", CompassionMoodMultiplier},

            
            // LoyaltyCompassionMoodMultiplier
            // Thoughts_Memory_Death
            { "BondedAnimalDied", LoyaltyCompassionMoodMultiplier},
            // Thoughts_Memory_Lost
            { "BondedAnimalLost", LoyaltyCompassionMoodMultiplier},
            
            // SociabilityCompassionMoodMultiplier
            // Thoughts_Memory_Death
            { "PawnWithGoodOpinionDied", SociabilityCompassionMoodMultiplier},
            // Thoughts_Memory_Lost
            { "PawnWithGoodOpinionLost", SociabilityCompassionMoodMultiplier},
            
            // CompassionPositiveMoodMultiplier
            // Thoughts_Memory_Death
            { "PawnWithBadOpinionDied", CompassionPositiveMoodMultiplier},
            // Thoughts_Memory_Lost
            { "PawnWithBadOpinionLost", CompassionPositiveMoodMultiplier},
            // Thoughts_Memory_Misc
            { "HarvestedOrgan_Bloodlust", CompassionPositiveMoodMultiplier},

            // LoyaltyMoodMultiplier
            // Thoughts_Memory_Lost
            { "BondedAnimalReleased", LoyaltyMoodMultiplier},

            // ExpectationMoodMultiplier
            // Thoughts_Memory_Eating
            { "AteLavishMeal", ExpectationMoodMultiplier},
            { "AteFineMeal", ExpectationMoodMultiplier},
            { "AteRawFood", ExpectationMoodMultiplier},
            { "AteKibble", ExpectationMoodMultiplier},
            { "AteCorpse", ExpectationMoodMultiplier},
            { "AteHumanlikeMeatDirect", ExpectationMoodMultiplier},
            { "AteHumanlikeMeatAsIngredient", ExpectationMoodMultiplier},
            { "AteInsectMeatDirect", ExpectationMoodMultiplier},
            { "AteInsectMeatAsIngredient", ExpectationMoodMultiplier},
            { "AteRottenFood", ExpectationMoodMultiplier},
            // Thoughts_Memory_Misc
            { "AteWithoutTable", ExpectationMoodMultiplier},
            { "SleptOutside", ExpectationMoodMultiplier},
            { "SleptOnGround", ExpectationMoodMultiplier},
            { "SleptInCold", ExpectationMoodMultiplier},
            { "SleptInHeat", ExpectationMoodMultiplier},
            
            // SociabilityMoodMultiplier
            // Thoughts_Memory_Gatherings
            { "AttendedWedding", SociabilityMoodMultiplier},
            { "AttendedParty", SociabilityMoodMultiplier},

            // ImaginationMoodMultiplier
            // Thoughts_Memory_Gatherings
            { "AttendedConcert", ImaginationMoodMultiplier},
        };

    }
}