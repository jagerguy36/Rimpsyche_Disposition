using RimWorld;
using UnityEngine;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public class Rimpsyche : Mod
    {
        public Rimpsyche(ModContentPack content) : base(content)
        {
            if (!ModsConfig.IsActive("maux36.rimpsyche"))
            {
                Log.Error("[Rimpsyche Disposition] Rimpsyche not loaded. The dependency was not met and the game will not run correctly");
            }
        }
    }
}