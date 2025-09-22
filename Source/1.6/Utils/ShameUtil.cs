using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public static class ShameUtil
    {
        private const int sightDistance = 13; //Interaction HorDistance is 6, witnessed distance is 12, flee all pawn flee distance is 23
        private const int runDistanceMax = 50; //How far the pawn will move
        private const int maxDistSquared = (sightDistance + runDistanceMax) * (sightDistance + runDistanceMax);
        private const int sightDistSquared = sightDistance * sightDistance;
        private static HashSet<Pawn> tmpLovePartners = new HashSet<Pawn>{};
        private static List<Pawn> tmpObservers = new List<Pawn>();
        private static HashSet<int> tmpInvIds = new HashSet<int> { };

        public static bool CanFeelShame(Pawn pawn)
        {
            if (pawn.InMentalState)
            {
                return false;
            }
            var bed = pawn.CurrentBed();
            if (bed != null) //Cover yourself with sheets
            {
                return false;
            }
            if (!pawn.Awake())
            {
                return false;
            }
            return true;
        }

        //TODO: RegionBased search with better performance and better location selection.
        //Also check TryFindDirectFleeDestination
        public static IntVec3 FindHideInShameLocation(Pawn pawn)
        {
            IntVec3 position = pawn.Position;
            IntVec3 bestCell = position;
            FloatRange temperature = pawn.ComfortableTemperatureRange();
            List<Pawn> all_pawns = ScanObservers(pawn, maxDistSquared);

            // Find best candidate
            int bestScore = -100;
            if (pawn.ownership.OwnedBed != null)
            {
                IntVec3 bedPosition = pawn.ownership.OwnedBed.Position;
                float distToBed = (pawn.Position - bedPosition).LengthHorizontalSquared;
                if (distToBed <= runDistanceMax * runDistanceMax
                    && bedPosition.InAllowedArea(pawn)
                    && !MightBeSeen(all_pawns, bedPosition, pawn, sightDistSquared))
                    {
                        return bedPosition;
                    }
            }
            for (int i = 0; i < 20; i++)
            {
                IntVec3 candidate = position + IntVec3.FromVector3(
                    Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) * Rand.RangeInclusive(1, runDistanceMax));

                if (!candidate.Standable(pawn.Map)) continue;
                if (!pawn.Map.pawnDestinationReservationManager.CanReserve(candidate, pawn)) continue;
                if (candidate.GetDangerFor(pawn, pawn.Map) == Danger.Deadly) continue;
                if (candidate.ContainsTrap(pawn.Map)) continue;
                if (candidate.ContainsStaticFire(pawn.Map)) continue;
                if (candidate.GetTerrain(pawn.Map).dangerous) continue;

                int score = 0;
                Room room = candidate.GetRoom(pawn.Map);
                bool might_be_seen = MightBeSeen(all_pawns, candidate, pawn, sightDistSquared);

                if (might_be_seen)
                    continue;

                if (candidate.GetTemperature(pawn.Map) > temperature.min && candidate.GetTemperature(pawn.Map) < temperature.max)
                    score += 20;
                else
                    score -= 20;
                if (candidate.GetDangerFor(pawn, pawn.Map) == Danger.Some)
                    score -= 25;
                else if (candidate.GetDangerFor(pawn, pawn.Map) == Danger.None)
                    score += 5;
                if (candidate.GetTerrain(pawn.Map) == TerrainDefOf.WaterShallow ||
                    candidate.GetTerrain(pawn.Map) == TerrainDefOf.WaterMovingShallow ||
                    candidate.GetTerrain(pawn.Map) == TerrainDefOf.WaterOceanShallow)
                    score -= 20;
                if (!room.Owners.Any())
                    score += 10;
                else if (room.Owners.Contains(pawn))
                    score += 50;
                if (room.IsDoorway)
                    score -= 50;
                if (room.Role == RoomRoleDefOf.Bedroom)
                    score += 10;
                if (score > bestScore && pawn.CanReach(candidate, PathEndMode.OnCell, Danger.Some))
                {
                    bestScore = score;
                    bestCell = candidate;
                    if (score >= 50)
                    {
                        return bestCell;
                    }
                }
            }
            return bestCell;
        }
        public static List<Pawn> ScanObservers(Pawn pawn, int distSquared = sightDistSquared)
        {
            var loverHash = ExistingLovePartners(pawn);
            List<Pawn> all_pawns = pawn.Map.mapPawns.AllPawnsSpawned.Where(x
                => x.RaceProps.Humanlike
                && x.Position.DistanceToSquared(pawn.Position) < distSquared
                && x.Awake()
                && x != pawn
                && !loverHash.Contains(x)
                ).ToList();
            return all_pawns;
        }
        public static List<Pawn> ScanObservers_R(Pawn pawn, int distSquared = sightDistSquared)
        {
            tmpObservers.Clear();
            tmpInvIds.Clear();
            tmpInvIds.Add(pawn.thingIDNumber);
            var loverHash = ExistingLovePartners(pawn);
            Region region = pawn.GetRegion();
            if (region == null)
            {
                return tmpObservers;
            }
            RegionTraverser.BreadthFirstTraverse(region, (Region from, Region to) => (to.extentsClose.ClosestDistSquaredTo(pawn.Position) <= distSquared), delegate (Region reg)
            {
                List<Thing> list = reg.ListerThings.ThingsInGroup(ThingRequestGroup.Pawn);
                for (int i = 0; i < list.Count; i++)
                {
                    if (!tmpInvIds.Contains(list[i].thingIDNumber)
                        && list[i] is Pawn otherPawn
                        && (otherPawn.RaceProps.Humanlike)
                        && otherPawn.Position.DistanceToSquared(pawn.Position) < distSquared
                        && otherPawn.Awake()
                        && !loverHash.Contains(otherPawn)
                    )
                    {
                        tmpObservers.Add(otherPawn);
                    }
                    tmpInvIds.Add(list[i].thingIDNumber);
                }
                return false;
            }, 99999);
            return tmpObservers;
        }

        public static bool MightBeSeen(List<Pawn> otherPawns, IntVec3 cell, Pawn pawn, int distSquared = sightDistSquared)
        {
            return otherPawns.Any(x
                    => x.Position.DistanceToSquared(cell) < distSquared
                    && x.Awake()
                    && GenSight.LineOfSight(x.Position, cell, pawn.Map)
                    );
        }
        public static bool BeingSeen(Pawn pawn, int distSquared = sightDistSquared)
        {
            tmpInvIds.Clear();
            tmpInvIds.Add(pawn.thingIDNumber);
            var loverHash = ExistingLovePartners(pawn);
            bool foundObserver = false;
            Region region = pawn.GetRegion();
            if (region == null)
            {
                return false;
            }
            RegionTraverser.BreadthFirstTraverse(region, (Region from, Region to) => (to.door == null || to.door.Open) && (to.extentsClose.ClosestDistSquaredTo(pawn.Position) <= distSquared), delegate (Region reg)
            {
                List<Thing> list = reg.ListerThings.ThingsInGroup(ThingRequestGroup.Pawn);
                for (int i = 0; i < list.Count; i++)
                {
                    if (!tmpInvIds.Contains(list[i].thingIDNumber)
                        && list[i] is Pawn otherPawn
                        && (otherPawn.RaceProps.Humanlike)
                        && !loverHash.Contains(otherPawn)
                        && otherPawn.Position.DistanceToSquared(pawn.Position) < distSquared
                        && otherPawn.Awake()
                        && GenSight.LineOfSightToThing(pawn.Position, otherPawn, pawn.Map)
                    )
                    {
                        foundObserver = true;
                        break;
                    }
                    tmpInvIds.Add(list[i].thingIDNumber);
                }
                return foundObserver;
            }, 99999);
            return foundObserver;
        }

        public static bool TryGiveFleeInShameJob(Pawn pawn, bool continuedJob=false)
        {
            if (pawn.Downed)
            {
                return false;
            }
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return false;
            }
            if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
            {
                return false;
            }
            if (!continuedJob)
            {
                PlayLogEntry_Interaction playLogEntry = new PlayLogEntry_Interaction(DefOfDisposition.Rimpsyche_Shamed, pawn, pawn, null);
                Find.PlayLog.Add(playLogEntry);
                if (RimpsycheDispositionSettings.sendShameMessage)
                {
                    Messages.Message("RP_MessageShamed".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
                }
            }
            var fleeDest = FindHideInShameLocation(pawn);
            var runawayjob = new Job(DefOfDisposition.RimPsyche_FleeInShame, fleeDest);
            runawayjob.mote = MoteMaker.MakeThoughtBubble(pawn, "Things/Mote/Flecks/Embarrassed", maintain: true);
            pawn.jobs.StartJob(runawayjob, JobCondition.InterruptForced, null, false, true, null);
            compPsyche.isOverwhelmed = true;
            return true;
        }

        public static bool TryDoRandomShameCausedMentalBreak(Pawn pawn)
        {
            var breaker = pawn.mindState.mentalBreaker;
            //TODO: chose mentalbreak intensity based on pawn's current mood
            if (!breaker.TryGetRandomMentalBreak(MentalBreakIntensity.Major, out MentalBreakDef result))
            {
                return false;
            }
            TaggedString taggedString = "RP_MentalStateReason_Shame".Translate();
            return breaker.TryDoMentalBreak(taggedString, result);
        }

        public static HashSet<Pawn> ExistingLovePartners(Pawn pawn)
        {
            tmpLovePartners.Clear();
            List<DirectPawnRelation> directRelations = pawn.relations.DirectRelations;
            for (int i = 0; i < directRelations.Count; i++)
            {
                if (LovePartnerRelationUtility.IsLovePartnerRelation(directRelations[i].def) && (!directRelations[i].otherPawn.Spawned))
                {
                    tmpLovePartners.Add(directRelations[i].otherPawn);
                }
            }
            return tmpLovePartners;
        }
    }
}
