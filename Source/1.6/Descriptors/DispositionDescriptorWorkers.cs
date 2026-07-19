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
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Ambition, direction)}");
            return sb.ToString();
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
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, direction)}");
            if (!direction)
                sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Discipline, direction)}");
            return sb.ToString();
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
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Optimism, direction)}");
            return sb.ToString();
        }
    }
    //Mood Strength
    public class EmotionalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Emotionality, direction)}");
            return sb.ToString();
        }
    }
    //Mood Swing: MoodRisingSpeedMultiplier, MoodFallingSpeedMultiplier
    public class MoodswingDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Stability);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Stability, direction)}");
            return sb.ToString();
        }
    }
    //Hide in shame
    public class ModestDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.ModestShameGain) * 10f / 3f;
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Propriety, direction)}");
            return sb.ToString();
        }
    }
    public class FierceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.AdrenalineGain) / 9f;
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            //float bravery = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
            //float aggresiveness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            //if (bravery <= 0.4f || bravery + aggresiveness <= 0f)
            //    sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness, direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tension, direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Stability, direction)}");
            return sb.ToString();
        }
    }
    public class CowardlyDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.Evaluate(FormulaDB.FlightThreshold) <= 0f) return 0f;
            return compPsyche.Evaluate(FormulaDB.FlightChance);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness, !direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, !direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, !direction)}");
            return sb.ToString();
        }
    }
    public class ExperimentalDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Experimentation, direction)}");
            return sb.ToString();
        }
    }

    public class DeliberationDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Deliberation, direction)}");
            return sb.ToString();
        }
    }
    public class IndustriousDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Diligence, direction)}");
            return sb.ToString();
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
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tension, direction)}");
            return sb.ToString();
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
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tact, direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Confidence, direction)}");
            sb.AppendLine($"  {GetNeutralBlame(compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest)}");
            return sb.ToString();
        }
    }
    public class OrganizedDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Organization, direction)}");
            return sb.ToString();
        }
    }
    public class BraceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var offset = compPsyche.Evaluate(PsychePainShockThresholdStatPart.PainShockThresholdOffset); //0.35f base
            return offset / 0.35f;
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, direction)}");
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, direction)}");
            return sb.ToString();
        }
    }
    public class ReflectionDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
        }
        protected override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = bScore > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(bDescription);
            sb.AppendLine();
            sb.AppendLine("RPC_DescriptorBlame".Translate());
            sb.AppendLine($"  {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Reflectiveness, direction)}");
            return sb.ToString();
        }
    }
}
