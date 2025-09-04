using RimWorld;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public class MentalState_PanicAttack : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }

        //TODO: Add recovery condition
    }
}