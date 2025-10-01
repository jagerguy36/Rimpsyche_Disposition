using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("PerformanceModeThought")]
    [HarmonyPatch(typeof(ThoughtHandler), nameof(ThoughtHandler.MoodOffsetOfGroup))]
    public static class ThoughtHandler_MoodOffsetOfGroup_Patch
    {
        private static readonly bool useIndividualThoughtsSetting = RimpsycheDispositionSettings.useIndividualThoughts;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            var moodOffsetMethod = AccessTools.Method(typeof(Thought), nameof(Thought.MoodOffset));
            var moodMultiplierMethod = AccessTools.Method(typeof(ThoughtUtil), nameof(ThoughtUtil.MoodMultiplier));
            var pawnField = AccessTools.Field(typeof(ThoughtHandler), nameof(ThoughtHandler.pawn));
            bool patched = false;
            for (int t = 0; t < codes.Count; t++)
            {
                var ci = codes[t];
                yield return ci;
                if (ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi == moodOffsetMethod)
                {
                    patched = true;
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // load this
                    yield return new CodeInstruction(OpCodes.Ldfld, pawnField); // load pawn
                    yield return new CodeInstruction(codes[t - 1]); // load thought
                    yield return new CodeInstruction(OpCodes.Call, moodMultiplierMethod); // call MoodMultiplier
                }
            }
            if (!patched)
            {
                Log.Error("[Rimpsyche] Mood thought transpiler failed to patch");
            }
        }
    }
}