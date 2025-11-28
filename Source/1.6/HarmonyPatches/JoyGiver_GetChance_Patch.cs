using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(JoyGiver))]
    [HarmonyPatch("GetChance")]
    public static class GetChance_Patch
    {
        public static void Postfix(JoyGiver __instance, Pawn pawn, ref float __result)
        {
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled == true && __result > 0)
            {
                float mult = 1f;
                //I think this is not the best way to do it, but ran into issues using JoyKindDefOf, as it seemed to only have a few JoyKinds available, not all of them.
                //I also would have preferred to use a switch statement, but would get errors with the DefDatabase command when using switches instead of if evaulations
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Meditate", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 3f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Focus, 3f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 2f); //Imaginative pawns are less focused on tangible matters
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("GoForWalk", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 3f); //Tenacious pawns like enduring hardship, going for a walk is somewhat comparable to hiking in Rimworld
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VisitGrave", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Bravery, 3f); //Fearful pawns are less likely to visit graves
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Optimism, 1/3f); //Pessimistic pawns are more likely to visit graves
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("ViewArt", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Authenticity, 1/3f); //The description for superficial pawns notes that they are more concerned with external presentation
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1/3f); //Diligent pawns prioritize productivity, are less attracted to art and leisure
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 4f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Inquisitiveness, 3f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Reflectiveness, 2f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("BuildSnowman", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 4f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("SocialRelax", true))
                {
                    mult = 1f;
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Sociability, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Talkativeness, 8f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VisitSickPawn", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Compassion, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 1/4f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tact, 3f); //I think diplomatic pawns would be interested in keeping everyone in high spirits
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Talkativeness, 3f); //It still involves talking!
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Sociability, 3f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Horseshoes", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f); //I think all the recreation games are competitive, even though they can be played alone
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1/4f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Hoopstone", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Billiards", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                    mult = Mathf.Clamp(mult, .01f, 6f);//Billiards has a higher base chance than everything else, so I clamped its top end lower.
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_GameOfUr", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Chess", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Poker", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 8f); //Poker is more directly competitive than the other games
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 6f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Confidence, 4f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Loyalty, 1/4f); //Pawns that are inclined towards deceit probably like bluffing in poker
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f); 
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Trust, 1/6f); 
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                    mult = Mathf.Clamp(mult, .01f, 6f); //Poker has a higher base chance than everything else, so I clamped its top end lower.
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("WatchTelevision", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 8f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("UseTelescope", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Inquisitiveness, 5f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("TakeDrug", true)) //I'm not sure that recreation drugs use the same chance-based joygiver system since they're generally scheduled/handled in the assignment menu
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Experimentation, 5f);
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }
                if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Reading", true))
                {
                    mult = 1f;
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Inquisitiveness, 4f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 4f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Reflectiveness, 2f);
                    mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 3f); //Ambitious pawns value self-improvement, would prefer reading skill books.
                    mult = Mathf.Clamp(mult, .01f, 10f);
                    __result *= mult;
                }

                float tolerancepercent = 1f - pawn.needs.joy.tolerances.JoyFactorFromTolerance(__instance.def.joyKind); //percent tolerance of the joykind in question
                float tolerancefactor = 10f * tolerancepercent + 1f; //number from 1 to 11 scaling with tolerance, to be used for the Mult function
                mult = 1f;
                //Experimental, Open-Minded, and Spontaneous pawns prefer doing things they haven't done in a while, so they prefer lower tolerance activities
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Experimentation, 1/tolerancefactor);
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Openness, 1 / tolerancefactor);
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Spontaneity, 1 / tolerancefactor);
                //Stable and Organized pawns prefer consistency, and stick to the same activities.
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Stability, tolerancefactor);
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Organization, tolerancefactor);
                mult = Mathf.Clamp(mult, .01f, 10f);
                __result *= mult;

                //I wanted to also have personality affect the fall rate of tolerances for these joy types, but didn't want to create a situation where the number of recreation types the game told the player they need didn't match up with reality due to their pawns hating the available recreation types.
            }
        }
    }
}
