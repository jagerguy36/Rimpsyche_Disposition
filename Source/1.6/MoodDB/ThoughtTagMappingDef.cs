using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public enum ThoughtTag
    {
        None,
        Tag_Affluence,
        Tag_Morality,
        Tag_Judgemental
    }

    public class ThoughtTagMappingDef : Def
    {
        public List<ThoughtTagMapping> moodThoughtMaps = new List<ThoughtTagMapping>();
        public List<ThoughtTagMapping> opinionThoughtMaps = new List<ThoughtTagMapping>();
        public List<StageThoughtTagMapping> stageMoodThoughtMaps = new List<StageThoughtTagMapping>();
        public List<StageThoughtTagMapping> stageOpinionThoughtMaps = new List<StageThoughtTagMapping>();
    }

    public class ThoughtTagMapping
    {
        public ThoughtTag tag;
        public List<string> defNames;
    }

    public class StageThoughtTagMapping
    {
        public string defName;
        public List<ThoughtTag> stageTags = new List<ThoughtTag>();
    }
}