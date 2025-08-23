using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class PsycheExtension
    {
        const int dayTick = 60000;
        const int maxDay = 10;
        const int maxDayTicks = 600000;
        public static void ProgressMade(this CompPsyche compPsyche, float days, int causeIndex = 1)
        {
            if (days >= 0f)
            {
                int prospect = Mathf.Min(maxDayTicks, (int)(days * dayTick)) + Find.TickManager.TicksGame;
                if (compPsyche.progressTick <= prospect)
                {
                    compPsyche.progressTick = prospect;
                    compPsyche.progressLastCauseIndex = causeIndex;
                }
                
            }
        }
    }
}
