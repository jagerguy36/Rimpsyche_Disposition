using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class Hediff_AdrenalineRush: Hediff_High
    {
        public override float Severity
        {
            get
            {
                return severityInt;
            }
            set
            {
                int curStageIndex = CurStageIndex;
                severityInt = Mathf.Clamp(value, def.minSeverity, def.maxSeverity);
                if (CurStageIndex != curStageIndex)
                {
                    if (curStageIndex < CurStageIndex && 0 < CurStageIndex)
                    {
                        MoteBubble mote = (MoteBubble)ThingMaker.MakeThing(DefOfDisposition.RimpsycheMote_AdrenalineRush, null);
                        mote.Attach(pawn);
                        GenSpawn.Spawn(mote, pawn.Position, pawn.Map);
                    }
                    OnStageIndexChanged(CurStageIndex);
                }
                if ((CurStageIndex != curStageIndex) && pawn.health.hediffSet.hediffs.Contains(this))
                {
                    pawn.health.Notify_HediffChanged(this);
                    if (!pawn.Dead && pawn.needs.mood != null)
                    {
                        pawn.needs.mood.thoughts.situational.Notify_SituationalThoughtsDirty();
                    }
                }
            }
        }
    }
}
