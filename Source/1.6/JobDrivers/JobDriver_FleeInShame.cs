using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{

    //Also add produced thought to naked memories, naked multiplier should implement shame too.
    public class JobDriver_FleeInShame: JobDriver
    {
        private int ticksLeft = 8750; //Three and a half hours
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        public override string GetReport()
        {
            if (pawn.CurJob == job && pawn.Position == job.GetTarget(TargetIndex.A).Cell)
            {
                return "ReportHidingInShame".Translate();
            }
            return base.GetReport();
        }
        
        protected override IEnumerable<Toil> MakeNewToils()
        {
            var compPsyche = pawn.compPsyche();
            this.AddEndCondition(() => (compPsyche.shame <= 0 ? JobCondition.Succeeded : JobCondition.Ongoing));
            this.AddFailCondition(pawn.Downed)
            this.AddFinishAction((condition) =>
            {
                Log.Message($"FleeInShame job ended for {pawn} with condition: {condition}");
                compPsyche.isOverwhelmed = false;
            });
            Toil gotoToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            gotoToil.AddPreTickAction(delegate
            {
                ticksLeft--;
                CheckTick()
            });
            gotoToil.socialMode = RandomSocialMode.Off;
            yield return gotoToil;

            Toil waitToil = ToilMaker.MakeToil("WaitWithCheck");
            waitToil.defaultCompleteMode = ToilCompleteMode.Delay;
            waitToil.defaultDuration = 8750;
            waitToil.AddPreTickAction(delegate
            {
                ticksLeft--;
                CheckTick()
            });
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

        private void CheckTick()
        {
            if (ticksLeft <= 0)
            {
                Log.Message($"{pawn} could not recover from shame, forcing mental break.");
                if (ShameUtil.TryDoRandomShameCausedMentalBreak(pawn))
                {
                    EndJobWith(JobCondition.InterruptForced);
                }
                else
                {
                    Log.Message($"{pawn} couldn't go into mental break.");
                    EndJobWith(JobCondition.InterruptForced);
                }
                
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksLeft, "ticksLeft", 0);
        }
    }
}
