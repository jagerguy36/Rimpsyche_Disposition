using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("SenseOfProgress")]
    [HarmonyPatch(typeof(RecruitUtility), nameof(RecruitUtility.Recruit))]
    public static class RecruitUtility_Recruit
    {
        private static void Prefix(Pawn pawn, Faction faction, Pawn recruiter)
        {
            if(pawn.Faction?.IsPlayer != true)
            {
                if (pawn.RaceProps.Humanlike)
                {
                    Log.Message("New colonist recruited");
                    foreach (Pawn colonist in pawn.MapHeld.mapPawns.FreeColonistsSpawned)
                    {
                        var compPsyche = colonist.compPsyche();
                        if (compPsyche?.Enabled == true)
                        {
                            //2~3(Ambition) * 0.5~2(social)
                            compPsyche.ProgressMade(compPsyche.Evaluate(AmbitionRecruitProgress), causeIndex: 3, reason: "RP_Recruited".Translate(pawn.LabelShort));
                        }
                    }
                }
                else
                {
                    Log.Message($"New animal tamed (market value: {pawn.def.BaseMarketValue}");
                    if (pawn.def.BaseMarketValue >= 300)
                    {
                        float marketvalueMult = 2f + (200f / (200f - pawn.def.BaseMarketValue));
                        foreach (Pawn colonist in pawn.MapHeld.mapPawns.FreeColonistsSpawned)
                        {
                            var compPsyche = colonist.compPsyche();
                            if (compPsyche?.Enabled == true)
                            {
                                //0~2(market value) * 1~2 (ambition)
                                compPsyche.ProgressMade(marketvalueMult * compPsyche.Evaluate(AmbitionRecruitProgress), causeIndex: 3, reason: "RP_Tamed".Translate(pawn.LabelShort));
                            }
                        }
                    }
                }
            }
        }

        public static RimpsycheFormula AmbitionRecruitProgress = new(
            "AmbitionRecruitProgress",
            (tracker) =>
            {
                float sociabilityMult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Sociability, 2f);
                float mult = 2.5f + (0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                return mult * sociabilityMult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );

        public static RimpsycheFormula AmbitionTameProgress = new(
            "AmbitionTameProgress",
            (tracker) =>
            {
                float mult = 1.5f + (0.5f * tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Ambition));
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
