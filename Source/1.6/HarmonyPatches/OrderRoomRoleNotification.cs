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

    [HarmonyPatch(typeof(Toils_Recipe), "DoRecipeWork")]
    public static class Toils_Recipe_DoRecipeWork_Patch
    {
        static void Postfix(Toil __result)
        {
            workout.AddFinishAction(delegate
            {
                NorifyToilFinish(__result.actor3);
            });
        }

        NorifyToilFinish(Pawn pawn)
        {
            Log.Message($"{pawn.Name} finished toil");
        }
    }
}
