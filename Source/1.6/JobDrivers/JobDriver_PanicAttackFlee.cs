using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{

    //Also add produced thought to naked memories, naked multiplier should implement shame too.
    public class JobDriver_PanicAttackFlee : JobDriver
    {
        private int ticks = 0; //Two hours
        private Vector3 pawnNudge = Vector3.zero;
        public override Vector3 ForcedBodyOffset
        {
            get
            {
                return pawnNudge;
            }
        }
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        public override string GetReport()
        {
            if (pawn.CurJob == job && pawn.Position == job.GetTarget(TargetIndex.A).Cell)
            {
                return "ReportPanicAttack".Translate();
            }
            return base.GetReport();
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.AddEndCondition(() => (pawn.mindState.mentalStateHandler.CurStateDef!= DefOfDisposition.Rimpsyche_PanicAttack ? JobCondition.Succeeded : JobCondition.Ongoing));
            this.AddFailCondition(() => (pawn.Downed));
            Toil gotoToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            gotoToil.AddPreTickAction(delegate
            {
                ticks++;
                if (pawn.IsHashIntervalTick(75))
                {
                    FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, DefOfDisposition.RimpsycheMote_PanicAttack);
                }
            });
            yield return gotoToil;

            Toil waitToil = ToilMaker.MakeToil("WaitWithCheck");
            waitToil.defaultCompleteMode = ToilCompleteMode.Delay;
            waitToil.defaultDuration = 8750;
            waitToil.AddPreTickAction(delegate
            {
                ticks++;
            });
            waitToil.tickAction = delegate
            {
                if (pawn.IsHashIntervalTick(75))
                {
                    if (RimpsycheDispositionSettings.showPanicMote)
                    {
                        FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, DefOfDisposition.RimpsycheMote_PanicAttack);
                    }
                    if (FightorFlightUtil.DangerousToBeAt(pawn, pawn.Position))
                    {
                        LocalTargetInfo newTarget = FightorFlightUtil.FindHideInFearLocation(pawn);
                        if (newTarget != TargetLocA)
                        {
                            job.SetTarget(TargetIndex.A, newTarget);
                            JumpToToil(gotoToil);
                        }
                    }
                }
                float xJitter = (Rand.RangeSeeded(-0.03f, 0.03f, ticks));
                Vector3 JitterVector = IntVec3.West.RotatedBy(pawn.Rotation).ToVector3() * xJitter;
                pawnNudge = JitterVector;
            };
            waitToil.socialMode = RandomSocialMode.Off;

            yield return waitToil;
        }

        public override void Notify_DamageTaken(DamageInfo dinfo)
        {
            base.Notify_DamageTaken(dinfo);
            if (pawn.Position == TargetLocA)
            {
                EndJobWith(JobCondition.InterruptForced);
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticks, "ticks", 0);
            Scribe_Values.Look(ref pawnNudge, "pawnNudge", Vector3.zero);
        }
    }
}
