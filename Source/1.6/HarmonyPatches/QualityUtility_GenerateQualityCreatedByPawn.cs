using HarmonyLib;
using Verse;
using RimWorld;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(QualityUtility))]
    [HarmonyPatch("GenerateQualityCreatedByPawn")]
    [HarmonyPatch(new[] { typeof(Pawn), typeof(SkillDef), typeof(bool) })]
    public static class QualityUtility_GenerateQualityCreatedByPawn_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(QualityCategory __result, Pawn pawn, SkillDef relevantSkill)
        {
            // Note: Using pawn.Name.ToStringShort is often better for logging.
            Log.Message($"GenerateQualityCreatedByPawn postfix: Pawn={pawn.Name.ToStringShort}, Skill={relevantSkill.defName}, OriginalQuality={__result}");
        }
    }
}
