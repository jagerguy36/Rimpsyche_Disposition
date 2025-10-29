using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class MiscModDB : BaseThoughtDB
    {
        private static readonly List<string> activeModIds = new List<string>();
        public static void AddDefs_MiscMods(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            activeModIds.Clear();
            if (ModsConfig.IsActive("dubwise.dubsbadhygiene")) { AddDefs_DBH(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("dubwise.dubsbadhygiene"); }
            if (ModsConfig.IsActive("ceteam.combatextended")) { AddDefs_CE(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("ceteam.combatextended"); }
            if (ModsConfig.IsActive("VanillaExpanded.VanillaSocialInteractionsExpanded")) { AddDefs_VSIE(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("VanillaExpanded.VanillaSocialInteractionsExpanded"); }
            if (ModsConfig.IsActive("vanillaexpanded.vcooke")) { AddDefs_VCE(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("vanillaexpanded.vcooke"); }
            if (ModsConfig.IsActive("vanillaexpanded.vbrewe")) { AddDefs_VBE(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("vanillaexpanded.vbrewe"); }
            if (ModsConfig.IsActive("hautarche.hautstraits")) { AddDefs_Haut(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("hautarche.hautstraits"); }
            if (ModsConfig.IsActive("sumghai.mousekinrace")) { AddDefs_MouseKin(MoodThoughtTagDB, OpinionThoughtTagDB); activeModIds.Add("sumghai.mousekinrace"); }

            if (activeModIds.Count > 0)
            {
                string integratedMods = string.Join(", ", activeModIds);
                Log.Message("[Rimpsyche - Disposition] tagged thoughts from: " + integratedMods);
            }
        }
        private static void AddDefs_DBH(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_DBH_Tag_Affluence = new(
                ["DrankDirtyWater",
                "DrankUrine",
                "HotShower",
                "HeatedPool",
                "HotBath",
                "ColdBath",
                "ColdShower",
                "ColdWater",
                "UsedPrivateBathroom",
                "HygieneLevel"]
                );
            List<string> moodList_DBH_Tag_Decency = new(
                ["WashPrivacy",
                "ToiletPrivacy",
                "SoiledSelf",
                "openDefecation"]
                );
            List<string> moodList_DBH_Tag_Needy = new(
                ["BowelLevel"]
                );

            RegisterThoughts(moodList_DBH_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_DBH_Tag_Decency, MoodThoughtTagDB, FormulaDB.Tag_Decency);
            RegisterThoughts(moodList_DBH_Tag_Needy, MoodThoughtTagDB, FormulaDB.Tag_Needy);
        }
        private static void AddDefs_CE(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_CE_Tag_Fear = new(
                ["Suppressed"]
                );

            RegisterThoughts(moodList_CE_Tag_Fear, MoodThoughtTagDB, FormulaDB.Tag_Fear);
        }
        private static void AddDefs_VSIE(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_VSIE_Tag_Gathering = new(
                ["VSIE_HadNiceChatWithBeer",
                "VSIE_AttendedBingeParty",
                "VSIE_AttendedOutdoorParty",
                "VSIE_AttendedBirthdayParty",
                "VSIE_HadBirthdayParty",
                "VSIE_SharingBurden",
                "VSIE_SocialEnvironment",
                "VSIE_DidNotAttendWedding",
                "VSIE_AttendedMyWedding"]
                );
            List<string> moodList_VSIE_Tag_Sympathy = new(
                ["VSIE_VentedOnMe"]
                );
            List<string> moodList_VSIE_Tag_Loved = new(
                ["VSIE_JealouslyMyPartnerDatedSomeoneElse",
                "VSIE_BrokeUpWithMe",
                "VSIE_StoleMyLover",
                "VSIE_GotSomeLovin"]
                );
            List<string> opinionList_VSIE_Tag_Gathering = new(
                ["VSIE_SharingBurden",
                "VSIE_DidNotAttendWedding",
                "VSIE_AttendedMyWedding"]
                );
            List<string> opinionList_VSIE_Tag_Sympathy = new(
                ["VSIE_VentedOnMe"]
                );
            List<string> opinionList_VSIE_Tag_Loved = new(
                ["VSIE_JealouslyMyPartnerDatedSomeoneElse",
                "VSIE_BrokeUpWithMe",
                "VSIE_StoleMyLover",
                "VSIE_GotSomeLovin"]
                );
            List<string> opinionList_VSIE_Tag_Bond = new(
                ["VSIE_BondedPetButcheredOpinion",
                "VSIE_ExposedCorpseOfMyFriendOpinion",
                "VSIE_HasBeenMyFriendSinceChildhood",
                "VSIE_CuredMyFriend"]
                );
            List<string> opinionList_VSIE_Tag_Fear = new(
                ["VSIE_IngestedHumanFlesh"]
                );

            RegisterThoughts(moodList_VSIE_Tag_Gathering, MoodThoughtTagDB, FormulaDB.Tag_Gathering);
            RegisterThoughts(moodList_VSIE_Tag_Sympathy, MoodThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(moodList_VSIE_Tag_Loved, MoodThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(opinionList_VSIE_Tag_Gathering, OpinionThoughtTagDB, FormulaDB.Tag_Gathering);
            RegisterThoughts(opinionList_VSIE_Tag_Sympathy, OpinionThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(opinionList_VSIE_Tag_Loved, OpinionThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(opinionList_VSIE_Tag_Bond, OpinionThoughtTagDB, FormulaDB.Tag_Bond);
            RegisterThoughts(opinionList_VSIE_Tag_Fear, OpinionThoughtTagDB, FormulaDB.Tag_Fear);
        }
        private static void AddDefs_VCE(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_VCE_Tag_Affluence = new(
                ["VCE_ConsumedSugar",
                "VCE_ConsumedChocolateSyrup",
                "VCE_ConsumedInsectJelly",
                "VCE_SmokeleafButterHigh",
                "VCE_ConsumedSalt",
                "VCE_ConsumedMayo",
                "VCE_ConsumedAgave",
                "VCE_ConsumedSpices",
                "VCE_ConsumedDigestibleResurrectorNanites",
                "VCE_AteGourmetMeal",
                "VCE_AteSimpleDessert",
                "VCE_AteFineDessert",
                "VCE_AteLavishDessert",
                "VCE_AteGourmetDessert",
                "VCE_AteSimpleGrill",
                "VCE_AteFineGrill",
                "VCE_AteLavishGrill",
                "VCE_AteGourmetGrill",
                "VCE_AteFriedGoods",
                "VCE_ConsumedCannedGoods",
                "VCE_AteCheese"]
                );

            RegisterThoughts(moodList_VCE_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
        }
        private static void AddDefs_VBE(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_VBE_Tag_Affluence = new(
                ["VBE_DrankSoda",
                "VBE_DrankTea"]
                );

            RegisterThoughts(moodList_VBE_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
        }
        private static void AddDefs_Haut(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_Haut_Tag_Art = new(
                ["HVT_NeedBeautyAestheticist",
                "HVT_WAUW"]
            );
            List<string> moodList_Haut_Tag_Fear = new(
                ["HVT_Agrizoophobia",
                "HVT_HauntingPresence"]
            );
            List<string> moodList_Haut_Tag_Loved = new(
                ["HVT_SickForLove"]
            );
            List<string> moodList_Haut_Tag_Empathy = new(
                ["HVT_LovesickLetdown"]
            );
            List<string> moodList_Haut_Tag_Needy = new(
                ["HVT_EnvironmentColdOutdoorsy",
                "HVT_EnvironmentHotOutdoorsy"]
            );
            List<string> moodList_Haut_Tag_Sympathy = new(
                ["HVT_SadistSawMentalBreak",
                "HVT_SadistSawHysteric"]
            );
            List<string> moodList_Haut_Tag_Decency = new(
                ["HVT_TextileIsNaked"]
            );
            List<string> moodList_Haut_Tag_Judgemental = new(
                ["HVT_TextileVsNaked",
                "HVT_VsDoubtful",
                "HVT_Intolerance",
                "HVT_GenePurism",
                "HVT_XenogenePurism"]
            );
            List<string> opinionList_Haut_Tag_Bond = new(
                ["HVT_BondsOfLoyalty"]
            );
            List<string> opinionList_Haut_Tag_Judgemental = new(
                ["HVT_TextileVsNaked",
                "HVT_VsDoubtful",
                "HVT_Intolerance",
                "HVT_GenePurism"]
            );
            RegisterThoughts(moodList_Haut_Tag_Art, MoodThoughtTagDB, FormulaDB.Tag_Art);
            RegisterThoughts(moodList_Haut_Tag_Fear, MoodThoughtTagDB, FormulaDB.Tag_Fear);
            RegisterThoughts(moodList_Haut_Tag_Loved, MoodThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(moodList_Haut_Tag_Empathy, MoodThoughtTagDB, FormulaDB.Tag_Empathy);
            RegisterThoughts(moodList_Haut_Tag_Needy, MoodThoughtTagDB, FormulaDB.Tag_Needy);
            RegisterThoughts(moodList_Haut_Tag_Sympathy, MoodThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(moodList_Haut_Tag_Decency, MoodThoughtTagDB, FormulaDB.Tag_Decency);
            RegisterThoughts(moodList_Haut_Tag_Judgemental, MoodThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(opinionList_Haut_Tag_Bond, OpinionThoughtTagDB, FormulaDB.Tag_Bond);
            RegisterThoughts(opinionList_Haut_Tag_Judgemental, OpinionThoughtTagDB, FormulaDB.Tag_Judgemental);
        }
        private static void AddDefs_MouseKin(Dictionary<int, RimpsycheFormula> MoodThoughtTagDB, Dictionary<int, RimpsycheFormula> OpinionThoughtTagDB)
        {
            List<string> moodList_MouseKin_Tag_Affluence = new(
                ["Humanlike_Thought_AtePoopGiantCavy",
                "Mousekin_Thought_AteCheese",
                "Mousekin_Thought_AteCorpse",
                "Mousekin_Thought_AteHumanlikeMeatAsIngredient",
                "Mousekin_Thought_AteHumanlikeMeatDirect",
                "Mousekin_Thought_AteKibble",
                "Mousekin_Thought_AteRawFood",
                "Mousekin_Thought_AteTrailMix",
                "Mousekin_Thought_AteWithoutTable",
                "Mousekin_Thought_DrankChonkoNutBrew"]
            );
            List<string> moodList_MouseKin_Tag_Morality = new(
                ["Humanlike_Thought_HereticExecutionTerrible",
                "Humanlike_Thought_HereticExecutionSatisfying",
                "Humanlike_Thought_HereticExecutionSpectacular",
                "Mousekin_Thought_OtherMousekinHasProstheticOpinions",
                "Mousekin_Thought_ReligiousDisgustAtOwnArtificalBodyParts"]
            );
            List<string> moodList_MouseKin_Tag_Empathy_M = new(
                ["Mousekin_Thought_AteMousekinMeatAsIngredient",
                "Mousekin_Thought_AteMousekinMeatDirect",
                "Mousekin_Thought_ButcheredMousekinCorpse",
                "Mousekin_Thought_KnowButcheredMousekinCorpse",
                "Mousekin_Thought_KnowColonistOrganHarvested",
                "Mousekin_Thought_KnowGuestOrganHarvested"]
            );
            List<string> moodList_MouseKin_Tag_Gathering = new(
                ["Mousekin_Thought_AttendedWedding"]
            );
            List<string> moodList_MouseKin_Tag_Judgemental = new(
                ["Mousekin_Thought_ChurchAttendedService",
                "Mousekin_Thought_ChurchHeldService",
                "Mousekin_Thought_ChurchMissedService",
                "Mousekin_Thought_ColonistLeftUnburied"]
            );
            List<string> moodList_MouseKin_Tag_Loved = new(
                ["Mousekin_Thought_GotMarried"]
            );
            List<string> moodList_MouseKin_Tag_Empathy = new(
                ["Mousekin_Thought_KnowColonistDied",
                "Mousekin_Thought_PawnWithGoodOpinionDied"]
            );
            List<string> moodList_MouseKin_Tag_Decency = new(
                ["Mousekin_Thought_MissingEars"]
            );
            List<string> moodList_MouseKin_Tag_Empathy_Kin = new(
                ["Mousekin_Thought_MyAuntDied",
                "Mousekin_Thought_MyBrotherDied",
                "Mousekin_Thought_MyCousinDied",
                "Mousekin_Thought_MyDaughterDied",
                "Mousekin_Thought_MyFatherDied",
                "Mousekin_Thought_MyGrandchildDied",
                "Mousekin_Thought_MyGrandparentDied",
                "Mousekin_Thought_MyHalfSiblingDied",
                "Mousekin_Thought_MyKinDied",
                "Mousekin_Thought_MyMotherDied",
                "Mousekin_Thought_MyNieceDied",
                "Mousekin_Thought_MyNephewDied",
                "Mousekin_Thought_MySisterDied",
                "Mousekin_Thought_MySonDied",
                "Mousekin_Thought_MyUncleDied"]
            );
            List<string> moodList_MouseKin_Tag_Empathy_Loved = new(
                ["Mousekin_Thought_MyFianceDied",
                "Mousekin_Thought_MyFianceeDied",
                "Mousekin_Thought_MyHusbandDied",
                "Mousekin_Thought_MyWifeDied"]
            );
            List<string> moodList_MouseKin_Tag_Harmed = new(
                ["Mousekin_Thought_MyOrganHarvested"]
            );
            List<string> moodList_MouseKin_Tag_Sympathy = new(
                ["Mousekin_Thought_PawnWithBadOpinionDied",
                "Mousekin_Thought_WearingPudgemouseFurApparel"]
            );
            List<string> moodList_MouseKin_Tag_Disquiet = new(
                ["Mousekin_Thought_PawnWithBadOpinionLost"]
            );
            List<string> moodList_MouseKin_Tag_Worry = new(
                ["Mousekin_Thought_PawnWithGoodOpinionLost"]
            );
            List<string> moodList_MouseKin_Tag_Outsider = new(
                ["Mousekin_Thought_ReleasedHealthyPrisoner"]
            );
            List<string> moodList_MouseKin_Tag_Needy = new(
                ["Mousekin_Thought_SleptInCold",
                "Mousekin_Thought_SleptInHeat",
                "Mousekin_Thought_SleptOnGround",
                "Mousekin_Thought_SleptOutside"]
            );
            List<string> moodList_MouseKin_Tag_Sympathy_M = new(
                ["Mousekin_Thought_WearingMousekinFurApparel"]
            );
            List<string> opinionList_MouseKin_Tag_Morality = new(
                ["Mousekin_Thought_OtherMousekinHasProstheticOpinions"]
            );
            RegisterThoughts(moodList_MouseKin_Tag_Affluence, MoodThoughtTagDB, FormulaDB.Tag_Affluence);
            RegisterThoughts(moodList_MouseKin_Tag_Morality, MoodThoughtTagDB, FormulaDB.Tag_Morality);
            RegisterThoughts(moodList_MouseKin_Tag_Empathy_M, MoodThoughtTagDB, FormulaDB.Tag_Empathy_M);
            RegisterThoughts(moodList_MouseKin_Tag_Gathering, MoodThoughtTagDB, FormulaDB.Tag_Gathering);
            RegisterThoughts(moodList_MouseKin_Tag_Judgemental, MoodThoughtTagDB, FormulaDB.Tag_Judgemental);
            RegisterThoughts(moodList_MouseKin_Tag_Loved, MoodThoughtTagDB, FormulaDB.Tag_Loved);
            RegisterThoughts(moodList_MouseKin_Tag_Empathy, MoodThoughtTagDB, FormulaDB.Tag_Empathy);
            RegisterThoughts(moodList_MouseKin_Tag_Decency, MoodThoughtTagDB, FormulaDB.Tag_Decency);
            RegisterThoughts(moodList_MouseKin_Tag_Empathy_Kin, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Kin);
            RegisterThoughts(moodList_MouseKin_Tag_Empathy_Loved, MoodThoughtTagDB, FormulaDB.Tag_Empathy_Loved);
            RegisterThoughts(moodList_MouseKin_Tag_Harmed, MoodThoughtTagDB, FormulaDB.Tag_Harmed);
            RegisterThoughts(moodList_MouseKin_Tag_Sympathy, MoodThoughtTagDB, FormulaDB.Tag_Sympathy);
            RegisterThoughts(moodList_MouseKin_Tag_Disquiet, MoodThoughtTagDB, FormulaDB.Tag_Disquiet);
            RegisterThoughts(moodList_MouseKin_Tag_Worry, MoodThoughtTagDB, FormulaDB.Tag_Worry);
            RegisterThoughts(moodList_MouseKin_Tag_Outsider, MoodThoughtTagDB, FormulaDB.Tag_Outsider);
            RegisterThoughts(moodList_MouseKin_Tag_Needy, MoodThoughtTagDB, FormulaDB.Tag_Needy);
            RegisterThoughts(moodList_MouseKin_Tag_Sympathy_M, MoodThoughtTagDB, FormulaDB.Tag_Sympathy_M);
            RegisterThoughts(opinionList_MouseKin_Tag_Morality, OpinionThoughtTagDB, FormulaDB.Tag_Morality);

            RegisterStageThought("Mousekin_Thought_FaithAffinityOpinions", OpinionThoughtTagDB,
                [null, //Apostates vs Nones
                null, //Apostates vs Apostate
                null, //Apostates vs Devotionist 
                null, //Apostates vs Pious 
                FormulaDB.Tag_Judgemental, //Apostates vs Inquisitionist
                null, //Devotionist vs Nones
                FormulaDB.Tag_Judgemental, //Devotionist vs Apostate
                FormulaDB.Tag_Judgemental, //Devotionist vs Devotionist 
                null, //Devotionist vs Pious 
                FormulaDB.Tag_Judgemental, //Devotionist vs Inquisitionist 
                null, //Pious vs Nones
                FormulaDB.Tag_Judgemental, //Pious vs Apostate
                FormulaDB.Tag_Judgemental, //Pious vs Devotionist 
                FormulaDB.Tag_Judgemental, //Pious vs Pious 
                FormulaDB.Tag_Judgemental, //Pious vs Inquisitionist 
                FormulaDB.Tag_Morality, //Inquisitionist vs Nones
                FormulaDB.Tag_Morality, //Inquisitionist vs Apostate
                FormulaDB.Tag_Morality, //Inquisitionist vs Devotionist 
                FormulaDB.Tag_Morality, //Inquisitionist vs Pious 
                FormulaDB.Tag_Morality] //Inquisitionist vs Inquisitionist
            );
            RegisterStageThought("Mousekin_Thought_KnowColonistExecuted", MoodThoughtTagDB,
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M] //someone was organ-murdered
            );
            RegisterStageThought("Mousekin_Thought_KnowGuestExecuted", MoodThoughtTagDB,
                [FormulaDB.Tag_JustifiedGuilt, //justified execution
                FormulaDB.Tag_Empathy, //someone was euthanized
                FormulaDB.Tag_Empathy_M, //someone was executed
                FormulaDB.Tag_Empathy_M] //someone was organ-murdered
            );
        }
    }
}
