using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lyricist.Core.Words
{
    public sealed class WordScan
    {
        public string Word { get; set; }
        public int Syllables { get; set; }
        /// <summary>"DUM" primary stress, "Dum" secondary, "da" unstressed; "?" when the word is unknown.</summary>
        public string Pattern { get; set; }
        public bool Known { get; set; }
        public string Sounds { get; set; }
    }

    public sealed class LineScan
    {
        public List<WordScan> Words { get; } = new List<WordScan>();
        public int Syllables => Words.Sum (w => w.Syllables);
        public bool AllKnown => Words.All (w => w.Known);

        /// <summary>e.g. "I'm (DUM) | going (DUM-da) | home (DUM)   = 4 syllables"</summary>
        public override string ToString() =>
            string.Join (" | ", Words.Select (w => $"{w.Word} ({w.Pattern})"))
            + $"   = {Syllables}{(AllKnown ? "" : "?")} syllable{(Syllables == 1 ? "" : "s")}";
    }

    /// <summary>Syllables and stress for words and lines (for scansion; Omnivocal needs one note per syllable).</summary>
    public static class Scansion
    {
        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+(?:-[A-Za-z’']+)*", RegexOptions.Compiled);
        static readonly Regex vowelGroups = new Regex ("[aeiouy]+", RegexOptions.Compiled);

        public static LineScan Line (string line, PronouncingDictionary dictionary, Accent accent)
        {
            var scan = new LineScan();
            foreach (Match match in wordPattern.Matches (line ?? ""))
                scan.Words.Add (Word (match.Value, dictionary, accent));
            return scan;
        }

        public static WordScan Word (string word, PronouncingDictionary dictionary, Accent accent)
        {
            IReadOnlyList<Phone[]> found = dictionary.Lookup (word, accent);
            if (found.Count == 0 && word.Contains ("-"))
            {
                // Hyphenated and not in the dictionary: the parts in turn.
                var parts = word.Split ('-').Where (p => p.Length > 0).Select (p => Word (p, dictionary, accent)).ToList();
                return new WordScan
                {
                    Word = word,
                    Syllables = parts.Sum (p => p.Syllables),
                    Pattern = string.Join ("-", parts.Select (p => p.Pattern)),
                    Known = parts.All (p => p.Known),
                    Sounds = string.Join (" - ", parts.Select (p => p.Sounds)),
                };
            }
            if (found.Count == 0)
            {
                int estimate = EstimateSyllables (word);
                return new WordScan { Word = word, Syllables = estimate, Pattern = estimate + "?", Known = false, Sounds = "?" };
            }
            Phone[] phones = found[0];
            return new WordScan
            {
                Word = word,
                Syllables = Rhymer.Syllables (phones),
                Pattern = Pattern (phones),
                Known = true,
                Sounds = Sounds (phones),
            };
        }

        public static string Pattern (Phone[] phones) =>
            string.Join ("-", phones.Where (p => p.IsVowel).Select (p => p.Stress == 1 ? "DUM" : p.Stress == 2 ? "Dum" : "da"));

        /// <summary>IPA-ish, with ˈ / ˌ before stressed vowels: /ˈlʌv/.</summary>
        public static string Sounds (Phone[] phones) =>
            "/" + string.Concat (phones.Select (p => (p.Stress == 1 ? "ˈ" : p.Stress == 2 ? "ˌ" : "") + p.Symbol)) + "/";

        /// <summary>Rough count for words not in the dictionary: vowel groups, less a silent final e.</summary>
        public static int EstimateSyllables (string word)
        {
            string w = word.ToLowerInvariant();
            int count = vowelGroups.Matches (w).Count;
            if (w.EndsWith ("e") && !w.EndsWith ("le") && count > 1)
                count--;
            return Math.Max (1, count);
        }
    }
}
