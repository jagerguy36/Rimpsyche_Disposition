using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
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
