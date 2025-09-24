using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public static class CompAbilityEffect_Counsel_Patch
    {
        [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "ChanceForPawn")]
        public static class CompAbilityEffect_Counsel_ChanceForPawn
        {
            static void Postfix(ref float __result, Pawn pawn, CompAbilityEffect ___parent)
            {
                return ___result;
            }
        }
    }
    public static class CompAbilityEffect_Counsel_Patch
    {
        [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "ExtraLabelMouseAttachment")]
        public static class CompAbilityEffect_Counsel_ChanceForPawn
        {
            static void Postfix(ref float __result, Pawn pawn, CompAbilityEffect ___parent)
            {
                return ___result;
            }
        }
    }
}
