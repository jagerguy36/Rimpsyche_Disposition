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
            harmony.PatchAllUncategorized(Assembly.GetExecutingAssembly());
        }
    }
}
