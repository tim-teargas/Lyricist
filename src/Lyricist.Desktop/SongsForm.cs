using System;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Lyricist.Data;

namespace Lyricist.Desktop
{
    /// <summary>The songs being written (table Song): New, Open (double-click), Delete.</summary>
    public partial class SongsForm : XtraForm
    {
        readonly ILyricHost host;
        XPCollection<Song> songs;

        public SongsForm (ILyricHost host)
        {
            InitializeComponent();
            this.host = host;
            WindowMemory.Remember (this, "Songs");
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            try
            {
                AddColumn ("Title", "Title", 260);
                AddColumn ("Status", "Status", 90);
                AddColumn ("Rating.Title", "Rating", 90);
                GridColumn updated = AddColumn ("UpdatedAt", "Updated", 130);
                updated.DisplayFormat.FormatType = FormatType.DateTime;
                updated.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
                updated.SortOrder = ColumnSortOrder.Descending;
                GridColumn created = AddColumn ("CreatedAt", "Created", 130);
                created.DisplayFormat.FormatType = FormatType.DateTime;
                created.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";

                songs = new XPCollection<Song> (host.Session);
                gcSongs.DataSource = songs;
                ShowCount();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem loading the songs.", ex);
            }
        }

        protected override void OnActivated (EventArgs e)
        {
            base.OnActivated (e);
            if (songs == null)
                return;
            try
            {
                songs.Reload();   // titles / status may have changed in a song's window
                ShowCount();
            }
            catch (Exception ex)
            {
                Ui.Log (ex);
            }
        }

        GridColumn AddColumn (string field, string caption, int width)
        {
            GridColumn column = gvSongs.Columns.AddField (field);
            column.Name = "col" + field.Replace (".", "");
            column.Caption = caption;
            column.Width = width;
            column.VisibleIndex = gvSongs.VisibleColumns.Count;
            return column;
        }

        void ShowCount() => lblSongs.Text = $"{songs.Count} song{(songs.Count == 1 ? "" : "s")}";

        Song FocusedSong => gvSongs.GetFocusedRow() as Song;

        void sbNewSong_Click (object sender, EventArgs e) =>
            host.OpenSong (new Song (host.Session) { Title = "" });

        void sbOpenSong_Click (object sender, EventArgs e)
        {
            if (FocusedSong != null)
                host.OpenSong (FocusedSong);
        }

        void gvSongs_DoubleClick (object sender, EventArgs e)
        {
            var view = (GridView) sender;
            GridHitInfo hit = view.CalcHitInfo (view.GridControl.PointToClient (MousePosition));
            if (hit.InRow && view.GetRow (hit.RowHandle) is Song song)
                host.OpenSong (song);
        }

        void sbDeleteSong_Click (object sender, EventArgs e)
        {
            Song song = FocusedSong;
            if (song == null || !Ui.Confirm ($"Delete the song \"{song.Title}\"?"))
                return;
            try
            {
                song.Delete();   // deferred (GCRecord); its idea links go with it
                songs.Reload();
                ShowCount();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem deleting the song.", ex);
            }
        }
    }
}
