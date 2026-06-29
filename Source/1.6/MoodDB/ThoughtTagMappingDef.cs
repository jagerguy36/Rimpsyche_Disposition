using System.Collections.Generic;
using Verse;

namespace Maux36.RimPsyche.Disposition
{
    public enum ThoughtTag
    {
        None,
        Tag_Empathy,
        Tag_Empathy_Bond,
        Tag_Empathy_Kin,
        Tag_Empathy_Loved,
        Tag_Empathy_J,
        Tag_Empathy_M,
        Tag_Sympathy,
        Tag_Sympathy_J,
        Tag_Sympathy_M,
        Tag_Sympathy_P,
        Tag_Worry,
        Tag_Worry_Bond,
        Tag_Worry_Kin,
        Tag_Worry_Loved,
        Tag_Worry_Outsider,
        Tag_Worry_Outsider_M,
        Tag_Worry_M,
        Tag_Worry_J,
        Tag_Disquiet,
        Tag_Harmed,
        Tag_Harmed_Bond,
        Tag_Harmed_Kin,
        Tag_Harmed_Loved,
        Tag_Affluence,
        Tag_Needy,
        Tag_Needy_Art,
        Tag_Fear,
        Tag_SawDeath,
        Tag_SawDeath_Kin,
        Tag_SawDeath_Outsider,
        Tag_Charity_J,
        Tag_Charity_M,
        Tag_Decency,
        Tag_Decency_J,
        Tag_Decency_M,
        Tag_Art,
        Tag_Gathering,
        Tag_Recluse,
        Tag_Concert,
        Tag_Bloodlust,
        Tag_Outsider,
        Tag_Loved,
        Tag_Bond,
        Tag_Morality,
        Tag_Judgemental,
        Tag_Preference,
        Tag_Openmindedness,
        // Only Stage
        Tag_JustifiedGuilt
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