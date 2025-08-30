using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("SenseOfProgress")]
    [HarmonyPatch(typeof(RecordsUtility), nameof(RecordsUtility.Notify_PawnKilled))]
    public static class RecordsUtility_Notify_PawnKilled
    {
        private static void Prefix(Pawn killed, Pawn killer)
        {
            if (killer?.Faction?.IsPlayer == true && killed.HostileTo(killer))
            {
                Log.Message("Colonist Killed enemy");
                var compPsyche = killer.compPsyche();
                if (compPsyche?.Enabled == true)
                {
                    float score = compPsyche.Evaluate(AmbitionKilledProgress);
                    if(score >=0) compPsyche.ProgressMade(score, causeIndex: 3, reason: "RP_EmenySlain".Translate());
                }
            }
        }

        public static RimpsycheFormula AmbitionKilledProgress = new(
            "AmbitionKilledProgress",
            (tracker) =>
            {
                float mult = 0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Aggressiveness);
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
