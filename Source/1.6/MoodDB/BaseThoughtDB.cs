using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class BaseThoughtDB
    {
        public static void RegisterThoughts(IEnumerable<string> defNames, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            foreach (var defName in defNames)
            {
                var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
                if (thoughtDef != null)
                {
                    targetDb[[(0 << 16) | thoughtDef.shortHash]] = value;
                }
                else
                {
                    Log.Warning($"[Rimpsyche] Could not find ThoughtDef named '{defName}'.");
                }
            }
        }
        public static void RegisterSingleThought(string defName, Dictionary<int, RimpsycheFormula> targetDb, RimpsycheFormula value)
        {
            var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
            if (thoughtDef != null)
            {
                targetDb[(0 << 16) | thoughtDef.shortHash] = value;
            }
            else
            {
                Log.Warning($"[Rimpsyche] Could not find ThoughtDef named '{defName}'.");
            }
        }
        public static void RegisterStageThought(string defName, Dictionary<int, RimpsycheFormula> targetDb, List<RimpsycheFormula> values)
        {
            var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
            if (thoughtDef != null)
            {
                for (int i = 0; i < values.Count; i ++)
                {
                    targetDb[(i << 16) | thoughtDef.shortHash] = values[i];
                }
            }
            else
            {
                Log.Warning($"[Rimpsyche] Could not find ThoughtDef named '{defName}'.");
            }
        }
    }
}
