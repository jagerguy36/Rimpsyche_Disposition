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
                if (ModsConfig.IsActive("ludeon.rimworld.royalty"))
                {
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_MusicalInstrument", true))
                    {
                        mult = 1f;
                        //I'm pretty sure the Royalty joygiver for playing an instrument is specifically for performing in front of others, so confident and performative pawns should prefer it
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Confidence, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Talkativeness, 3f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }
                /* Leaving this commented out because I couldn't think of a good justification to tie it to any of the personality traits.
                if (ModsConfig.IsActive("ludeon.rimworld.odyssey"))
                {
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("GoSwimming", true))
                    {
                        mult = 1f;
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }
                */
                if (ModsConfig.IsActive("dubwise.dubsbadhygiene"))
                {
                    /* Like with Odyssey swimming, I couldn't really justify tying these to any personality traits, so I'm leaving them commented out for the time being.
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("UseDBHSauna", true))
                    {
                        mult = 1f;
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("UseDBHSwimmingPool", true))
                    {
                        mult = 1f;
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("UseHotTub", true))
                    {
                        mult = 1f;
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    */
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("WatchWashingMachine", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 6f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 6f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tension, 1/3f); //Watching the washing machine is kind of silly, so it makes less sense for a tense pawn to do it
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }
                if (ModsConfig.IsActive("vanillaexpanded.vfecore"))
                {
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Roulette", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 6f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Arcade", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_Piano", true))
                    {
                        mult = 1f;
                        //This joydriver gets replaced with the Royalty one if Royalty is active, so I'll use the same stats as the royalty one
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Confidence, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Talkativeness, 3f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_DartsBoard", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("Play_PunchingBag", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Aggressiveness, 8f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VFE_Play_Computer", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    /* Couldn't justify associating sunbathing with any of the personality traits, so I'll leave it commented out.
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VFE_Play_Sunbathing", true))
                    {
                        mult = 1f;
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    */
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VFE_ListenToMusic", true))
                    {
                        //This joygiver is used both for listening to people perform on instruments AND for listening to music on the radio
                        mult = 1f;
                        //I'll use the same multipliers as viewing art, excepting the bonus for superficiality since that related to the pawn fixating on aesthetics
                        //I think it makes sense for attending concerts to have similar multipliers to viewing art, it does feel odd to have radio listening share the same multipliers. Not much I can do about it in this implementation.
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 3f); 
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Imagination, 4f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Inquisitiveness, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Reflectiveness, 2f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }
                if (ModsConfig.IsActive("haplo.miscellaneous.training"))
                {
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("ShootSomeArrows", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 5f); //Ambitious pawns value self-improvement, so they should prefer to train
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f); //SelfInterested pawns get a mild modifier for self-improvement as well
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 5f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("PracticeShooting", true))
                    {
                        mult = 1f;
                        //Shooting practice is essentially the same as shooting arrows
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 5f); 
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 5f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("PracticeMartialArts", true))
                    {
                        mult = 1f;
                        //Similar ideas apply here as other forms of training. Additional wait for aggression since it's melee combat and not shooting.
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Aggressiveness, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 5f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }
                if (ModsConfig.IsActive("oskarpotocki.vfe.medieval2"))
                {
                    //same logic here as for the misc training mod, since it's effectively the same.
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VFEM2_Play_Archery", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 5f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("VFEM2_Play_TrainingDummy", true))
                    {
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Aggressiveness, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Ambition, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Discipline, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Tenacity, 5f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }

                if (ModsConfig.IsActive("dankpyon.medieval.overhaul"))
                {
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("DankPyon_Play_CupAndDice", true))
                    {
                        mult = 1f;
                        //I think this is supposed to be Liar's Dice, so it's essentially the same as Poker, at least in terms of how personalities play into it.
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 8f); 
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 6f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Confidence, 4f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Loyalty, 1 / 4f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_SelfInterest, 3f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Trust, 1 / 6f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 6f); 
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("DankPyon_Play_Tarocco", true))
                    {
                        //I do not know what Tarocco is or what game it is supposed to be. The description mentions it being "strategic" so I think it's fitting to use the same modifiers as Chess and Ur
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                    if (__instance.def == DefDatabase<JoyGiverDef>.GetNamed("DankPyon_Play_RimWar", true))
                    {
                        //RimWar looks to be a strategy game, I think it's most comparable to chess.
                        mult = 1f;
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Competitiveness, 5f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Playfulness, 8f);
                        mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Diligence, 1 / 4f);
                        mult = Mathf.Clamp(mult, .01f, 10f);
                        __result *= mult;
                    }
                }




                float tolerancepercent = 1f - pawn.needs.joy.tolerances.JoyFactorFromTolerance(__instance.def.joyKind); //percent tolerance of the joykind in question
                float tolerancefactor = 10f * tolerancepercent + 1f; //number from 1 to 11 scaling with tolerance, to be used for the Mult function
                mult = 1f;
                //Experimental, Open-Minded, and Spontaneous pawns prefer doing things they haven't done in a while, so they prefer lower tolerance activities
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Experimentation, 1/tolerancefactor);
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Openness, 1 / tolerancefactor);
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Spontaneity, 1 / tolerancefactor);
                //Organized pawns prefer consistency, and stick to the same activities.
                mult *= compPsyche.Personality.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Organization, tolerancefactor);
                mult = Mathf.Clamp(mult, .01f, 10f);
                __result *= mult;

                //I wanted to also have personality affect the fall rate of tolerances for these joy types, but didn't want to create a situation where the number of recreation types the game told the player they need didn't match up with reality due to their pawns hating the available recreation types.
            }
        }
    }
}
