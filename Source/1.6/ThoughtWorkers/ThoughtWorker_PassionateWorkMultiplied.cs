using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_PassionateWorkMultiplied : ThoughtWorker_PassionateWork
    {
        public override float MoodMultiplier(Pawn p)
        {
            float val = base.MoodMultiplier(p);
            var compPsyche = p.compPsyche();
            if (compPsyche != null)
            {
                return val * compPsyche.Personality.Evaluate(PassionWorkMultiplier);
            }
            return val;
        }

        public static RimpsycheFormula PassionWorkMultiplier = new(
            "PassionWorkMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Passion) * 0.5f;
                return optimism;
            }
        );
    }
}
