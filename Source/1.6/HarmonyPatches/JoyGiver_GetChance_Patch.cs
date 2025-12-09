using HarmonyLib;
using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("IndividualJoyChance")]
    [HarmonyPatch(typeof(JoyGiver), "GetChance")]
    public static class JoyGiver_GetChance_Patch
    {
        public static void Postfix(ref float __result, JoyGiver __instance, Pawn pawn)
        {
            if (__result == 0f || pawn == null)
                return;
            var compPsyche = pawn.compPsyche();
            if (compPsyche == null || !compPsyche.Enabled)
                return;
            int hashKey = __instance.def.shortHash;
            var cache = compPsyche.JoyChanceEvaluationCache;
            //cache hit
            if (cache.TryGetValue(hashKey, out float value))
            {
                if (value >= 0f)
                    __result *= value;
                return;
            }
            //cache miss
            float eval = -1f;
            if (JoyGiverUtil.JoyChanceDB.TryGetValue(hashKey, out RimpsycheFormula joychanceFormula))
            {
                //Log.Message($"{pawn.Name} registered {__instance.def.defName} chance. Key: {hashKey}");
                if (joychanceFormula != null)
                {
                    eval = compPsyche.Evaluate(joychanceFormula);
                    __result *= eval;
                }
            }
            //if (eval <0) Log.Message($"{pawn.Name} blacklisted {__instance.def.defName}.");
            cache[hashKey] = eval;
            return;
        }
    }
}
