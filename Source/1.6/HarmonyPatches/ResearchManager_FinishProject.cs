using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.FinishProject))]
    public static class ResearchManager_FinishProject
    {
        private static void Postfix(ResearchProjectDef proj, Pawn researcher)
        {
            if (researcher != null)
            {
                Log.Message($"project {proj.defName} w/ cost: {proj.Cost} finished. Pawn: {researcher?.Name}");
                foreach (Pawn p in researcher.MapHeld.mapPawns.FreeColonistsSpawned)
                {
                    var compPsyche = p.compPsyche();
                    if (compPsyche?.Enabled == true)
                    {
                        Log.Message($"{p.Name} prospect: {proj.Cost / compPsyche.Evaluate(AmbitionResearchBaseTick)}");
                        compPsyche.ProgressMade(proj.Cost / compPsyche.Evaluate(AmbitionResearchBaseTick), 3);
                    }

                }

            }

        }

        public static RimpsycheFormula AmbitionResearchBaseTick = new(
            "AmbitionResearchBaseTick",
            (tracker) =>
            {
                float mult = 500f * (0.2f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition) + 1f);
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
