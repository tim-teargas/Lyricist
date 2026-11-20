using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyricist.Core.Words
{
    public enum RhymeKind { None, Vowel, Near, Perfect }

    public sealed class RhymeMatch
    {
        public RhymeMatch (string word, int syllables, bool common)
        {
            Word = word;
            Syllables = syllables;
            Common = common;
        }

        public string Word { get; }
        public int Syllables { get; }
        public bool Common { get; }
        public override string ToString() => Word;
    }

    public sealed class RhymeResult
    {
        public string Word { get; set; }
        public bool Known { get; set; }
        public List<RhymeMatch> Perfect { get; } = new List<RhymeMatch>();
        public List<RhymeMatch> Near { get; } = new List<RhymeMatch>();
        public List<RhymeMatch> Vowel { get; } = new List<RhymeMatch>();
    }

    /// <summary>
    /// Rhymes for one accent.
    /// Perfect: identical from the last primary-stressed vowel to the end (love / above).
    /// Near: the same vowels, and consonants that are close: the same kind (m / n / ŋ, p / t / k,
    ///       f / θ / s ...), one extra consonant, or an added s / z (time / line / mind, love / loves).
    /// Vowel: the same vowels only (assonance: love / up / sun).
    /// Unstressed vowels after the stress count as one sound (ə, ɪ, ʌ, ɚ), so "lover" and "mother" match.
    /// </summary>
    public sealed class Rhymer
    {
        readonly PronouncingDictionary dictionary;
        readonly Accent accent;
        readonly Dictionary<string, HashSet<string>> byRhyme = new Dictionary<string, HashSet<string>> (StringComparer.Ordinal);
        readonly Dictionary<string, HashSet<string>> byVowels = new Dictionary<string, HashSet<string>> (StringComparer.Ordinal);

        public Rhymer (PronouncingDictionary dictionary, Accent accent)
        {
            this.dictionary = dictionary;
            this.accent = accent;
            foreach (var entry in dictionary.Entries (accent))
                foreach (Phone[] phones in entry.Value)
                {
                    AddTo (byRhyme, RhymeKey (phones), entry.Key);
                    AddTo (byVowels, VowelKey (phones), entry.Key);
                }
        }

        public Accent Accent => accent;

        public RhymeResult Find (string word)
        {
            word = PronouncingDictionary.Normalise (word);
            var result = new RhymeResult { Word = word };
            IReadOnlyList<Phone[]> pronunciations = dictionary.Lookup (word, accent);
            result.Known = pronunciations.Count > 0;

            var perfect = new HashSet<string> (StringComparer.Ordinal);
            var near = new HashSet<string> (StringComparer.Ordinal);
            var vowel = new HashSet<string> (StringComparer.Ordinal);
            int syllables = 1;

            foreach (Phone[] phones in pronunciations)
            {
                syllables = Syllables (phones);
                string rhymeKey = RhymeKey (phones);
                if (rhymeKey != null && byRhyme.TryGetValue (rhymeKey, out HashSet<string> same))
                    perfect.UnionWith (same);

                string vowelKey = VowelKey (phones);
                if (vowelKey == null || !byVowels.TryGetValue (vowelKey, out HashSet<string> sameVowels))
                    continue;
                foreach (string candidate in sameVowels)
                {
                    if (perfect.Contains (candidate))
                        continue;
                    bool isNear = dictionary.Lookup (candidate, accent).Any (other => Compare (phones, other) >= RhymeKind.Near);
                    (isNear ? near : vowel).Add (candidate);
                }
            }

            perfect.Remove (word);
            near.ExceptWith (perfect);
            near.Remove (word);
            vowel.ExceptWith (perfect);
            vowel.ExceptWith (near);
            vowel.Remove (word);

            result.Perfect.AddRange (Sorted (perfect, syllables));
            result.Near.AddRange (Sorted (near, syllables));
            result.Vowel.AddRange (Sorted (vowel, syllables));
            return result;
        }

        /// <summary>The word's perfect-rhyme keys (one per pronunciation), e.g. "aɪ t" for night.</summary>
        public IEnumerable<string> RhymeKeys (string word) =>
            dictionary.Lookup (word, accent).Select (RhymeKey).Where (k => k != null).Distinct();

        /// <summary>The word's "same vowels" keys (one per pronunciation): words that can rhyme share one.</summary>
        public IEnumerable<string> VowelKeys (string word) =>
            dictionary.Lookup (word, accent).Select (VowelKey).Where (k => k != null).Distinct();

        /// <summary>How two words rhyme (best over their pronunciations).</summary>
        public RhymeKind Compare (string a, string b)
        {
            RhymeKind best = RhymeKind.None;
            foreach (Phone[] pa in dictionary.Lookup (a, accent))
                foreach (Phone[] pb in dictionary.Lookup (b, accent))
                {
                    RhymeKind kind = Compare (pa, pb);
                    if (kind > best)
                        best = kind;
                }
            return best;
        }

        public static RhymeKind Compare (Phone[] a, Phone[] b)
        {
            string ka = RhymeKey (a);
            if (ka == null || RhymeKey (b) == null)
                return RhymeKind.None;
            if (ka == RhymeKey (b))
                return RhymeKind.Perfect;
            if (VowelKey (a) != VowelKey (b))
                return RhymeKind.None;
            List<List<string>> ca = Clusters (a), cb = Clusters (b);
            if (ca.Count != cb.Count)
                return RhymeKind.Vowel;
            for (int i = 0; i < ca.Count; i++)
                if (!NearCluster (ca[i], cb[i]))
                    return RhymeKind.Vowel;
            return RhymeKind.Near;
        }

        // ---------------------------------------------------------------------------------

        IEnumerable<RhymeMatch> Sorted (IEnumerable<string> words, int syllables) =>
            words.Select (w => (word: w, phones: dictionary.Lookup (w, accent)))
                 .Where (x => x.phones.Count > 0)
                 .Select (x => new RhymeMatch (x.word, Syllables (x.phones[0]), dictionary.IsCommon (x.word)))
                 .OrderBy (m => m.Common ? 0 : 1)
                 .ThenBy (m => Math.Abs (m.Syllables - syllables))
                 .ThenBy (m => m.Syllables)
                 .ThenBy (m => m.Word, StringComparer.Ordinal);

        static void AddTo (Dictionary<string, HashSet<string>> index, string key, string word)
        {
            if (key == null)
                return;
            if (!index.TryGetValue (key, out HashSet<string> set))
                index[key] = set = new HashSet<string> (StringComparer.Ordinal);
            set.Add (word);
        }

        public static int Syllables (Phone[] phones) => Math.Max (1, phones.Count (p => p.IsVowel));

        /// <summary>The last primary-stressed vowel (else the last stressed, else the last vowel).</summary>
        static int StressedIndex (Phone[] phones)
        {
            int primary = -1, stressed = -1, any = -1;
            for (int i = 0; i < phones.Length; i++)
            {
                if (!phones[i].IsVowel)
                    continue;
                any = i;
                if (phones[i].Stress == 1) primary = i;
                if (phones[i].Stress > 0) stressed = i;
            }
            return primary >= 0 ? primary : stressed >= 0 ? stressed : any;
        }

        static string RhymeKey (Phone[] phones)
        {
            int start = StressedIndex (phones);
            if (start < 0)
                return null;
            return string.Join (" ", phones.Skip (start).Select (p => p.Symbol));
        }

        static readonly HashSet<string> weak = new HashSet<string> { "ə", "ɪ", "ɚ", "ʌ" };

        static string VowelKey (Phone[] phones)
        {
            int start = StressedIndex (phones);
            if (start < 0)
                return null;
            return string.Join (" ", phones.Skip (start).Where (p => p.IsVowel)
                                           .Select ((p, i) => i > 0 && p.Stress == 0 && weak.Contains (p.Symbol) ? "ə" : p.Symbol));
        }

        /// <summary>The consonant groups after the stressed vowel: one per vowel (after it).</summary>
        static List<List<string>> Clusters (Phone[] phones)
        {
            var clusters = new List<List<string>>();
            var current = new List<string>();
            for (int i = StressedIndex (phones) + 1; i < phones.Length; i++)
            {
                if (phones[i].IsVowel)
                {
                    clusters.Add (current);
                    current = new List<string>();
                }
                else
                    current.Add (phones[i].Symbol);
            }
            clusters.Add (current);
            return clusters;
        }

        static readonly Dictionary<string, char> kind = BuildKinds();

        static Dictionary<string, char> BuildKinds()
        {
            var map = new Dictionary<string, char>();
            void Group (char k, params string[] symbols) { foreach (string s in symbols) map[s] = k; }
            Group ('P', "p", "t", "k");
            Group ('B', "b", "d", "g");
            Group ('F', "f", "θ", "s", "ʃ", "h");
            Group ('V', "v", "ð", "z", "ʒ");
            Group ('C', "tʃ", "dʒ");
            Group ('N', "m", "n", "ŋ");
            Group ('L', "l", "ɹ");
            Group ('W', "w", "j");
            return map;
        }

        static char KindOf (string symbol) => kind.TryGetValue (symbol, out char k) ? k : '?';

        static bool NearCluster (List<string> a, List<string> b)
        {
            if (a.SequenceEqual (b))
                return true;
            if (a.Count == 0 || b.Count == 0)
                return false;   // open vs closed ending
            if (PlusS (a, b) || PlusS (b, a))
                return true;
            if (a.Count == b.Count)
                return a.Select (KindOf).SequenceEqual (b.Select (KindOf));
            if (Math.Abs (a.Count - b.Count) == 1)
            {
                List<string> shorter = a.Count < b.Count ? a : b, longer = a.Count < b.Count ? b : a;
                for (int i = 0; i < longer.Count; i++)
                {
                    var without = new List<string> (longer);
                    without.RemoveAt (i);
                    if (without.SequenceEqual (shorter))
                        return true;
                }
            }
            return false;
        }

        // "loves" vs "love": the longer one is the shorter plus a final s / z.
        static bool PlusS (List<string> longer, List<string> shorter) =>
            longer.Count == shorter.Count + 1 && (longer[longer.Count - 1] == "s" || longer[longer.Count - 1] == "z")
            && longer.Take (shorter.Count).SequenceEqual (shorter);
    }
}
