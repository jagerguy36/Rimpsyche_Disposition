using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class PsycheOrganizationRoomWorkspeedStatPart : StatPart// M 0.8 ~ 1.2
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    val *= compPsyche.roomRoleFactor;
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing is Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche != null)
                {
                    float roomRoleFactor = compPsyche.roomRoleFactor;
                    if (roomRoleFactor == 1f)
                    {
                        return null;
                    }
                    return "RP_Stat_OrganizationRoomWorkspeed".Translate() + ": x" + roomRoleFactor.ToStringPercent();
                }
            }
            return null;
        }
    }
}
