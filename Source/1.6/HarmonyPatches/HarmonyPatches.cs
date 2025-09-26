using HarmonyLib;
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
        }
    }
}
