using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maux36.RimPsyche.Disposition
{
    public static class FormulaDB
    {
        public static RimpsycheFormula PrudishNakedMultiplier = new(
            "PrudishNakedMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness) * 0.7f;
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

        public static RimpsycheFormula PositiveMoodOffsetMultiplier = new(
            "PositiveMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            }
        );

        public static RimpsycheFormula NegativeMoodOffsetMultiplier = new(
            "NegativeMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            }
        );

        public static RimpsycheFormula PreceptMoodOffsetMultiplier = new(
            "PreceptMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * 0.55f;
                return mult * moralityMult;
            }
        );

        public static RimpsycheFormula PreceptOpinionOffsetMultiplier = new(
            "PreceptOpinionOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                //Openness concerns what could be right/wrong. Morality adhering to the right/wrong.
                //High open + High moral pawns will be open to question their right and wrong, but will still feel strongly about adhering to what is considered right.
                //So openness's effect here is very little, just enough to reflect their 'doubt' about the moral standard.
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * 0.45f;
                float opennessMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness) * 0.1f;
                return mult * moralityMult * opennessMult;
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
