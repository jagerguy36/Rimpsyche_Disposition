using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition.Ideology
{
    [HarmonyPatch(typeof(InteractionWorker_EnslaveAttempt), nameof(InteractionWorker_EnslaveAttempt.Interacted))]
    public static class InteractionWorker_EnslaveAttempt_Interacted
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var code = new List<CodeInstruction>(instructions);
            var negotiationAbilityField = AccessTools.Field(typeof(StatDefOf), nameof(StatDefOf.NegotiationAbility));
            var getStatValueMethod = AccessTools.Method( typeof(StatExtension), nameof(StatExtension.GetStatValue),
                new[] { typeof(Thing), typeof(StatDef), typeof(bool), typeof(int) }
            );
            var mMultiplier = AccessTools.Method(typeof(WillMult), nameof(WillMult.WillMultiplier), new[] { typeof(Pawn) });

            var fldOpinion = AccessTools.Field(typeof(InteractionWorker_RecruitAttempt), "ResistanceImpactFactorCurve_Opinion");
            var mEvaluate = AccessTools.Method(typeof(SimpleCurve), nameof(SimpleCurve.Evaluate), new[] { typeof(float) });

            bool injected = false;

            for (int i = 0; i < code.Count; i++)
            {
                var instr = code[i];
                if (!injected && code[i].opcode == OpCodes.Mul && i >= 4)
                {
                    bool isGetStatValue = code[i - 1].opcode == OpCodes.Call && code[i - 1].operand as System.Reflection.MethodInfo == getStatValueMethod;
                    bool isNegotiationAbility = code[i - 4].opcode == OpCodes.Ldsfld && code[i - 4].operand as System.Reflection.FieldInfo == negotiationAbilityField;

                    if (isGetStatValue && isNegotiationAbility)
                    {
                        // Inject our custom multiplier logic right after the existing mul
                        yield return new CodeInstruction(OpCodes.Ldarg_2); // Load recipient
                        yield return new CodeInstruction(OpCodes.Call, mMultiplier); // Call multiplier
                        yield return new CodeInstruction(OpCodes.Mul); // Multiply it into the local variable
                        injected = true;
                    }
                }
                yield return instr;
            }
            if (!injected) Log.Warning("[RimPsyche - Disposition] InteractionWorker_EnslaveAttempt_Interacted: failed to inject multiplier (pattern not found).");
        }
    }

    public static class WillMult
    {
        public static float WillMultiplier(Pawn recipient)
        {
            var compPsyche = recipient.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return 1f;
            }
            var tenacity = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Tenacity);
            //Log.Message($"Pawn {recipient.Name} tenacity: {tenacity}. | Multiplier: {1f + -tenacity * 0.25f}");
            return 1f + - (tenacity * 0.25f);
        }
    }
}
