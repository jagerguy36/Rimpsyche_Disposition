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
        static bool Prefix(Pawn ___pawn, bool ___neverFleeIndividual, DamageInfo dinfo)
        {
            if (___pawn.Faction == Faction.OfPlayer || RimpsycheDispositionSettings.enemyFightorFlight)
            {
                if (___pawn.Spawned && ___pawn.MentalStateDef == null && !___pawn.Downed && dinfo.Def.ExternalViolenceFor(___pawn) && ___pawn.RaceProps.Humanlike)
                {
                    var compPsyche = ___pawn.compPsyche();
                    if (compPsyche != null)
                    {
                        //Flight
                        if (___pawn.mindState.canFleeIndividual && !___neverFleeIndividual)
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
                                    if (___pawn.Faction == Faction.OfPlayer)
                                    {
                                        ___pawn.mindState.mentalStateHandler.TryStartMentalState(DefOfDisposition.Rimpsyche_PanicAttack, "Psyche_PanicAttack".Translate(), forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                                    }
                                    else if (___pawn.Faction != Faction.OfPlayer && ___pawn.HostFaction == null)
                                    {
                                        ___pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee, null, forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                                    }
                                    return false;
                                }
                            }
                        }
                        //Fight
                        float gain = compPsyche.Evaluate(FormulaDB.AdrenalineGain);
                        if (gain > 0f)
                        {
                            Log.Message($"gained: {gain}");
                            HealthUtility.AdjustSeverity(___pawn, DefOfDisposition.Rimpsyche_AdrenalineRush, gain);
                            return false;
                        }
                    }
                    else
                    {
                        return true; //pawns without psyche should be treated as vanilla pawns.
                    }
                }
                else
                {
                    return false; //Preclude the same condition for the original
                }
            }
            return true; //Settings
        }
    }
}