using HarmonyLib;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("Experimentation")]
    [HarmonyPatch(typeof(QualityUtility))]
    [HarmonyPatch("GenerateQualityCreatedByPawn")]
    [HarmonyPatch(new[] { typeof(Pawn), typeof(SkillDef), typeof(bool) })]
    public static class QualityUtility_GenerateQualityCreatedByPawn_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var originalMethod = AccessTools.Method(typeof(QualityUtility), "GenerateQualityCreatedByPawn", new[] { typeof(int), typeof(bool) });
            var replacementMethod = AccessTools.Method(typeof(Maux36.RimPsyche.Disposition.QualityUtil), "GenerateQualityCreatedByPawnWithPsyche");

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Calls(originalMethod))
                {
                    codes.Insert(i, new CodeInstruction(OpCodes.Ldarg_0));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldarg_1));
                    codes.Insert(i + 2, new CodeInstruction(OpCodes.Ldc_R4, 0f));
                    codes[i + 3] = new CodeInstruction(OpCodes.Call, replacementMethod);
                    break;
                }
            }
            return codes;
        }
    }

}
