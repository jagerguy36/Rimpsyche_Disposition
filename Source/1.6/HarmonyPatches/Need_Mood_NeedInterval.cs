using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
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
                if (compPsyche?.Enabled == true)
                {
                    if (compPsyche.ShameThoughts.Count > 0)
                    {
                        foreach ((var def, var count) in compPsyche.ShameThoughts)
                        {
                            if (!ThoughtUtility.ThoughtNullified(___pawn, def))
                            {
                                //Log.Message($"{___pawn.Name} has shame thoguhts");
                                //Adjust the interval based on shame
                                if (compPsyche.lastOverwhelmedTick + 1000 < Find.TickManager.TicksGame)
                                {
                                    if (ShameUtil.BeingSeen(___pawn))
                                    {
                                        Log.Message($"{___pawn.Name} BeingSeen");
                                        //bool shouldRun = compPsyche.AddShame(___pawn);
                                        compPsyche.lastOverwhelmedTick = Find.TickManager.TicksGame + 1000;
                                        if (true)
                                        {
                                            PlayLogEntry_Interaction playLogEntry = new PlayLogEntry_Interaction(DefOfDisposition.Rimpsyche_Shamed, ___pawn, ___pawn, null);
                                            Find.PlayLog.Add(playLogEntry);
                                            var fleeDest = ShameUtil.FindHideInShameLocation(___pawn);
                                            Log.Message($"Location at: {fleeDest}");
                                            var runawayjob = new Job(DefOfDisposition.RimPsyche_FleeInShame, fleeDest);
                                            runawayjob.mote = MoteMaker.MakeThoughtBubble(___pawn, "Things/Mote/SpeechSymbols/Ashamed", maintain: true);
                                            ___pawn.jobs.StartJob(runawayjob, JobCondition.InterruptForced, null, false, true, null);
                                        }

                                        //Todo: Make new jobdriver that 1) go to the target 2) See if that place might be seen--> go somewhere else. 3) Cower at the place
                                        //Set the last overwhelmed tick to current+alpha.
                                        //While current < overwhelmedTick, they should flee and cower. When the overwhelmedTick < current, the fleeing stops.
                                        //Also add produced thought to naked memories
                                    }
                                }
                                break;
                            }
                        }
                        
                    }
                }
            }

        }
    }
}