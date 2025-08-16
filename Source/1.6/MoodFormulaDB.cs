using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maux36.RimPsyche.Disposition
{
    public static class MoodFormulaDB
    {
        public static RimpsycheFormula PrudishNakedMultiplier = new(
            "PrudishNakedMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Prudishness) * 0.7f;
                return optimism;
            }
        );

        public static RimpsycheFormula PassionWorkMultiplier = new(
            "PassionWorkMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion) * 0.5f;
                return optimism;
            }
        );
        
        public static RimpsycheFormula CompassionMoodMultiplier = new(
            "CompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula LoyaltyCompassionMoodMultiplier = new(
            "LoyaltyCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = (1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f) + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty) * 0.5f;
                return mult;//0.0 ~ 2.0
            }
        );

        public static RimpsycheFormula SociabilityCompassionMoodMultiplier = new(
            "SociabilityCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability) * 0.5f;
                return mult;//0.0 ~ 2.0
            }
        );

        public static RimpsycheFormula CompassionPositiveMoodMultiplier = new(
            "CompassionPositiveMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f;
                return mult;//0.5 ~ 1.5
            }
        );

        public static RimpsycheFormula LoyaltyMoodMultiplier = new(
            "LoyaltyMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty) * 0.5f;
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
    }
}
