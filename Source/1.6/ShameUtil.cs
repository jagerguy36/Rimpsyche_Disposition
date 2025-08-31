using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class ShameUtil
    {
        //TODO: Check the distance numbers.
        public static IntVec3 FindHideInShameLocation(Pawn pawn)
        {
            IntVec3 position = pawn.Position;
            int bestPosition = -100;
            IntVec3 cell = pawn.Position;
            int maxDistance = 40;
            // First check if the pawn's got a bedroom and that is near enough. If that's the case, hide in their own room.
            FloatRange temperature = pawn.ComfortableTemperatureRange();
            List<Pawn> all_pawns = pawn.Map.mapPawns.AllPawnsSpawned.Where(x
                => x.Position.DistanceTo(pawn.Position) < 100
                && x.RaceProps.Humanlike
                && x != pawn
                ).ToList();

            List<IntVec3> random_cells = new List<IntVec3>();
            for (int loop = 0; loop < 50; ++loop)
            {
                random_cells.Add(position + IntVec3.FromVector3(Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) * Rand.RangeInclusive(1, maxDistance)));
            }
            random_cells = random_cells.Where(x
                => x.Standable(pawn.Map)
                && x.InAllowedArea(pawn)
                && x.GetDangerFor(pawn, pawn.Map) != Danger.Deadly
                && !x.ContainsTrap(pawn.Map)
                && !x.ContainsStaticFire(pawn.Map)
                ).Distinct().ToList();

            foreach (IntVec3 random_cell in random_cells)
            {
                if (pawn.Position.DistanceTo(random_cell) > 100)
                    continue;// too far

                int score = 0;
                Room room = random_cell.GetRoom(pawn.Map);

                bool might_be_seen = MightBeSeen(all_pawns, random_cell, pawn);

                if (might_be_seen)
                    score -= 100;

                if (random_cell.GetTemperature(pawn.Map) > temperature.min && random_cell.GetTemperature(pawn.Map) < temperature.max)
                    score += 20;
                else
                    score -= 20;
                if (random_cell.Roofed(pawn.Map))
                    score += 5;
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

                if (score <= bestPosition) continue;
                bestPosition = score;
                cell = random_cell;
            }

            return cell;
        }
        public static bool MightBeSeen(List<Pawn> otherPawns, IntVec3 cell, Pawn pawn)
        {
            return otherPawns.Any(x
                    => x.Awake()
                    && x.Position.DistanceTo(cell) < 50
                    && GenSight.LineOfSight(x.Position, cell, pawn.Map)
                    );
        }

    }
}
