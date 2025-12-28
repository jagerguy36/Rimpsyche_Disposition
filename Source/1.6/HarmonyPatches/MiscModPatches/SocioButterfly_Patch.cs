using HarmonyLib;
using System.Reflection;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class SocioButterfly_Patch
    {
        [HarmonyPatch]
        public static class JoinInRecrationChance_Patch
        {
            public static bool Prepare()
            {
                if (ModsConfig.IsActive("lovelydovey.recreation.witheuterpe"))
                    return true;
                return false;
            }
            static MethodBase TargetMethod()
            {
                var type = AccessTools.TypeByName("RecreationalSexWithEuterpe.SocialUtilities");
                return AccessTools.Method(type, "GetPawnJoinRecreationChance");
            }
            public static void Postfix(ref float __result, Pawn pawn)
            {
                var compPsyche = pawn.compPsyche();
                if (compPsyche?.Enabled != true)
                    return;
                __result *= compPsyche.Evaluate(ButterflyJoinMult); //0~[0.5]~1
            }
        }

        public static RimpsycheFormula ButterflyJoinMult = new(
            "ButterflyJoinMult",
            (tracker) =>
            {
                float sociability = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability);
                if (sociability < 0f)
                {
                    var x = sociability + 1f;
                    return 0.5f * x * x * x;
                }
                else
                {
                    var y = sociability - 1f;
                    return -0.5f * y * y + 1f;
                }
            },
        RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
