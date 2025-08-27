using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(MentalBreaker), nameof(MentalBreaker.TryDoMentalBreak))]
    public static class MentalBreaker_TryDoMentalBreak
    {
        [HarmonyPriority(Priority.First)]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var codes = new List<CodeInstruction>(instructions);
            MethodInfo getWorkerMethod = AccessTools.Method(typeof(MentalBreakDef), "get_Worker");
            FieldInfo pawnField = AccessTools.Field(typeof(MentalBreaker), "pawn");
            MethodInfo customMethod = AccessTools.Method(typeof(ResilienceUtil), "TestResilientSpirit");
            var skiplabel = generator.DefineLabel();
            var normallabel = generator.DefineLabel();

            for (int i = 0; i < codes.Count; i++)
            {
                var instr = codes[i];
                if (i + 1 < codes.Count && codes[i].opcode == OpCodes.Ldarg_2 && codes[i + 1].Calls(getWorkerMethod))
                {
                    yield return instr;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, pawnField);
                    yield return new CodeInstruction(OpCodes.Call, customMethod);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, skiplabel); //If Tested True, then skil and return false.
                    yield return new CodeInstruction(OpCodes.Br, normallabel); //If Tested false, go to the vanilla code

                    yield return new CodeInstruction(OpCodes.Ldc_I4_0).WithLabels(skiplabel);
                    yield return new CodeInstruction(OpCodes.Ret);
                    yield return new CodeInstruction(OpCodes.Ldarg_2).WithLabels(normallabel);
                }
                else yield return instr;
            }
        }
    }
}
