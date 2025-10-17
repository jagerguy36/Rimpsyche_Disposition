﻿using HarmonyLib;
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
                            if (hpp <= threshold)
                            {
                                float chance = compPsyche.Evaluate(FormulaDB.FlightChance);
                                if (Rand.Chance(chance))
                                {
                                    //Vanilla logic applies to enemies
                                    if (___pawn.Faction != Faction.OfPlayer && ___pawn.HostFaction == null && ___pawn.kindDef.fleeHealthThresholdRange.max > 0f)
                                    {
                                        ___pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee, null, forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                                        return false;
                                    }

                                    //Unify into Rimpsyche Flee. ... At least for now.
                                    ___pawn.mindState.mentalStateHandler.TryStartMentalState(DefOfDisposition.Rimpsyche_PanicAttack, "RP_MentalStateReason_PanicAttack".Translate(), forced: false, forceWake: false, causedByMood: false, null, transitionSilently: false, causedByDamage: true);
                                    return false; //Mentalstate started. Should block adrenaline gain
                                }
                            }
                        }
                        //Fight
                        float adrenalineMult = compPsyche.Evaluate(FormulaDB.AdrenalineGain);
                        float dmgPercent = dinfo.Amount/___pawn.health.LethalDamageThreshold;
                        float gain = adrenalineMult * dmgPercent;
                        if (gain > 0f)
                        {
                            HealthUtility.AdjustSeverity(___pawn, DefOfDisposition.Rimpsyche_AdrenalineRush, gain);
                        }
                        return false;
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