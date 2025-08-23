using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_AmbitionMood : ThoughtWorker
    {
        private const int dayTick = 60000;
        private const int maxMinusTick = -600000;
        private const float halfDayTick = 30000f;
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.Faction?.IsPlayer != true)
            {
                return ThoughtState.Inactive;
            }
            var compPsyche = p.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return ThoughtState.Inactive;
            }
            int pTick = compPsyche.progressTick - Find.TickManager.TicksGame;
            if (0 <= pTick)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            else
            {
                if (pTick <= compPsyche.Personality.Evaluate(AmbitionDissatisfactionTick))
                {
                    return ThoughtState.ActiveAtStage(0);
                }
                return ThoughtState.Inactive;
            }
        }
        public override float MoodMultiplier(Pawn p)
        {
            float val = base.MoodMultiplier(p);
            var compPsyche = p.compPsyche();
            if (compPsyche?.Enabled == true)
            {
                var personality = compPsyche.Personality;
                var curTick = Find.TickManager.TicksGame;
                var pTick = compPsyche.progressTick - curTick;
                if (0 <= pTick)
                {
                    return val * personality.Evaluate(AmbitionAccomplishmentMood) * pTick;
                }
                var mult = (Mathf.Max(maxMinusTick, pTick) - compPsyche.Personality.Evaluate(AmbitionDissatisfactionTick)) / halfDayTick; // In days: x - (8*A - 10)
                return mult;

            }
            return val;
        }

        public static RimpsycheFormula AmbitionAccomplishmentMood = new(
            "AmbitionAccomplishmentMood",
            (tracker) =>
            {
                float mult = 0.00002f * (tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) + 1f);
                return mult;
            }
        );

        public static RimpsycheFormula AmbitionDissatisfactionTick = new(
            "AmbitionDissatisfactionTick",
            (tracker) =>
            {
                //Ticks after which pawns become dissatisfied.
                //Ambition 1 -> -120000 (-2days)
                //Ambition 0.5 -> -360000 (-6days)
                //Ambition 0 -> -600000 (-10days) (This is the cut-off)
                //Ambition -1 -> -1800000 (-30days)
                float tick = (480000f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition)) - 600000f; 
                return tick;
            }
        );
    }
}