using HarmonyLib;
using RimWorld;
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
            Unpatcher.UnpatchModContents(harmony);


            //RPD harmony patches
            harmony.PatchAllUncategorized(Assembly.GetExecutingAssembly());
            if (RimpsycheDispositionSettings.usePerformanceModeThought)
            {
                harmony.PatchCategory("PerformanceModeThought");
            }
            else
            {
                harmony.PatchCategory("PrecisionModeThought");
            }
            if (RimpsycheDispositionSettings.useExperimentation)
            {
                harmony.PatchCategory("Experimentation");
            }
            if (RimpsycheDispositionSettings.useSenseOfProgress)
            {
                harmony.PatchCategory("SenseOfProgress");
            }
            if (RimpsycheDispositionSettings.useResilientSpirit)
            {
                harmony.PatchCategory("ResilientSpirit");
            }
            if (RimpsycheDispositionSettings.useHideInShame)
            {
                harmony.PatchCategory("HideInShame");
            }
            if (RimpsycheDispositionSettings.useFightorFlight)
            {
                harmony.PatchCategory("FightorFlight");
            }
            if (RimpsycheDispositionSettings.useIndividualJoychance)
            {
                harmony.PatchCategory("IndividualJoyChance");
                var MeditateDef = DefDatabase<JoyGiverDef>.GetNamed("Meditate", false);
                if (MeditateDef != null)
                    MeditateDef.pctPawnsEverDo = 1f;
                else
                    Log.Error("[Rimpsyche - Disposition] Failed to patch Meditate JoyGiver.");
            }

            //Mod Supports
            if (ModsConfig.IsActive("slimesenpai.endlessgrowth"))
            {
                harmony.PatchCategory("EndlessGrowth");
            }
        }
    }
}
