using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class ThoughtWorker_OrganizedMood : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            var compPsyche = p.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return ThoughtState.Inactive;
            }
            if (compPsyche.organizedMood == -1)
            {
                return ThoughtState.Inactive;
            }
            return ThoughtState.ActiveAtStage(compPsyche.organizedMood);
        }
        public override float MoodMultiplier(Pawn p)
        {
            float val = base.MoodMultiplier(p);
            var compPsyche = p.compPsyche();
            if (compPsyche?.Enabled == true)
            {
                return val * compPsyche.Evaluate(OrganizedRoomMood);
            }
            return val;
        }

        public static RimpsycheFormula OrganizedRoomMood = new(
            "OrganizedRoomMood",
            (tracker) =>
            {
                float mult = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Organization) * 12f;
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}