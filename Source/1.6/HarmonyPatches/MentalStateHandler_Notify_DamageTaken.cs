using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(MentalStateHandler), "Notify_DamageTaken")]
    public static class MentalStateHandler_Notify_DamageTaken
    {
        static bool Prefix(Pawn ___pawn, DamageInfo dinfo)
        {
            if (___pawn.Faction == Faction.OfPlayer)
            {
                if (___pawn.Spawned && ___pawn.MentalStateDef == null && !___pawn.Downed && dinfo.Def.ExternalViolenceFor(___pawn) && ___pawn.RaceProps.Humanlike && ___pawn.mindState.canFleeIndividual)
                {
                    float threshold = 0.5f;
                    float hpp = ___pawn.health.summaryHealth.SummaryHealthPercent;
                    Log.Message($"{___pawn.Name} took damage| threshold: {threshold} | hpp: {hpp}");
                    if (true)
                    {
                        ___pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee, "cowardFlee".Translate(), forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                    }
                    return false;
                }
            }
            return true;
        }
    }
}