using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class QualityUtil
    {
        public static QualityCategory GenerateQualityCreatedByPawnWithPsyche(int relevantSkillLevel, bool inspired, Pawn pawn, SkillDef relevantSkill, float numOffset = 0f)
        {
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return QualityUtility.GenerateQualityCreatedByPawn(relevantSkillLevel, inspired);
            }
            bool experimented = false;
            float highVarianceMultiplier;
            float lowVarianceMultiplier;
            float experimentChance;
            float successChance;
            float num = numOffset;
            var p = compPsyche.Personality;
            var pSpontaneityF = (p.GetPersonality(PersonalityDefOf.Rimpsyche_Spontaneity) + 1f) * 0.05f; //0~[0.05]~0.1
            highVarianceMultiplier = p.Evaluate(QualityVarianceMultiplierHigh) + Rand.Range(-pSpontaneityF, pSpontaneityF);
            lowVarianceMultiplier = p.Evaluate(QualityVarianceMultiplierLow) + Rand.Range(-pSpontaneityF, pSpontaneityF);
            if (relevantSkill == SkillDefOf.Artistic)
            {
                successChance = 0.1f + relevantSkillLevel * p.Evaluate(ArtExperimentSuccessChanceMultiplier);
                num += p.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination) * 0.3f;// -0.3~0.3
            }
            else
            {
                successChance = 0.1f + relevantSkillLevel * p.Evaluate(CraftExperimentSuccessChanceMultiplier);
            }
            experimentChance = p.Evaluate(ExperimentChanceMultiplier) * (successChance) + Rand.Range(-pSpontaneityF, pSpontaneityF);
            switch (relevantSkillLevel)
            {
                case 0:
                    num += 0.7f;
                    break;
                case 1:
                    num += 1.1f;
                    break;
                case 2:
                    num += 1.5f;
                    break;
                case 3:
                    num += 1.8f;
                    break;
                case 4:
                    num += 2f;
                    break;
                case 5:
                    num += 2.2f;
                    break;
                case 6:
                    num += 2.4f;
                    break;
                case 7:
                    num += 2.6f;
                    break;
                case 8:
                    num += 2.8f;
                    break;
                case 9:
                    num += 2.95f;
                    break;
                case 10:
                    num += 3.1f;
                    break;
                case 11:
                    num += 3.25f;
                    break;
                case 12:
                    num += 3.4f;
                    break;
                case 13:
                    num += 3.5f;
                    break;
                case 14:
                    num += 3.6f;
                    break;
                case 15:
                    num += 3.7f;
                    break;
                case 16:
                    num += 3.8f;
                    break;
                case 17:
                    num += 3.9f;
                    break;
                case 18:
                    num += 4f;
                    break;
                case 19:
                    num += 4.1f;
                    break;
                case 20:
                    num += 4.2f;
                    break;
            }
            int value = (int)Rand.GaussianAsymmetric(num, 0.6f * lowVarianceMultiplier, 0.8f * highVarianceMultiplier);
            if (value >= 6)
            {
                if (Rand.Value < experimentChance * 2f)
                {
                    //Brilliant Success
                    experimented = true;
                    if(Rand.Value < successChance)
                    {
                        if (!inspired)
                        {
                            Find.LetterStack.ReceiveLetter("LetterBrilliantSuccessLabel".Translate(), "LetterBrilliantSuccessMessage".Translate(pawn.Named("PAWN")).AdjustedFor(pawn).CapitalizeFirst(), LetterDefOf.PositiveEvent, pawn);
                        }
                        value = 6;
                    }
                    else
                    {
                        value = 5;
                    }
                }
                else
                {
                    value = 5;
                }
            }
            //Vanilla clamping
            if (value == 5 && Rand.Value < 0.5f)
            {
                value = (int)Rand.GaussianAsymmetric(num, 0.6f * lowVarianceMultiplier, 0.95f * highVarianceMultiplier);
                value = Mathf.Clamp(value, 0, 5);
            }

            //Inspiration or Experimentation
            if (inspired)
            {
                value += 2;
            }
            else
            {
                if (!experimented && value < 5 && Rand.Value < experimentChance)
                {
                    Log.Message("decided to experiment");
                    if (Rand.Value < successChance)
                    {
                        //Successful Experimentation
                        Messages.Message("MessageExperimentSuccess".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
                        value += 1;
                    }
                    else
                    {
                        if (value > 0)
                        {
                            //Failed Experimentation
                            Messages.Message("MessageExperimentFail".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
                            value -= 1;
                        }
                    }
                }
            }
            value = Mathf.Clamp(value, 0, 6);
            QualityCategory qualityCategory = (QualityCategory)value;


            //Ambition Check
            float expectation = ExpectedQMean(relevantSkillLevel);
            int higherExpectation = (int)(expectation + p.Evaluate(QualityExpectationHighOffset));
            //int lowerExpectatino = (int)(expectation + p.Evaluate(QualityExpectationLowOffset));
            //Log.Message($"value: {value}| level {relevantSkillLevel} expectation w/ ambition {p.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition)}:  {lowerExpectatino}~[{expectation}]~{higherExpectation}");
            if (value >= higherExpectation)
            {
                int qOffset = (value - higherExpectation) + 1;
                compPsyche.ProgressMade(3f * qOffset, 2);
                //Log.Message($"value higher than good expectation. Setting Progressday {3f * qOffset}. This should give {1.2f * (p.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) + 1f) * (3f * qOffset)} mood");
            }
            else if (value >= (int)(expectation + p.Evaluate(QualityExpectationLowOffset)))
            {
                compPsyche.ProgressMade(0f, 2);
                //Log.Message($"value good enough. Setting Progressday {0}");
            }

            return qualityCategory;
        }
        public static float ExpectedQMean(int relevantSkillLevel)
        {
            switch (relevantSkillLevel)
            {
                case 0:
                    return 0.41f;
                case 1:
                    return 0.70f;
                case 2:
                    return 1.09f;
                case 3:
                    return 1.38f;
                case 4:
                    return 1.56f;
                case 5:
                    return 1.78f;
                case 6:
                    return 1.99f;
                case 7:
                    return 2.19f;
                case 8:
                    return 2.38f;
                case 9:
                    return 2.51f;
                case 10:
                    return 2.66f;
                case 11:
                    return 2.82f;
                case 12:
                    return 2.96f;
                case 13:
                    return 3.06f;
                case 14:
                    return 3.15f;
                case 15:
                    return 3.24f;
                case 16:
                    return 3.32f;
                case 17:
                    return 3.40f;
                case 18:
                    return 3.48f;
                case 19:
                    return 3.58f;
                case 20:
                    return 3.67f;
                default:
                    return 0f;
            }
        }

        public static RimpsycheFormula QualityVarianceMultiplierHigh = new(
            "QualityVarianceMultiplierHigh",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Experimentation, 1.5f);
                return experimentation;
            }
        );

        public static RimpsycheFormula QualityVarianceMultiplierLow = new(
            "QualityVarianceMultiplierLow",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                return (-1.5f/(experimentation + 2f)) + 1.75f;
            }
        );

        public static RimpsycheFormula ArtExperimentSuccessChanceMultiplier = new(
            "ArtExperimentSuccessChanceMultiplier",
            (tracker) =>
            {
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float emotionality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
                return 0.0125f * (1f + (0.75f * imagination) + (0.25f * emotionality));
            }
        );

        public static RimpsycheFormula CraftExperimentSuccessChanceMultiplier = new(
            "CraftExperimentSuccessChanceMultiplier",
            (tracker) =>
            {
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float deliberation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                return 0.0125f * (1f + (0.25f * imagination) + (0.5f * deliberation));
            }
        );
        public static RimpsycheFormula ExperimentChanceMultiplier = new(
            "ExperimentChanceMultiplier",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                return 0.2f * (experimentation + 1f) * (experimentation + 1f);
            }
        );
        public static RimpsycheFormula QualityExpectationHighOffset = new(
            "QualityExpectationHighOffset",
            (tracker) =>
            {
                float offset = 0.5f + 0.8f*tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
                return offset;
            }
        );
        public static RimpsycheFormula QualityExpectationLowOffset = new(
            "QualityExpectationLowOffset",
            (tracker) =>
            {
                float offset = -0.2f + 0.3f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
                return offset;
            }
        );
    }
}
