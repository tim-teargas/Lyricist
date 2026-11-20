using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DumbAngel;
using Lyricist.Core.Words;

namespace Lyricist.Core.Workbench
{
    public sealed class WordCount
    {
        public string Word { get; set; }
        /// <summary>Ideas using the word (or its other forms: dream / dreams / dreaming).</summary>
        public int Ideas { get; set; }
        /// <summary>Ideas using the word or any of its synonyms that appear in the collection.</summary>
        public int WithSynonyms { get; set; }
        public List<string> Synonyms { get; } = new List<string>();
        public List<Lyric> Lyrics { get; } = new List<Lyric>();
    }

    /// <summary>
    /// The words you keep coming back to. Counts ideas, not occurrences; common little words are
    /// left out; plurals and -ing / -ed forms count with their base word when the dictionary knows
    /// it. With a thesaurus, each word also gathers the ideas that use its synonyms (light: lamp,
    /// glow...), so a theme shows up even when the wording varies. Synonyms aren't chained (a
    /// synonym's synonyms don't count), or everything would end up in one group. Thematic words
    /// (time, day, never, nothing, always, ...) are deliberately not treated as little words.
    /// </summary>
    public static class RecurringWords
    {
        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+", RegexOptions.Compiled);

        public static List<WordCount> Count (IEnumerable<Lyric> lyrics, PronouncingDictionary dictionary,
                                             Thesaurus thesaurus, int minimum = 2)
        {
            // word -> the ideas using it
            var uses = new Dictionary<string, HashSet<Lyric>> (StringComparer.Ordinal);
            foreach (Lyric lyric in lyrics)
            {
                if (string.IsNullOrWhiteSpace (lyric?.Text))
                    continue;
                foreach (Match match in wordPattern.Matches (lyric.Text))
                {
                    string raw = PronouncingDictionary.Normalise (match.Value).Trim ('\'');
                    if (raw.Length < 3 || stopWords.Contains (raw))   // before BaseForm: "does" isn't "doe"
                        continue;
                    string word = BaseForm (raw, dictionary);
                    if (word.Length < 3 || stopWords.Contains (word))
                        continue;
                    if (!uses.TryGetValue (word, out HashSet<Lyric> set))
                        uses[word] = set = new HashSet<Lyric>();
                    set.Add (lyric);
                }
            }

            var counts = new List<WordCount>();
            foreach (var entry in uses)
            {
                var count = new WordCount { Word = entry.Key, Ideas = entry.Value.Count };
                var all = new HashSet<Lyric> (entry.Value);
                if (thesaurus != null)
                {
                    foreach (string synonym in Synonyms (entry.Key, thesaurus, dictionary))
                        if (uses.TryGetValue (synonym, out HashSet<Lyric> more))
                        {
                            count.Synonyms.Add (synonym);
                            all.UnionWith (more);
                        }
                }
                count.WithSynonyms = all.Count;
                if (Math.Max (count.Ideas, count.WithSynonyms) < minimum)
                    continue;
                count.Lyrics.AddRange (all.OrderByDescending (l => l.ConceivedAt ?? DateTime.MinValue));
                counts.Add (count);
            }

            return counts.OrderByDescending (c => thesaurus != null ? c.WithSynonyms : c.Ideas)
                         .ThenByDescending (c => c.Ideas)
                         .ThenBy (c => c.Word, StringComparer.Ordinal)
                         .ToList();
        }

        /// <summary>Plain one-word synonyms (no "(generic term)" etc.), in their base forms.</summary>
        static IEnumerable<string> Synonyms (string word, Thesaurus thesaurus, PronouncingDictionary dictionary)
        {
            var found = new HashSet<string> (StringComparer.Ordinal);
            foreach (Meaning meaning in thesaurus.Lookup (word))
                foreach (string synonym in meaning.Words)
                {
                    if (synonym.Contains ("(") || synonym.Contains (" "))
                        continue;
                    string s = BaseForm (synonym.ToLowerInvariant(), dictionary);
                    if (s != word && s.Length >= 3 && !stopWords.Contains (s))
                        found.Add (s);
                }
            return found;
        }

        /// <summary>dreams -> dream, dreaming -> dream, loved -> love, cities -> city (only to a real word).</summary>
        public static string BaseForm (string word, PronouncingDictionary dictionary)
        {
            bool Known (string w) => w.Length >= 3 && dictionary.Lookup (w, Accent.US).Count > 0;

            if (word.EndsWith ("ies") && Known (word.Substring (0, word.Length - 3) + "y"))
                return word.Substring (0, word.Length - 3) + "y";
            if (word.EndsWith ("s") && !word.EndsWith ("ss") && Known (word.Substring (0, word.Length - 1)))
                return word.Substring (0, word.Length - 1);
            foreach (string ending in new[] { "ing", "ed" })
            {
                if (!word.EndsWith (ending) || word.Length <= ending.Length + 2)
                    continue;
                string stem = word.Substring (0, word.Length - ending.Length);
                if (Known (stem + "e")) return stem + "e";          // loving -> love
                if (stem.Length >= 2 && stem[stem.Length - 1] == stem[stem.Length - 2] && Known (stem.Substring (0, stem.Length - 1)))
                    return stem.Substring (0, stem.Length - 1);      // running -> run
                if (Known (stem)) return stem;                       // dreaming -> dream
            }
            return word;
        }

        static readonly HashSet<string> stopWords = new HashSet<string> (StringComparer.Ordinal)
        {
            "the", "and", "for", "are", "but", "not", "you", "all", "any", "can", "had", "her", "was", "one",
            "our", "out", "get", "has", "him", "his", "how", "now", "see", "two",
            "way", "who", "did", "its", "let", "put", "say", "she", "too", "use", "that", "with", "have",
            "this", "will", "your", "from", "they", "know", "want", "been", "much", "some", 
            "very", "when", "come", "here", "just", "like", "make", "many", "more", "only", "over",
            "such", "take", "than", "them", "well", "were", "what", "into", "then", "there", "their", "these",
            "those", "would", "could", "should", "about", "after", "again", "also", "because", "before",
            "being", "below", "between", "both", "each", "even", "ever", "every", "going",
            "gonna", "gotta", "into", "itself", "least", "less", "might", "most", "must", "myself",
            "next", "none", "off", "once", "other", "ourselves", "own", "same", "since",
            "something", "still", "thing", "things", "though", "through", "till", "under", "until", "upon",
            "where", "which", "while", "whom", "whose", "why", "yet", "yourself", "yourselves", "yeah", "yes",
            "oh", "ooh", "got", "get", "gets", "getting", "does", "doing", "done", "made", "make", "said",
            "says", "tell", "told", "went", "goes", "came", "really", "maybe",
            "anything", "someone", "anyone", "everyone", "nobody", "somebody", "everybody",
            "can't", "don't", "won't", "isn't", "aren't", "wasn't", "weren't", "didn't", "doesn't", "i'm",
            "you're", "we're", "they're", "it's", "that's", "there's", "i've", "you've", "i'll", "you'll",
            "i'd", "you'd", "he's", "she's", "let's", "ain't", "wanna", "who's", "what's", "let", "lets",
            "our", "ours", "hers", "theirs", "yours", 
        };
    }
}
