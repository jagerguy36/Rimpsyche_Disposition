using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition;

public class JoyGiver_Meditate_Reflectiveness : JoyGiver_Meditate
{
	public override float GetChance(Pawn pawn)
	{
		compPsyche = pawn.compPsyche();
		if(compPsyche == null)
		{
			return def.baseChance;
		}
		else
		{
			return def.baseChance*compPsyche.Personality.Evaluate(SkyGazeChanceMultiplier);;
		}
	}

	public static RimpsycheFormula MeditateChanceMultiplier = new(
		"MeditateChanceMultiplier",
		(tracker) =>
		{
			float mult = 1f;
			float imaginationMult = tracker.GetPersonalityAsMult(PersonalityDefOf.Rimpsyche_Reflectiveness, 1.5f);
			return mult * imaginationMult;
		}
	);
}