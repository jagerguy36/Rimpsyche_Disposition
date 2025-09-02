using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class PsycheExtension
    {
        const int dayTick = 60000;
        const int maxDay = 10;
        const int maxDayTicks = 600000;
        public static void ProgressMade(this CompPsyche compPsyche, float days, int causeIndex = 1, string reason = null)
        {
            if (days >= 0f)
            {
                int prospect = Mathf.Min(maxDayTicks, (int)(days * dayTick)) + Find.TickManager.TicksGame;
                if (compPsyche.progressTick <= prospect)
                {
                    compPsyche.progressTick = prospect;
                    compPsyche.progressLastCauseIndex = causeIndex;
                    if (reason != null)
                    {
                        Log.Message($"reason: {reason}");
                    }
                    compPsyche.progressLastCause = reason;
                }
                
            }
        }

        public static bool CanFeelShame(this CompPsyche compPsyche)
        {
            if (compPsyche.parentPawn.InMentalState)
            {
                return false;
            }
            if (compPsyche.overhwelmRecoveryTick > Find.TickManager.TicksGame)
            {
                return false;
            }
            return true;
        }
        public static bool GainShame(this CompPsyche compPsyche)
        {
            var shame = compPsyche.shame;
            var shameAmount = compPsyche.Evaluate(FormulaDB.ModestShameGain);
            if (shameAmount > 0f)
            {
                if (shameAmount + shame > 1f)
                {
                    Log.Message("Shame gained. it is now : 1. Flee!");
                    compPsyche.shame = 1f;
                    return true;
                }
                else
                {
                    compPsyche.shame += shameAmount;
                    Log.Message($"Shame gained. it is now : {compPsyche.shame}");
                }
            }
            return false;

        }
        public static void LoseShame(this CompPsyche compPsyche)
        {
            var shame = compPsyche.shame;
            var shameAmount = compPsyche.Evaluate(FormulaDB.ModestShameLose);
            if (shame - shameAmount >= 0f)
            {
                compPsyche.shame -= shameAmount;
            }
            else
            {
                compPsyche.shame = 0f;
            }
        }
    }
}
