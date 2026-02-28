using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("HideInShame")]
    [HarmonyPatch(typeof(SituationalThoughtHandler), nameof(SituationalThoughtHandler.Notify_SituationalThoughtsDirty))]
    public class SituationalThoughtHandler_Notify_SituationalThoughtsDirty
    {
        public static void Postfix(Pawn ___pawn)
        {
            var compPsyche = ___pawn.compPsyche();
            compPsyche?.CleanShame();
        }
    }
}
