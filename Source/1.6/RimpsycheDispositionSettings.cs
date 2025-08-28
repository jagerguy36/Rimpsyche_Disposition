using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class RimpsycheDispositionSettings : ModSettings
    {

        public static bool useExperimentation = true;
        public static bool useSenseOfProgress = true;
        public static bool useResilientSpirit = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref useExperimentation, "Rimpsyche_useExperimentation", true, true);
            Scribe_Values.Look(ref useSenseOfProgress, "Rimpsyche_useSenseOfProgress", true, true);
            Scribe_Values.Look(ref useResilientSpirit, "Rimpsyche_useResilientSpirit", true, true);
        }
    }
}
