using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_PsychologicallyNudeMultiplied : ThoughtWorker_PsychologicallyNude
    {
        public override float MoodMultiplier(Pawn p)
        {
            float val = base.MoodMultiplier(p);
            var compPsyche = p.compPsyche();
            if (compPsyche != null)
            {
                return val * compPsyche.Personality.GetMultiplier(PrudishNakedMultiplier);
            }
            return val;
        }

        public static RimpsycheMultiplier PrudishNakedMultiplier = new(
            "PrudishNakedMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Prudishness) * 0.7f;
                return optimism;
            }
        );
    }
}
