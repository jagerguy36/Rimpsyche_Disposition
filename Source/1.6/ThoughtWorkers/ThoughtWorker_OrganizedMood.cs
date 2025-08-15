using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_OrganizedMood : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            var compPsyche = p.compPsyche();
            if (compPsyche == null)
            {
                return ThoughtState.Inactive;
            }
            if (compPsyche.organizedMood == -1)
            {
                return ThoughtState.Inactive;
            }
            if (compPsyche.organizedMood == 0)
            {
                return ThoughtState.ActiveAtStage(0);
            }
            if (compPsyche.organizedMood == 1)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            if (compPsyche.organizedMood == 2)
            {
                return ThoughtState.ActiveAtStage(2);
            }
            if (compPsyche.organizedMood == 3)
            {
                return ThoughtState.ActiveAtStage(3);
            }
            return ThoughtState.Inactive;
        }
        public override float MoodMultiplier(Pawn p)
        {
            float val = base.MoodMultiplier(p);
            var compPsyche = p.compPsyche();
            if (compPsyche != null)
            {
                return val * compPsyche.Personality.Evaluate(OrganizedRoomMood);
            }
            return val;
        }

        public static RimpsycheFormula OrganizedRoomMood = new(
            "OrganizedRoomMood",
            (tracker) =>
            {
                float optimism = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Organization) * 12f;
                return optimism;
            }
        );
    }
}