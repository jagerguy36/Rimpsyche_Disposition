using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class Rimpsyche : Mod
    {
        public static RimpsycheDispositionSettings settings;
        public Rimpsyche(ModContentPack content) : base(content)
        {
            settings = GetSettings<RimpsycheDispositionSettings>();
            if (!ModsConfig.IsActive("maux36.rimpsyche"))
            {
                Log.Error("[Rimpsyche Disposition] Rimpsyche not loaded. The dependency was not met and the game will not run correctly");
            }
        }
        public override string SettingsCategory()
        {
            return "RimpsycheDispositionSettingCategory".Translate();
        }

        private static Vector2 scrollPosition = new Vector2(0f, 0f);
        private static float totalContentHeight = ModsConfig.BiotechActive ? 770f : 720f;
        private const float ScrollBarWidthMargin = 18f;
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect outerRect = inRect.ContractedBy(10f);
            bool scrollBarVisible = totalContentHeight > outerRect.height;
            var scrollViewTotal = new Rect(0f, 0f, outerRect.width - (scrollBarVisible ? ScrollBarWidthMargin : 0), totalContentHeight);
            Widgets.BeginScrollView(outerRect, ref scrollPosition, scrollViewTotal);

            var listing_Standard = new Listing_Standard();
            listing_Standard.Begin(new Rect(0f, 0f, scrollViewTotal.width, 9999f));
            listing_Standard.Gap(12f);

            listing_Standard.Label("RimpsycheDispositionContentSetting".Translate());
            listing_Standard.Gap(12f);
            listing_Standard.Label("RimpsycheDispositionRestartNeeded".Translate());
            listing_Standard.Gap(12f);
            listing_Standard.Label("RimpsycheMoodSettings".Translate());
            listing_Standard.Gap(6f);
            RimpsycheDispositionSettings.moodEmotionalityC = (float)Math.Round(listing_Standard.SliderLabeled("RimpsycheMoodEmotionalityC".Translate() + " (" + "Default".Translate() + " 0.2): " + RimpsycheDispositionSettings.moodEmotionalityC, RimpsycheDispositionSettings.moodEmotionalityC, 0.05f, 0.95f, tooltip: "RimpsycheMoodEmotionalityCTooltip".Translate()), 2);
            listing_Standard.Gap(6f);
            RimpsycheDispositionSettings.moodOptimismC = (float)Math.Round(listing_Standard.SliderLabeled("RimpsycheMoodOptimismC".Translate() + " (" + "Default".Translate() + " 0.2): " + RimpsycheDispositionSettings.moodOptimismC, RimpsycheDispositionSettings.moodOptimismC, 0.05f, 0.95f, tooltip: "RimpsycheMoodOptimismCTooltip".Translate()), 2);
            listing_Standard.Gap(6f);
            RimpsycheDispositionSettings.moodPreceptC = (float)Math.Round(listing_Standard.SliderLabeled("RimpsycheMoodPreceptC".Translate() + " (" + "Default".Translate() + " 0.2): " + RimpsycheDispositionSettings.moodPreceptC, RimpsycheDispositionSettings.moodPreceptC, 0.05f, 0.95f, tooltip: "RimpsycheMoodPreceptCTooltip".Translate()), 2);
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheUseIndividualThoughts".Translate(), ref RimpsycheDispositionSettings.useIndividualThoughts, "RimpsycheUseIndividualThoughtsTooltip".Translate());
            listing_Standard.Gap(12f);
            listing_Standard.Label("RimpsycheGameplaySettings".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheUseExperimentation".Translate(), ref RimpsycheDispositionSettings.useExperimentation, "RimpsycheUseExperimentationTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheUseSenseOfProgress".Translate(), ref RimpsycheDispositionSettings.useSenseOfProgress, "RimpsycheUseSenseOfProgressTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheUseResilientSpirit".Translate(), ref RimpsycheDispositionSettings.useResilientSpirit, "RimpsycheUseResilientSpiritTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheUseFightorFlight".Translate(), ref RimpsycheDispositionSettings.useFightorFlight, "RimpsycheUseFightorFlightTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheEnemyFightorFlight".Translate(), ref RimpsycheDispositionSettings.enemyFightorFlight, "RimpsycheEnemyFightorFlightTooltip".Translate());
            listing_Standard.Gap(24f);

            listing_Standard.Label("RimpsycheDispositionMessageSetting".Translate());
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("RimpsycheSendExperimentMessage".Translate(), ref RimpsycheDispositionSettings.sendExperimentMessage, "RimpsycheUseSendExperimentMessageTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheSendShameMessage".Translate(), ref RimpsycheDispositionSettings.sendShameMessage, "RimpsycheUseSendShameMessageTooltip".Translate());
            listing_Standard.Gap(24f);

            listing_Standard.Label("RimpsycheDispositionMoteSetting".Translate());
            listing_Standard.Gap(12f);
            listing_Standard.CheckboxLabeled("RimpsycheShowExperimentMote".Translate(), ref RimpsycheDispositionSettings.showExperimentMote, "RimpsycheShowExperimentMoteTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheShowResilientSpiritMote".Translate(), ref RimpsycheDispositionSettings.showResilientSpiritMote, "RimpsycheShowResilientSpiritMoteTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheShowAdrenalineMote".Translate(), ref RimpsycheDispositionSettings.showAdrenalineMote, "RimpsycheShowAdrenalineMoteTooltip".Translate());
            listing_Standard.Gap(6f);
            listing_Standard.CheckboxLabeled("RimpsycheShowPanicMote".Translate(), ref RimpsycheDispositionSettings.showPanicMote, "RimpsycheShowPanicMoteTooltip".Translate());
            listing_Standard.Gap(24f);

            if (listing_Standard.ButtonText("RimpsycheDispositionDefaultSetting".Translate(), "RimpsycheDispositionDefaultSettingTooltip".Translate()))
            {
                //General Moods
                RimpsycheDispositionSettings.moodEmotionalityC = RimpsycheDispositionSettings.default_moodOptimismC;
                RimpsycheDispositionSettings.moodOptimismC = RimpsycheDispositionSettings.default_moodEmotionalityC;
                RimpsycheDispositionSettings.moodPreceptC = RimpsycheDispositionSettings.default_moodPreceptC;

                RimpsycheDispositionSettings.useExperimentation = true;
                RimpsycheDispositionSettings.useSenseOfProgress = true;
                RimpsycheDispositionSettings.useResilientSpirit = true;
                RimpsycheDispositionSettings.useFightorFlight = true;
                RimpsycheDispositionSettings.enemyFightorFlight = false;

                //UI
                RimpsycheDispositionSettings.sendExperimentMessage = true;
                RimpsycheDispositionSettings.sendShameMessage = true;

                //Motes
                RimpsycheDispositionSettings.showExperimentMote = true;
                RimpsycheDispositionSettings.showResilientSpiritMote = true;
                RimpsycheDispositionSettings.showAdrenalineMote = true;
                RimpsycheDispositionSettings.showPanicMote = true;
            }

            listing_Standard.End();
            Widgets.EndScrollView();
        }
    }
}