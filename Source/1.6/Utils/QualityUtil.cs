using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class QualityUtil
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
            highVarianceMultiplier = compPsyche.Evaluate(QualityVarianceMultiplierHigh) + Rand.Range(-pSpontaneityF, pSpontaneityF);
            lowVarianceMultiplier = compPsyche.Evaluate(QualityVarianceMultiplierLow) + Rand.Range(-pSpontaneityF, pSpontaneityF);
            if (relevantSkill == SkillDefOf.Artistic)
            {
                successChance = 0.1f + relevantSkillLevel * compPsyche.Evaluate(ArtExperimentSuccessChanceMultiplier);
                num += p.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination) * 0.3f;// -0.3~0.3
            }
            else
            {
                successChance = 0.1f + relevantSkillLevel * compPsyche.Evaluate(CraftExperimentSuccessChanceMultiplier);
            }
            experimentChance = compPsyche.Evaluate(ExperimentChanceMultiplier) * (successChance) + Rand.Range(-pSpontaneityF, pSpontaneityF);
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
            //Log.Message($"cahce: {compPsyche.Evaluate(ExperimentChanceMultiplier)} | experimental chance: {experimentChance} | successChance: {successChance} | relevantSkillLevel: {relevantSkillLevel} | lowVarianceMultiplier: {lowVarianceMultiplier} | highVarianceMultiplier: {highVarianceMultiplier} | value: {value}");
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
                            Find.LetterStack.ReceiveLetter("RP_BrilliantSuccessLabel".Translate(), "RP_BrilliantSuccessMessage".Translate(pawn.Named("PAWN")).AdjustedFor(pawn).CapitalizeFirst(), LetterDefOf.PositiveEvent, pawn);
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
                    if (Rand.Value < successChance)
                    {
                        //Successful Experimentation
                        if (RimpsycheDispositionSettings.showExperimentMote)
                        {
                            MoteBubble mote = (MoteBubble)ThingMaker.MakeThing(DefOfDisposition.RimpsycheMote_ExperimentGood, null);
                            mote.Attach(pawn);
                            GenSpawn.Spawn(mote, pawn.Position, pawn.Map);
                        }
                        if (RimpsycheDispositionSettings.sendExperimentMessage)
                        {
                            Messages.Message("RP_MessageExperimentSuccess".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
                        }
                        value += 1;
                    }
                    else
                    {
                        if (value > 0)
                        {
                            //Failed Experimentation
                            if (RimpsycheDispositionSettings.showExperimentMote)
                            {
                                MoteBubble mote = (MoteBubble)ThingMaker.MakeThing(DefOfDisposition.RimpsycheMote_ExperimentBad, null);
                                mote.Attach(pawn);
                                GenSpawn.Spawn(mote, pawn.Position, pawn.Map);
                            }
                            if (RimpsycheDispositionSettings.sendExperimentMessage)
                            {
                                Messages.Message("RP_MessageExperimentFail".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
                            }
                            value -= 1;
                        }
                    }
                }
            }
            value = Mathf.Clamp(value, 0, 6);
            QualityCategory qualityCategory = (QualityCategory)value;
            

            //Ambition Check
            if (RimpsycheDispositionSettings.useSenseOfProgress)
            {
                float expectation = ExpectedQMean(relevantSkillLevel);
                int higherExpectation = (int)(expectation + compPsyche.Evaluate(QualityExpectationHighOffset));
                //int lowerExpectatino = (int)(expectation + p.Evaluate(QualityExpectationLowOffset));
                //Log.Message($"value: {value}| level {relevantSkillLevel} expectation w/ ambition {p.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition)}:  {lowerExpectatino}~[{expectation}]~{higherExpectation}");
                if (value >= higherExpectation)
                {
                    int qOffset = (value - higherExpectation) + 1;
                    compPsyche.ProgressMade(3f * qOffset, 2, "RP_Crafted".Translate(qualityCategory.GetLabel()));
                    //Log.Message($"value higher than good expectation. Setting Progressday {3f * qOffset}. This should give {1.2f * (p.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) + 1f) * (3f * qOffset)} mood");
                }
                else if (value >= (int)(expectation + compPsyche.Evaluate(QualityExpectationLowOffset)))
                {
                    compPsyche.ProgressMade(0f, 2, "RP_Crafted".Translate(qualityCategory.GetLabel()));
                    //Log.Message($"value good enough. Setting Progressday {0}");
                }
            }

            return qualityCategory;
        }
        public static float ExpectedQMean(int relevantSkillLevel)
        {
            return relevantSkillLevel switch
            {
                0 => 0.41f,
                1 => 0.70f,
                2 => 1.09f,
                3 => 1.38f,
                4 => 1.56f,
                5 => 1.78f,
                6 => 1.99f,
                7 => 2.19f,
                8 => 2.38f,
                9 => 2.51f,
                10 => 2.66f,
                11 => 2.82f,
                12 => 2.96f,
                13 => 3.06f,
                14 => 3.15f,
                15 => 3.24f,
                16 => 3.32f,
                17 => 3.40f,
                18 => 3.48f,
                19 => 3.58f,
                20 => 3.67f,
                _ => 4f,
            };
        }

        public static RimpsycheFormula QualityVarianceMultiplierHigh = new(
            "QualityVarianceMultiplierHigh",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Experimentation, 1.5f);
                return experimentation;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula QualityVarianceMultiplierLow = new(
            "QualityVarianceMultiplierLow",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                return (-1.5f/(experimentation + 2f)) + 1.75f;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula ArtExperimentSuccessChanceMultiplier = new(
            "ArtExperimentSuccessChanceMultiplier",
            (tracker) =>
            {
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float emotionality = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
                return 0.0125f * (1f + (0.5f * imagination) + (0.25f * emotionality));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula CraftExperimentSuccessChanceMultiplier = new(
            "CraftExperimentSuccessChanceMultiplier",
            (tracker) =>
            {
                float imagination = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
                float deliberation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                return 0.0125f * (1f + (0.25f * imagination) + (0.5f * deliberation));
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula ExperimentChanceMultiplier = new(
            "ExperimentChanceMultiplier",
            (tracker) =>
            {
                float experimentation = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
                return 0.2f * (experimentation + 1f) * (experimentation + 1f);
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula QualityExpectationHighOffset = new(
            "QualityExpectationHighOffset",
            (tracker) =>
            {
                float offset = 0.5f + 0.8f*tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
                return offset;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula QualityExpectationLowOffset = new(
            "QualityExpectationLowOffset",
            (tracker) =>
            {
                float offset = -0.2f + 0.3f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
                return offset;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
