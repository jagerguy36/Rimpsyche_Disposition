using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            var harmony = new Harmony("Harmony_RimpsycheIdelogy");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Log.Message($"[Rimpsyche Disposition] Ideology patched");
            }
            catch (Exception e)
            {
                Log.Error($"[Rimpsyche] Ideology patch failed: {e}");
            }
        }
    }
}
