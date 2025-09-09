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

        public static bool GainShame(this CompPsyche compPsyche)
        {
            var shame = compPsyche.shame;
            var shameAmount = compPsyche.Evaluate(FormulaDB.ModestShameGain);
            if (shameAmount > 0f)
            {
                if (shame + shameAmount >= 1f)
                {
                    compPsyche.shame = 1f;
                    return true;
                }
                else
                {
                    compPsyche.shame += shameAmount;
                }
            }
            return false;

        }
        public static void LoseShame(this CompPsyche compPsyche)
        {
            var shame = compPsyche.shame;
            if (shame > 0f)
            {
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
}
