using RimWorld;
using Verse;
using Verse.AI;

namespace Maux36.RimPsyche.Disposition;

public class JoyGiver_Meditate_Reflectiveness : JoyGiver_Meditate
{
	public override float GetChance(Pawn pawn)
	{
		var compPsyche = pawn.compPsyche();
		if(compPsyche.Enabled != true)
		{
			return def.baseChance;
		}
		else
		{
			return def.baseChance*compPsyche.Personality.Evaluate(MeditateChanceMultiplier);;
		}
	}

	public static RimpsycheFormula MeditateChanceMultiplier = new(
		"MeditateChanceMultiplier",
		(tracker) =>
		{
			float r = tracker.GetPersonality(PersonalityDefOf.Rimpsyche_Reflectiveness);
			float c = -2.5f * r * (r - 2f);
			if (c > 0) return c;
			return 0f;
		}
	);
}