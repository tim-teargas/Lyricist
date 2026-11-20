using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lyricist.Core.Words;

namespace Lyricist.Core.Songs
{
    /// <summary>One line of a song as analysed.</summary>
    public sealed class SongLine
    {
        public int Number { get; set; }            // 1-based line in the text
        public string Text { get; set; }
        public bool IsHeader { get; set; }         // [Verse 1]
        public bool IsBlank { get; set; }
        public string Section { get; set; }        // the header it falls under ("" before any)
        public string LastWord { get; set; }
        /// <summary>A, B, C... by rhyme within the section; lower case = near rhyme; "" = none yet / blank.</summary>
        public string Letter { get; set; } = "";
        public int Syllables { get; set; }
        public bool SyllablesSure { get; set; }
        public string Pattern { get; set; } = "";
    }

    public sealed class SongSection
    {
        public string Name { get; set; }
        public List<SongLine> Lines { get; } = new List<SongLine>();
        /// <summary>e.g. "ABAB" (near rhymes in lower case).</summary>
        public string Scheme => string.Concat (Lines.Where (l => !l.IsBlank && !l.IsHeader).Select (l => l.Letter.Length > 0 ? l.Letter : "-"));
    }

    public sealed class SongAnalysisResult
    {
        public List<SongLine> Lines { get; } = new List<SongLine>();
        public List<SongSection> Sections { get; } = new List<SongSection>();

        /// <summary>"Verse 1: ABAB · Chorus: AABB"</summary>
        public string Summary => string.Join ("  ·  ", Sections.Where (s => s.Lines.Any (l => !l.IsBlank && !l.IsHeader))
                                                               .Select (s => (s.Name.Length > 0 ? s.Name + ": " : "") + s.Scheme));
    }

    /// <summary>
    /// Works through a song's text line by line: syllables and stress pattern (Scansion), and rhyme
    /// letters for the line endings. Letters restart at each [header] line. A line takes the letter
    /// of the first earlier line in its section that its last word rhymes with: perfect = upper
    /// case, near = lower case; otherwise it gets the next new letter. A blank line also starts a
    /// new section when there are no headers in sight.
    /// </summary>
    public static class SongAnalysis
    {
        static readonly Regex header = new Regex (@"^\s*\[(?<name>[^\]]*)\]\s*$", RegexOptions.Compiled);
        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+(?:-[A-Za-z’']+)*", RegexOptions.Compiled);

        public static SongAnalysisResult Analyse (string text, Rhymer rhymer, PronouncingDictionary dictionary, Accent accent)
        {
            var result = new SongAnalysisResult();
            string[] raw = (text ?? "").Replace ("\r\n", "\n").Split ('\n');
            var section = new SongSection { Name = "" };
            result.Sections.Add (section);
            bool sawHeader = false;

            for (int i = 0; i < raw.Length; i++)
            {
                var line = new SongLine { Number = i + 1, Text = raw[i] };
                Match h = header.Match (raw[i]);
                if (h.Success)
                {
                    sawHeader = true;
                    line.IsHeader = true;
                    section = new SongSection { Name = h.Groups["name"].Value.Trim() };
                    result.Sections.Add (section);
                }
                else if (string.IsNullOrWhiteSpace (raw[i]))
                {
                    line.IsBlank = true;
                    // Without headers, a blank line separates verses: start a new (unnamed) section.
                    if (!sawHeader && section.Lines.Any (l => !l.IsBlank))
                    {
                        section = new SongSection { Name = "" };
                        result.Sections.Add (section);
                    }
                }
                else
                {
                    LineScan scan = Scansion.Line (raw[i], dictionary, accent);
                    line.Syllables = scan.Syllables;
                    line.SyllablesSure = scan.AllKnown;
                    line.Pattern = string.Join (" ", scan.Words.Select (w => w.Pattern));
                    MatchCollection words = wordPattern.Matches (raw[i]);
                    line.LastWord = words.Count > 0 ? words[words.Count - 1].Value : "";
                }
                line.Section = section.Name;
                section.Lines.Add (line);
                result.Lines.Add (line);
            }

            foreach (SongSection s in result.Sections)
                AssignLetters (s, rhymer);
            NameUnnamedSections (result);
            return result;
        }

        static void AssignLetters (SongSection section, Rhymer rhymer)
        {
            var anchors = new List<(string letter, string word)>();   // first line of each letter
            foreach (SongLine line in section.Lines)
            {
                if (line.IsHeader || line.IsBlank || string.IsNullOrEmpty (line.LastWord))
                    continue;
                string word = PronouncingDictionary.Normalise (line.LastWord);
                string letter = null;
                foreach (var anchor in anchors)
                {
                    RhymeKind kind = word == anchor.word ? RhymeKind.Perfect : rhymer.Compare (word, anchor.word);
                    if (kind == RhymeKind.Perfect) { letter = anchor.letter; break; }
                    if (kind == RhymeKind.Near && letter == null) letter = anchor.letter.ToLowerInvariant();
                }
                if (letter == null)
                {
                    letter = LetterFor (anchors.Count);
                    anchors.Add ((letter, word));
                }
                line.Letter = letter;
            }
        }

        static string LetterFor (int index) =>
            index < 26 ? ((char) ('A' + index)).ToString() : "Z" + (index - 25);

        static void NameUnnamedSections (SongAnalysisResult result)
        {
            int n = 0;
            foreach (SongSection s in result.Sections)
                if (s.Name.Length == 0 && s.Lines.Any (l => !l.IsBlank && !l.IsHeader))
                {
                    s.Name = "Part " + (++n);
                    foreach (SongLine line in s.Lines)
                        line.Section = s.Name;
                }
        }

        /// <summary>The song with each line's letter and syllables in front, for prompts:
        /// "A  8 | I walked out in the morning light".</summary>
        public static string Annotated (SongAnalysisResult analysis)
        {
            var text = new StringBuilder();
            foreach (SongLine line in analysis.Lines)
            {
                if (line.IsHeader || line.IsBlank)
                    text.AppendLine (line.Text.Trim());
                else
                    text.AppendLine ($"{(line.Letter.Length > 0 ? line.Letter : "-"),-3}{line.Syllables,3}{(line.SyllablesSure ? " " : "?")}| {line.Text.Trim()}");
            }
            return text.ToString().TrimEnd();
        }
    }
}
