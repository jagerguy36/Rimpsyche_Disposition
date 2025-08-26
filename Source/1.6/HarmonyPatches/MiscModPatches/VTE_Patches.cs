using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class IntegrationDatabase
    {
        public static TraitDef VTE_Perfectionist = DefDatabase<TraitDef>.GetNamedSilentFail("VTE_Perfectionist");
        static IntegrationDatabase()
        {

        }
    }

    [HarmonyPatchCategory("VTE")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.GenerateQualityCreatedByPawnWithPsyche))]
    public class QualityUtil_GenerateQualityCreatedByPawnWithPsyche_Patch
    {
        public static void Prefix(Pawn pawn, ref float numOffset)
        {
            if (IntegrationDatabase.VTE_Perfectionist != null && (pawn?.story?.traits?.HasTrait(IntegrationDatabase.VTE_Perfectionist) ?? false))
            {
                numOffset = 1f;
            }
        }
    }
}
