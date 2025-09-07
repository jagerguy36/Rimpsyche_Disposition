using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("FightorFlight")]
    [HarmonyPatch(typeof(MentalStateHandler), "Notify_DamageTaken")]
    public static class MentalStateHandler_Notify_DamageTaken
    {
        static bool Prefix(Pawn ___pawn, DamageInfo dinfo)
        {
            if (___pawn.Faction == Faction.OfPlayer)
            {
                if (___pawn.Spawned && ___pawn.MentalStateDef == null && !___pawn.Downed && dinfo.Def.ExternalViolenceFor(___pawn) && ___pawn.RaceProps.Humanlike)
                {
                    var compPsyche = ___pawn.compPsyche();
                    if (compPsyche != null)
                    {
                        if (___pawn.mindState.canFleeIndividual)
                        {
                            float threshold = compPsyche.Evaluate(FormulaDB.FlightThreshold);
                            float hpp = ___pawn.health.summaryHealth.SummaryHealthPercent;
                            Log.Message($"{___pawn.Name} took damage {dinfo.Amount}| threshold: {threshold} | hpp: {hpp}");
                            if (hpp <= threshold)
                            {
                                float chance = compPsyche.Evaluate(FormulaDB.FlightChance);
                                Log.Message($"chance : {chance}");
                                if (Rand.Chance(chance))
                                {
                                    ___pawn.mindState.mentalStateHandler.TryStartMentalState(DefOfDisposition.Rimpsyche_PanicAttack, "Psyche_PanicAttack".Translate(), forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                                }
                            }
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}