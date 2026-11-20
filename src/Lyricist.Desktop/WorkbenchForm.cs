using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DumbAngel;
using Lyricist.Core;
using Lyricist.Core.Workbench;
using Lyricist.Core.Words;

namespace Lyricist.Desktop
{
    /// <summary>
    /// Exploring the collection: rhyme pairs (ready-made couplets), shuffle three (random
    /// collisions), the review queue (longest unreviewed first) and recurring words (optionally
    /// counting synonyms). The flag checkboxes at the top filter every tab (ticked = all must be set).
    /// Inactive ideas are left out. Double-click an idea anywhere to open it.
    /// </summary>
    public partial class WorkbenchForm : XtraForm
    {
        readonly AppSettings settings;
        readonly Func<IEnumerable<Lyric>> allLyrics;
        readonly Action<Lyric> openLyric;
        readonly FlagFilterControl filter = new FlagFilterControl { Dock = DockStyle.Fill };
        readonly Random random = new Random();
        readonly Lyric[] shuffled = new Lyric[3];
        readonly HashSet<XtraTabPageKey> stale = new HashSet<XtraTabPageKey>();

        enum XtraTabPageKey { Pairs, Shuffle, Review, Words }

        public WorkbenchForm (AppSettings settings, Func<IEnumerable<Lyric>> lyrics, Action<Lyric> openLyric)
        {
            InitializeComponent();
            this.settings = settings;
            allLyrics = lyrics;
            this.openLyric = openLyric;

            pnlFilter.Controls.Add (filter);
            filter.FilterChanged += (s, e) => MarkAllStale();
            rgAccent.EditValue = settings.Accent == "US" ? 1 : 0;
            rgAccent.EditValueChanged += (s, e) =>
            {
                settings.Accent = Accent == Accent.US ? "US" : "UK";
                try { settings.Save(); } catch { }
                ShowPairs();
            };
            ceNear.CheckedChanged += (s, e) => ShowPairs();
            ceSynonyms.CheckedChanged += (s, e) => ShowWords();
            tpShuffle.Resize += (s, e) => LayOutShuffle();
            MarkAllStale (refresh: false);
        }

        Accent Accent => Convert.ToInt32 (rgAccent.EditValue) == 1 ? Accent.US : Accent.UK;

        /// <summary>Active ideas passing the flag filter.</summary>
        List<Lyric> Lyrics() => allLyrics().Where (l => l != null && l.IsActive && filter.Matches (l)).ToList();

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            if (Owner != null)
                Location = new Point (Math.Max (0, Owner.Left + 40), Math.Max (0, Owner.Top + 40));
            LayOutShuffle();
            RefreshCurrent();
        }

        // Coming back to the window (after opening / editing ideas): the review queue has moved on.
        protected override void OnActivated (EventArgs e)
        {
            base.OnActivated (e);
            stale.Add (XtraTabPageKey.Review);
            if (IsHandleCreated && tabWork.SelectedTabPage == tpReview)
                RefreshCurrent();
        }

        // ---------------------------------------------------------------------------------

        void MarkAllStale (bool refresh = true)
        {
            foreach (XtraTabPageKey key in Enum.GetValues (typeof (XtraTabPageKey)))
                stale.Add (key);
            if (refresh && IsHandleCreated)
                RefreshCurrent();
        }

        XtraTabPageKey CurrentKey =>
            tabWork.SelectedTabPage == tpShuffle ? XtraTabPageKey.Shuffle
          : tabWork.SelectedTabPage == tpReview ? XtraTabPageKey.Review
          : tabWork.SelectedTabPage == tpWords ? XtraTabPageKey.Words
          : XtraTabPageKey.Pairs;

        /// <summary>Recomputes the tab on show only when its inputs changed.</summary>
        void RefreshCurrent()
        {
            XtraTabPageKey key = CurrentKey;
            if (!stale.Remove (key))
                return;
            switch (key)
            {
                case XtraTabPageKey.Pairs: ShowPairs(); break;
                case XtraTabPageKey.Shuffle: Shuffle (keepLocked: true); break;
                case XtraTabPageKey.Review: ShowReview(); break;
                case XtraTabPageKey.Words: ShowWords(); break;
            }
        }

        void tabWork_SelectedPageChanged (object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) => RefreshCurrent();

        string FilterNote => filter.Any ? $"   (only: {filter.Description})" : "";

        void Busy (Action action)
        {
            try
            {
                UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                action();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem working that out.", ex);
            }
            finally
            {
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
            }
        }

        // ---------------------------------------------------------------------------------
        // Rhyme pairs

        void ShowPairs() => Busy (() =>
        {
            Rhymer rhymer = WordData.RhymerFor (Accent);
            List<RhymePair> pairs = RhymePairs.Find (Lyrics(), rhymer, ceNear.Checked);
            var rows = pairs.Select (p => new PairRow (p)).ToList();
            gcPairs.DataSource = rows;
            ConfigureColumns (gvPairs);
            lblPairs.Text = $"{rows.Count} pair{(rows.Count == 1 ? "" : "s")}{FilterNote}";
        });

        void gvPairs_DoubleClick (object sender, EventArgs e)
        {
            var view = (GridView) sender;
            GridHitInfo hit = view.CalcHitInfo (view.GridControl.PointToClient (MousePosition));
            if (!hit.InRow || !(view.GetRow (hit.RowHandle) is PairRow row))
                return;
            bool second = hit.Column != null && hit.Column.FieldName.EndsWith ("B");
            openLyric (second ? row.Pair.B.Lyric : row.Pair.A.Lyric);
        }

        // ---------------------------------------------------------------------------------
        // Shuffle three

        CheckEdit[] Locks => new[] { ceLock1, ceLock2, ceLock3 };
        MemoEdit[] Memos => new[] { meShuffle1, meShuffle2, meShuffle3 };
        SimpleButton[] OpenButtons => new[] { sbOpen1, sbOpen2, sbOpen3 };

        void Shuffle (bool keepLocked)
        {
            List<Lyric> pool = Lyrics();
            for (int i = 0; i < 3; i++)
                if (keepLocked && Locks[i].Checked && shuffled[i] != null)
                    pool.Remove (shuffled[i]);

            for (int i = 0; i < 3; i++)
            {
                if (keepLocked && Locks[i].Checked && shuffled[i] != null)
                    continue;
                if (pool.Count == 0)
                {
                    shuffled[i] = null;
                    continue;
                }
                int pick = random.Next (pool.Count);
                shuffled[i] = pool[pick];
                pool.RemoveAt (pick);
            }

            for (int i = 0; i < 3; i++)
            {
                Memos[i].Text = Describe (shuffled[i]);
                OpenButtons[i].Enabled = shuffled[i] != null;
            }
            lblShuffle.Text = $"from {Lyrics().Count} ideas{FilterNote}";
        }

        static string Describe (Lyric lyric)
        {
            if (lyric == null)
                return "(no more ideas match the filter)";
            var text = new StringBuilder();
            text.AppendLine (lyric.Text).AppendLine();
            if (!string.IsNullOrWhiteSpace (lyric.SourceInfo))
                text.AppendLine (lyric.SourceInfo.Trim()).AppendLine();
            string themes = string.Join (", ", LyricFlags.All.Where (f => LyricFlags.IsSet (lyric, f)).Select (f => f.ShortCaption));
            if (themes.Length > 0)
                text.AppendLine (themes);
            text.Append ($"Rating: {lyric.Rating?.Title ?? "none"}   #{lyric.Oid}");
            return text.ToString();
        }

        void sbShuffle_Click (object sender, EventArgs e) => Shuffle (keepLocked: true);

        void sbOpen_Click (object sender, EventArgs e)
        {
            int i = Array.IndexOf (OpenButtons, sender);
            if (i >= 0 && shuffled[i] != null)
                openLyric (shuffled[i]);
        }

        void LayOutShuffle()
        {
            int gap = 12, left = 6, top = 70;
            int width = (tpShuffle.ClientSize.Width - left * 2 - gap * 2) / 3;
            int height = Math.Max (60, tpShuffle.ClientSize.Height - top - 6);
            for (int i = 0; i < 3; i++)
            {
                int x = left + i * (width + gap);
                Locks[i].Location = new Point (x, 42);
                OpenButtons[i].Location = new Point (x + 80, 40);
                Memos[i].SetBounds (x, top, width, height);
            }
        }

        // ---------------------------------------------------------------------------------
        // Review queue

        void ShowReview() => Busy (() =>
        {
            var rows = Lyrics().OrderBy (l => l.LastReviewedAt).Select (l => new ReviewRow (l)).ToList();
            gcReview.DataSource = rows;
            ConfigureColumns (gvReview);
            lblReview.Text = $"{rows.Count} ideas, longest unreviewed first; opening one counts as reviewing it{FilterNote}";
        });

        void sbOpenNext_Click (object sender, EventArgs e)
        {
            if (gvReview.GetRow (0) is ReviewRow row)
                openLyric (row.Item);
        }

        void gvReview_DoubleClick (object sender, EventArgs e)
        {
            var view = (GridView) sender;
            GridHitInfo hit = view.CalcHitInfo (view.GridControl.PointToClient (MousePosition));
            if (hit.InRow && view.GetRow (hit.RowHandle) is ReviewRow row)
                openLyric (row.Item);
        }

        // ---------------------------------------------------------------------------------
        // Recurring words

        void ShowWords() => Busy (() =>
        {
            Thesaurus thesaurus = ceSynonyms.Checked ? WordData.Thesaurus : null;
            List<WordCount> counts = RecurringWords.Count (Lyrics(), WordData.Dictionary, thesaurus);
            gcWords.DataSource = counts.Select (c => new WordRow (c)).ToList();
            ConfigureColumns (gvWords);
            foreach (string field in new[] { nameof (WordRow.WithSynonyms), nameof (WordRow.Synonyms) })
                if (gvWords.Columns[field] != null)
                    gvWords.Columns[field].Visible = ceSynonyms.Checked;
            lblWords.Text = $"{counts.Count} words used in 2 or more ideas{FilterNote}";
            ShowWordIdeas();
        });

        void gvWords_FocusedRowChanged (object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) => ShowWordIdeas();

        void ShowWordIdeas()
        {
            lbWordIdeas.BeginUpdate();
            try
            {
                lbWordIdeas.Items.Clear();
                if (gvWords.GetFocusedRow() is WordRow row)
                    foreach (Lyric lyric in row.Data.Lyrics)
                        lbWordIdeas.Items.Add (new IdeaItem (lyric));
            }
            finally
            {
                lbWordIdeas.EndUpdate();
            }
        }

        void lbWordIdeas_DoubleClick (object sender, EventArgs e)
        {
            if (lbWordIdeas.SelectedItem is IdeaItem item)
                openLyric (item.Lyric);
        }

        // ---------------------------------------------------------------------------------

        /// <summary>Columns come from the row class; this tidies them once they exist.</summary>
        static void ConfigureColumns (GridView view)
        {
            view.PopulateColumns();
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
            {
                if (column.ColumnType == typeof (DateTime))
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
                }
                if (column.ColumnType == typeof (string) && column.FieldName.StartsWith ("Line"))
                    column.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            }
            view.BestFitColumns();
        }

        public sealed class PairRow
        {
            internal PairRow (RhymePair pair) { Pair = pair; }

            [Browsable (false)] public RhymePair Pair { get; }
            [DisplayName ("Rhyme")] public string Kind => Pair.Kind == RhymeKind.Perfect ? "Perfect" : "Near";
            [DisplayName ("Words")] public string Words => $"{Pair.A.LastWord} / {Pair.B.LastWord}";
            [DisplayName ("First idea")] public string LineA => Pair.A.Line;
            [DisplayName ("Second idea")] public string LineB => Pair.B.Line;
        }

        public sealed class ReviewRow
        {
            internal ReviewRow (Lyric lyric) { Item = lyric; }

            [Browsable (false)] public Lyric Item { get; }
            [DisplayName ("Last reviewed")] public DateTime Reviewed => Item.LastReviewedAt;
            [DisplayName ("Idea")] public string Text => Item.Text;
            [DisplayName ("Rating")] public string Rating => Item.Rating?.Title ?? "";
            [DisplayName ("Conceived")] public DateTime? Conceived => Item.ConceivedAt;
        }

        public sealed class WordRow
        {
            internal WordRow (WordCount data) { Data = data; }

            [Browsable (false)] public WordCount Data { get; }
            [DisplayName ("Word")] public string Word => Data.Word;
            [DisplayName ("Ideas")] public int Ideas => Data.Ideas;
            [DisplayName ("With synonyms")] public int WithSynonyms => Data.WithSynonyms;
            [DisplayName ("Synonyms found")] public string Synonyms => string.Join (", ", Data.Synonyms);
        }

        sealed class IdeaItem
        {
            public IdeaItem (Lyric lyric) { Lyric = lyric; }
            public Lyric Lyric { get; }
            public override string ToString() => $"{Lyric.Text.Replace ("\r", " ").Replace ("\n", " / ")}   (#{Lyric.Oid})";
        }
    }
}
