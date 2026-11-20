namespace Lyricist.Desktop
{
    partial class SongsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sbNewSong = new DevExpress.XtraEditors.SimpleButton();
            this.sbOpenSong = new DevExpress.XtraEditors.SimpleButton();
            this.sbDeleteSong = new DevExpress.XtraEditors.SimpleButton();
            this.lblSongs = new DevExpress.XtraEditors.LabelControl();
            this.gcSongs = new DevExpress.XtraGrid.GridControl();
            this.gvSongs = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcSongs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSongs)).BeginInit();
            this.SuspendLayout();
            // 
            // sbNewSong
            // 
            this.sbNewSong.Location = new System.Drawing.Point(12, 12);
            this.sbNewSong.Name = "sbNewSong";
            this.sbNewSong.Size = new System.Drawing.Size(100, 23);
            this.sbNewSong.TabIndex = 0;
            this.sbNewSong.Text = "New song";
            this.sbNewSong.Click += new System.EventHandler(this.sbNewSong_Click);
            // 
            // sbOpenSong
            // 
            this.sbOpenSong.Location = new System.Drawing.Point(118, 12);
            this.sbOpenSong.Name = "sbOpenSong";
            this.sbOpenSong.Size = new System.Drawing.Size(100, 23);
            this.sbOpenSong.TabIndex = 1;
            this.sbOpenSong.Text = "Open";
            this.sbOpenSong.Click += new System.EventHandler(this.sbOpenSong_Click);
            // 
            // sbDeleteSong
            // 
            this.sbDeleteSong.Location = new System.Drawing.Point(224, 12);
            this.sbDeleteSong.Name = "sbDeleteSong";
            this.sbDeleteSong.Size = new System.Drawing.Size(100, 23);
            this.sbDeleteSong.TabIndex = 2;
            this.sbDeleteSong.Text = "Delete...";
            this.sbDeleteSong.Click += new System.EventHandler(this.sbDeleteSong_Click);
            // 
            // lblSongs
            // 
            this.lblSongs.Location = new System.Drawing.Point(340, 17);
            this.lblSongs.Name = "lblSongs";
            this.lblSongs.TabIndex = 3;
            this.lblSongs.Text = "";
            // 
            // gcSongs
            // 
            this.gcSongs.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.gcSongs.Location = new System.Drawing.Point(12, 44);
            this.gcSongs.Name = "gcSongs";
            this.gcSongs.MainView = this.gvSongs;
            this.gcSongs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSongs});
            this.gcSongs.Size = new System.Drawing.Size(776, 444);
            this.gcSongs.TabIndex = 4;
            // 
            // gvSongs
            // 
            this.gvSongs.Name = "gvSongs";
            this.gvSongs.GridControl = this.gcSongs;
            this.gvSongs.OptionsBehavior.Editable = false;
            this.gvSongs.OptionsFind.AlwaysVisible = true;
            this.gvSongs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvSongs.OptionsView.RowAutoHeight = true;
            this.gvSongs.OptionsView.ShowGroupPanel = false;
            this.gvSongs.DoubleClick += new System.EventHandler(this.gvSongs_DoubleClick);
            // 
            // SongsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.gcSongs);
            this.Controls.Add(this.lblSongs);
            this.Controls.Add(this.sbDeleteSong);
            this.Controls.Add(this.sbOpenSong);
            this.Controls.Add(this.sbNewSong);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "SongsForm";
            this.Text = "Songs";
            ((System.ComponentModel.ISupportInitialize)(this.gcSongs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSongs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbNewSong;
        private DevExpress.XtraEditors.SimpleButton sbOpenSong;
        private DevExpress.XtraEditors.SimpleButton sbDeleteSong;
        private DevExpress.XtraEditors.LabelControl lblSongs;
        private DevExpress.XtraGrid.GridControl gcSongs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSongs;
    }
}
