using System.Collections.Generic;

namespace Lyricist.Core
{
    /// <summary>A yes / no column on Lyric, with the caption the prototype's form gave it.</summary>
    public sealed class LyricFlag
    {
        public LyricFlag (string property, string caption, string shortCaption)
        {
            Property = property;
            Caption = caption;
            ShortCaption = shortCaption;
        }

        public string Property { get; }       // Lyric property / column name, e.g. "IsTime"
        public string Caption { get; }        // "Time / The Past"
        public string ShortCaption { get; }   // grid column heading, e.g. "Time"
    }

    /// <summary>The single list of Lyric's flags, for grids, filters and (later) prompts.</summary>
    public static class LyricFlags
    {
        /// <summary>What kind of idea it is (the first three can be "not sure").</summary>
        public static readonly IReadOnlyList<LyricFlag> Kinds = new[]
        {
            new LyricFlag ("IsOriginal", "Is Original", "Original"),
            new LyricFlag ("IsWellKnownPhrase", "Is Well Known Phrase", "WKP"),
            new LyricFlag ("IsUsedInSong", "Is Used In Song", "Used"),
            new LyricFlag ("IsConcept", "Concept / Title", "Concept"),
            new LyricFlag ("IsNotLyric", "Not Lyric", "Not Lyric"),
        };

        /// <summary>What it's about: the 15 themes.</summary>
        public static readonly IReadOnlyList<LyricFlag> Themes = new[]
        {
            new LyricFlag ("IsTime", "Time / The Past", "Time"),
            new LyricFlag ("IsAlienation", "Alienation / Isolation", "Alienation"),
            new LyricFlag ("IsViolence", "Violence / Death", "Violence"),
            new LyricFlag ("IsReligion", "Religion", "Religion"),
            new LyricFlag ("IsSlacker", "Slacker", "Slacker"),
            new LyricFlag ("IsLove", "Love / Relationships", "Love"),
            new LyricFlag ("IsSociety", "Society / Politics", "Society"),
            new LyricFlag ("IsFriends", "Friends", "Friends"),
            new LyricFlag ("IsDrinking", "Drinking", "Drinking"),
            new LyricFlag ("IsDream", "Dream / Narco / Wonder", "Dream"),
            new LyricFlag ("IsSeductive", "Seductive World / Glamour", "Seductive"),
            new LyricFlag ("IsPeople", "Specific People", "People"),
            new LyricFlag ("IsUpbeat", "Upbeat", "Upbeat"),
            new LyricFlag ("IsOther", "Other", "Other"),
            new LyricFlag ("IsGeneral", "General Use Phrase", "General"),
        };
    }
}
