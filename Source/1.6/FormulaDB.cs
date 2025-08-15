using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maux36.RimPsyche.Disposition
{
    public static class FormulaDB
    {
        public static RimpsycheFormula PrudishNakedMultiplier = new(
            "PrudishNakedMultiplier",
            (tracker) =>
            {
                float optimism = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Prudishness) * 0.7f;
                return optimism;
            }
        );

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
