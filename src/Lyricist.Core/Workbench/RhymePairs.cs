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

    public sealed class RhymePair
    {
        public RhymePair (LineEnd a, LineEnd b, RhymeKind kind)
        {
            A = a;
            B = b;
            Kind = kind;
        }

        public LineEnd A { get; }
        public LineEnd B { get; }
        public RhymeKind Kind { get; }
    }

    /// <summary>
    /// Pairs of ideas whose lines end in rhyming words: ready-made couplets. Each line of an idea's
    /// text counts (a one-liner has one); lines from the same idea aren't paired with each other,
    /// and the same word twice isn't a rhyme.
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

        public static List<RhymePair> Find (IEnumerable<Lyric> lyrics, Rhymer rhymer, bool includeNear)
        {
            List<LineEnd> ends = LineEnds (lyrics);

            // Only lines whose words share a "same vowels" key can rhyme: compare within those groups.
            var groups = new Dictionary<string, List<int>> (StringComparer.Ordinal);
            for (int i = 0; i < ends.Count; i++)
                foreach (string key in rhymer.VowelKeys (ends[i].LastWord))
                {
                    if (!groups.TryGetValue (key, out List<int> members))
                        groups[key] = members = new List<int>();
                    members.Add (i);
                }

            var pairs = new List<RhymePair>();
            var seen = new HashSet<long>();
            var compared = new Dictionary<string, RhymeKind> (StringComparer.Ordinal);

            foreach (List<int> members in groups.Values)
                for (int x = 0; x < members.Count; x++)
                    for (int y = x + 1; y < members.Count; y++)
                    {
                        int i = Math.Min (members[x], members[y]), j = Math.Max (members[x], members[y]);
                        if (i == j || !seen.Add (((long) i << 32) | (uint) j))
                            continue;
                        LineEnd a = ends[i], b = ends[j];
                        if (ReferenceEquals (a.Lyric, b.Lyric))
                            continue;
                        string wa = PronouncingDictionary.Normalise (a.LastWord), wb = PronouncingDictionary.Normalise (b.LastWord);
                        if (wa == wb)
                            continue;

                        string pairKey = string.CompareOrdinal (wa, wb) < 0 ? wa + "|" + wb : wb + "|" + wa;
                        if (!compared.TryGetValue (pairKey, out RhymeKind kind))
                            compared[pairKey] = kind = rhymer.Compare (wa, wb);

                        if (kind == RhymeKind.Perfect || (includeNear && kind == RhymeKind.Near))
                            pairs.Add (new RhymePair (a, b, kind));
                    }

            return pairs.OrderByDescending (p => p.Kind)
                        .ThenBy (p => PronouncingDictionary.Normalise (p.A.LastWord), StringComparer.Ordinal)
                        .ToList();
        }
    }
}
