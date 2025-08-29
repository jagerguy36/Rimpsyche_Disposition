using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class RimpsycheDispositionSettings : ModSettings
    {

        public static bool useExperimentation = true;
        public static bool useSenseOfProgress = true;
        public static bool useResilientSpirit = true;

        //UI
        public static bool sendExperimentMessage = true;
        public static bool showExperimentMote = true;

        public static bool showResilientSpiritMote = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref useExperimentation, "Rimpsyche_useExperimentation", true, true);
            Scribe_Values.Look(ref useSenseOfProgress, "Rimpsyche_useSenseOfProgress", true, true);
            Scribe_Values.Look(ref useResilientSpirit, "Rimpsyche_useResilientSpirit", true, true);
            
            Scribe_Values.Look(ref sendExperimentMessage, "Rimpsyche_sendExperimentMessage", true, true);
            Scribe_Values.Look(ref showExperimentMote, "Rimpsyche_showExperimentMote", true, true);
            Scribe_Values.Look(ref showResilientSpiritMote, "Rimpsyche_showResilientSpiritMote", true, true);
        }
    }
}
