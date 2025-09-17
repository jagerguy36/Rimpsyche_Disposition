using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("SenseOfProgress")]
    [HarmonyPatch(typeof(QuestManager), nameof(QuestManager.Notify_PawnBorn))]
    public static class QuestManager_Notify_PawnBorn
    {
        private static void Prefix(Thing baby)
        {
            Pawn pawn = baby as Pawn;
            if(pawn != null)
            {
                if(pawn.Faction.IsPlayer)
                {
                    foreach (Pawn colonist in pawn.MapHeld.mapPawns.FreeColonistsSpawned)
                    {
                        var compPsyche = colonist.compPsyche();
                        if (compPsyche?.Enabled == true)
                        {
                            //2~3(Ambition) * 0.5~2(social)
                            compPsyche.ProgressMade(compPsyche.Evaluate(AmbitionBabyBornProgress), causeIndex: 3, reason: "RP_BabyBorn".Translate());
                        }
                    }
                }
            }
        }

        public static RimpsycheFormula AmbitionBabyBornProgress = new(
            "AmbitionBabyBornProgress",
            (tracker) =>
            {
                float sociabilityMult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Sociability, 2f);
                float mult = 2.5f + (0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                return mult * sociabilityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
