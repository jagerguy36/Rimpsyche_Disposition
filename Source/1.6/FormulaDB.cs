using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Maux36.RimPsyche.Disposition
{
    public static class FormulaDB
    {
        public static RimpsycheFormula ModestShameGain = new(
            "ModestShameGain",
            (tracker) =>
            {
                float p = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness);
                if (p <= 0.35f) return 0f; //Pretty meaningless to calculate under 0.35f 
                return p*p*p*(p-0.25f)*0.4f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula ModestShameLose = new(
            "ModestShameLose",
            (tracker) =>
            {
                float p = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness);
                if (p <= 0) return 2;
                var p1 = 1 / (4f * p + 1f);
                return p1 * p1;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula PrudishNakedMultiplier = new(
            "PrudishNakedMultiplier",
            (tracker) =>
            {
                float mult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness) * 0.7f;
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula PassionWorkMultiplier = new(
            "PassionWorkMultiplier",
            (tracker) =>
            {
                float mult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion) * 0.5f;
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula PositiveMoodOffsetMultiplier = new(
            "PositiveMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula NegativeMoodOffsetMultiplier = new(
            "NegativeMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * 0.45f;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * 0.4f;
                return mult * optimismMult * emotionalityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula PreceptMoodOffsetMultiplier = new(
            "PreceptMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * 0.55f;
                return mult * moralityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
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
            },
            RimpsycheFormulaManager.FormulaIdDict
        );


        public static RimpsycheFormula CompassionMoodMultiplier = new(
            "CompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula LoyaltyCompassionMoodMultiplier = new(
            "LoyaltyCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = (1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f) + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty) * 0.5f;
                return mult;//0.0 ~ 2.0
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula SociabilityCompassionMoodMultiplier = new(
            "SociabilityCompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability) * 0.5f;
                return mult;//0.0 ~ 2.0
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula CompassionPositiveMoodMultiplier = new(
            "CompassionPositiveMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula LoyaltyMoodMultiplier = new(
            "LoyaltyMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula ExpectationMoodMultiplier = new(
            "ExpectationMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula SociabilityMoodMultiplier = new(
            "SociabilityMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula ImaginationMoodMultiplier = new(
            "ImaginationMoodMultiplier",
            (tracker) =>
            {
                float mult = 1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination) * 0.5f;
                return mult;//0.5 ~ 1.5
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
