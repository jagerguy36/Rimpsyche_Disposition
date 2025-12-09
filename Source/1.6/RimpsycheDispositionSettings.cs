using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class RimpsycheDispositionSettings : ModSettings
    {
        public const float default_moodOptimismC = 0.3f;
        public const float default_moodEmotionalityC = 0.3f;
        public const float default_moodIndividualC = 0.6f;

        public static bool useIndividualThoughts = true;
        public static bool useIndividualJoychance = true;
        public static bool usePerformanceModeThought = false;
        public static bool useExperimentation = true;
        public static bool useSenseOfProgress = true;
        public static bool useResilientSpirit = true;
        public static bool useHideInShame = true;
        public static bool checkMedicalOverShame = false;
        public static bool useFightorFlight = true;
        public static bool enemyFightorFlight = false;

        public static float moodOptimismC = default_moodOptimismC;
        public static float moodEmotionalityC = default_moodEmotionalityC;
        public static float moodIndividualC = default_moodIndividualC;

        //UI
        public static bool sendExperimentMessage = true;
        public static bool sendShameMessage = true;

        //Motes
        public static bool showExperimentMote = true;
        public static bool showResilientSpiritMote = true;
        public static bool showAdrenalineMote = true;
        public static bool showPanicMote = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref useIndividualThoughts, "Rimpsyche_useIndividualThoughts", true, true);
            //Scribe_Values.Look(ref useIndividualJoychance, "Rimpsyche_useIndividualJoychance", true, true);
            Scribe_Values.Look(ref usePerformanceModeThought, "Rimpsyche_usePerformanceModeThought", false, true);
            Scribe_Values.Look(ref useExperimentation, "Rimpsyche_useExperimentation", true, true);
            Scribe_Values.Look(ref useSenseOfProgress, "Rimpsyche_useSenseOfProgress", true, true);
            Scribe_Values.Look(ref useResilientSpirit, "Rimpsyche_useResilientSpirit", true, true);
            Scribe_Values.Look(ref useHideInShame, "Rimpsyche_useHideInShame", true, true);
            Scribe_Values.Look(ref checkMedicalOverShame, "Rimpsyche_checkMedicalOverShame", false, true);
            Scribe_Values.Look(ref useFightorFlight, "Rimpsyche_useFightorFlight", true, true);
            Scribe_Values.Look(ref enemyFightorFlight, "Rimpsyche_enemyFightorFlight", false, true);


            Scribe_Values.Look(ref moodOptimismC, "Rimpsyche_moodOptimismC", default_moodOptimismC, true);
            Scribe_Values.Look(ref moodEmotionalityC, "Rimpsyche_moodEmotionalityC", default_moodEmotionalityC, true);
            Scribe_Values.Look(ref moodIndividualC, "Rimpsyche_moodIndividualC", default_moodIndividualC, true);

            Scribe_Values.Look(ref sendExperimentMessage, "Rimpsyche_sendExperimentMessage", true, true);
            Scribe_Values.Look(ref sendShameMessage, "Rimpsyche_sendShameMessage", true, true);


            Scribe_Values.Look(ref showExperimentMote, "Rimpsyche_showExperimentMote", true, true);
            Scribe_Values.Look(ref showResilientSpiritMote, "Rimpsyche_showResilientSpiritMote", true, true);
            Scribe_Values.Look(ref showAdrenalineMote, "Rimpsyche_showAdrenalineMote", true, true);
            Scribe_Values.Look(ref showPanicMote, "Rimpsyche_showPanicMote", true, true);
        }
    }
}
