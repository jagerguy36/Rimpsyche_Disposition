using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

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

    [HarmonyPatch(typeof(Toils_Recipe), "DoRecipeWork")]
    public static class Toils_Recipe_DoRecipeWork_Patch
    {
        static void Postfix(Toil __result)
        {
            __result.AddFinishAction(delegate
            {
                NotifyToilFinished(__result.actor);
            });
        }

        public static void NotifyToilFinished(Pawn pawn)
        {
            Log.Message($"{pawn.Name} finished toil");
        }
    }
}
