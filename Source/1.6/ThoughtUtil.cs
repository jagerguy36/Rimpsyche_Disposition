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

        public static bool TestResilientSpirit(MentalBreakDef mentalbreak, Pawn pawn)
        {
            if (!causedByMood)
            {
                return false;
            }
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled!=true)
            {
                return false;
            }
            if (compPsyche.lastResilientSpiritTick + 3600000 > Find.TickManager.TicksGame)
            {
                return false;
            }
            if (Rand.Chance(compPsyche.Personality.Evaluate(ResilientSpiritChance)))
            {
                compPsyche.lastResilientSpiritTick = Find.TickManager.TicksGame+180000; //3days
                //Add thought
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.PsycheResilientSpirit);
                //Send letter
                if (!PawnUtility.ShouldSendNotificationAbout(pawn))
                {
                    return true;
                }
                TaggedString label = "RP_ResilientSpiritLabel".Translate() + ": " + pawn.LabelShortCap;
                TaggedString taggedString = "RP_ResilientSpiritMessage".Translate(pawn.Label, pawn.Named("PAWN"), mentalbreak.Named("MENTALBREAK")).CapitalizeFirst();
                taggedString = taggedString.AdjustedFor(pawn);
                Find.LetterStack.ReceiveLetter(label, taggedString, LetterDefOf.PositiveEvent, pawn);
                return true;
            }
            return false;

        }
        public static RimpsycheFormula ResilientSpiritChance = new(
            "ResilientSpiritChance",
            (tracker) =>
            {
                float mult = (tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience));
                //Discipline
                return mult;
            }
        );

        public static void Initialize()
        {
            Log.Message("[Rimpsyche - Disposition] ThoughtUtil initialized.");
            //Compassion (Dead or lost)
            foreach (var defName in compassionMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.CompassionMoodMultiplier;
            }
            //Loyalty + Compassion (Bonded dead or lost)
            foreach (var defName in LoyaltyCompassionMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.LoyaltyCompassionMoodMultiplier;
            }
            //Sociability + Compassion (Good opinion dead or lost)
            foreach (var defName in SociabilityCompassionMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.SociabilityCompassionMoodMultiplier;
            }
            //Compassion Positive (schadenfeude)
            foreach (var defName in CompassionPositiveMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.CompassionPositiveMoodMultiplier;
            }
            //Loyalty (Bonded released)
            foreach (var defName in LoyaltyMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.LoyaltyMoodMultiplier;
            }
            //Expectation (Eating, Sleeping)
            foreach (var defName in ExpectationMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.ExpectationMoodMultiplier;
            }
            //Sociability (Gathering)
            foreach (var defName in SociabilityMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.SociabilityMoodMultiplier;
            }
            //Imagination (Concert)
            foreach (var defName in ImaginationMoodMultiplierList)
            {
                MoodMultiplierDB[defName] = FormulaDB.ImaginationMoodMultiplier;
            }
        }

        public static void ModCompat()
        {
            if (ModsConfig.IdeologyActive)
            {
                //Log.Message("[Rimpsyche - Disposition] Ideology thoughts added.");
                //SelfInterest
                //Charity_Essential Charity_Important Charity_Worthwhile
                
                //Compassion
                //Precepts_AnimalSlaughter

                //Openmindedness
                //Precepts_Apostasy.
            }
        }

        public static Dictionary<string, RimpsycheFormula> MoodMultiplierDB = new()
        {
            //Special Thoughts
            { "Naked", FormulaDB.PrudishNakedMultiplier},
            { "DoingPassionateWork", FormulaDB.PassionWorkMultiplier},
        };

        private static readonly List<string> compassionMoodMultiplierList = new(
            [
                // Thoughts_Memory_Death
                "KnowGuestExecuted",
                "KnowColonistExecuted",
                "KnowPrisonerDiedInnocent",
                "KnowColonistDied",
                "MySonDied",
                "MyDaughterDied",
                "MyHusbandDied",
                "MyWifeDied",
                "MyFianceDied",
                "MyFianceeDied",
                "MyLoverDied",
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
                "MyKinDied",            
                // Thoughts_Memory_Lost
                "ColonistLost",
                "MySonLost",
                "MyDaughterLost",
                "MyHusbandLost",
                "MyWifeLost",
                "MyFianceLost",
                "MyFianceeLost",
                "MyLoverLost",
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
                // Thoughts_Memory_Misc
                "KnowGuestOrganHarvested",
                "KnowColonistOrganHarvested"
            ]
        );
        private static readonly List<string> LoyaltyCompassionMoodMultiplierList = new(
            [
                // Thoughts_Memory_Death
                "BondedAnimalDied",
                "BondedAnimalLost"
            ]
        );
        private static readonly List<string> SociabilityCompassionMoodMultiplierList = new(
            [
                // Thoughts_Memory_Death
                "PawnWithGoodOpinionDied",
                // Thoughts_Memory_Lost
                "PawnWithGoodOpinionLost"
            ]
        );
        private static readonly List<string> CompassionPositiveMoodMultiplierList = new(
            [
                // Thoughts_Memory_Death
                "PawnWithBadOpinionDied",
                // Thoughts_Memory_Lost
                "PawnWithBadOpinionLost",
                // Thoughts_Memory_Misc
                "HarvestedOrgan_Bloodlust"
            ]
        );
        private static readonly List<string> LoyaltyMoodMultiplierList = new(
            [
                // Thoughts_Memory_Lost
                "BondedAnimalReleased"
            ]
        );
        private static readonly List<string> ExpectationMoodMultiplierList = new(
            [
                // Thoughts_Memory_Eating
                "AteLavishMeal",
                "AteFineMeal",
                "AteRawFood",
                "AteKibble",
                "AteCorpse",
                "AteHumanlikeMeatDirect",
                "AteHumanlikeMeatAsIngredient",
                "AteInsectMeatDirect",
                "AteInsectMeatAsIngredient",
                "AteRottenFood",
                // Thoughts_Memory_Misc
                "AteWithoutTable",
                "SleptOutside",
                "SleptOnGround",
                "SleptInCold",
                "SleptInHeat"
            ]
        );
        private static readonly List<string> SociabilityMoodMultiplierList = new(
            [
                // Thoughts_Memory_Gatherings
                "AttendedWedding",
                "AttendedParty"
            ]
        );
        private static readonly List<string> ImaginationMoodMultiplierList = new(
            [
                // Thoughts_Memory_Gatherings
                "AttendedConcert"
            ]
        );
    }
}