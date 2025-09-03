using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{

    //Also add produced thought to naked memories, naked multiplier should implement shame too.
    //Should transition into random mood-caused mental break after 7500 tick (three hours)
    public class JobDriver_FleeInShame: JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

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
            this.AddEndCondition(() => (compPsyche.shame <= 0 ? JobCondition.Succeeded : JobCondition.Ongoing));
            this.AddFinishAction((condition) =>
            {
                Log.Message($"FleeInShame job ended for {pawn} with condition: {condition}");
                compPsyche.isOverwhelmed = false;
            });
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
                        LocalTargetInfo newTarget = ShameUtil.FindHideInShameLocation(pawn);
                        if (newTarget != TargetLocA)
                        {
                            job.SetTarget(TargetIndex.A, newTarget);
                            Log.Message($"New hide location: {newTarget}");
                            JumpToToil(gotoToil);
                        }
                    }
                    else
                    {
                        compPsyche.LoseShame();
                    }
                }
            };
            waitToil.socialMode = RandomSocialMode.Off;

            yield return waitToil;
        }
    }
}
