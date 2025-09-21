using HarmonyLib;
using Verse;

namespace Maux36.RimPsyche.Disposition
{

    [HarmonyPatchCategory("VTE")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.GenerateQualityCreatedByPawnWithPsyche))]
    public class VTE_QualityUtil_GenerateQualityCreatedByPawnWithPsyche_Patch
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
