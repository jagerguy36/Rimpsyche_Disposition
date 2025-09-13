using UnityEngine;

namespace Maux36.RimPsyche.Disposition
{
    public static class FormulaDB
    {
        //Shame mechanics
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

        //Fight or Flight mechanics
        public static RimpsycheFormula FlightThreshold = new( // 0.92 ~ 0.568
            "FlightThreshold",
            (tracker) =>
            {
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                float resilience = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience);
                resilience = (1f - 0.2f * resilience);
                if (bravery <= -0.4f)
                {
                    return 0.6f - 0.4f * (bravery * resilience + 0.4f);
                }
                return -1f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula FlightChance = new(
            "FlightChance",
            (tracker) =>
            {
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                if (bravery > 0.4f)
                {
                    return 0f;
                }
                float aggresiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                aggresiveness = (1f - 0.5f * aggresiveness);
                float resilience = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience);
                resilience = (1f - 0.2f * resilience);
                float mult = (-0.5f * bravery * resilience) + 0.1f; //0.26~0.7
                return mult * aggresiveness;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //Adrenaline mechanics
        public static RimpsycheFormula AdrenalineGain = new(
            "AdrenalineGain",
            (tracker) =>
            {
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                float aggresiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                float tension = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tension, 1.5f);
                float stability = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Stability, 1.2f);
                if (bravery > -0.4f && aggresiveness > 0f && bravery+aggresiveness > 0f)
                {
                    return 5f * aggresiveness * tension * stability;
                }
                return 0f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //General mood
        private static readonly float optimismC = RimpsycheDispositionSettings.moodOptimismC;
        public static readonly float emotionalityC = RimpsycheDispositionSettings.moodEmotionalityC;
        private static readonly float preceptC = RimpsycheDispositionSettings.moodPreceptC;
        public static RimpsycheFormula PositiveMoodOffsetMultiplier = new(
            "PositiveMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * optimismC;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * emotionalityC;
                return mult * optimismMult * emotionalityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula NegativeMoodOffsetMultiplier = new(
            "NegativeMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float optimismMult = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism) * optimismC;
                float emotionalityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality) * emotionalityC;
                return mult * optimismMult * emotionalityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //Precept mood
        public static RimpsycheFormula PreceptMoodOffsetMultiplier = new(
            "PreceptMoodOffsetMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * preceptC;
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
                float moralityMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality) * preceptC;
                float opennessMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness) * 0.1f;
                return mult * moralityMult * opennessMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );



        //=================

        //Specific Mood

        //=================


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

        public static RimpsycheFormula Mood_Bond = new(
            "Mood_Bond",
            (tracker) =>
            {
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(loyalty);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );


        //Died
        public static RimpsycheFormula Mood_Died = new(
            "Mood_Died",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(compassion);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Innocent = new(
            "Mood_Died_Innocent",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float morality = Mathf.Max(0f,tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality));
                return ThoughtUtil.MoodMultCurve(compassion + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Bond = new(
            "Mood_Died_Bond",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Loved = new(
            "Mood_Died_Loved",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(compassion + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Kin = new(
            "Mood_Died_Kin",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(compassion + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Social = new(
            "Mood_Died_Social",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, sociability));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Died_Glad_Social = new(
            "Mood_Died_Glad_Social",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                return ThoughtUtil.MoodMultCurve(-compassion + Mathf.Max(0f, sociability));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //Lost
        public static RimpsycheFormula Mood_Lost = new(
            "Mood_Lost",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Bond = new(
            "Mood_Lost_Bond",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Loved = new(
            "Mood_Lost_Loved",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Kin = new(
            "Mood_Lost_Kin",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Social = new(
            "Mood_Lost_Social",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore + Mathf.Max(0f, sociability));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Glad_Social = new(
            "Mood_Lost_Glad_Social",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                return ThoughtUtil.MoodMultCurve(-lostScore + Mathf.Max(0f, sociability));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Lost_Trust = new(
            "Mood_Lost_Trust",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                float lostScore = compassion * (2f - optimism) * 0.5f;
                return ThoughtUtil.MoodMultCurve(lostScore + Mathf.Min(0f, trust));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //Expect
        public static RimpsycheFormula Mood_Expect = new(
            "Mood_Expect",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                return ThoughtUtil.MoodMultCurve(expectation);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Human = new(
            "Mood_Expect_Human",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(expectation + Mathf.Max(0f, compassion));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Glad_Human = new(
            "Mood_Expect_Glad_Human",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(- Mathf.Max(0f, compassion));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Organize = new(
            "Mood_Expect_Organize",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float organization = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
                return ThoughtUtil.MoodMultCurve(expectation  + organization);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Need = new(
            "Mood_Expect_Need",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float tension = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
                return ThoughtUtil.MoodMultCurve(expectation + tension);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Need_Fear = new(
            "Mood_Expect_Need_Fear",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float tension = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                return ThoughtUtil.MoodMultCurve(expectation + tension - Mathf.Min(0f, bravery));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Joy = new(
            "Mood_Expect_Joy",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float playfulness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Playfulness);
                return ThoughtUtil.MoodMultCurve(expectation + playfulness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Expect_Art = new(
            "Mood_Expect_Art",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                return ThoughtUtil.MoodMultCurve(expectation + imagination);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Mood_Drug = new(
            "Mood_Drug",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float discipline = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                return ThoughtUtil.MoodMultCurve(expectation - Mathf.Max(0f, discipline));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
