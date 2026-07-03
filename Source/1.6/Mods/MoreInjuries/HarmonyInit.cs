using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition.MoreInjuries
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            if (!RimpsycheDispositionSettings.useFightorFlight)
            {
                Log.Message("[Rimpsyche - Disposition] Not using Fight or Flight. Skip patching More Injuries");
                return;
            }
            var harmony = new Harmony("rimworld.mod.Maux.RimPsyche.Disposition.MoreInjuries");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                Log.Message("[Rimpsyche - Disposition] Patched More Injuries");
            }
            catch (Exception e)
            {
                Log.Error($"[Rimpsyche - Disposition] More Injuries patch failed: {e}");
            }
        }
    }
}
