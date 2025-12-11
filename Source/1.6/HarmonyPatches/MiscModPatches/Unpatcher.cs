using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class Unpatcher
    {
        public static void UnpatchModContents(Harmony harmony)
        {
            if (ModsConfig.IsActive("VanillaExpanded.VanillaTraitsExpanded") && RimpsycheDispositionSettings.useExperimentation)
                UnpatchVTE(harmony);
            if (ModsConfig.IsActive("mlie.syrindividuality") && RimpsycheDispositionSettings.useExperimentation)
                UnpatchIndividuality(harmony);
            if (ModsConfig.IsActive("goji.thesimstraits"))
                UnpatchSims(harmony);

        }

        public static void UnpatchVTE(Harmony harmony)
        {
            Log.Message("[Rimpsyche - Disposition] Patching VTE");
            MethodInfo VTE_GenerateQualityCreatedByPawn_Patch_fixes = typeof(QualityUtility).GetMethod("GenerateQualityCreatedByPawn", new Type[] { typeof(Pawn), typeof(SkillDef), typeof(bool) });
            if (VTE_GenerateQualityCreatedByPawn_Patch_fixes == null)
            {
                Log.Error("[Rimpsyche - Disposition] Failed to unpatch GenerateQualityCreatedByPawn of VanillaTraitsExpanded");
                return;
            }
            harmony.Unpatch(VTE_GenerateQualityCreatedByPawn_Patch_fixes, HarmonyPatchType.All, "OskarPotocki.VanillaTraitsExpanded");
            harmony.PatchCategory("VTE");
        }

        public static void UnpatchIndividuality(Harmony harmony)
        {
            Log.Message("[Rimpsyche - Disposition] Patching idividuality");
            MethodInfo Individuality_GenerateQualityCreatedByPawn_Patch_fixes = typeof(QualityUtility).GetMethod("GenerateQualityCreatedByPawn", new Type[] { typeof(Pawn), typeof(SkillDef), typeof(bool) });
            if (Individuality_GenerateQualityCreatedByPawn_Patch_fixes == null)
            {
                Log.Error("[Rimpsyche - Disposition] Failed to unpatch GenerateQualityCreatedByPawn of SYR Individuality");
                return;
            }
            harmony.Unpatch(Individuality_GenerateQualityCreatedByPawn_Patch_fixes, HarmonyPatchType.All, "Syrchalis.Rimworld.Traits");
            harmony.PatchCategory("Individuality");
        }
        public static void UnpatchSims(Harmony harmony)
        {
            Log.Message("[Rimpsyche - Disposition] Patching The Sims Traits");
            MethodInfo Pawn_InteractionsTracker_InteractionsTrackerTick = typeof(Pawn_InteractionsTracker).GetMethod("InteractionsTrackerTickInterval");
            if (Pawn_InteractionsTracker_InteractionsTrackerTick == null)
            {
                Log.Error("[Rimpsyche - Disposition] Failed to unpatch InteractionsTrackerTickInterval of The Sims Traits");
                return;
            }
            harmony.Unpatch(Pawn_InteractionsTracker_InteractionsTrackerTick, HarmonyPatchType.Prefix, "SimsTraitsMod");

            if (RimpsycheDispositionSettings.useFightorFlight)
            {
                MethodInfo TheSimsTraits_Thing_TakeDamage_Patch = typeof(Thing).GetMethod("TakeDamage");
                if (TheSimsTraits_Thing_TakeDamage_Patch == null)
                {
                    Log.Error("[Rimpsyche - Disposition] Failed to unpatch TakeDamage of The Sims Traits");
                    return;
                }
                harmony.Unpatch(TheSimsTraits_Thing_TakeDamage_Patch, HarmonyPatchType.Postfix, "SimsTraitsMod");
            }

            if (RimpsycheDispositionSettings.useExperimentation)
            {
                MethodInfo GenerateQualityCreatedByPawn_Patch_fixes = typeof(QualityUtility).GetMethod("GenerateQualityCreatedByPawn", new Type[] { typeof(Pawn), typeof(SkillDef), typeof(bool) });
                if (GenerateQualityCreatedByPawn_Patch_fixes == null)
                {
                    Log.Error("[Rimpsyche - Disposition] Failed to unpatch GenerateQualityCreatedByPawn of The Sims Traits");
                    return;
                }
                harmony.Unpatch(GenerateQualityCreatedByPawn_Patch_fixes, HarmonyPatchType.All, "SimsTraitsMod");
                harmony.PatchCategory("TheSimsTraits");
            }
        }
    }


    [StaticConstructorOnStartup]
    public static class IntegrationDatabase
    {
        public static TraitDef VTE_Perfectionist = DefDatabase<TraitDef>.GetNamedSilentFail("VTE_Perfectionist");
        public static TraitDef SYR_Perfectionist = DefDatabase<TraitDef>.GetNamedSilentFail("SYR_Perfectionist");
        public static TraitDef ST_Procrastinator = DefDatabase<TraitDef>.GetNamedSilentFail("ST_Procrastinator");

        static IntegrationDatabase()
        {

        }
    }
}
