using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public static class FightorFlightUtil
    {
        private static List<Thing> tmpThreats = new List<Thing>();
        private static HashSet<int> tmpInvIds = new HashSet<int> { };
        private const int threatDistance = 25; //"Medium" range tiles
        private const int threatDistSquared = threatDistance * threatDistance;
        private const int runDistanceMax = 23;
        private const int potentialThreatDistance = threatDistance + runDistanceMax;
        private const int potentialThreatDistSquared = potentialThreatDistance * potentialThreatDistance;

        public static bool ShouldInduceFear(Thing t, Pawn pawn, int distSquared)
        {
            if (t.def.alwaysFlee)
            {
                return true;
            }
            if (t.Position.DistanceToSquared(pawn.Position) > distSquared)
            {
                return false;
            }
            if (!t.HostileTo(pawn))
            {
                return false;
            }
            if (t is not IAttackTarget attackTarget || attackTarget.ThreatDisabled(pawn) || t is not IAttackTargetSearcher)
            {
                return false;
            }
            return true;
        }

        public static bool DangerousToBeAt(Pawn pawn, IntVec3 location, int threatDistSquared=threatDistSquared)
        {
            tmpInvIds.Clear();
            tmpInvIds.Add(pawn.thingIDNumber);
            bool foundThreat = false;
            Region pawnRegion = pawn.GetRegion();
            if (pawnRegion == null)
            {
                return false;
            }
            RegionTraverser.BreadthFirstTraverse(pawnRegion, (Region from, Region to) => (to.door == null || to.door.Open) && (to.extentsClose.ClosestDistSquaredTo(pawn.Position) <= threatDistSquared), delegate (Region reg)
            {
                List<Thing> list = reg.ListerThings.ThingsInGroup(ThingRequestGroup.AttackTarget);
                for (int i = 0; i < list.Count; i++)
                {
                    if (ShouldInduceFear(list[i], pawn, threatDistSquared) && list[i].CanSee(pawn)) //GenSight.LineOfSightToThing(location, list[i], pawn.Map)
                    {
                        foundThreat = true;
                        break;
                    }
                    tmpInvIds.Add(list[i].thingIDNumber);
                }
                return foundThreat;
            }, 99999);
            return foundThreat;
        }

        public static IntVec3 FindHideInFearLocation(Pawn pawn)
        {
            IntVec3 position = pawn.Position;
            Region pawnRegion = pawn.GetRegion();
            if (pawnRegion == null)
            {
                return position;
            }
            //Get all potential threats within range
            tmpThreats.Clear();
            tmpInvIds.Clear();
            tmpInvIds.Add(pawn.thingIDNumber);
            RegionTraverser.BreadthFirstTraverse(pawnRegion, (Region from, Region reg) => reg.extentsClose.ClosestDistSquaredTo(pawn.Position) <= potentialThreatDistSquared, delegate (Region reg)
            {
                List<Thing> list = reg.ListerThings.ThingsInGroup(ThingRequestGroup.AttackTarget);
                for (int i = 0; i < list.Count; i++)
                {
                    if (!tmpInvIds.Contains(list[i].thingIDNumber) && ShouldInduceFear(list[i], pawn, threatDistSquared))
                    {
                        tmpThreats.Add(list[i]);
                    }
                    tmpInvIds.Add(list[i].thingIDNumber);
                }
                return false;
            }, 99999);
            if (tmpThreats.Count > 0)
            {
                position = CellFinderLoose.GetFleeDest(pawn, tmpThreats, potentialThreatDistance);
            }
            else
            {
                Log.Message("no threat found. random dest");
                position = RCellFinder.RandomWanderDestFor(pawn, pawn.Position, 7f, (pawn, loc, root) => WanderRoomUtility.IsValidWanderDest(pawn, loc, root), Danger.Deadly);
                Log.Message($"random dest: {position}");
            }
            return position;
        }
    }
}
