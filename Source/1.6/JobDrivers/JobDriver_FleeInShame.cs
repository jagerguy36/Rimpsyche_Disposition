using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{

    //Also add produced thought to naked memories, naked multiplier should implement shame too.
    public class JobDriver_FleeInShame: JobDriver
    {
        private const int breakTick = 8750; //Three and a half hours
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
            this.AddEndCondition(() => (compPsyche.tickOverwhelmed > breakTick ? JobCondition.Succeeded : JobCondition.Ongoing));
            this.AddFailCondition(() => (pawn.Downed));
            this.AddFinishAction((condition) =>
            {
                compPsyche.tickOverwhelmed = 0;
            });
            Toil gotoToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            gotoToil.AddPreTickAction(delegate
            {
                compPsyche.tickOverwhelmed++;
                CheckTick(compPsyche.tickOverwhelmed);
            });
            gotoToil.socialMode = RandomSocialMode.Off;
            yield return gotoToil;

            Toil waitToil = ToilMaker.MakeToil("WaitWithCheck");
            waitToil.defaultCompleteMode = ToilCompleteMode.Delay;
            waitToil.defaultDuration = 8750;
            waitToil.AddPreTickAction(delegate
            {
                compPsyche.tickOverwhelmed++;
                CheckTick(compPsyche.tickOverwhelmed);
            });
            waitToil.socialMode = RandomSocialMode.Off;

            yield return waitToil;
        }

        private void CheckTick(int tickOverwhelmed)
        {
            if (breakTick <= tickOverwhelmed)
            {
                ShameUtil.TryDoRandomShameCausedMentalBreak(pawn);
            }
        }
    }
}
