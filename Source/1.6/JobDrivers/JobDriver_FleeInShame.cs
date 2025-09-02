using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{

    //Set the last overwhelmed tick to current+alpha.\
    //Also add produced thought to naked memories, naked multiplier should implement shame too.
    public class JobDriver_FleeInShame: JobDriver
    {
        public override string GetReport()
        {
            if (pawn.CurJob == job && pawn.Position == job.GetTarget(TargetIndex.A).Cell)
            {
                return "ReportCowering".Translate();
            }
            return base.GetReport();
        }
        
        protected override IEnumerable<Toil> MakeNewToils()
        {
            var compPsyche = pawn.compPsyche();
            this.AddEndCondition(() => (compPsyche.Shame <= 0 ? JobCondition.Succeeded : JobCondition.Ongoing));
            Toil gotoToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            gotoToil.socialMode = RandomSocialMode.Off;
            gotoToil.FailOn(() => pawn.Downed);
            yield return gotoToil;

            Toil waitToil = ToilMaker.MakeToil("WaitWithCheck");
            waitToil.defaultCompleteMode = ToilCompleteMode.Delay;
            waitToil.defaultDuration = 4000;
            waitToil.tickAction = delegate
            {
                if (pawn.IsHashIntervalTick(150))
                {
                    if (ShameUtil.BeingSeen(pawn))
                    {
                        LocalTargetInfo newTarget;
                        if (TryFindTarget(pawn, out newTarget))
                        {
                            job.SetTarget(TargetIndex.A, newTarget);
                            waitToil.JumpToToil(gotoToil);
                        }
                    }
                    else
                    {
                        compPsyche.LoseShame();
                    }
                }
            };
            wiatToil.socialMode = RandomSocialMode.Off;

            yield return waitToil;
        }

        public override void Cleanup(JobCondition condition)
        {
            base.Cleanup(condition);
            Log.Message("Ended Flee");
        }
    }
}
