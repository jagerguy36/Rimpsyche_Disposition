using HarmonyLib;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class EndlessGrowth_Patches
    {
        public static readonly SimpleCurve EG_QualityModifierCurve = new()
        {
            {
                new CurvePoint(0f, 0.7f),
                true
            },
            {
                new CurvePoint(2f, 1.5f),
                true
            },
            {
                new CurvePoint(3f, 1.8f),
                true
            },
            {
                new CurvePoint(4f, 2.0f),
                true
            },
            {
                new CurvePoint(8f, 2.8f),
                true
            },
            {
                new CurvePoint(12f, 3.4f),
                true
            },
            {
                new CurvePoint(20f, 4.2f),
                true
            },
            {
                new CurvePoint(30f, 5.0f),
                true
            },
            {
                new CurvePoint(40f, 5.7f),
                true
            },
            {
                new CurvePoint(50f, 6.0f),
                true
            },
            {
                new CurvePoint(100f, 10.0f),
                true
            }
        };
    }


    [HarmonyPatchCategory("EndlessGrowth")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.Qvalue))]
    public class EndlessGrowth_QualityUtil_Qvalue_Patch
    {
        public static bool Prefix(int relevantSkillLevel, ref float __result)
        {
            __result = EndlessGrowth_Patches.EG_QualityModifierCurve.Evaluate(relevantSkillLevel);
            return false;
        }
    }
}
