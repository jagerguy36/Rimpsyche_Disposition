using System.Text;
using UnityEngine;

namespace Maux36.RimPsyche.Disposition
{
    //Sense of Achievement
    public class AmbitiousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Ambition);
        }
    }

    //ResilientSpirit, MentalBreakIntervalMultiplier 
    public class TenaciousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            return tenacity;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity);
        }
    }
    public class ResilientDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            var discipline = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
            return (10f * tenacity + discipline) / 11f;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Discipline);
        }
    }
    //General Mood
    public class OptimisticDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var optimism = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
            return optimism;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Optimism);
        }
    }
    //Mood Strength
    public class EmotionalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Emotionality);
        }
    }
    //Mood Swing: MoodRisingSpeedMultiplier, MoodFallingSpeedMultiplier
    public class MoodswingDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Stability);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Stability);
        }
    }
    //Hide in shame
    public class ModestDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.ModestShameGain) * 10f / 3f;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche,  PersonalityDefOf.Rimpsyche_Propriety);
        }
    }
    public class FierceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.AdrenalineGain) / 9f;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            //if (bravery <= 0.4f || bravery + aggresiveness <= 0f)
            //    sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, direction)}");
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tension);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Stability);
        }
    }
    public class CowardlyDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.Evaluate(FormulaDB.FlightThreshold) <= 0f) return 0f;
            return compPsyche.Evaluate(FormulaDB.FlightChance);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            if (compPsyche.Evaluate(FormulaDB.FlightThreshold) > 0f)
            {
                Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness, PsycheDescDirection.Negative);
                Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, PsycheDescDirection.Negative);
                Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Bravery, PsycheDescDirection.Negative);
            }
        }
    }
    public class ExperimentalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Experimentation);
        }
    }

    public class DeliberationDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Deliberation);
        }
    }
    public class IndustriousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Diligence);
        }
    }
    public class TenseDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tension);
        }
    }
    public class NegotiationAbilityDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Evaluate(PsycheNegotiationAbilityStatPart.NegotiationAbilityFactor); //0.76~1.26
            score -= score;
            if (score > 0f)
                score /= 0.26f;
            else
                score /= 0.24f;
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tact);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Confidence);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Neutral);
        }
    }
    public class TradePriceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Evaluate(PsycheTradePriceImprovementStatPart.TradePriceImprovementOffset);
            score = 2.5f * score - 2.5f;
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tact);
            if (compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tact) > 0f)
                Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Positive);
            else
                Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Negative);
        }
    }
    public class OrganizedDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Organization);
        }
    }
    public class BraceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var offset = compPsyche.Evaluate(PsychePainShockThresholdStatPart.PainShockThresholdOffset); //0.35f base
            return offset / 0.35f;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Bravery);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity);
        }
    }
    public class ReflectionDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Reflectiveness);
        }
    }
    public class ArtQualityDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Imagination);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Imagination);
        }
    }
    public class AddictionChanceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Discipline);
        }
    }
    public class FocusedLearningDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Focus);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Focus);
        }
    }
    public class ResistanceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.parentPawn.IsColonistPlayerControlled)
                return 0f;
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Loyalty);
        }
    }

    public class ThoughtSympathyDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Compassion);
        }
    }
    public class ThoughtHarmedDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var selfInterest = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
            return Mathf.Max(selfInterest, 0f);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest);
        }
    }
    public class ThoughtDecencyDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Propriety);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Propriety);
        }
    }
    public class ThoughtNeedDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var expectation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
            var selfInterest = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
            selfInterest = (Mathf.Max(selfInterest, 0f) + 2f) * 0.5f;
            return expectation * selfInterest;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Expectation);
            var expectation = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Expectation);
            var selfInterest = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
            if (selfInterest > 0f)
            {
                if (expectation > 0f)
                    Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest);
                else
                    Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Negative);
            }
        }
    }
    public class ThoughtSocialDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
            return score;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Sociability);
        }
    }
    public class ThoughtBondsDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
           var loyalty = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            if (loyalty <= 0f)
                return 0f;
            return loyalty;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Loyalty);
            //Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Openness, PsycheDescDirection.Neutral);
            //Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Passion, PsycheDescDirection.Neutral);
        }
    }
    public class ThoughtOutsidersDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var distrust = -compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
            if (distrust <= 0f)
                return 0f;
            return distrust;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Trust, PsycheDescDirection.Negative);
        }
    }
    public class ThoughtMoralityDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var morality = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Morality);
            return morality;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Morality);
        }
    }
    public class ThoughtJudgementalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var judgemental = -compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
            return judgemental;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Openness, PsycheDescDirection.Negative);
        }
    }
}
