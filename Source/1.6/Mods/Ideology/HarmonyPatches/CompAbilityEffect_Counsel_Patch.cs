using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    public static class CompAbilityEffect_Counsel_Patch
    {
        [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "ChanceForPawn")]
        public static class CompAbilityEffect_Counsel_ChanceForPawn
        {
            static void Postfix(ref float __result, Pawn pawn, Ability ___parent)
            {
                var counselorPsyche = ___parent.pawn.compPsyche();
                var listenerPsyche = pawn.compPsyche();
                if (counselorPsyche?.Enabled == true && listenerPsyche?.Enabled == true)
                {
                    __result *= counselorPsyche.Evaluate(CounselTactMultiplier) * listenerPsyche.Evaluate(CounselTrustMultiplier);
                }
            }
        }
        [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "ExtraLabelMouseAttachment")]
        public static class CompAbilityEffect_Counsel_ExtraLabelMouseAttachment
        {
            static void Postfix(ref string __result, LocalTargetInfo target, Ability ___parent)
            {
                Pawn targetPawn = target.Pawn;
                if (targetPawn == null)
                    return;
                var counselorPsyche = ___parent.pawn.compPsyche();
                var listenerPsyche = targetPawn.compPsyche();

                if (counselorPsyche?.Enabled == true && listenerPsyche?.Enabled == true)
                {
                    string multiplier = (counselorPsyche.Evaluate(CounselTactMultiplier) * listenerPsyche.Evaluate(CounselTrustMultiplier)).ToStringPercent();
                    string additionalString = "\n" + " -  " + "Psyche_CounselEffect".Translate() + " " + multiplier;
                    __result += additionalString;
                }

            }
        }

        public static RimpsycheFormula CounselTactMultiplier = new(
            "CounselTactMultiplier",
            (tracker) =>
            {
                float mult = 1f + 0.15f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Tact);
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
        public static RimpsycheFormula CounselTrustMultiplier = new(
            "CounselTrustMultiplier",
            (tracker) =>
            {
                float mult = 1f + 0.2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Trust);
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
