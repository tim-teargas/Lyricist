using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DumbAngel;
using Lyricist.Core;
using Lyricist.Core.Words;
using Lyricist.Core.Workbench;

namespace Lyricist.Desktop
{
    /// <summary>
    /// The word panel: rhymes (perfect / near / vowel only), syllables and stress, synonyms, and
    /// your ideas that rhyme with the word, use it, or use one of its synonyms. One window, kept open beside the edit forms. UK or US
    /// pronunciation. Double-click any word in a list (or in the synonyms) to look it up.
    /// </summary>
    public partial class WordsForm : XtraForm
    {
        readonly AppSettings settings;
        readonly Func<IEnumerable<Lyric>> lyrics;   // the ideas, for "In your ideas"
        readonly Action<Lyric> openLyric;
        readonly Stack<string> history = new Stack<string>();
        string currentWord = "";
        bool loading;

        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+(?:-[A-Za-z’']+)*", RegexOptions.Compiled);

        public WordsForm (AppSettings settings, Func<IEnumerable<Lyric>> lyrics, Action<Lyric> openLyric)
        {
            InitializeComponent();
            this.settings = settings;
            this.lyrics = lyrics;
            this.openLyric = openLyric;

            rgAccent.EditValue = settings.Accent == "US" ? 1 : 0;
            ceRare.Checked = settings.IncludeRareWords;
            rgAccent.EditValueChanged += (s, e) => { settings.Accent = Accent.ToString(); SaveSettings(); Requery(); };
            ceRare.CheckedChanged += (s, e) => { settings.IncludeRareWords = ceRare.Checked; SaveSettings(); Requery(); };
            teWord.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LookUp (teWord.Text, null); } };
            teLine.EditValueChanged += (s, e) => ShowLine();
            tpRhymes.Resize += (s, e) => LayOutRhymeColumns();
            sbBack.Enabled = false;
        }

        Accent Accent => Convert.ToInt32 (rgAccent.EditValue) == 1 ? Accent.US : Accent.UK;

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            if (!WindowMemory.Restore (this, "Words") && Owner != null)
                Location = new Point (Math.Max (0, Owner.Right - Width - 20), Owner.Top + 60);
            LayOutRhymeColumns();
        }

        protected override void OnFormClosing (FormClosingEventArgs e)
        {
            WindowMemory.Store (this, "Words");
            base.OnFormClosing (e);
        }

        // ---------------------------------------------------------------------------------

        /// <summary>Looks a word up; the line (if given) is scanned for syllables and stress.</summary>
        public async void LookUp (string word, string line)
        {
            word = FirstWord (word);
            if (line != null)
                teLine.Text = line.Trim();
            if (word.Length == 0)
                return;
            if (currentWord.Length > 0 && !string.Equals (currentWord, word, StringComparison.OrdinalIgnoreCase))
                history.Push (currentWord);
            sbBack.Enabled = history.Count > 0;
            currentWord = word;
            teWord.Text = word;

            if (loading)
                return;   // the load in progress will show the newest word
            if (WordData.IsLoaded (Accent))
            {
                ShowWord();
                ShowLine();
                return;
            }
            try
            {
                loading = true;
                lblStatus.Text = "Loading the word data (first time only)...";
                UseWaitCursor = true;
                await WordData.Preload (Accent);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "";
                Ui.Error ("Couldn't load the word data (WordData folder next to Lyricist.exe).", ex);
                return;
            }
            finally
            {
                loading = false;
                UseWaitCursor = false;
            }
            lblStatus.Text = "";
            ShowWord();
            ShowLine();
        }

        /// <summary>The same word again (accent or rare-words changed).</summary>
        void Requery()
        {
            if (currentWord.Length > 0)
            {
                string word = currentWord;
                currentWord = "";   // so LookUp doesn't push it again
                LookUp (word, null);
            }
        }

        void ShowWord()
        {
            string word = currentWord;
            Accent accent = Accent;
            PronouncingDictionary dictionary = WordData.Dictionary;
            WordScan scan = Scansion.Word (word, dictionary, accent);
            lblInfo.Text = scan.Known
                ? $"{word}: {scan.Syllables} syllable{(scan.Syllables == 1 ? "" : "s")}   {scan.Pattern}   {scan.Sounds}"
                : $"{word}: not in the {accent} dictionary (about {scan.Syllables} syllable{(scan.Syllables == 1 ? "" : "s")})";

            RhymeResult result = WordData.RhymerFor (accent).Find (word);
            Fill (lbPerfect, lblPerfect, "Perfect", result.Perfect);
            Fill (lbNear, lblNear, "Near", result.Near);
            Fill (lbVowel, lblVowel, "Vowel only", result.Vowel);

            ShowSynonyms (word);
            ShowIdeas (word);
        }

        void Fill (ListBoxControl list, LabelControl label, string title, List<RhymeMatch> matches)
        {
            List<RhymeMatch> shown = ceRare.Checked ? matches : matches.Where (m => m.Common).ToList();
            int hidden = matches.Count - shown.Count;
            label.Text = $"{title} ({shown.Count})" + (hidden > 0 ? $"  + {hidden} rare" : "");
            list.BeginUpdate();
            try
            {
                list.Items.Clear();
                foreach (RhymeMatch match in shown.Take (500))
                    list.Items.Add (new RhymeItem (match));
            }
            finally
            {
                list.EndUpdate();
            }
        }

        void ShowSynonyms (string word)
        {
            List<Meaning> meanings = WordData.Thesaurus.Lookup (word);
            if (meanings.Count == 0)
            {
                meSynonyms.Text = $"No synonyms for \"{word}\".";
                return;
            }
            var text = new StringBuilder();
            foreach (Meaning meaning in meanings)
                text.AppendLine (meaning.ToString()).AppendLine();
            meSynonyms.Text = text.ToString().TrimEnd();
        }

        /// <summary>
        /// Your ideas and this word: lines whose last word rhymes with it (perfect, then near), then
        /// ideas that use the word anywhere, then ideas that use one of its synonyms.
        /// </summary>
        void ShowIdeas (string word)
        {
            Rhymer rhymer = WordData.RhymerFor (Accent);
            PronouncingDictionary dictionary = WordData.Dictionary;
            string baseWord = RecurringWords.BaseForm (PronouncingDictionary.Normalise (word).Trim ('\''), dictionary);
            HashSet<string> synonyms = RecurringWords.SynonymsOf (baseWord, WordData.Thesaurus, dictionary);

            var rhymes = new List<IdeaItem>();
            var uses = new List<IdeaItem>();
            var viaSynonyms = new List<IdeaItem>();
            foreach (Lyric lyric in lyrics())
            {
                if (lyric == null || !lyric.IsActive || string.IsNullOrWhiteSpace (lyric.Text))
                    continue;
                bool rhymed = false;
                foreach (string line in lyric.Text.Split (new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MatchCollection words = wordPattern.Matches (line);
                    if (words.Count == 0)
                        continue;
                    string last = words[words.Count - 1].Value;
                    if (string.Equals (PronouncingDictionary.Normalise (last), PronouncingDictionary.Normalise (word), StringComparison.Ordinal))
                        continue;
                    RhymeKind kind = rhymer.Compare (word, last);
                    if (kind >= RhymeKind.Near)
                    {
                        rhymes.Add (new IdeaItem (lyric, line.Trim(), kind == RhymeKind.Perfect ? "Rhymes" : "Near rhyme", (int) kind));
                        rhymed = true;
                    }
                }
                if (rhymed)
                    continue;
                HashSet<string> content = RecurringWords.ContentWords (lyric.Text, dictionary);
                string flat = lyric.Text.Replace ("\r", " ").Replace ("\n", " / ").Trim();
                if (content.Contains (baseWord))
                    uses.Add (new IdeaItem (lyric, flat, "Uses it", 0));
                else
                {
                    string synonym = content.FirstOrDefault (synonyms.Contains);
                    if (synonym != null)
                        viaSynonyms.Add (new IdeaItem (lyric, flat, "Synonym: " + synonym, 0));
                }
            }

            lbIdeas.BeginUpdate();
            try
            {
                lbIdeas.Items.Clear();
                foreach (IdeaItem item in rhymes.OrderByDescending (i => i.Rank).ThenBy (i => i.Line)
                                                .Concat (uses.OrderBy (i => i.Line))
                                                .Concat (viaSynonyms.OrderBy (i => i.Label).ThenBy (i => i.Line)))
                    lbIdeas.Items.Add (item);
            }
            finally
            {
                lbIdeas.EndUpdate();
            }
            tpIdeas.Text = $"In your ideas ({rhymes.Count} rhyme, {uses.Count} use it, {viaSynonyms.Count} synonyms)";
        }

        void ShowLine()
        {
            if (string.IsNullOrWhiteSpace (teLine.Text))
            {
                meLineScan.Text = "";
                return;
            }
            try
            {
                // Only once the data is in (LookUp loads it); before that the line waits.
                if (WordData.IsLoaded (Accent))
                    meLineScan.Text = Scansion.Line (teLine.Text, WordData.Dictionary, Accent).ToString();
            }
            catch (Exception ex)
            {
                meLineScan.Text = ex.Message;
            }
        }

        // ---------------------------------------------------------------------------------

        static string FirstWord (string text)
        {
            Match match = wordPattern.Match (text ?? "");
            return match.Success ? match.Value : "";
        }

        void LayOutRhymeColumns()
        {
            int gap = 8, left = 6, top = 24;
            int width = (tpRhymes.ClientSize.Width - left * 2 - gap * 2) / 3;
            int height = Math.Max (50, tpRhymes.ClientSize.Height - top - 6);
            ListBoxControl[] lists = { lbPerfect, lbNear, lbVowel };
            LabelControl[] labels = { lblPerfect, lblNear, lblVowel };
            for (int i = 0; i < 3; i++)
            {
                int x = left + i * (width + gap);
                labels[i].Location = new Point (x, 6);
                lists[i].SetBounds (x, top, width, height);
            }
        }

        void SaveSettings()
        {
            try { settings.Save(); } catch { /* not worth interrupting for */ }
        }

        // ---------------------------------------------------------------------------------
        // Events

        void sbLookUp_Click (object sender, EventArgs e) => LookUp (teWord.Text, null);

        void sbBack_Click (object sender, EventArgs e)
        {
            if (history.Count == 0)
                return;
            string previous = history.Pop();
            currentWord = "";   // going back shouldn't push the word we're leaving
            LookUp (previous, null);
            sbBack.Enabled = history.Count > 0;
        }

        void lbRhyme_DoubleClick (object sender, EventArgs e)
        {
            if (((ListBoxControl) sender).SelectedItem is RhymeItem item)
                LookUp (item.Match.Word, null);
        }

        void meSynonyms_DoubleClick (object sender, EventArgs e)
        {
            string word = FirstWord (meSynonyms.SelectedText);
            if (word.Length > 0)
                LookUp (word, null);
        }

        void lbIdeas_DoubleClick (object sender, EventArgs e)
        {
            if (lbIdeas.SelectedItem is IdeaItem item)
                openLyric (item.Lyric);
        }

        // ---------------------------------------------------------------------------------

        sealed class RhymeItem
        {
            public RhymeItem (RhymeMatch match) { Match = match; }
            public RhymeMatch Match { get; }
            public override string ToString() => $"{Match.Word}  ({Match.Syllables})" + (Match.Common ? "" : "  · rare");
        }

        sealed class IdeaItem
        {
            public IdeaItem (Lyric lyric, string line, string label, int rank)
            {
                Lyric = lyric;
                Line = line;
                Label = label;
                Rank = rank;
            }

            public Lyric Lyric { get; }
            public string Line { get; }
            public string Label { get; }
            public int Rank { get; }
            public override string ToString() => $"{Label}:  {Line}   (#{Lyric.Oid})";
        }
    }
}
