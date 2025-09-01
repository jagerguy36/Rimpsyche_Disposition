using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class ShameUtil
    {
        private const float sightDistance = 13f; //Interaction HorDistance is 6, flee all pawn flee distance is 23
        private const float runDistanceMax = 50;
        private const float maxDistSquared = 63f*63f;
        private const float sightDistSquared = 13f*13f;

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

            FloatRange temperature = pawn.ComfortableTemperatureRange();
            List<Pawn> all_pawns = pawn.Map.mapPawns.AllPawnsSpawned.Where(x
                => x.Position.DistanceToSquared(pawn.Position) < maxDistSquared //sight+rundist
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
                bool might_be_seen = MightBeSeen(all_pawns, candidate, pawn, sightDistSquared);

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

        public static bool MightBeSeen(List<Pawn> otherPawns, IntVec3 cell, Pawn pawn, int distSquared=169)
        {
            return otherPawns.Any(x
                    => x.Awake()
                    && x.Position.DistanceToSquared(cell) < distSquared
                    && GenSight.LineOfSight(x.Position, cell, pawn.Map)
                    );
        }

    }
}
