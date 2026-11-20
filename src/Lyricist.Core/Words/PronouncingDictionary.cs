using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Lyricist.Core.Words
{
    public enum Accent { UK, US }

    /// <summary>One sound. Stress: -1 = consonant, 0 = unstressed vowel, 1 = primary, 2 = secondary.</summary>
    public readonly struct Phone
    {
        public Phone (string symbol, int stress)
        {
            Symbol = symbol;
            Stress = stress;
        }

        public string Symbol { get; }
        public int Stress { get; }
        public bool IsVowel => Stress >= 0;
        public override string ToString() => Symbol;
    }

    /// <summary>
    /// Word -> pronunciations, for British (Britfone, else CMU converted) and American (CMU) English.
    /// Symbols are IPA-like and shared by both, so the rhyme code doesn't care where they came from.
    /// </summary>
    public sealed class PronouncingDictionary
    {
        static readonly Regex validWord = new Regex (@"^[a-z']+(-[a-z']+)*$", RegexOptions.Compiled);
        static readonly Regex variant = new Regex (@"\(\d+\)$", RegexOptions.Compiled);

        readonly Dictionary<string, List<Phone[]>> us = new Dictionary<string, List<Phone[]>> (StringComparer.Ordinal);
        readonly Dictionary<string, List<Phone[]>> uk = new Dictionary<string, List<Phone[]>> (StringComparer.Ordinal);
        readonly HashSet<string> common = new HashSet<string> (StringComparer.Ordinal);

        public static PronouncingDictionary Load (string folder)
        {
            var dictionary = new PronouncingDictionary();
            dictionary.LoadCmu (Path.Combine (folder, "cmudict.dict.gz"));
            dictionary.LoadBritfone (Path.Combine (folder, "britfone.main.csv.gz"));
            // Words Britfone doesn't have: the American pronunciation, made British.
            foreach (var entry in dictionary.us)
                if (!dictionary.uk.ContainsKey (entry.Key))
                {
                    var list = new List<Phone[]>();
                    foreach (Phone[] phones in entry.Value)
                        list.Add (ToBritish (phones));
                    dictionary.uk[entry.Key] = list;
                }
            return dictionary;
        }

        /// <summary>Lower case, curly apostrophes straightened, surrounding punctuation removed.</summary>
        public static string Normalise (string word) =>
            (word ?? "").Trim().Replace ('’', '\'').Replace ('‘', '\'').ToLowerInvariant().Trim ('\'', '-');

        /// <summary>The word's pronunciations (empty if unknown).</summary>
        public IReadOnlyList<Phone[]> Lookup (string word, Accent accent)
        {
            var map = accent == Accent.UK ? uk : us;
            return map.TryGetValue (Normalise (word), out List<Phone[]> list) ? list : (IReadOnlyList<Phone[]>) Array.Empty<Phone[]>();
        }

        /// <summary>In Britfone's list (the ~16,000 commonest words).</summary>
        public bool IsCommon (string word) => common.Contains (Normalise (word));

        public IEnumerable<KeyValuePair<string, List<Phone[]>>> Entries (Accent accent) => accent == Accent.UK ? uk : us;

        // ---------------------------------------------------------------------------------
        // Loading

        static readonly Dictionary<string, string> cmuVowels = new Dictionary<string, string>
        {
            ["AA"] = "ɑ", ["AE"] = "æ", ["AO"] = "ɔ", ["AW"] = "aʊ", ["AY"] = "aɪ", ["EH"] = "ɛ", ["EY"] = "eɪ",
            ["IH"] = "ɪ", ["IY"] = "i", ["OW"] = "oʊ", ["OY"] = "ɔɪ", ["UH"] = "ʊ", ["UW"] = "u",
        };

        static readonly Dictionary<string, string> cmuConsonants = new Dictionary<string, string>
        {
            ["B"] = "b", ["CH"] = "tʃ", ["D"] = "d", ["DH"] = "ð", ["F"] = "f", ["G"] = "g", ["HH"] = "h",
            ["JH"] = "dʒ", ["K"] = "k", ["L"] = "l", ["M"] = "m", ["N"] = "n", ["NG"] = "ŋ", ["P"] = "p",
            ["R"] = "ɹ", ["S"] = "s", ["SH"] = "ʃ", ["T"] = "t", ["TH"] = "θ", ["V"] = "v", ["W"] = "w",
            ["Y"] = "j", ["Z"] = "z", ["ZH"] = "ʒ",
        };

        static readonly HashSet<string> ipaVowels = new HashSet<string>
        {
            "ɑ", "æ", "ɔ", "aʊ", "aɪ", "ɛ", "eɪ", "ɪ", "i", "oʊ", "ɔɪ", "ʊ", "u", "ʌ", "ə", "ɝ", "ɚ",
            "ɑː", "ɔː", "ɒ", "əʊ", "ɜː", "iː", "uː", "ɪə", "ɛə", "ʊə", "aɪə", "aʊə", "ɐ",
        };

        static IEnumerable<string> ReadLines (string gzPath)
        {
            using (var file = File.OpenRead (gzPath))
            using (var gz = new GZipStream (file, CompressionMode.Decompress))
            using (var reader = new StreamReader (gz, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }

        static void Add (Dictionary<string, List<Phone[]>> map, string word, Phone[] phones)
        {
            if (!map.TryGetValue (word, out List<Phone[]> list))
                map[word] = list = new List<Phone[]>();
            list.Add (phones);
        }

        // "love L AH1 V" / "read(2) R EH1 D # comment"
        void LoadCmu (string path)
        {
            foreach (string raw in ReadLines (path))
            {
                int hash = raw.IndexOf ('#');
                string line = (hash >= 0 ? raw.Substring (0, hash) : raw).Trim();
                if (line.Length == 0)
                    continue;
                string[] parts = line.Split (new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string word = variant.Replace (parts[0], "");
                if (!validWord.IsMatch (word))
                    continue;

                var phones = new Phone[parts.Length - 1];
                bool ok = true;
                for (int i = 1; i < parts.Length && ok; i++)
                {
                    string token = parts[i];
                    char last = token[token.Length - 1];
                    if (last >= '0' && last <= '2')
                    {
                        int stress = last - '0';
                        string vowel = token.Substring (0, token.Length - 1);
                        string symbol = vowel == "AH" ? (stress > 0 ? "ʌ" : "ə")
                                      : vowel == "ER" ? (stress > 0 ? "ɝ" : "ɚ")
                                      : cmuVowels.TryGetValue (vowel, out string v) ? v : null;
                        ok = symbol != null;
                        phones[i - 1] = new Phone (symbol, stress);
                    }
                    else
                    {
                        ok = cmuConsonants.TryGetValue (token, out string c);
                        phones[i - 1] = new Phone (c, -1);
                    }
                }
                if (ok)
                    Add (us, word, phones);
            }
        }

        // "RAINBOW, ɹ ˈeɪ n b ˌəʊ" ; multi-word entries use "_"
        void LoadBritfone (string path)
        {
            foreach (string line in ReadLines (path))
            {
                int comma = line.IndexOf (", ", StringComparison.Ordinal);
                if (comma <= 0)
                    continue;
                string word = variant.Replace (line.Substring (0, comma), "").ToLowerInvariant();
                if (word.Contains ("_") || !validWord.IsMatch (word))
                    continue;
                common.Add (word);

                string[] tokens = line.Substring (comma + 2).Split (new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var phones = new List<Phone>();
                foreach (string raw in tokens)
                {
                    string token = raw;
                    int stress = -1;
                    if (token.StartsWith ("ˈ")) { stress = 1; token = token.Substring (1); }
                    else if (token.StartsWith ("ˌ")) { stress = 2; token = token.Substring (1); }
                    if (token == "ɐ") token = "ʌ";   // Britfone's STRUT
                    if (token == "i") token = "iː";  // happY: rhymes as "ee"
                    if (stress < 0 && ipaVowels.Contains (token))
                        stress = 0;
                    phones.Add (new Phone (token, stress));
                }
                if (phones.Count > 0)
                    Add (uk, word, phones.ToArray());
            }
        }

        /// <summary>
        /// American (CMU) -> British approximation, for words Britfone hasn't got: non-rhotic
        /// (an R after a vowel and not before one is dropped, colouring the vowel), LOT -> ɒ,
        /// GOAT -> əʊ, NURSE -> ɜː. BATH words can't be told apart, so they keep æ.
        /// </summary>
        public static Phone[] ToBritish (Phone[] phones)
        {
            var output = new List<Phone>();
            for (int i = 0; i < phones.Length; i++)
            {
                Phone p = phones[i];
                bool rNext = i + 1 < phones.Length && phones[i + 1].Symbol == "ɹ"
                          && (i + 2 >= phones.Length || !phones[i + 2].IsVowel);
                if (p.IsVowel)
                {
                    if (p.Symbol == "ɝ") { output.Add (new Phone ("ɜː", p.Stress)); continue; }
                    if (p.Symbol == "ɚ") { output.Add (new Phone ("ə", p.Stress)); continue; }
                    if (rNext)
                    {
                        output.Add (new Phone (RColoured (p.Symbol), p.Stress));
                        i++;   // the R goes
                        continue;
                    }
                    output.Add (new Phone (Plain (p.Symbol), p.Stress));
                    continue;
                }
                // An R after a consonant and not before a vowel can't be sounded either.
                if (p.Symbol == "ɹ" && output.Count > 0 && !output[output.Count - 1].IsVowel
                    && (i + 1 >= phones.Length || !phones[i + 1].IsVowel))
                    continue;
                output.Add (p);
            }
            return output.ToArray();
        }

        static string RColoured (string vowel)
        {
            switch (vowel)
            {
                case "ɑ": return "ɑː";
                case "ɔ": return "ɔː";
                case "oʊ": return "ɔː";
                case "ɛ": case "eɪ": return "ɛə";
                case "ɪ": case "i": return "ɪə";
                case "ʊ": case "u": return "ʊə";
                case "aɪ": return "aɪə";
                case "aʊ": return "aʊə";
                case "ʌ": return "ɜː";
                default: return vowel;
            }
        }

        static string Plain (string vowel)
        {
            switch (vowel)
            {
                case "ɑ": return "ɒ";
                case "ɔ": return "ɔː";
                case "oʊ": return "əʊ";
                case "u": return "uː";
                case "i": return "iː";
                default: return vowel;
            }
        }
    }
}
