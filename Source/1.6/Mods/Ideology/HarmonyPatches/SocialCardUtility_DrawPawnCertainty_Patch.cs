using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    [HarmonyPatch(typeof(SocialCardUtility), nameof(SocialCardUtility.DrawPawnCertainty))]
    public static class SocialCardUtility_DrawPawnCertainty_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var codes = new List<CodeInstruction>(instructions);
            var getCertaintyReductionMethod = AccessTools.Method(typeof(ConversionUtility), nameof(ConversionUtility.GetCertaintyReductionFactorsDescription));
            var certaintyDescGiverMethod = AccessTools.Method(typeof(CertaintyDescGiverClass), nameof(CertaintyDescGiverClass.CertaintyDescGiver));

            int targetIndex = -1;
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo callMi && callMi == getCertaintyReductionMethod)
                {
                    for (int j = i - 1; j > 0; j--)
                    {
                        if (codes[j].opcode == OpCodes.Ldstr && codes[j].operand is string s && s == "\n\n")
                        {
                            targetIndex = j+1;
                            break;
                        }
                    }
                    if (targetIndex > 0) break;
                }
            }
            if (targetIndex != -1)
            {
                var injected = new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Call, certaintyDescGiverMethod)
                };
                codes.InsertRange(targetIndex, injected);
            }
            else
            {
                Log.Message("[RimPsyche] failed to patch Certainty Change Per Day UI.");
            }
            return codes;
        }
    }

    public static class CertaintyDescGiverClass
    {
        public static string CertaintyDescGiver(string original, Pawn pawn)
        {
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled == true)
            {
                return $"\n -  " + "RP_Stat_CertaintyLossMult".Translate() + ": x" + compPsyche.Evaluate(PsycheCertaintyLossStatPart.RP_CertaintyLossMult).ToStringPercent() + original;
            }
            else
            {
                return original;
            }
        }
    }
}