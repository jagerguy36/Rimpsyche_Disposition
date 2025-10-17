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
    }
}
