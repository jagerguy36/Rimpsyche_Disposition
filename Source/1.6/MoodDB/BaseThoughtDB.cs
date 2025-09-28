using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class BaseThoughtDB
    {
        public static void RegisterThoughts<T>(IEnumerable<string> defNames, Dictionary<int, T> targetDb, T value)
        {
            foreach (var defName in defNames)
            {
                var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
                if (thoughtDef != null)
                {
                    targetDb[thoughtDef.shortHash] = value;
                }
                else
                {
                    Log.Error($"[Rimpsyche] Could not find ThoughtDef named '{defName}'.");
                }
            }
        }
        public static void RegisterSingleThought<T>(string defName, Dictionary<int, T> targetDb, T value)
        {
            var thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defName, false);
            if (thoughtDef != null)
            {
                targetDb[thoughtDef.shortHash] = value;
            }
            else
            {
                Log.Error($"[Rimpsyche] Could not find ThoughtDef named '{defName}'.");
            }
        }
    }
}
