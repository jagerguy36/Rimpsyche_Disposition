using System.Diagnostics;
using System.Text;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class IndustriousDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Diligence);
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Diligence, direction)}");
            return sb.ToString();
        }
    }
    public class AmbitiousDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition);
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Ambition, direction)}");
            return sb.ToString();
        }
    }
    public class ResilientDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            var tension = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tension);
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            var score = (tenacity - tension) * 0.5f;
            if ( score > 0f)
            {
                return score;
            }
            else
            {
                var discipline = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                return (score + 0.2f * discipline) / 1.2f;
            }
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tension, !direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, direction)}");
            if (!direction)
                sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Discipline, direction)}");
            return sb.ToString();
        }
    }
    public class OptimisticDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            var optimism = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Optimism);
            var emotion = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
            if (Mathf.Abs(optimism) + emotion > 0f)
            {
                return optimism;
            }
            else
            {
                return 0f;
            }
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            var score = Score(compPsyche);
            bool direction = score > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Optimism, direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Emotionality, true)}");
            return sb.ToString();
        }
    }
    public class StoicDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            var emotion = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Emotionality);
            if (emotion > 0f)
            {
                return 0f;
            }
            else
            {
                return -emotion;
            }
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Emotionality, !direction)}");
            return sb.ToString();
        }
    }
    public class ModestDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.ModestShameGain) * 10f / 3f;
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Propriety, direction)}");
            return sb.ToString();
        }
    }
    public class FierceDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Evaluate(FormulaDB.AdrenalineGain) / 9f;
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            //float bravery = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
            //float aggresiveness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            //if (bravery <= 0.4f || bravery + aggresiveness <= 0f)
            //    sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness, direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tension, direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Stability, direction)}");
            return sb.ToString();
        }
    }
    public class CowardlyDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.Evaluate(FormulaDB.FlightThreshold) <= 0f) return 0f;
            return compPsyche.Evaluate(FormulaDB.FlightChance);
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness, !direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Tenacity, !direction)}");
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Bravery, !direction)}");
            return sb.ToString();
        }
    }
    public class ExperimentalDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Experimentation);
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Experimentation, direction)}");
            return sb.ToString();
        }
    }
    public class OrganizedDescriptorWorker : PsycheDescriptorWorker
    {
        public override float Score(CompPsyche compPsyche)
        {
            return compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
        }
        public override string GetTooltip(CompPsyche compPsyche)
        {
            bool direction = Score(compPsyche) > 0f;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDescription(compPsyche));
            sb.AppendLine();
            sb.AppendLine("  " + "RPC_DescriptorBlame".Translate());
            sb.AppendLine($"    {GetBlame(compPsyche, PersonalityDefOf.Rimpsyche_Organization, direction)}");
            return sb.ToString();
        }
    }
}
