using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    [HarmonyPatch(typeof(ITab_Pawn_Visitor), "DoSlaveTab")]
    public static class ITab_Pawn_Visitor_DoSlaveTab_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var codes = new List<CodeInstruction>(instructions);

            // Define the methods to patch
            var add_TaggedString_string = AccessTools.Method(typeof(TaggedString), "op_Addition", new[] { typeof(TaggedString), typeof(string) });
            var myDescGiverMethod = AccessTools.Method(typeof(TerrorDescGiverClass), "TerrorDescGiver", new[] { typeof(Pawn) });
            var selPawnGetter = AccessTools.PropertyGetter(typeof(ITab), "SelPawn");

            int targetLdLocS = -1;

            // Search for the unique instruction pattern
            for (int i = 0; i < codes.Count; i++)
            {
                // We're looking for the `ldstr` instruction that loads "TerrorCurrentThoughts"
                if (codes[i].opcode == OpCodes.Ldstr && codes[i].operand is string s && s == "TerrorCurrentThoughts")
                {
                    for (int j = i+1; j < i + 10; j++)
                    {
                        if (codes[j].opcode == OpCodes.Call && codes[j].operand is MethodInfo callMi && callMi == add_TaggedString_string && codes[j+1].opcode == OpCodes.Ldloc_S)
                        {
                            targetLdLocS = j+1;
                        }
                    }

                }
            }
            if (targetLdLocS != -1)
            {
                int insertAt = targetLdLocS + 2;
                var injected = new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Callvirt, selPawnGetter),
                    new CodeInstruction(OpCodes.Call, myDescGiverMethod),
                    new CodeInstruction(OpCodes.Call, add_TaggedString_string)
                };
                codes.InsertRange(insertAt, injected);
            }
            else
            {
                Log.Message("[RimPsyche] failed to patch Current Terror Description.");
            }
            return codes;
        }
    }

    public static class TerrorDescGiverClass
    {
        public static string TerrorDescGiver(Pawn pawn)
        {
            // Your custom logic here
            return $"\n\nMy custom addition to the tooltip description about {pawn.Name}.";
        }
    }
}