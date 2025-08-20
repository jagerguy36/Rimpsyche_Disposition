using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheShootingAccuracyPawnStatPart : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Personality.Evaluate(ShootingAccuracyPawnOffset);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    return "RP_Stat_ShootingAccuracyPawnOffset".Translate() + ": " + compPsyche.Personality.Evaluate(ShootingAccuracyPawnOffset).ToStringSign();
                }
            }
            return null;
        }

        public static RimpsycheFormula ShootingAccuracyPawnOffset = new(
            "ShootingAccuracyPawnOffset",
            (tracker) =>
            {
                float diligence = 3f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Deliberation);
                return diligence;
            }
        );
    }
}
