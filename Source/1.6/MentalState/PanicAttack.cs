using RimWorld;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public class PanicAttack : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }
    }
}