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
                if (compPsyche?.Enabled == true && compPsyche.isAdult)//TODO: use developmental stage instead
                {
                    if (compPsyche.ShameThoughts.Count > 0 && compPsyche.CanFeelShame() && ___pawn.Awake())
                    {
                        foreach ((var def, var count) in compPsyche.ShameThoughts)
                        {
                            if (!ThoughtUtility.ThoughtNullified(___pawn, def))
                            {
                                //Log.Message($"{___pawn.Name} has shame thoguhts");
                                if (ShameUtil.BeingSeen(___pawn))
                                {
                                    Log.Message($"{___pawn.Name} BeingSeen");
                                    bool overwhelmed = commpPsyche.GainShame();
                                    if (overwhelmed)
                                    {
                                        compPsyche.overhwelmRecoveryTick = Find.TickManager.TicksGame + 1000;
                                        PlayLogEntry_Interaction playLogEntry = new PlayLogEntry_Interaction(DefOfDisposition.Rimpsyche_Shamed, ___pawn, ___pawn, null);
                                        Find.PlayLog.Add(playLogEntry);
                                        var fleeDest = ShameUtil.FindHideInShameLocation(___pawn);
                                        Log.Message($"Location at: {fleeDest}");
                                        var runawayjob = new Job(DefOfDisposition.RimPsyche_FleeInShame, fleeDest);
                                        runawayjob.mote = MoteMaker.MakeThoughtBubble(___pawn, "Things/Mote/SpeechSymbols/Ashamed", maintain: true);
                                        ___pawn.jobs.StartJob(runawayjob, JobCondition.InterruptForced, null, false, true, null);
                                    }
                                }
                                return;
                            }
                        }
                    }
                    //Lose shame if it reached here (recovering or asleep or no active shame thoughts)
                    commpPsyche.LoseShame();
                }
            }

        }
    }
}