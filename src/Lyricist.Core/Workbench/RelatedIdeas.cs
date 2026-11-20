using System;
using System.Collections.Generic;
using System.Linq;
using DumbAngel;
using Lyricist.Core.Words;

namespace Lyricist.Core.Workbench
{
    public sealed class RelatedIdea
    {
        public Lyric Lyric { get; set; }
        public int Score { get; set; }
        public List<string> Reasons { get; } = new List<string>();
        public string ReasonText => string.Join ("; ", Reasons);
    }

    /// <summary>
    /// Ideas related to one idea, with the reasons: its line endings rhyme (perfect 3, near 2), it
    /// shares words (2 each), it uses synonyms of the idea's words (1 each), it shares themes
    /// (1 each; only from two shared themes up, or everything would match).
    /// </summary>
    public static class RelatedIdeas
    {
        public static List<RelatedIdea> Find (Lyric focus, IEnumerable<Lyric> lyrics, Rhymer rhymer,
                                              PronouncingDictionary dictionary, Thesaurus thesaurus, int max = 200)
        {
            List<LineEnd> focusEnds = RhymePairs.LineEnds (new[] { focus });
            HashSet<string> focusWords = RecurringWords.ContentWords (focus.Text, dictionary);

            // synonym -> the focus word it stands for
            var synonyms = new Dictionary<string, string> (StringComparer.Ordinal);
            if (thesaurus != null)
                foreach (string word in focusWords)
                    foreach (string synonym in RecurringWords.SynonymsOf (word, thesaurus, dictionary))
                        if (!focusWords.Contains (synonym) && !synonyms.ContainsKey (synonym))
                            synonyms[synonym] = word;

            List<LyricFlag> focusThemes = LyricFlags.Themes.Where (f => LyricFlags.IsSet (focus, f)).ToList();
            var rhymeMemo = new Dictionary<string, RhymeKind> (StringComparer.Ordinal);
            var results = new List<RelatedIdea>();

            foreach (Lyric other in lyrics)
            {
                if (other == null || ReferenceEquals (other, focus) || other.Oid == focus.Oid || string.IsNullOrWhiteSpace (other.Text))
                    continue;
                var related = new RelatedIdea { Lyric = other };

                // Rhyming line endings
                foreach (LineEnd end in RhymePairs.LineEnds (new[] { other }))
                    foreach (LineEnd mine in focusEnds)
                    {
                        string a = PronouncingDictionary.Normalise (mine.LastWord), b = PronouncingDictionary.Normalise (end.LastWord);
                        if (a == b)
                            continue;
                        string key = string.CompareOrdinal (a, b) < 0 ? a + "|" + b : b + "|" + a;
                        if (!rhymeMemo.TryGetValue (key, out RhymeKind kind))
                            rhymeMemo[key] = kind = rhymer.Compare (a, b);
                        if (kind == RhymeKind.Perfect || kind == RhymeKind.Near)
                        {
                            related.Score += kind == RhymeKind.Perfect ? 3 : 2;
                            string reason = $"{(kind == RhymeKind.Perfect ? "rhymes" : "near rhyme")}: {mine.LastWord} / {end.LastWord}";
                            if (!related.Reasons.Contains (reason))
                                related.Reasons.Add (reason);
                        }
                    }

                // Shared words, synonyms
                HashSet<string> words = RecurringWords.ContentWords (other.Text, dictionary);
                List<string> shared = words.Where (focusWords.Contains).OrderBy (w => w, StringComparer.Ordinal).ToList();
                if (shared.Count > 0)
                {
                    related.Score += 2 * shared.Count;
                    related.Reasons.Add ("same word" + (shared.Count > 1 ? "s" : "") + ": " + string.Join (", ", shared));
                }
                List<string> viaSynonym = words.Where (synonyms.ContainsKey)
                                               .Select (w => $"{w} (~{synonyms[w]})")
                                               .OrderBy (w => w, StringComparer.Ordinal).ToList();
                if (viaSynonym.Count > 0)
                {
                    related.Score += viaSynonym.Count;
                    related.Reasons.Add ("synonym" + (viaSynonym.Count > 1 ? "s" : "") + ": " + string.Join (", ", viaSynonym));
                }

                // Shared themes
                List<LyricFlag> sharedThemes = focusThemes.Where (f => LyricFlags.IsSet (other, f)).ToList();
                if (sharedThemes.Count >= 2)
                {
                    related.Score += sharedThemes.Count;
                    related.Reasons.Add ("themes: " + string.Join (", ", sharedThemes.Select (f => f.ShortCaption)));
                }

                if (related.Score > 0)
                    results.Add (related);
            }

            return results.OrderByDescending (r => r.Score)
                          .ThenByDescending (r => r.Lyric.Rating?.Oid ?? 0)
                          .Take (max)
                          .ToList();
        }
    }
}
