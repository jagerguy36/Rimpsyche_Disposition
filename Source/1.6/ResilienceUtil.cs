using RimWorld;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    [StaticConstructorOnStartup]
    public static class ResilienceUtil
    {
        public static bool TestResilientSpirit(MentalBreakDef mentalbreak, Pawn pawn)
        {
            Log.Message($"Test util called with {mentalbreak.defName}, {pawn.Name}");
            var compPsyche = pawn.compPsyche();
            if (compPsyche?.Enabled != true)
            {
                return false;
            }
            if (compPsyche.lastResilientSpiritTick > Find.TickManager.TicksGame)
            {
                Log.Message($"Already resilient");
                return true;
            }
            if (compPsyche.lastResilientSpiritTick + 3600000 > Find.TickManager.TicksGame)
            {
                Log.Message($"Too recent");
                return false;
            }
            if (Rand.Chance(compPsyche.Evaluate(ResilientSpiritChance)))
            {
                Log.Message($"Chance Success");
                compPsyche.lastResilientSpiritTick = Find.TickManager.TicksGame + 180000; //3days
                //Add thought
                pawn.needs.mood.thoughts.memories.TryGainMemory(DefOfDisposition.Rimpsyche_ResilientSpirit);
                //Send letter
                if (!PawnUtility.ShouldSendNotificationAbout(pawn))
                {
                    return true;
                }

                if (RimpsycheDispositionSettings.showResilientSpiritMote)
                {
                    MoteBubble mote = (MoteBubble)ThingMaker.MakeThing(DefOfDisposition.RimpsycheMote_ResilientSpirit, null);
                    mote.Attach(pawn);
                    GenSpawn.Spawn(mote, pawn.Position, pawn.Map);
                }
                TaggedString label = "RP_ResilientSpiritLabel".Translate() + ": " + pawn.LabelShortCap;
                TaggedString taggedString = "RP_ResilientSpiritMessage".Translate(pawn.Label, pawn.Named("PAWN"), mentalbreak.Named("MENTALBREAK")).CapitalizeFirst();
                taggedString = taggedString.AdjustedFor(pawn);
                Find.LetterStack.ReceiveLetter(label, taggedString, LetterDefOf.PositiveEvent, pawn);
                return true;
            }
            Log.Message($"Chance fail");
            return false;

        }
        //TODO: determine formula
        public static RimpsycheFormula ResilientSpiritChance = new(
            "ResilientSpiritChance",
            (tracker) =>
            {
                float mult = 0.125f * (1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Resilience)) + 0.025f * (1 + tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Discipline));
                return mult;
            },
            RimpsycheFormulaManager.FormulaIdDict
        );
    }
}
