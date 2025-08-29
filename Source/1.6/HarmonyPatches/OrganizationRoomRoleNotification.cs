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
            if (billGiver.def?.building?.workTableRoomRole != null && billGiver.def.building.workTableNotInRoomRoleFactor != 0f)
            {
                Room room = billGiver.GetRoom();
                if (room != null && !room.PsychologicallyOutdoors)
                {
                    var compPsyche = billDoer.compPsyche();
                    if (compPsyche?.Enabled != true) return;
                    var organization = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Organization);
                    if (room.Role != billGiver.def.building.workTableRoomRole) //Wrong room
                    {
                        Log.Message($"{billDoer.Name} wrong room modifier applied: {billGiver.def.building.workTableNotInRoomRoleFactor}");
                        compPsyche.roomRoleFactor = 1f / (1f + organization*(1f- billGiver.def.building.workTableNotInRoomRoleFactor)); //organization -1:nullify | 0:1 | 1:make worse
                        if (organization > 0.6f)
                        {
                            compPsyche.organizedMood = 0;
                        }
                        else if (organization > 0.1f)
                        {
                            compPsyche.organizedMood = 1;
                        }
                    }
                    else //Correct room
                    {
                        Log.Message($"{billDoer.Name} correct room.");
                        var correctFactor = compPsyche.Evaluate(OrganizationRightRoomWorkspeedMultiplier);
                        if (correctFactor > 1f)
                        {
                            compPsyche.roomRoleFactor = correctFactor;
                            if (organization > 0.6f)
                            {
                                compPsyche.organizedMood = 3;
                            }
                            else if (organization > 0.1f)
                            {
                                compPsyche.organizedMood = 2;
                            }
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
            },
            RimpsycheFormulaManager.FormulaIdDict
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
