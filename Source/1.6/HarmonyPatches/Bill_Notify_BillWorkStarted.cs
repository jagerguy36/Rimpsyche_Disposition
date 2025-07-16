using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Bill), "Notify_BillWorkStarted")]
    public static class Bill_Notify_BillWorkStarted_Patch
    {
        static void Postfix(Pawn billDoer)
        {
            Log.Message($"{billDoer.Name} started bill");
        }
    }
}
