using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(QualityUtility))]
    public static class QualityUtility_GenerateQualityCreatedByPawn
    {
        [HarmonyPatch("GenerateQualityCreatedByPawn")]
        [HarmonyPatch(new Type[] { typeof(Pawn), typeof(SkillDef) })]
        public static class MyPostfixPatch
        {
            [HarmonyPostfix]
            public static void Postfix(QualityCategory __result, Pawn pawn, SkillDef relevantSkill)
            {
                Log.Message($"GenerateQualityCreatedByPawn postfix: Pawn={pawn.NameShort}, Skill={relevantSkill.defName}, OriginalQuality={__result}");
            }
        }
    }
}
