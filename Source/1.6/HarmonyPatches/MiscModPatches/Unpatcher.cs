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
            {
                UnpatchVTE(harmony);
            }

            if (ModsConfig.IsActive("mlie.syrindividuality") && RimpsycheDispositionSettings.useExperimentation)
            {
                UnpatchIndividuality(harmony);
            }
        }

        public static void UnpatchVTE(Harmony harmony)
        {
            MethodInfo VTE_GenerateQualityCreatedByPawn_Patch_fixes = typeof(QualityUtility).GetMethod("GenerateQualityCreatedByPawn", new Type[] { typeof(Pawn), typeof(SkillDef), typeof(bool) });
            if (VTE_GenerateQualityCreatedByPawn_Patch_fixes == null)
            {
                Log.Error("[Rimpsyche] Failed to unpatch GenerateQualityCreatedByPawn of VanillaTraitsExpanded");
                return;
            }
            harmony.Unpatch(VTE_GenerateQualityCreatedByPawn_Patch_fixes, HarmonyPatchType.All, "OskarPotocki.VanillaTraitsExpanded");
            harmony.PatchCategory("VTE");
        }

        public static void UnpatchIndividuality(Harmony harmony)
        {
            Log.Message("harmony patching idividuality");
            MethodInfo Individuality_GenerateQualityCreatedByPawn_Patch_fixes = typeof(QualityUtility).GetMethod("GenerateQualityCreatedByPawn", new Type[] { typeof(Pawn), typeof(SkillDef), typeof(bool) });
            if (Individuality_GenerateQualityCreatedByPawn_Patch_fixes == null)
            {
                Log.Error("[Rimpsyche] Failed to unpatch GenerateQualityCreatedByPawn of SYR Individuality");
                return;
            }
            harmony.Unpatch(Individuality_GenerateQualityCreatedByPawn_Patch_fixes, HarmonyPatchType.All, "Syrchalis.Rimworld.Traits");
            harmony.PatchCategory("Individuality");
        }
    }


    [StaticConstructorOnStartup]
    public static class IntegrationDatabase
    {
        public static TraitDef VTE_Perfectionist = DefDatabase<TraitDef>.GetNamedSilentFail("VTE_Perfectionist");
        public static TraitDef SYR_Perfectionist = DefDatabase<TraitDef>.GetNamedSilentFail("SYR_Perfectionist");

        static IntegrationDatabase()
        {

        }
    }
}
