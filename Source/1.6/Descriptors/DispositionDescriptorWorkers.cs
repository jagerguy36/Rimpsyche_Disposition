using System.Diagnostics;
using System.Text;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    //Sense of Achievement
    public class AmbitiousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Ambition);
        }
    }

    //ResilientSpirit, MentalBreakIntervalMultiplier 
    public class ResilientDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            var score = tenacity;
            if (score > 0f)
            {
                return score;
            }
            else
            {
                var discipline = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                return (score + 0.1f * discipline) / 1.1f;
            }
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Tenacity);
            Blame(PersonalityDefOf.Rimpsyche_Discipline, PsycheDescDirection.Positive, compPsyche => bScore <= 0f);
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
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Optimism);
        }
    }
    //Mood Strength
    public class EmotionalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Emotionality);
        }
    }
    //Mood Swing: MoodRisingSpeedMultiplier, MoodFallingSpeedMultiplier
    public class MoodswingDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Stability);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Stability);
        }
    }
    //Hide in shame
    public class ModestDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.ModestShameGain) * 10f / 3f;
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Propriety);
        }
    }
    public class FierceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.AdrenalineGain) / 9f;
        }
        protected override void SetupBlamers()
        {
            //if (bravery <= 0.4f || bravery + aggresiveness <= 0f)
            //    sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, direction)}");
            Blame(PersonalityDefOf.Rimpsyche_Aggressiveness);
            Blame(PersonalityDefOf.Rimpsyche_Tension);
            Blame(PersonalityDefOf.Rimpsyche_Stability);
        }
    }
    public class CowardlyDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.Evaluate(FormulaDB.FlightThreshold) <= 0f) return 0f;
            return compPsyche.Evaluate(FormulaDB.FlightChance);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Aggressiveness);
            Blame(PersonalityDefOf.Rimpsyche_Tenacity);
            Blame(PersonalityDefOf.Rimpsyche_Bravery);
        }
    }
    public class ExperimentalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Experimentation);
        }
    }

    public class DeliberationDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Deliberation);
        }
    }
    public class IndustriousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Diligence);
        }
    }
    //Mentalbreak threshold
    public class TenseDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var score = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
            return score;
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Tension);
        }
    }
    //Trade, Negotiation etc
    public class TactfulDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var tact = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tact);
            var confidence = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
            var score = (tact + 0.2f * confidence) / 1.2f;
            return score;
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Tact);
            Blame(PersonalityDefOf.Rimpsyche_Confidence);
            Blame(PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Neutral);
        }
    }
    public class OrganizedDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Organization);
        }
    }
    public class BraceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var offset = compPsyche.Evaluate(PsychePainShockThresholdStatPart.PainShockThresholdOffset); //0.35f base
            return offset / 0.35f;
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Bravery);
            Blame(PersonalityDefOf.Rimpsyche_Tenacity);
        }
    }
    public class ReflectionDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
        }
        protected override void SetupBlamers()
        {
            Blame(PersonalityDefOf.Rimpsyche_Reflectiveness);
        }
    }
}
