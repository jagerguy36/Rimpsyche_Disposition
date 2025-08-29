using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheReflectivenessMeditationFocusGainStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val += compPsyche.Evaluate(ReflectivenessMeditationMult);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_ReflectivenessMeditation".Translate() + ": " + compPsyche.Evaluate(ReflectivenessMeditationMult).ToStringPercentSigned() + "\n";
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
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
