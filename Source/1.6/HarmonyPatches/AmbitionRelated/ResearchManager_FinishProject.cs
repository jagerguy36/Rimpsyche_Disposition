using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("SenseOfProgress")]
    [HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.FinishProject))]
    public static class ResearchManager_FinishProject
    {
        private static void Postfix(ResearchProjectDef proj, Pawn researcher)
        {
            if (researcher != null)
            {
                foreach (Pawn p in researcher.MapHeld.mapPawns.FreeColonistsSpawned)
                {
                    var compPsyche = p.compPsyche();
                    if (compPsyche?.Enabled == true)
                    {
                        //base tick: 400(-1)~[500]~600(1). Progress 1 per base tick
                        compPsyche.ProgressMade(proj.Cost / compPsyche.Evaluate(AmbitionResearchBaseTick), 3, "RP_Researched".Translate(proj.LabelCap));
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
