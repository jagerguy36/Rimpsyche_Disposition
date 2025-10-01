using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{

    [HarmonyPatch(typeof(Pawn_GuestTracker), "SetGuestStatus")]
    public static class Pawn_GuestTracker_SetGuestStatus
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            FieldInfo targetField = AccessTools.Field(typeof(PawnKindDef), "initialWillRange");
            MethodInfo originalMethod = AccessTools.Method(typeof(FloatRange), "get_RandomInRange");
            MethodInfo customMethod = AccessTools.Method(typeof(RimPsycheGuestTracker), "PsycheWillRange");
            bool found = false;
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call && codes[i].operand as MethodInfo == originalMethod && !found)
                {
                    if (codes[i - 1].opcode == OpCodes.Ldloca_S
                        && codes[i - 2].opcode == OpCodes.Stloc_S
                        && codes[i - 3].opcode == OpCodes.Call
                        && codes[i - 4].operand as FieldInfo == targetField
                        && codes[i - 5].opcode == OpCodes.Ldfld)
                    {
                        found = true;
                        codes[i].operand = customMethod;
                        codes.RemoveAt(i - 1);
                        codes.RemoveAt(i - 2);
                        codes.RemoveAt(i - 3);
                        codes.RemoveAt(i - 4);
                        codes.RemoveAt(i - 5);
                        break;
                    }
                }
            }
            if (!found)
            {
                Log.Warning("[Rimpsyche] Could not find the target instruction for Pawn_GuestTracker.SetGuestStatus transpiler patch. (Ideology)");
            }
            return codes;
        }
    }
}
