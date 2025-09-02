using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition
{
    public class InteractionWorker_ShameOverwhelmed : InteractionWorker
    {
        private const int overwhelmIntervalTick = 5000;
        public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
        {
            return 0;
        }

        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks, out string letterText, out string letterLabel, out LetterDef letterDef, out LookTargets lookTargets)
        {
            letterText = null;
            letterLabel = null;
            letterDef = null;
            lookTargets = null;
            var initiatorPsyche = initiator.compPsyche();
            if (initiatorPsyche?.Enabled == true)
            {
            }

        }
    }
}

