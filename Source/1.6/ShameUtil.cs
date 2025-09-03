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
        private const int sightDistance = 13; //Interaction HorDistance is 6, flee all pawn flee distance is 23
        private const int runDistanceMax = 50;
        private const int maxDistSquared = 63*63;
        private const int sightDistSquared = 13*13;
        private static HashSet<Pawn> tmpLovePartners = new HashSet<Pawn>{};

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

        //Also check TryFindDirectFleeDestination
        public static IntVec3 FindHideInShameLocation(Pawn pawn)
        {
            IntVec3 position = pawn.Position;
            IntVec3 bestCell = position;
            int bestScore = -100;
            if (pawn.ownership.OwnedBed != null)
            {
                IntVec3 bedPosition = pawn.ownership.OwnedBed.Position;
                if ((pawn.Position - bedPosition).LengthHorizontalSquared <= 75)
                {
                    return bedPosition;
                }
            }

            FloatRange temperature = pawn.ComfortableTemperatureRange();
            List<Pawn> all_pawns = ScanObservers(pawn, maxDistSquared);

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

        public static List<Pawn> ScanObservers(Pawn pawn, int distSquared = 169)
        {
            var loverHash = ExistingLovePartners(pawn);
            List<Pawn> all_pawns = pawn.Map.mapPawns.AllPawnsSpawned.Where(x
                => x.RaceProps.Humanlike
                && x.Position.DistanceToSquared(pawn.Position) < distSquared //sight+rundist
                && x != pawn
                && !loverHash.Contains(x)
                ).ToList();
            return all_pawns;
        }

        public static bool MightBeSeen(List<Pawn> otherPawns, IntVec3 cell, Pawn pawn, int distSquared=169)
        {
            return otherPawns.Any(x
                    => x.Awake()
                    && x.Position.DistanceToSquared(cell) < distSquared
                    && GenSight.LineOfSight(x.Position, cell, pawn.Map)
                    );
        }
        public static bool BeingSeen(Pawn pawn, int distSquared = 169)
        {
            var loverHash = ExistingLovePartners(pawn);
            foreach (var otherPawn in pawn.Map.mapPawns.AllPawnsSpawned)
            {
                if (otherPawn.RaceProps.Humanlike
                    && otherPawn != pawn
                    && !loverHash.Contains(otherPawn)
                    && otherPawn.Position.DistanceToSquared(pawn.Position) < distSquared
                    && GenSight.LineOfSight(otherPawn.Position, pawn.Position, pawn.Map))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TryGiveFleeInShameJob(Pawn pawn)
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
            if (compPsyche.isOverwhelmed)
            {
                return false;
            }
            if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
            {
                return false;
            }
            compPsyche.isOverwhelmed = true;
            PlayLogEntry_Interaction playLogEntry = new PlayLogEntry_Interaction(DefOfDisposition.Rimpsyche_Shamed, pawn, pawn, null);
            Find.PlayLog.Add(playLogEntry);
            var fleeDest = FindHideInShameLocation(pawn);
            Log.Message($"Start running! Location at: {fleeDest}");
            var runawayjob = new Job(DefOfDisposition.RimPsyche_FleeInShame, fleeDest);
            runawayjob.mote = MoteMaker.MakeThoughtBubble(pawn, "Things/Mote/SpeechSymbols/Ashamed", maintain: true);
            pawn.jobs.StartJob(runawayjob, JobCondition.InterruptForced, null, false, true, null);
            if (RimpsycheDispositionSettings.sendShameMessage)
            {
                Messages.Message("MessageShamed".Translate(pawn.Named("PAWN")).AdjustedFor(pawn), pawn, MessageTypeDefOf.NeutralEvent);
            }
            return true;
        }

        public static bool TryDoRandomShameCausedMentalBreak(Pawn pawn)
        {
            var breaker = pawn.mindState.mentalBreaker;
            if (!breaker.CanHaveMentalBreak())
            {
                return false;
            }
            //TODO: chose mentalbreak intensity based on pawn's current mood
            if (!breaker.TryGetRandomMentalBrea(MentalBreakIntensity.Major, out MentalBreakDef result))
            {
                return false;
            }
            TaggedString taggedString = "MentalStateReason_Shame".Translate();
            return TryDoMentalBreak(taggedString, result);
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
