using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Need_Mood), "NeedInterval")]
    public static class Need_Mood_NeedInterval
    {
        public static void Postfix(Pawn ___pawn)
        {
            if (___pawn.Spawned && ___pawn.Faction?.IsPlayer==true)
            {
                var compPsyche = ___pawn.compPsyche();
                if (compPsyche?.Enabled == true && ___pawn.DevelopmentalStage == DevelopmentalStage.Adult)
                {
                    if (compPsyche.ShameThoughts.Count > 0 && ShameUtil.CanFeelShame(___pawn))
                    {
                        foreach ((var def, var count) in compPsyche.ShameThoughts)
                        {
                            if (!ThoughtUtility.ThoughtNullified(___pawn, def)) //Found at least one active shame thought
                            {
                                //Sight capacity consideration requires frequent calls to ___pawn.health.capacities.GetLevel(), and only affect the dist from 13->6 at most.
                                if (ShameUtil.BeingSeen(___pawn)) //Being watched: gain shame
                                {
                                    bool overwhelm = compPsyche.GainShame();
                                    if (compPsyche.tickOverwhelmed > 0)
                                    {
                                        //If they are at the hiding destination and is still being seen, give job again
                                        if (___pawn.jobs.curJob.targetA.Cell == ___pawn.Position)
                                        {
                                            ShameUtil.TryGiveFleeInShameJob(___pawn, true);
                                        }
                                    }
                                    else if (overwhelm)
                                    {
                                        //If pawn can't start fleeing job, their shame will still stay at 1 as long as they are being seen and keep on trying to give job.
                                        ShameUtil.TryGiveFleeInShameJob(___pawn);
                                    }
                                }
                                else //Being unwatched: lose shaem
                                {
                                    compPsyche.LoseShame();
                                }
                                return;
                            }
                            //If there is no active shame thought it will pass through out of the foreach loop.
                        }
                    }
                    //Lose shame if it reached here (no active shame thoughts or can't feel shame (mentalstate/in bed /asleep))
                    compPsyche.LoseShame();
                }
            }

        }
    }
}