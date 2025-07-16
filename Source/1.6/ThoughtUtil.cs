using System.Collections.Generic;

namespace Maux36.RimPsyche.Disposition
{
    public static class ThoughtUtil
    {
        public static RimpsycheFormula CompassionMoodMultiplier = new(
            "CompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float compassionMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f;
                return mult * compassionMult;
            }
        );

        public static RimpsycheFormula LoyaltyCompassionMoodMultiplier = new(
            "LoyaltyCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float loyaltyCompassionMult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty)*0.5f;
                return mult * loyaltyCompassionMult;
            }
        );

        public static RimpsycheFormula SociabilityCompassionMoodMultiplier = new(
            "SociabilityCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float loyaltyCompassionMult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability)*0.5f;
                return mult * loyaltyCompassionMult;
            }
        );

        public static RimpsycheFormula CompassionPositiveMoodMultiplier = new(
            "CompassionPositiveMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float loyaltyCompassionMult = 1 - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5f;
                return mult * loyaltyCompassionMult;
            }
        );

        public static RimpsycheFormula LoyaltyMoodMultiplier = new(
            "LoyaltyMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float loyaltyCompassionMult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty)*0.5f;
                return mult * loyaltyCompassionMult;
            }
        );


        public static Dictionary<string, RimpsycheFormula> MoodMultiplierDB = new()
        {
            // Thoughts_Memory_Death
            { "KnowGuestExecuted", CompassionMoodMultiplier},
            { "KnowColonistExecuted", CompassionMoodMultiplier},
            { "KnowPrisonerDiedInnocent", CompassionMoodMultiplier},
            { "KnowColonistDied", CompassionMoodMultiplier},

            { "BondedAnimalDied", LoyaltyCompassionMoodMultiplier},

            { "PawnWithGoodOpinionDied", SociabilityCompassionMoodMultiplier},
            { "PawnWithBadOpinionDied", CompassionPositiveMoodMultiplier},

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

            { "BondedAnimalReleased", LoyaltyMoodMultiplier},
            { "BondedAnimalLost", LoyaltyCompassionMoodMultiplier},

            { "PawnWithGoodOpinionLost", SociabilityCompassionMoodMultiplier},
            { "PawnWithBadOpinionLost", CompassionPositiveMoodMultiplier},

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
        };

    }
}