using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheReflectivenessMeditationFocusGainStatPart : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Personality.Evaluate(ReflectivenessMeditationMult);
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
                    return "RP_Stat_ReflectivenessMeditation".Translate() + ": " + compPsyche.Personality.Evaluate(ReflectivenessMeditationMult).ToStringPercentSigned();
                }
            }
            return null;
        }

        public static RimpsycheFormula ReflectivenessMeditationMult = new(
            "ReflectivenessMeditationMult",
            (tracker) =>
            {
                float reflectiveness = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness) * 0.2f;
                return reflectiveness;
            }
        );
    }
}
