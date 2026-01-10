using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatchCategory("TheSimsTraits")]
    [HarmonyPatch(typeof(QualityUtil), nameof(QualityUtil.GenerateQualityCreatedByPawnWithPsyche))]
    public class TheSimsTraits_QualityUtil_GenerateQualityCreatedByPawnWithPsyche_Patch
    {
        public static void Prefix(Pawn pawn, ref float numOffset)
        {
            if (IntegrationDatabase.ST_Procrastinator != null && (pawn?.story?.traits?.HasTrait(IntegrationDatabase.ST_Procrastinator) ?? false))
            {
                numOffset = -1f;
            }
        }
    }

    public class TheSimsAddrenalinePatches
    {
        [HarmonyPatch]
        public static class ParanoidAdrenaline_Patch
        {
            public static bool Prepare()
            {
                if (ModsConfig.IsActive("goji.thesimstraits") && RimpsycheDispositionSettings.useFightorFlight)
                    return true;
                return false;
            }
            static MethodBase TargetMethod()
            {
                var targetType = AccessTools.TypeByName("SimsTraits.LetterStack_ReceiveLetter_Patch");
                return AccessTools.Method(targetType, "Postfix");
            }

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = instructions.ToList();
                var addHediffMethod = AccessTools.Method(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.AddHediff), new Type[]
                {
                    typeof(HediffDef),
                    typeof(BodyPartRecord),
                    typeof(DamageInfo?),
                    typeof(DamageWorker.DamageResult)
                });
                var newHediffField = AccessTools.Field(typeof(DefOfDisposition), nameof(DefOfDisposition.Rimpsyche_AdrenalineRush));
                var adjustSeverityMethod = AccessTools.Method(typeof(HealthUtility), nameof(HealthUtility.AdjustSeverity));

                for (int i = 0; i < codes.Count; i++)
                {
                    if (i + 8 < codes.Count && codes[i].opcode == OpCodes.Ldloc_S && codes[i + 8].Calls(addHediffMethod))
                    {
                        yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Ldsfld, newHediffField);
                        yield return new CodeInstruction(OpCodes.Ldc_R4, 1f);
                        yield return new CodeInstruction(OpCodes.Call, adjustSeverityMethod);

                        // Need to remove the pop IL line as well.
                        i += 9;
                        continue;
                    }
                    yield return codes[i];
                }
            }
        }

        [HarmonyPatch]
        public static class DaredevilAdrenaline_Patch
        {
            public static bool Prepare()
            {
                if (ModsConfig.IsActive("goji.thesimstraits") && RimpsycheDispositionSettings.useFightorFlight)
                    return true;
                return false;
            }
            static IEnumerable<MethodBase> TargetMethods()
            {
                yield return AccessTools.Method(AccessTools.TypeByName("SimsTraits.Thing_TakeDamage_Patch"), "Postfix");
                yield return AccessTools.Method(AccessTools.TypeByName("SimsTraits.Verb_TryCastNextBurstShot_Patch"), "Prefix");
            }

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = instructions.ToList();
                var addHediffMethod = AccessTools.Method(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.AddHediff), new Type[]
                {
                    typeof(HediffDef),
                    typeof(BodyPartRecord),
                    typeof(DamageInfo?),
                    typeof(DamageWorker.DamageResult)
                });
                var newHediffField = AccessTools.Field(typeof(DefOfDisposition), nameof(DefOfDisposition.Rimpsyche_AdrenalineRush));
                var adjustSeverityMethod = AccessTools.Method(typeof(HealthUtility), nameof(HealthUtility.AdjustSeverity));
                for (int i = 0; i < codes.Count; i++)
                {
                    if (i + 8 < codes.Count && codes[i + 8].Calls(addHediffMethod))
                    {
                        yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Ldsfld, newHediffField);
                        //To make RP_adrenaline dissapear in 600tick (the same as TS_adrenaline), the severity needs to be 0.048f
                        //However, since RP_adrenaline does not grant any bonus until sev becomes 0.1, we're buffing it a bit.
                        //This stacks with bravety fight or flight. But with Daredevil, even gentle pawns can get adrenaline.
                        yield return new CodeInstruction(OpCodes.Ldc_R4, 0.060f);
                        yield return new CodeInstruction(OpCodes.Call, adjustSeverityMethod);

                        // Need to remove the pop IL line as well.
                        i += 9;
                        continue;
                    }
                    yield return codes[i];
                }
            }
        }
    }
}
