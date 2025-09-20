using System;
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

        //Tags

        //=================

        public static RimpsycheFormula Tag_Empathy = new(
            "Tag_Empathy",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(compassion);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Empathy_Bond = new(
            "Tag_Empathy_Bond",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Empathy_Kin = new(
            "Tag_Empathy_Kin",
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
        public static RimpsycheFormula Tag_Empathy_Loved = new(
            "Tag_Empathy_Loved",
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
        public static RimpsycheFormula Tag_Empathy_J = new(
            "Tag_Empathy_J",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(compassion - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Empathy_M = new(
            "Tag_Empathy_M",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(compassion + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Sympathy = new(
            "Tag_Sympathy",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(-compassion);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Sympathy_J = new(
            "Tag_Sympathy_J",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(-compassion - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Sympathy_M = new(
            "Tag_Sympathy_M",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(-compassion + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Sympathy_P = new(
            "Tag_Sympathy_P",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float preferenceScore;
                if (expectation > 0)
                {
                    preferenceScore = expectation * (-openness + 1) * 0.5f;
                }
                else
                {
                    preferenceScore = expectation * (+openness + 1) * 0.5f;
                }
                return ThoughtUtil.MoodMultCurve(-compassion + preferenceScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry = new(
            "Tag_Worry",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(worryScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Bond = new(
            "Tag_Worry_Bond",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(worryScore + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Kin = new(
            "Tag_Worry_Kin",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(worryScore + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Loved = new(
            "Tag_Worry_Loved",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(worryScore + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Outsider = new(
            "Tag_Worry_Outsider",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float trust = tracker.GetPersonality( PersonalityDefOf.Rimpsyche_Trust);
                return ThoughtUtil.MoodMultCurve(worryScore + Mathf.Min(0f, trust));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Outsider_M = new(
            "Tag_Worry_Outsider_M",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(worryScore + Mathf.Min(0f, trust) + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_M = new(
            "Tag_Worry_M",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(worryScore + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_J = new(
            "Tag_Worry_J",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(worryScore - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Disquiet = new(
            "Tag_Disquiet",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float disquietScore = -compassion * (-optimism + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(disquietScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Harmed = new(
            "Tag_Harmed",
            (tracker) =>
            {
                float selfinterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Harmed_Bond = new(
            "Tag_Harmed_Bond",
            (tracker) =>
            {
                float selfinterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest) + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Harmed_Kin = new(
            "Tag_Harmed_Kin",
            (tracker) =>
            {
                float selfinterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest) + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Harmed_Loved = new(
            "Tag_Harmed_Loved",
            (tracker) =>
            {
                float selfinterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest) + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Affluence = new(
            "Tag_Affluence",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float selfInterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float demandScore = expectation * (Mathf.Max(0f, selfInterest) + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(demandScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Needy = new(
            "Tag_Needy",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float selfInterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float demandScore = expectation * (Mathf.Max(0f, selfInterest) + 2f) * 0.5f;
                float tension = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
                return ThoughtUtil.MoodMultCurve(demandScore + tension);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Fear = new(
            "Tag_Fear",
            (tracker) =>
            {
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, -bravery));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_SawDeath = new(
            "Tag_SawDeath",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, -bravery));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_SawDeath_Kin = new(
            "Tag_SawDeath_Kin",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, -bravery) + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_SawDeath_Outsider = new(
            "Tag_SawDeath_Outsider",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                return ThoughtUtil.MoodMultCurve(compassion + Mathf.Max(0f, -bravery) + Mathf.Min(0f, trust));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Charity_J = new(
            "Tag_Charity_J",
            (tracker) =>
            {
                float selfInterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(-selfInterest - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Charity_M = new(
            "Tag_Charity_M",
            (tracker) =>
            {
                float selfInterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(-selfInterest + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Decency = new(
            "Tag_Decency",
            (tracker) =>
            {
                float appropriateness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness);
                return ThoughtUtil.MoodMultCurve(appropriateness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Decency_J = new(
            "Tag_Decency_J",
            (tracker) =>
            {
                float appropriateness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(appropriateness - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Decency_M = new(
            "Tag_Decency_M",
            (tracker) =>
            {
                float appropriateness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Appropriateness);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(appropriateness + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Art = new(
            "Tag_Art",
            (tracker) =>
            {
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                return ThoughtUtil.MoodMultCurve(imagination);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Gathering = new(
            "Tag_Gathering",
            (tracker) =>
            {
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                return ThoughtUtil.MoodMultCurve(sociability);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Concert = new(
            "Tag_Concert",
            (tracker) =>
            {
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                return ThoughtUtil.MoodMultCurve(sociability + imagination);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Bloodlost = new(
            "Tag_Bloodlost",
            (tracker) =>
            {
                float aggressiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                return ThoughtUtil.MoodMultCurve(aggressiveness - Mathf.Max(0f, compassion));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Outsider = new(
            "Tag_Outsider",
            (tracker) =>
            {
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                return ThoughtUtil.MoodMultCurve(Mathf.Min(0f, trust));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Loved = new(
            "Tag_Loved",
            (tracker) =>
            {
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return ThoughtUtil.MoodMultCurve(loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Bond = new(
            "Tag_Bond",
            (tracker) =>
            {
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Morality = new(
            "Tag_Morality",
            (tracker) =>
            {
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Judgemental = new(
            "Tag_Judgemental",
            (tracker) =>
            {
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(-openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Preference = new(
            "Tag_Preference",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float preferenceScore;
                if (expectation > 0)
                {
                    preferenceScore = expectation * (-openness + 1) * 0.5f;
                }
                else
                {
                    preferenceScore = expectation * (+openness + 1) * 0.5f;
                }
                return ThoughtUtil.MoodMultCurve(preferenceScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Openmindedness = new(
            "Tag_Openmindedness",
            (tracker) =>
            {
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //=================

        //Stage Tags

        //=================

        public static RimpsycheFormula Tag_JustifiedGuilt = new(
            "Tag_JustifiedGuilt",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(compassion - Mathf.Max(0f, morality));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
