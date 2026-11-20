using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DumbAngel;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    /// <summary>
    /// The lyric list (the prototype's ucLyricList). Every Lyric column is available: the
    /// main ones show, the rest (kinds, themes, dates, Ref) are in the Column Chooser
    /// (right-click a column header). Column layout, sorting and filters are remembered.
    /// Lyrics open in their own windows (several at once), beside the Words window.
    /// </summary>
    public partial class MainForm : XtraForm
    {
        readonly AppSettings settings;
        readonly Session session = new Session();   // uses XpoDefault.DataLayer (Database.Connect)
        XPCollection<Lyric> lyrics;
        readonly System.Collections.Generic.Dictionary<int, LyricEditForm> openForms =
            new System.Collections.Generic.Dictionary<int, LyricEditForm>();   // by Oid (saved lyrics)
        WordsForm words;
        WorkbenchForm workbench;
        FlagFilterControl filter;

        static string GridLayoutPath => Path.Combine (AppSettings.Folder, "grid-layout.xml");

        public MainForm (AppSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            try
            {
                filter = new FlagFilterControl { Dock = DockStyle.Fill };
                pnlFilters.Controls.Add (filter);
                filter.FilterChanged += (s, a) => gridView1.RefreshData();

                BuildColumns();
                lyrics = new XPCollection<Lyric> (session);
                gridControl1.DataSource = lyrics;
                if (File.Exists (GridLayoutPath))
                    gridView1.RestoreLayoutFromXml (GridLayoutPath);
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem loading the lyric list.", ex);
            }
        }

        protected override void OnFormClosing (FormClosingEventArgs e)
        {
            try
            {
                Directory.CreateDirectory (AppSettings.Folder);
                gridView1.SaveLayoutToXml (GridLayoutPath);
            }
            catch
            {
                // Not worth stopping the app closing for.
            }
            base.OnFormClosing (e);
        }

        // ---------------------------------------------------------------------------------
        // Columns

        void BuildColumns()
        {
            gridView1.Columns.Clear();

            GridColumn id = AddColumn ("Oid", "ID", 50, true, true);
            id.Summary.Add (new GridColumnSummaryItem (SummaryItemType.Count));
            AddColumn ("Text", "Lyric", 254, true);
            AddColumn ("SourceInfo", "Source Info", 161, true);
            AddColumn ("AdditionalInfo", "Additional Info", 165, true);
            AddColumn ("Rating.Title", "Rating", 69, true, true);
            AddColumn ("IsActiveText", "Active", 60, true, true);
            GridColumn conceived = AddColumn ("ConceivedAt", "Conceived", 132, true);
            conceived.DisplayFormat.FormatType = FormatType.DateTime;
            conceived.DisplayFormat.FormatString = "G";
            conceived.SortOrder = ColumnSortOrder.Descending;

            foreach (LyricFlag flag in LyricFlags.Kinds)
                AddColumn (flag.Property, flag.ShortCaption, 60, false, true);
            foreach (LyricFlag flag in LyricFlags.Themes)
                AddColumn (flag.Property, flag.ShortCaption, 60, false, true);

            AddColumn ("CreatedAt", "Created", 120, false);
            AddColumn ("UpdatedAt", "Updated", 120, false);
            AddColumn ("LastReviewedAt", "Reviewed", 120, false);
            AddColumn ("UniqueRef", "Ref", 200, false);
        }

        GridColumn AddColumn (string field, string caption, int width, bool visible, bool centred = false)
        {
            GridColumn column = gridView1.Columns.AddField (field);
            column.Name = "col" + field.Replace (".", "");   // stable names: the saved layout matches by name
            column.Caption = caption;
            column.Width = width;
            column.VisibleIndex = visible ? gridView1.VisibleColumns.Count : -1;
            if (centred)
            {
                column.AppearanceCell.Options.UseTextOptions = true;
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                column.AppearanceHeader.Options.UseTextOptions = true;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }
            return column;
        }

        // ---------------------------------------------------------------------------------
        // List

        void ReloadList (int focusOid = 0)
        {
            gridControl1.BeginUpdate();
            try
            {
                session.DropIdentityMap();
                lyrics.Reload();
            }
            finally
            {
                gridControl1.EndUpdate();
            }
            if (focusOid > 0)
            {
                int handle = gridView1.LocateByValue ("Oid", focusOid);
                if (handle != GridControl.InvalidRowHandle)
                {
                    gridView1.FocusedRowHandle = handle;
                    gridView1.MakeRowVisible (handle);
                }
            }
        }

        /// <summary>Opens a lyric in its own (modeless) window, or brings its window forward.</summary>
        void Edit (Lyric lyric)
        {
            try
            {
                if (lyric.HasBeenSaved && openForms.TryGetValue (lyric.Oid, out LyricEditForm open) && !open.IsDisposed)
                {
                    if (open.WindowState == FormWindowState.Minimized)
                        open.WindowState = FormWindowState.Normal;
                    open.Activate();
                    return;
                }

                bool isNew = !lyric.HasBeenSaved;
                if (!isNew)
                {
                    lyric.Reload();   // pick up changes made elsewhere since the list loaded
                    try
                    {
                        lyric.MarkReviewed();   // opening it counts as reviewing it (UpdatedAt untouched)
                    }
                    catch (Exception ex)
                    {
                        Ui.Log (ex);   // e.g. changed elsewhere a moment ago: not worth stopping for
                    }
                }

                var form = new LyricEditForm (lyric, settings, ShowWords);
                int oid = lyric.Oid;
                if (!isNew)
                    openForms[oid] = form;
                form.FormClosed += (s, e) =>
                {
                    if (!isNew)
                        openForms.Remove (oid);
                    if (!form.Saved)
                        return;
                    if (isNew)
                        ReloadList (lyric.Oid);
                    else
                        gridView1.RefreshData();
                };
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new System.Drawing.Point (
                    Math.Max (0, Left + (Width - form.Width) / 2 + 30 * openForms.Count),
                    Math.Max (0, Top + (Height - form.Height) / 2 + 30 * openForms.Count));
                form.Show (this);
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem editing the lyric.", ex);
            }
        }

        /// <summary>
        /// Opens (or reuses) the Words window and looks a word up. onlyIfOpen: do nothing unless
        /// it's already open (used by double-clicking a word in an edit form).
        /// </summary>
        public void ShowWords (string word, string line, bool onlyIfOpen)
        {
            try
            {
                if (words == null || words.IsDisposed)
                {
                    if (onlyIfOpen)
                        return;
                    words = new WordsForm (settings, () => lyrics, Edit);
                    words.Show (this);
                }
                else if (!words.Visible)
                    words.Show (this);
                words.LookUp (word, line);
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem opening the Words window.", ex);
            }
        }

        Lyric FocusedLyric => gridView1.GetRow (gridView1.FocusedRowHandle) as Lyric;

        // ---------------------------------------------------------------------------------
        // Events

        void gridView1_DoubleClick (object sender, EventArgs e)
        {
            var view = (GridView) sender;
            GridHitInfo info = view.CalcHitInfo (view.GridControl.PointToClient (Control.MousePosition));
            if ((info.InRow || info.InRowCell) && view.GetRow (info.RowHandle) is Lyric lyric)
                Edit (lyric);
        }

        /// <summary>The flag checkboxes: ticked flags must all be set (AND).</summary>
        void gridView1_CustomRowFilter (object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if (filter == null || !filter.Any || e.ListSourceRow < 0 || e.ListSourceRow >= lyrics.Count)
                return;
            if (!filter.Matches (lyrics[e.ListSourceRow]))
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        void gridView1_PopupMenuShowing (object sender, PopupMenuShowingEventArgs e)
        {
            if (!e.HitInfo.InRow)
                return;
            var view = (GridView) sender;
            view.FocusedRowHandle = e.HitInfo.RowHandle;
            if (!(view.GetRow (e.HitInfo.RowHandle) is Lyric lyric))
                return;

            e.Menu.Items.Add (new DXMenuItem ("Edit...", (s, a) => Edit (lyric)));
            e.Menu.Items.Add (new DXMenuItem ("Delete...", (s, a) => Delete (lyric)) { BeginGroup = true });
        }

        void Delete (Lyric lyric)
        {
            try
            {
                if (!Ui.Confirm ("Delete this idea?" + Environment.NewLine + Environment.NewLine + lyric.Text))
                    return;
                lyric.Delete();   // XPO deferred deletion (GCRecord), as the prototype
                ReloadList();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem deleting the lyric.", ex);
            }
        }

        void sbNew_Click (object sender, EventArgs e) =>
            Edit (new Lyric (session) { SourceInfo = settings.NewIdeaSource ?? "" });

        void sbRefresh_Click (object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ReloadList (FocusedLyric?.Oid ?? 0);
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem refreshing the list.", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void sbImport_Click (object sender, EventArgs e)
        {
            ImportResult result;
            try
            {
                Cursor = Cursors.WaitCursor;
                result = LyricImporter.ImportFromFtp (settings, session);
                ReloadList();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Ui.Error ("Problem importing lyric files.", ex);
                return;
            }
            Cursor = Cursors.Default;
            string text = result.Summary();
            if (result.Imported > 0 && string.IsNullOrWhiteSpace (settings.FtpArchiveDir))
                text += Environment.NewLine + Environment.NewLine
                      + "The files are still in the FTP folder (no archive folder is set in Settings). "
                      + "Importing again is safe: ideas already in the database are skipped.";
            Ui.Info (text);
        }

        void sbExport_Click (object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new SaveFileDialog { Title = "Export the list to", Filter = "Excel workbooks|*.xlsx" })
                {
                    if (dialog.ShowDialog (this) != DialogResult.OK)
                        return;
                    gridView1.ExportToXlsx (dialog.FileName);
                    Ui.Open (dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem exporting the list.", ex);
            }
        }

        void sbWords_Click (object sender, EventArgs e) =>
            ShowWords (FocusedLyric?.Text ?? "", null, false);

        void sbWorkbench_Click (object sender, EventArgs e)
        {
            try
            {
                if (workbench == null || workbench.IsDisposed)
                {
                    workbench = new WorkbenchForm (settings, () => lyrics, Edit);
                    workbench.Show (this);
                }
                else
                {
                    if (workbench.WindowState == FormWindowState.Minimized)
                        workbench.WindowState = FormWindowState.Normal;
                    workbench.Activate();
                }
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem opening the Workbench.", ex);
            }
        }

        void sbSettings_Click (object sender, EventArgs e)
        {
            string connection = settings.ConnectionString;
            using (var form = new SettingsForm (settings))
                if (form.ShowDialog (this) == DialogResult.OK && settings.ConnectionString != connection)
                    Ui.Info ("The new database connection is used the next time Lyricist starts.");
        }
    }
}
