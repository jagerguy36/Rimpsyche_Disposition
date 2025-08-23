using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.FinishProject))]
    public static class ResearchManager_FinishProject
    {
        private static void Postfix(ResearchProjectDef proj, Pawn researcher)
        {
            Log.Message($"project {proj.defName} w/ cost: {proj.Cost} finished. Pawn: {researcher?.Name}");
        }
    }
}
