using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;


namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("PerformanceModeThought")]
    [HarmonyPatch(typeof(CompAbilityEffect_Counsel), "Apply")]
    public static class Patch_CompAbilityEffect_Counsel_Apply
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var moodOffsetMethod = AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            var moodMultiplierMethod = AccessTools.Method(typeof(ThoughtUtil), nameof(ThoughtUtil.MoodMultiplier));
            var pawnField = AccessTools.PropertyGetter(typeof(LocalTargetInfo), nameof(LocalTargetInfo.Pawn));

            for (int i = 0; i < codes.Count; i++)
            {
                var ci = codes[i];
                yield return ci;
                if (ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi == moodOffsetMethod)
                {
                    yield return new CodeInstruction(OpCodes.Ldarga_S, (byte)1); // target index is 1
                    yield return new CodeInstruction(OpCodes.Callvirt, pawnField); // load pawn
                    yield return new CodeInstruction(codes[i - 1]); // load thought
                    yield return new CodeInstruction(OpCodes.Call, moodMultiplierMethod); // call MoodMultiplier
                }
            }
        }
    }
}