using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(Bill), "Notify_BillWorkStarted")]
    public static class Bill_Notify_BillWorkStarted_Patch
    {
        static void Postfix(Pawn billDoer)
        {
            Log.Message($"{billDoer.Name} started bill");
            Job curJob = billDoer.jobs.curJob;
            var billGiver = curJob.targetA.Thing;
            if (billGiver == null)
            {
                return;
            }
            if (billGiver.def?.building?.workTableRoomRole != null)
            {
                Room room = billGiver.GetRoom();
                if (room != null && !room.PsychologicallyOutdoors)
                {
                    var compPsyche = billDoer.compPsyche();
                    if (room.Role != billGiver.def.building.workTableRoomRole)
                    {
                        Log.Message($"{billDoer.Name} wrong room modifier applied: {billGiver.def.building.workTableNotInRoomRoleFactor}");
                        var organization = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
                        compPsyche.roomRoleFactor = 1f / (1f + organization*(1f- billGiver.def.building.workTableNotInRoomRoleFactor));
                        if (organization > 0f)
                        {
                            compPsyche.organizedMood = 0;
                        }
                    }
                    else
                    {
                        Log.Message($"{billDoer.Name} correct room.");
                        var correctFactor = compPsyche.Personality.Evaluate(OrganizationRightRoomWorkspeedMultiplier);
                        if (correctFactor > 1f)
                        {
                            compPsyche.roomRoleFactor = correctFactor;
                            compPsyche.organizedMood = 1;
                        }
                    }
                }
            }
        }


        public static RimpsycheFormula OrganizationRightRoomWorkspeedMultiplier = new(
            "OrganizationRightRoomWorkspeedMultiplier",
            (tracker) =>
            {
                float organization = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Organization) * 0.1f;
                return organization;
            }
        );
    }


    [HarmonyPatch(typeof(Toils_Recipe), "DoRecipeWork")]
    public static class Toils_Recipe_DoRecipeWork_Patch
    {
        static void Postfix(Toil __result)
        {
            __result.AddFinishAction(delegate
            {
                NotifyToilFinished(__result.actor);
            });
        }

        public static void NotifyToilFinished(Pawn pawn)
        {
            Log.Message($"{pawn.Name} finished toil");
            var compPsyche = pawn.compPsyche();
            if (compPsyche != null)
            {
                compPsyche.roomRoleFactor = 1f;
                compPsyche.organizedMood = -1;
            }
        }
    }
}
