using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DumbAngel;
using Lyricist.Core;
using Lyricist.Core.Prompts;
using Lyricist.Core.Songs;
using Lyricist.Core.Words;
using Lyricist.Data;

namespace Lyricist.Desktop
{
    /// <summary>
    /// One song: free text on the left (optional [Verse] / [Chorus] header lines), and on the right
    /// each line's rhyme letter, syllables and stress (updated as you type), the ideas gathered for
    /// it, and Prompt. Nothing is written to the database until Save.
    /// </summary>
    public partial class SongEditForm : XtraForm
    {
        readonly ILyricHost host;
        readonly AppSettings settings;
        readonly List<SongLyric> ideas = new List<SongLyric>();
        readonly List<SongLyric> removed = new List<SongLyric>();
        readonly Timer analyseTimer = new Timer { Interval = 500 };
        static readonly Regex wordPattern = new Regex (@"[A-Za-z’']+(?:-[A-Za-z’']+)*", RegexOptions.Compiled);
        SongAnalysisResult analysis;
        bool loading, dirty, fitted;

        public Song Song { get; }

        public SongEditForm (Song song, AppSettings settings, ILyricHost host)
        {
            InitializeComponent();
            Song = song;
            this.settings = settings;
            this.host = host;
            gvLines.OptionsFind.AlwaysVisible = false;
            gvLines.OptionsView.RowAutoHeight = false;

            analyseTimer.Tick += (s, e) => { analyseTimer.Stop(); Analyse(); };
            meBody.EditValueChanged += (s, e) => { if (!loading) { dirty = true; analyseTimer.Stop(); analyseTimer.Start(); } };
            teTitle.EditValueChanged += (s, e) => { if (!loading) { dirty = true; ShowTitle(); } };
            cbStatus.EditValueChanged += (s, e) => { if (!loading) dirty = true; };
            meNotes.EditValueChanged += (s, e) => { if (!loading) dirty = true; };
            meBody.DoubleClick += (s, e) => BeginInvoke ((Action) (() => SendToWords (onlyIfOpen: true)));
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            loading = true;
            try
            {
                teTitle.Text = Song.Title ?? "";
                cbStatus.EditValue = string.IsNullOrWhiteSpace (Song.Status) ? "Idea" : Song.Status;
                meBody.Text = Song.Body ?? "";
                meNotes.Text = Song.Notes ?? "";
                ideas.AddRange (Song.Ideas.OrderBy (i => i.Position));
                ShowIdeas();
                ShowTitle();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem loading the song.", ex);
            }
            finally
            {
                loading = false;
            }
            Analyse();
            meBody.Focus();
        }

        protected override void OnFormClosing (FormClosingEventArgs e)
        {
            if (dirty && e.CloseReason != CloseReason.WindowsShutDown)
            {
                DialogResult answer = XtraMessageBox.Show ($"Save the changes to \"{TitleText}\"?", "Lyricist",
                                                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (answer == DialogResult.Cancel || (answer == DialogResult.Yes && !SaveSong()))
                {
                    e.Cancel = true;
                    return;
                }
            }
            analyseTimer.Dispose();
            base.OnFormClosing (e);
        }

        string TitleText => string.IsNullOrWhiteSpace (teTitle.Text) ? "untitled" : teTitle.Text.Trim();

        void ShowTitle() => Text = "Song: " + TitleText;

        // ---------------------------------------------------------------------------------
        // Analysis (letters, syllables, stress), half a second after typing stops

        async void Analyse()
        {
            try
            {
                Accent accent = settings.Accent == "US" ? Accent.US : Accent.UK;
                if (!WordData.IsLoaded (accent))
                {
                    lblSchemes.Text = "Loading the word data (first time only)...";
                    await WordData.Preload (accent);
                }
                analysis = SongAnalysis.Analyse (meBody.Text, WordData.RhymerFor (accent), WordData.Dictionary, accent);
                lblSchemes.Text = analysis.Summary.Length > 0 ? $"Rhyme schemes ({accent}):  {analysis.Summary}" : "";

                int top = gvLines.TopRowIndex, focused = gvLines.FocusedRowHandle;
                gcLines.DataSource = analysis.Lines.Where (l => !l.IsBlank).Select (l => new LineRow (l)).ToList();
                if (!fitted && gvLines.Columns.Count > 0)
                {
                    gvLines.BestFitColumns();
                    fitted = true;
                }
                gvLines.FocusedRowHandle = focused;
                gvLines.TopRowIndex = top;
            }
            catch (Exception ex)
            {
                lblSchemes.Text = "Couldn't analyse: " + ex.Message;
                Ui.Log (ex);
            }
        }

        /// <summary>Clicking a line in the analysis puts the cursor on that line of the text.</summary>
        void gvLines_RowClick (object sender, RowClickEventArgs e)
        {
            if (!(gvLines.GetRow (e.RowHandle) is LineRow row))
                return;
            string text = meBody.Text ?? "";
            int index = 0;
            for (int line = 1; line < row.Line && index >= 0; line++)
            {
                index = text.IndexOf ('\n', index);
                if (index >= 0) index++;
            }
            if (index < 0)
                return;
            meBody.Focus();
            meBody.SelectionStart = index;
            meBody.SelectionLength = 0;
            meBody.MaskBox.ScrollToCaret();
        }

        // ---------------------------------------------------------------------------------
        // Ideas gathered

        /// <summary>Adds an idea from the list (right-click > Add to song). Saved with the song.</summary>
        public void AddIdea (Lyric lyric)
        {
            if (lyric == null || ideas.Any (i => i.Lyric != null && i.Lyric.Oid == lyric.Oid))
                return;
            ideas.Add (new SongLyric (host.Session) { Lyric = lyric });
            dirty = true;
            ShowIdeas();
            Activate();
        }

        void ShowIdeas()
        {
            lbIdeas.BeginUpdate();
            try
            {
                lbIdeas.Items.Clear();
                foreach (SongLyric idea in ideas.Where (i => i.Lyric != null))
                    lbIdeas.Items.Add (new IdeaItem (idea));
            }
            finally
            {
                lbIdeas.EndUpdate();
            }
            lblIdeas.Text = $"Ideas gathered ({ideas.Count}): right-click an idea in the list, Add to song";
        }

        void sbInsertIdea_Click (object sender, EventArgs e)
        {
            if (!(lbIdeas.SelectedItem is IdeaItem item))
                return;
            string insert = item.Idea.Lyric.Text.Trim();
            int at = Math.Min (meBody.SelectionStart, (meBody.Text ?? "").Length);
            string text = meBody.Text ?? "";
            // On a line of its own.
            string before = at > 0 && text[at - 1] != '\n' ? Environment.NewLine : "";
            string after = at < text.Length && text[at] != '\r' && text[at] != '\n' ? Environment.NewLine : "";
            meBody.Text = text.Substring (0, at) + before + insert + after + text.Substring (at);
            meBody.SelectionStart = at + before.Length + insert.Length;
            meBody.Focus();
        }

        void sbRemoveIdea_Click (object sender, EventArgs e)
        {
            if (!(lbIdeas.SelectedItem is IdeaItem item))
                return;
            ideas.Remove (item.Idea);
            if (!host.Session.IsNewObject (item.Idea))
                removed.Add (item.Idea);
            dirty = true;
            ShowIdeas();
        }

        void lbIdeas_DoubleClick (object sender, EventArgs e)
        {
            if (lbIdeas.SelectedItem is IdeaItem item)
                host.OpenLyric (item.Idea.Lyric);
        }

        // ---------------------------------------------------------------------------------
        // Save, Words, Prompt

        bool SaveSong()
        {
            try
            {
                Song.Title = teTitle.Text.Trim();
                Song.Status = cbStatus.Text;
                Song.Body = meBody.Text;
                Song.Notes = meNotes.Text;
                Song.Save();
                for (int i = 0; i < ideas.Count; i++)
                {
                    ideas[i].Song = Song;
                    ideas[i].Position = i;
                    ideas[i].Save();
                }
                foreach (SongLyric link in removed)
                    link.Delete();
                removed.Clear();
                dirty = false;
                return true;
            }
            catch (DevExpress.Xpo.DB.Exceptions.LockingException)
            {
                Ui.Error ("This song was changed somewhere else since it was opened. Copy your text out, close and reopen it.");
                return false;
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem saving the song.", ex);
                return false;
            }
        }

        void sbSave_Click (object sender, EventArgs e) => SaveSong();

        void sbClose_Click (object sender, EventArgs e) => Close();

        void sbWords_Click (object sender, EventArgs e) => SendToWords (onlyIfOpen: false);

        void SendToWords (bool onlyIfOpen)
        {
            string text = meBody.Text ?? "";
            int at = Math.Min (meBody.SelectionStart, text.Length);
            string word = meBody.SelectedText.Trim();
            if (word.Length == 0)
                foreach (Match m in wordPattern.Matches (text))
                    if (at >= m.Index && at <= m.Index + m.Length)
                    {
                        word = m.Value;
                        break;
                    }
            int start = at == 0 ? 0 : text.LastIndexOf ('\n', at - 1) + 1;
            int end = text.IndexOf ('\n', at);
            string line = text.Substring (start, (end < 0 ? text.Length : end) - start).Trim();
            if (word.Length > 0)
                host.ShowWords (word, line, onlyIfOpen);
            else if (!onlyIfOpen)
                Ui.Info ("Put the cursor on a word first.");
        }

        void sbPrompt_Click (object sender, EventArgs e)
        {
            if (analysis == null)
                Analyse();
            // The prompt uses what's on screen, saved or not.
            Song.Title = teTitle.Text.Trim();
            Song.Status = cbStatus.Text;
            Song.Notes = meNotes.Text;
            host.ShowPrompt (new PromptContext
            {
                Song = Song,
                Ideas = ideas.Where (i => i.Lyric != null).Select (i => i.Lyric).ToList(),
                SongAnnotated = analysis != null ? SongAnalysis.Annotated (analysis) : meBody.Text,
                SongSchemes = analysis?.Summary ?? "",
                Description = "gathered for the song",
            });
        }

        // ---------------------------------------------------------------------------------

        public sealed class LineRow
        {
            internal LineRow (Core.Songs.SongLine line) { Data = line; }

            [Browsable (false)] public Core.Songs.SongLine Data { get; }
            [DisplayName ("Line")] public int Line => Data.Number;
            [DisplayName ("Rhyme")] public string Rhyme => Data.IsHeader ? "" : Data.Letter;
            [DisplayName ("Syl")] public string Syllables => Data.IsHeader ? "" : Data.Syllables + (Data.SyllablesSure ? "" : "?");
            [DisplayName ("Ends")] public string Ends => Data.IsHeader ? Data.Text.Trim() : Data.LastWord;
            [DisplayName ("Stress")] public string Stress => Data.Pattern;
        }

        sealed class IdeaItem
        {
            public IdeaItem (SongLyric idea) { Idea = idea; }
            public SongLyric Idea { get; }
            public override string ToString() =>
                $"{(Idea.Lyric.Text ?? "").Replace ("\r", " ").Replace ("\n", " / ")}   (#{Idea.Lyric.Oid})";
        }
    }
}
