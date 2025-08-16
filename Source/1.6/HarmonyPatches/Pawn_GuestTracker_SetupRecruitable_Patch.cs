using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Pawn_GuestTracker), nameof(Pawn_GuestTracker.SetupRecruitable))]
    public static class Pawn_GuestTracker_SetupRecruitable_Patch
    {
        static bool Prefix(ref bool ___recruitable, Pawn ___pawn)
        {
            if (___pawn?.compPsyche() is not { } compPsyche)
                return true;
            var loyalty = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            var newNonrecruitableChance = 2f * loyalty * HealthTuning.NonRecruitableChanceOverPopulationIntentCurve.Evaluate(StorytellerUtilityPopulation.PopulationIntent);
            ___recruitable = Rand.Value >= newNonrecruitableChance;
            Log.Message($"{___pawn.Name}: loyalty: {loyalty} | chance: {newNonrecruitableChance} | result: {___recruitable}");
            return false;
        }
    }
}
