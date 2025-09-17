using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public class JobGivers_PanicAttack : ThinkNode_JobGiver//From Flee all pawns
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            Region region = pawn.GetRegion();
            if (region == null)
            {
                return null;
            }
            IntVec3 fleeDest = FightorFlightUtil.FindHideInFearLocation(pawn);
            Log.Message($"New location: {fleeDest}");
            if (fleeDest.IsValid)
            {
                Job job = JobMaker.MakeJob(DefOfDisposition.RimPsyche_PanicAttackFlee, fleeDest);
                return job;
            }
            return null;
        }
    }
}