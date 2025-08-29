using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(TaleRecorder), nameof(TaleRecorder.RecordTale))]
    public static class TaleRecorder_RecordTale
    {
        private static void Postfix(Tale __result, TaleDef def, object[] args)
        {
            if (__result != null)
            {
                if (TaleDB.TaleWorkerDB.TryGetValue(def.defName, out (string, int, int) indices))
                {
                    var causeKey = indices.Item1;
                    var causeIndex = indices.Item2;
                    var score = indices.Item3;
                    Log.Message($"gaining score {score} with key {causeKey}");
                    if (causeIndex >= 0 && args.Length >causeIndex)
                    {
                        var causeDef = args[causeIndex] as ThingDef;
                        Log.Message($"from cause {causeDef.label}");
                    }
                }
                else if (def.defName == "GaveBirth")
                {
                    Pawn birther = args[2] as Pawn;
                    if (birther != null)
                    {
                        if (birther.IsColonist)
                        {
                            Log.Message($"gaining score {1000} with birth: RP_Achieve_Birth");
                        }
                    }
                }
            }
        }
    }

    public static class TaleDB
    {
        public static Dictionary<string, (string, int, int)> TaleWorkerDB = new Dictionary<string, (string, int, int)>()
        {
            { "DefeatedHostileFactionLeader", ("RP_Achieve_DefeatedLeader",-1, 1000) }, // >= 10000f
        };
    }

}
