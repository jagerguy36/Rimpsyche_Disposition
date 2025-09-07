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
            Log.Message("trying to give panic Attack job");
            Region region = pawn.GetRegion();
            if (region == null)
            {
                return null;
            }
            Log.Message("trying to find location");
            IntVec3 fleeDest = FightorFlightUtil.FindHideInFearLocation(pawn);
            Log.Message($"New location: {fleeDest}");
            if (fleeDest.IsValid)
            {
                Log.Message($"making job with new valid destination: {fleeDest}");
                Job job = JobMaker.MakeJob(DefOfDisposition.RimPsyche_PanicAttackFlee, fleeDest);
                return job;
            }
            Log.Message("end with null");
            return null;
        }
    }
}