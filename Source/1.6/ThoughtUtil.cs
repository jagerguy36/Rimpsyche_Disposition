using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public static class ThoughtUtil
    {
        public static Dictionary<string, RimpsycheFormula> MoodMultiplierDB = new()
        {
            { "KnowPrisonerSold", CompassionMoodMultiplier},
            { "KnowColonistOrganHarvested", CompassionMoodMultiplier},
        };

        public static RimpsycheFormula CompassionMoodMultiplier = new(
            "CompassionMoodMultiplier",
            (tracker) =>
            {
                float mult = 1f;
                float compassionMult = 1f + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Compassion)*0.5;
                return mult * compassionMult;
            }
        );
    }
}