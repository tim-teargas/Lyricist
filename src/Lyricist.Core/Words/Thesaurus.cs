using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Lyricist.Core.Words
{
    public sealed class Meaning
    {
        public string PartOfSpeech { get; set; }   // noun, verb, adj, adv
        public List<string> Words { get; } = new List<string>();
        public override string ToString() => $"({PartOfSpeech}) {string.Join (", ", Words)}";
    }

    /// <summary>
    /// MyThes thesaurus (LibreOffice / OpenOffice, from WordNet). Format after an encoding line:
    /// "word|N" then N lines "(pos)|synonym|synonym|..."; a synonym may carry "(generic term)" etc.
    /// The whole text is kept in memory with an index of where each word's lines start.
    /// </summary>
    public sealed class Thesaurus
    {
        string text;
        readonly Dictionary<string, List<int>> index = new Dictionary<string, List<int>> (StringComparer.Ordinal);

        public static Thesaurus Load (string gzPath)
        {
            var thesaurus = new Thesaurus();
            using (var file = File.OpenRead (gzPath))
            using (var gz = new GZipStream (file, CompressionMode.Decompress))
            using (var reader = new StreamReader (gz, Encoding.UTF8))
                thesaurus.text = reader.ReadToEnd();
            thesaurus.BuildIndex();
            return thesaurus;
        }

        public List<Meaning> Lookup (string word)
        {
            var meanings = new List<Meaning>();
            if (!index.TryGetValue (PronouncingDictionary.Normalise (word), out List<int> starts))
                return meanings;
            foreach (int start in starts)
            {
                int pos = start;
                string header = ReadLine (ref pos);
                int bar = header.LastIndexOf ('|');
                if (bar < 0 || !int.TryParse (header.Substring (bar + 1), out int count))
                    continue;
                for (int i = 0; i < count && pos < text.Length; i++)
                {
                    string[] parts = ReadLine (ref pos).Split ('|');
                    var meaning = new Meaning { PartOfSpeech = parts[0].Trim ('(', ')') };
                    for (int j = 1; j < parts.Length; j++)
                        if (parts[j].Length > 0)
                            meaning.Words.Add (parts[j]);
                    meanings.Add (meaning);
                }
            }
            return meanings;
        }

        void BuildIndex()
        {
            int pos = 0;
            ReadLine (ref pos);   // encoding
            while (pos < text.Length)
            {
                int start = pos;
                string header = ReadLine (ref pos);
                int bar = header.LastIndexOf ('|');
                if (bar <= 0 || !int.TryParse (header.Substring (bar + 1), out int count))
                    continue;
                string key = header.Substring (0, bar).ToLowerInvariant();
                if (!index.TryGetValue (key, out List<int> starts))
                    index[key] = starts = new List<int>();
                starts.Add (start);
                for (int i = 0; i < count && pos < text.Length; i++)
                    SkipLine (ref pos);
            }
        }

        string ReadLine (ref int pos)
        {
            int end = text.IndexOf ('\n', pos);
            if (end < 0) end = text.Length;
            string line = text.Substring (pos, end - pos).TrimEnd ('\r');
            pos = end + 1;
            return line;
        }

        void SkipLine (ref int pos)
        {
            int end = text.IndexOf ('\n', pos);
            pos = end < 0 ? text.Length : end + 1;
        }
    }
}
