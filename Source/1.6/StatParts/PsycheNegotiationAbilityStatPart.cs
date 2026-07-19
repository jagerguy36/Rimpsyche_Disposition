using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheNegotiationAbilityStatPart : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    val *= compPsyche.Evaluate(NegotiationAbilityFactor);
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
                    return "RP_Stat_Psyche".Translate() + "\n    " + "RP_Stat_NegotiationAbilityOffset".Translate() + ": x" + compPsyche.Evaluate(NegotiationAbilityFactor).ToStringPercent() + "\n";
                }
            }
            return null;
        }

        public static RimpsycheFormula NegotiationAbilityFactor = new(
            "NegotiationAbilityFactor",
            (tracker) =>
            {
                var baselinemult = 1f + 0.1f * (tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tact) + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Confidence));
                var interestmult = 1.05f - 0.1f * Mathf.Abs(tracker.GetPersonality(PersonalityDefOf.Rimpsyche_SelfInterest));
                return baselinemult * interestmult; //0.76~1.26
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
