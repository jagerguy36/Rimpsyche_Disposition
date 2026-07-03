using HarmonyLib;
using MoreInjuries;
using MoreInjuries.Defs.WellKnown;
using System;
using System.Reflection;
using UnityEngine;
using Verse;
using static System.Net.Mime.MediaTypeNames;

namespace Maux36.RimPsyche.Disposition.MoreInjuries
{
    [HarmonyPatch]
    public static class AdrenalineWorkerPatch
    {
        public static bool Prepare()
        {
            if (RimpsycheDispositionSettings.useFightorFlight)
                return true;
            return false;
        }

        static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName(
                "MoreInjuries.HealthConditions.AdrenalineRush.AdrenalineWorker");
            return AccessTools.PropertyGetter(type, "IsEnabled");
        }

        static bool Prefix(ref bool __result)
        {
            __result = false;
            return false;
        }
    }

    [HarmonyPatch(typeof(FightorFlightUtil), nameof(FightorFlightUtil.NotifyDamageTaken))]
    public static class NotifyDamageTakenPatch
    {
        static bool Prefix(Pawn pawn, float damage, float psycheAdrenalineValue)
        {
            if (damage <= 0f) return false;
            if (MoreInjuriesMod.Settings.EnableAdrenaline)
            {
                if (psycheAdrenalineValue > 0f)
                {
                    if (!pawn.health.hediffSet.TryGetHediff(KnownHediffDefOf.AdrenalineRush, out Hediff adrenalineRush))
                    {
                        adrenalineRush = HediffMaker.MakeHediff(KnownHediffDefOf.AdrenalineRush, pawn);
                        adrenalineRush.Severity = 0;
                        pawn.health.AddHediff(adrenalineRush);
                    }
                    float dmgPercent = damage / pawn.health.LethalDamageThreshold;
                    float severity = psycheAdrenalineValue * dmgPercent;
                    float newSeverity = adrenalineRush.Severity + severity;
                    adrenalineRush.Severity = Mathf.Min(newSeverity, 1.8f);
                }
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(FightorFlightUtil), nameof(FightorFlightUtil.ApplyAdrenaline))]
    public static class ApplyAdrenalinePatch
    {
        static bool Prefix(Pawn pawn, float gain)
        {
            if (gain <= 0) return false;
            if (MoreInjuriesMod.Settings.EnableAdrenaline)
            {
                if (!pawn.health.hediffSet.TryGetHediff(KnownHediffDefOf.AdrenalineRush, out Hediff adrenalineRush))
                {
                    adrenalineRush = HediffMaker.MakeHediff(KnownHediffDefOf.AdrenalineRush, pawn);
                    adrenalineRush.Severity = 0;
                    pawn.health.AddHediff(adrenalineRush);
                }
                float newSeverity = adrenalineRush.Severity + gain;
                adrenalineRush.Severity = Mathf.Min(newSeverity, 1f);
                return false;
            }
            return true;
        }
    }
}
