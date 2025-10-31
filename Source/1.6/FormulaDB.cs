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
                float p = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
                if (p <= 0.35f) return 0f; //Pretty meaningless to calculate under 0.35f 
                return p*p*p*(p-0.25f)*0.4f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula ModestShameLose = new(
            "ModestShameLose",
            (tracker) =>
            {
                float p = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
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
                float tenacity = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
                tenacity = (1f - 0.2f * tenacity);
                if (bravery <= -0.4f)
                {
                    return 0.6f - 0.4f * (bravery * tenacity + 0.4f);
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
                float tenacity = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
                tenacity = (1f - 0.2f * tenacity);
                float mult = (-0.5f * bravery * tenacity) + 0.1f; //0.26~0.7
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
        private static readonly float individualC = RimpsycheDispositionSettings.moodIndividualC;
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


        //=================
        //Mod support

        public static RimpsycheFormula BraveryTerrorMultiplier = new(
            "BraveryTerrorMultiplier",
            (tracker) =>
            {
                float bravery = 1f - tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery) * 0.5f;
                return bravery;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        //=================

        //Tags

        //=================

        public static RimpsycheFormula Part_Worry = new(
            "Part_Worry",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
                float worryScore = compassion * (-optimism + 2f) * 0.5f;
                return worryScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Part_Demand = new(
            "Part_Demand",
            (tracker) =>
            {
                float expectation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
                float selfInterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float demandScore = expectation * (Mathf.Max(0f, selfInterest) + 2f) * 0.5f;
                return demandScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Part_SawDeath = new(
            "Part_SawDeath",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float bravery = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
                float sawDeathScore = compassion + Mathf.Max(0f, -bravery);
                return sawDeathScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Part_Kin = new(
            "Part_Kin",
            (tracker) =>
            {
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                float kinScore = Mathf.Max(0f, loyalty) * (-openness + 2f) * 0.5f;
                return kinScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Part_Lover = new(
            "Part_Lover",
            (tracker) =>
            {
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                float passion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
                float loverScore = Mathf.Max(0f, loyalty) * (passion + 3f) * 0.5f;
                return loverScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Part_Pref = new(
            "Part_Pref",
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
                return preferenceScore;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

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
                float kinScore = tracker.Evaluate(Part_Kin);
                return ThoughtUtil.MoodMultCurve(compassion + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Empathy_Loved = new(
            "Tag_Empathy_Loved",
            (tracker) =>
            {
                float compassion = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
                float loverScore = tracker.Evaluate(Part_Lover);
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
                float preferenceScore = tracker.Evaluate(Part_Pref);
                return ThoughtUtil.MoodMultCurve(-compassion + preferenceScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry = new(
            "Tag_Worry",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
                return ThoughtUtil.MoodMultCurve(worryScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Bond = new(
            "Tag_Worry_Bond",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
                float loyalty = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
                return ThoughtUtil.MoodMultCurve(worryScore + Mathf.Max(0f, loyalty));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Kin = new(
            "Tag_Worry_Kin",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
                float kinScore = tracker.Evaluate(Part_Kin);
                return ThoughtUtil.MoodMultCurve(worryScore + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Loved = new(
            "Tag_Worry_Loved",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
                float loverScore = tracker.Evaluate(Part_Lover);
                return ThoughtUtil.MoodMultCurve(worryScore + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Outsider = new(
            "Tag_Worry_Outsider",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
                float trust = tracker.GetPersonality( PersonalityDefOf.Rimpsyche_Trust);
                return ThoughtUtil.MoodMultCurve(worryScore + Mathf.Min(0f, trust));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_Outsider_M = new(
            "Tag_Worry_Outsider_M",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
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
                float worryScore = tracker.Evaluate(Part_Worry);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(worryScore + morality);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Worry_J = new(
            "Tag_Worry_J",
            (tracker) =>
            {
                float worryScore = tracker.Evaluate(Part_Worry);
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
                float kinScore = tracker.Evaluate(Part_Kin);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest) + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Harmed_Loved = new(
            "Tag_Harmed_Loved",
            (tracker) =>
            {
                float selfinterest = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
                float loverScore = tracker.Evaluate(Part_Lover);
                return ThoughtUtil.MoodMultCurve(Mathf.Max(0f, selfinterest) + loverScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Affluence = new(
            "Tag_Affluence",
            (tracker) =>
            {
                float demandScore = tracker.Evaluate(Part_Demand);
                return ThoughtUtil.MoodMultCurve(demandScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Needy = new(
            "Tag_Needy",
            (tracker) =>
            {
                float demandScore = tracker.Evaluate(Part_Demand);
                float tension = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
                return ThoughtUtil.MoodMultCurve(demandScore + tension);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Needy_Art = new(
            "Tag_Needy_Art",
            (tracker) =>
            {
                float demandScore = tracker.Evaluate(Part_Demand);
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                return ThoughtUtil.MoodMultCurve(demandScore + imagination);
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
                float sawDeathScore = tracker.Evaluate(Part_SawDeath);
                return ThoughtUtil.MoodMultCurve(sawDeathScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_SawDeath_Kin = new(
            "Tag_SawDeath_Kin",
            (tracker) =>
            {
                float sawDeathScore = tracker.Evaluate(Part_SawDeath);
                float kinScore = tracker.Evaluate(Part_Kin);
                return ThoughtUtil.MoodMultCurve(sawDeathScore + kinScore);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_SawDeath_Outsider = new(
            "Tag_SawDeath_Outsider",
            (tracker) =>
            {
                float sawDeathScore = tracker.Evaluate(Part_SawDeath);
                float trust = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                return ThoughtUtil.MoodMultCurve(sawDeathScore + Mathf.Min(0f, trust));
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
                float propriety = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
                return ThoughtUtil.MoodMultCurve(propriety);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Decency_J = new(
            "Tag_Decency_J",
            (tracker) =>
            {
                float propriety = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
                float openness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
                return ThoughtUtil.MoodMultCurve(propriety - openness);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula Tag_Decency_M = new(
            "Tag_Decency_M",
            (tracker) =>
            {
                float propriety = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
                float morality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
                return ThoughtUtil.MoodMultCurve(propriety + morality);
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
        public static RimpsycheFormula Tag_Recluse = new(
            "Tag_Recluse",
            (tracker) =>
            {
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                return ThoughtUtil.MoodMultCurve(-sociability);
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
        public static RimpsycheFormula Tag_Bloodlust = new(
            "Tag_Bloodlust",
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
                float loverScore = tracker.Evaluate(Part_Lover);
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
                float preferenceScore = tracker.Evaluate(Part_Pref);
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
