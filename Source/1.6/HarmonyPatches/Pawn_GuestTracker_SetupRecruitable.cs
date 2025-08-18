using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Pawn_GuestTracker), nameof(Pawn_GuestTracker.SetupRecruitable))]
    public static class Pawn_GuestTracker_SetupRecruitable
    {
        static bool Prefix(ref bool ___recruitable, Pawn ___pawn)
        {
            if (___pawn?.compPsyche() is not { } compPsyche)
                return true;
            var loyalty = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            //var newNonrecruitableChance = 2f * loyalty * HealthTuning.NonRecruitableChanceOverPopulationIntentCurve.Evaluate(StorytellerUtilityPopulation.PopulationIntent);
            var newNonrecruitableChance = (loyalty - 1f) * (StorytellerUtilityPopulation.PopulationIntent + 1.5f) + 1f;
            ___recruitable = Rand.Value >= newNonrecruitableChance;
            //Log.Message($"{___pawn.Name}: loyalty: {loyalty} | chance: {newNonrecruitableChance} | result: {___recruitable}");
            return false;
        }
    }

    [HarmonyPatch(typeof(Pawn_GuestTracker), "SetGuestStatus")]
    public static class Pawn_GuestTracker_SetGuestStatus
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            MethodInfo originalMethod = AccessTools.Method(typeof(FloatRange), "get_RandomInRange");
            MethodInfo customMethod = AccessTools.Method(typeof(RimPsycheGuestTracker), "PsycheResistanceRange");
            bool found = false;
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call && codes[i].operand as MethodInfo == originalMethod && !found)
                {
                    if (codes[i-1].opcode == OpCodes.Ldloca_S && codes[i-2].opcode == OpCodes.Stloc_S && codes[i-3].opcode == OpCodes.Call && codes[i-4].opcode == OpCodes.Ldflda && codes[i-5].opcode == OpCodes.Ldfld)
                    {
                        found = true;
                        codes[i].operand = customMethod;
                        codes.RemoveAt(i - 1);
                        codes.RemoveAt(i - 2);
                        codes.RemoveAt(i - 3);
                        codes.RemoveAt(i - 4);
                        codes.RemoveAt(i - 5);
                        break;
                    }
                }
            }
            if (!found)
            {
                Log.Warning("[Rimpsyche] Could not find the target instruction for Pawn_GuestTracker.SetGuestStatus transpiler patch.");
            }
            return codes;
        }
    }

    public static class RimPsycheGuestTracker
    {
        public static float PsycheResistanceRange(Pawn pawn)
        {
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return pawn.kindDef.initialResistanceRange.Value.RandomInRange;
            }
            var loayalty = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            var range = pawn.kindDef.initialResistanceRange.Value;
            var resistance = Mathf.Lerp(range.min, range.max, (loayalty+1)*0.5f);
            //Log.Message($"custom range called on pawn {pawn.Name}. loyalty: {loayalty}. original range: {range.min} | {range.max}. resistance: {resistance}");
            return resistance;
        }
    }
}
