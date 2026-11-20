using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DumbAngel;
using Lyricist.Core.Words;

namespace Lyricist.Core.Workbench
{
    /// <summary>One line of an idea and the word it ends on.</summary>
    public sealed class LineEnd
    {
        public LineEnd (Lyric lyric, string line, string lastWord)
        {
            Lyric = lyric;
            Line = line;
            LastWord = lastWord;
        }

        public Lyric Lyric { get; }
        public string Line { get; }
        public string LastWord { get; }
    }

    /// <summary>Lines whose last words rhyme perfectly with each other (the "-ight" family...).</summary>
    public sealed class RhymeFamily
    {
        public string Key { get; set; }                       // "aɪ t"
        public List<LineEnd> Lines { get; } = new List<LineEnd>();
        public List<string> Words { get; } = new List<string>();   // distinct last words
        public int Ideas => Lines.Select (l => l.Lyric).Distinct().Count();
    }

    /// <summary>
    /// Ready-made couplets, as rhyme families: every line of every idea, grouped by the rhyme
    /// sound its last word ends on. A family needs two different words from two different ideas
    /// (ten lines ending in "you" aren't a rhyme). Pairing every line with every other was tried
    /// first (0.3.0) and swamped the screen: a family of 30 lines is 435 pairs.
    /// Near rhymes for a family are worked out when it's picked (NearLines).
    /// </summary>
    public static class RhymePairs
    {
        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+(?:-[A-Za-z’']+)*", RegexOptions.Compiled);

        public static List<LineEnd> LineEnds (IEnumerable<Lyric> lyrics)
        {
            var ends = new List<LineEnd>();
            foreach (Lyric lyric in lyrics)
            {
                if (string.IsNullOrWhiteSpace (lyric?.Text))
                    continue;
                foreach (string raw in lyric.Text.Split (new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MatchCollection words = wordPattern.Matches (raw);
                    if (words.Count > 0)
                        ends.Add (new LineEnd (lyric, raw.Trim(), words[words.Count - 1].Value));
                }
            }
            return ends;
        }

        public static List<RhymeFamily> Families (IList<LineEnd> ends, Rhymer rhymer)
        {
            var byKey = new Dictionary<string, RhymeFamily> (StringComparer.Ordinal);
            foreach (LineEnd end in ends)
                foreach (string key in rhymer.RhymeKeys (end.LastWord))
                {
                    if (!byKey.TryGetValue (key, out RhymeFamily family))
                        byKey[key] = family = new RhymeFamily { Key = key };
                    family.Lines.Add (end);
                }

            var families = new List<RhymeFamily>();
            foreach (RhymeFamily family in byKey.Values)
            {
                foreach (string word in family.Lines.Select (l => PronouncingDictionary.Normalise (l.LastWord)).Distinct())
                    family.Words.Add (word);
                // Two different words, in two different ideas.
                bool rhymes = family.Lines.Any (a => family.Lines.Any (b =>
                    !ReferenceEquals (a.Lyric, b.Lyric)
                    && PronouncingDictionary.Normalise (a.LastWord) != PronouncingDictionary.Normalise (b.LastWord)));
                if (!rhymes)
                    continue;
                family.Lines.Sort ((a, b) => string.CompareOrdinal (PronouncingDictionary.Normalise (a.LastWord),
                                                                   PronouncingDictionary.Normalise (b.LastWord)));
                families.Add (family);
            }
            return families.OrderByDescending (f => f.Words.Count)
                           .ThenByDescending (f => f.Ideas)
                           .ThenBy (f => f.Key, StringComparer.Ordinal)
                           .ToList();
        }

        /// <summary>Lines (not in the family) whose last word is a near rhyme for one of its words.</summary>
        public static List<LineEnd> NearLines (RhymeFamily family, IList<LineEnd> ends, Rhymer rhymer)
        {
            var inFamily = new HashSet<LineEnd> (family.Lines);
            var memo = new Dictionary<string, bool> (StringComparer.Ordinal);
            var near = new List<LineEnd>();
            foreach (LineEnd end in ends)
            {
                if (inFamily.Contains (end))
                    continue;
                string word = PronouncingDictionary.Normalise (end.LastWord);
                if (!memo.TryGetValue (word, out bool isNear))
                    memo[word] = isNear = family.Words.Any (w => rhymer.Compare (w, word) == RhymeKind.Near);
                if (isNear)
                    near.Add (end);
            }
            return near;
        }
    }
}
