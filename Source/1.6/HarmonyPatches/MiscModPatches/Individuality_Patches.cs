using HarmonyLib;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("Individuality")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.GenerateQualityCreatedByPawnWithPsyche))]
    public class Individuality_QualityUtil_GenerateQualityCreatedByPawnWithPsyche_Patch
    {
        public static void Prefix(Pawn pawn, ref float numOffset)
        {
            if (IntegrationDatabase.SYR_Perfectionist != null && (pawn?.story?.traits?.HasTrait(IntegrationDatabase.SYR_Perfectionist) ?? false))
            {
                numOffset = Rand.Chance(0.5f) ? 1f : 0f;
            }
        }
    }
}
