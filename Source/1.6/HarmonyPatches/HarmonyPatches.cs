using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rimworld.mod.Maux.RimPsyche.Disposition");


            if (ModsConfig.IsActive("VanillaExpanded.VanillaTraitsExpanded"))
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


            harmony.PatchAllUncategorized(Assembly.GetExecutingAssembly());
        }
    }
}
