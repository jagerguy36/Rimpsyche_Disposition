using HarmonyLib;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("TheSimsTraits")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.GenerateQualityCreatedByPawnWithPsyche))]
    public class TheSimsTraits_QualityUtil_GenerateQualityCreatedByPawnWithPsyche_Patch
    {
        public static void Prefix(Pawn pawn, ref float numOffset)
        {
            if (IntegrationDatabase.ST_Procrastinator != null && (pawn?.story?.traits?.HasTrait(IntegrationDatabase.ST_Procrastinator) ?? false))
            {
                numOffset = -1f;
            }
        }
    }
}
