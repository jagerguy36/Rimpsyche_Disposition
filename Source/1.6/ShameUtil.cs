using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class ShameUtil
    {
        //Also check TryFindDirectFleeDestination
        public static IntVec3 FindHideInShameLocation(Pawn pawn)
        {
            IntVec3 position = pawn.Position;
            IntVec3 bestCell = position;
            int bestScore = -100;
            if (pawn.ownership.OwnedBed != null)
            {
                IntVec3 bedPosition = pawn.ownership.OwnedBed.Position;
                if ((pawn.Position - bedPosition).LengthHorizontalSquared =< 75f)
                {
                    return bedPosition
                }
            }

            float sightDistance = 13f; //Interaction HorDistance is 6, flee all pawn flee distance is 23
            float runDistanceMax = 50;
            float maxDistSquared = 2500;
            FloatRange temperature = pawn.ComfortableTemperatureRange();
            List<Pawn> all_pawns = pawn.Map.mapPawns.AllPawnsSpawned.Where(x
                => x.Position.DistanceTo(pawn.Position) < 63f //sight+rundist
                && x.RaceProps.Humanlike
                && x != pawn
                ).ToList();

            // Find best candidate
            for (int i = 0; i < 20; i++)
            {
                IntVec3 candidate = position + IntVec3.FromVector3(
                    Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) * Rand.RangeInclusive(1, runDistanceMax));

                if (!candidate.InBounds(pawn.Map)) continue;
                if (!candidate.Standable(pawn.Map)) continue;
                if (!candidate.InAllowedArea(pawn)) continue;
                if (candidate.GetDangerFor(pawn, pawn.Map) == Danger.Deadly) continue;
                if (candidate.ContainsTrap(pawn.Map)) continue;
                if (candidate.ContainsStaticFire(pawn.Map)) continue;

                int score = 0;
                Room room = candidate.GetRoom(pawn.Map);
                bool might_be_seen = MightBeSeen(all_pawns, candidate, pawn, sightDistance);

                if (might_be_seen)
                    continue;

                if (random_cell.GetTemperature(pawn.Map) > temperature.min && random_cell.GetTemperature(pawn.Map) < temperature.max)
                    score += 20;
                else
                    score -= 20;
                if (random_cell.GetDangerFor(pawn, pawn.Map) == Danger.Some)
                    score -= 25;
                else if (random_cell.GetDangerFor(pawn, pawn.Map) == Danger.None)
                    score += 5;
                if (random_cell.GetTerrain(pawn.Map) == TerrainDefOf.WaterShallow ||
                    random_cell.GetTerrain(pawn.Map) == TerrainDefOf.WaterMovingShallow ||
                    random_cell.GetTerrain(pawn.Map) == TerrainDefOf.WaterOceanShallow)
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

        public static bool MightBeSeen(List<Pawn> otherPawns, IntVec3 cell, Pawn pawn, int dist=13)
        {
            return otherPawns.Any(x
                    => x.Awake()
                    && x.Position.DistanceTo(cell) < dist
                    && GenSight.LineOfSight(x.Position, cell, pawn.Map)
                    );
        }

    }
}
