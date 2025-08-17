using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), nameof(InteractionWorker_RecruitAttempt.Interacted))]
    public static class InteractionWorker_RecruitAttempt_Interacted
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var code = new List<CodeInstruction>(instructions);
            var fldOpinion = AccessTools.Field(typeof(InteractionWorker_RecruitAttempt), "ResistanceImpactFactorCurve_Opinion");
            var mEvaluate = AccessTools.Method(typeof(SimpleCurve), nameof(SimpleCurve.Evaluate), new[] { typeof(float) });
            var mMultiplier = AccessTools.Method(typeof(ResistanceMult), nameof(ResistanceMult.ResistanceMultiplier), new[] { typeof(Pawn) });

            bool sawOpinionLoad = false;
            bool injected = false;

            for (int i = 0; i < code.Count; i++)
            {
                var instr = code[i];

                // Track: ldsfld ...Opinion -> (anything) -> callvirt SimpleCurve::Evaluate -> stloc.*
                if (instr.opcode == OpCodes.Ldsfld && Equals(instr.operand, fldOpinion))
                {
                    sawOpinionLoad = true;
                }
                //Evaluate of ResistanceImpactFactorCurve_Opinion
                else if (sawOpinionLoad && instr.Calls(mEvaluate))
                {
                    if (i + 1 < code.Count && code[i + 1].IsStloc())
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_2); // recipient (this:0, initiator:1, recipient:2)
                        yield return new CodeInstruction(OpCodes.Call, mMultiplier);
                        yield return new CodeInstruction(OpCodes.Mul);
                        injected = true;
                    }
                    sawOpinionLoad = false;
                }
                yield return instr;
            }
            if (!injected) Log.Warning("[Rimpsyche] InteractionWorker_RecruitAttempt_Interacted: failed to inject multiplier (pattern not found).");
        }
    }

    public static class ResistanceMult
    {
        public static float ResistanceMultiplier(Pawn recipient)
        {
            var compPsyche = recipient.compPsyche();
            if (compPsyche == null)
            {
                return 1f;
            }
            var loyalty = compPsyche.Personality.GetPersonality(PersonalityDefOf.Rimpsyche_Loyalty);
            //Log.Message($"Pawn {recipient.Name} loyalty: {loyalty}. | Multiplier: {1f + -loyalty * 0.5f}");
            return 1f + - (loyalty * 0.5f);
        }
    }
}
