using HarmonyLib;
using System.Reflection;
using UnityEngine;
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
                Log.Message($"Social Butterfly chance mult for {pawn.LabelShort}: {compPsyche.Evaluate(ButterflyJoinMult)}");
                __result *= compPsyche.Evaluate(ButterflyJoinMult);
            }
        }

        public static RimpsycheFormula ButterflyJoinMult = new(
            "ButterflyJoinMult",
            (tracker) =>
            {
                float reserved = 1 + Mathf.Min(0f, tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Sociability));
                return reserved;
            },
        RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
