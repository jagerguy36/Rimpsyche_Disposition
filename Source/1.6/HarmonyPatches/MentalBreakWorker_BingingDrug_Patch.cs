using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(MentalBreakWorker_BingingDrug), nameof(MentalBreakWorker_BingingDrug.CommonalityFor))]
    public static class MentalBreakWorker_BingingDrug_Patch
    {
        private static void Postfix(ref float __result, Pawn pawn)
        {
            var compPsyche = pawn.compPsyche();
            float multiplier = compPsyche.Personality.Evaluate(DisciplineBingingMultiplier);
            __result = multiplier * __result;
        }

        public static RimpsycheFormula DisciplineBingingMultiplier = new(
            "DisciplineBingingMultiplier",
            (tracker) =>
            {
                float discipline = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline);
                return 3f/( discipline -3f) +2f;
            }
        );
    }
}
