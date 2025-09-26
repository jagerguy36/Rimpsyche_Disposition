using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_AmbitionMood : ThoughtWorker
    {
        private const int dayTick = 60000;
        private const int maxMinusTick = -600000;
        //private const float halfDayTick = 30000f;
        private static readonly bool useThought = RimpsycheDispositionSettings.useSenseOfProgress;
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!useThought)
            {
                return ThoughtState.Inactive;
            }
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
            if (0 < pTick)
            {
                return ThoughtState.ActiveAtStage(compPsyche.progressLastCauseIndex);
            }
            else
            {
                //Pawns with ambition < 0 will never be in this state due to cut-off point
                if (Mathf.Max(maxMinusTick, pTick) < compPsyche.Evaluate(AmbitionDissatisfactionTick))
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
                var curTick = Find.TickManager.TicksGame;
                var pTick = compPsyche.progressTick - curTick;
                if (0 <= pTick)
                {
                    // Positive mood ambition -1 -> 0
                    // Positive mood ambition 0 -> 0.6*days
                    // Positive mood ambition 1 -> 1.2*days
                    // Content pawns can feel satisfaction too.
                    return val * compPsyche.Evaluate(AmbitionAccomplishmentMood) * pTick;
                }
                var mult = Mathf.Min(0f, Mathf.Max(maxMinusTick, pTick) - compPsyche.Evaluate(AmbitionDissatisfactionTick)) / dayTick; // In days: (x - (8*A - 10))
                return mult;

            }
            return val;
        }

        public override string PostProcessDescription(Pawn p, string description)
        {
            var compPsyche = p.compPsyche();
            if (compPsyche?.Enabled == true && compPsyche.progressLastCause != null)
            {
                return description + "\n\n" + compPsyche.progressLastCause;
            }
            return description;
        }

        public static RimpsycheFormula AmbitionAccomplishmentMood = new(
            "AmbitionAccomplishmentMood",
            (tracker) =>
            {
                float mult = 0.00001f * (tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) + 1f);
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula AmbitionDissatisfactionTick = new(
            "AmbitionDissatisfactionTick",
            (tracker) =>
            {
                //Ticks after which pawns become dissatisfied.
                //Ambition 1 -> -120000 (-2days)
                //Ambition 0.5 -> -360000 (-6days)
                //Ambition 0 -> -600000 (-10days) (This is the cut-off)
                //Ambition -1 -> -1800000 (-30days) (Doesn't matter)
                float tick = (480000f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition)) - 600000f; 
                return tick;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}