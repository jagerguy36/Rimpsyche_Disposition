using System.Diagnostics;
using System.Text;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public class ThoughtCharityDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (!RimpsycheDispositionSettings.useIndividualThoughts)
                return 0f;
            if (!RimpsycheSettings.ShowThoughtTagEffect)
                return 0f;
            var charitability = -compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest);
            return Mathf.Max(charitability, 0f);
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_SelfInterest, PsycheDescDirection.Negative);
        }
    }
    public class SlaveWillDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.parentPawn.IsColonistPlayerControlled)
                return 0f;
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            return tenacity;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity);
        }
    }
    public class SuppressionResistanceDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.parentPawn.IsColonistPlayerControlled)
                return 0f;
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            var bravery = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
            return (tenacity + bravery) * 0.5f;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Bravery);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tenacity);
        }
    }
    public class SuppressionPowerDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            var aggressiveness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
            return aggressiveness;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Aggressiveness);
        }
    }
    public class TerrorDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (compPsyche.parentPawn.IsColonistPlayerControlled)
                return 0f;
            var aggressiveness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Bravery);
            return aggressiveness;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Bravery);
        }
    }
    public class CertaintylossfactorDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (Find.IdeoManager.classicMode)
                return 0f;
            var confidence = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
            var openness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
            var trust = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
            float conviction = (confidence - openness) * 0.5f; // -1~1
            float shake = (trust - conviction) * 0.5f; // -1~1
            return shake;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Confidence, PsycheDescDirection.Negative);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Openness);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Trust);
        }
    }
    public class ConversionPowerDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (Find.IdeoManager.classicMode)
                return 0f;
            float confidence = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
            float openness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
            float authenticity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Authenticity);
            float conviction = (confidence - openness) * 0.5f;
            float sincerity = 0.25f * ((conviction * (3f + authenticity) + (1 - authenticity))); //-1~1
            float tact = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tact); //-1~1
            float conversionPower = (tact + sincerity) * 0.5f;
            return conversionPower;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Confidence);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Openness, PsycheDescDirection.Negative);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Authenticity, PsycheDescDirection.Neutral);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Tact);
        }
    }
    public class IdeoSpreadMultDescriptorWorker : PsycheDescriptorWorker
    {
        protected override float Score(CompPsyche compPsyche)
        {
            if (Find.IdeoManager.classicMode)
                return 0f;
            float confidence = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence);
            float openness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Openness);
            float passion = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Passion);
            float conviction = (confidence - openness) * 0.5f;
            float zealousy = 0.25f * ((conviction * (3f - passion) + (1 + passion))); //-1~1
            float talkativeness = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Talkativeness); //-1~1
            float spread = 0.5f * (talkativeness + zealousy);
            return spread;
        }
        protected override void Evaluate(StringBuilder ctx, CompPsyche compPsyche, float score)
        {
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Confidence);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Openness, PsycheDescDirection.Negative);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Passion, PsycheDescDirection.Neutral);
            Blame(ctx, compPsyche, PersonalityDefOf.Rimpsyche_Talkativeness);
        }
    }
}
