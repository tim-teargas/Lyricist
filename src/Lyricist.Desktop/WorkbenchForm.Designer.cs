namespace Lyricist.Desktop
{
    partial class WorkbenchForm
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
            this.pnlFilter = new DevExpress.XtraEditors.PanelControl();
            this.tabWork = new DevExpress.XtraTab.XtraTabControl();
            this.tpPairs = new DevExpress.XtraTab.XtraTabPage();
            this.tpShuffle = new DevExpress.XtraTab.XtraTabPage();
            this.tpReview = new DevExpress.XtraTab.XtraTabPage();
            this.tpWords = new DevExpress.XtraTab.XtraTabPage();
            this.rgAccent = new DevExpress.XtraEditors.RadioGroup();
            this.ceNear = new DevExpress.XtraEditors.CheckEdit();
            this.lblPairs = new DevExpress.XtraEditors.LabelControl();
            this.gcPairs = new DevExpress.XtraGrid.GridControl();
            this.gvPairs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblFamilyLines = new DevExpress.XtraEditors.LabelControl();
            this.lbFamilyLines = new DevExpress.XtraEditors.ListBoxControl();
            this.sbShuffle = new DevExpress.XtraEditors.SimpleButton();
            this.lblShuffle = new DevExpress.XtraEditors.LabelControl();
            this.ceLock1 = new DevExpress.XtraEditors.CheckEdit();
            this.sbOpen1 = new DevExpress.XtraEditors.SimpleButton();
            this.meShuffle1 = new DevExpress.XtraEditors.MemoEdit();
            this.ceLock2 = new DevExpress.XtraEditors.CheckEdit();
            this.sbOpen2 = new DevExpress.XtraEditors.SimpleButton();
            this.meShuffle2 = new DevExpress.XtraEditors.MemoEdit();
            this.ceLock3 = new DevExpress.XtraEditors.CheckEdit();
            this.sbOpen3 = new DevExpress.XtraEditors.SimpleButton();
            this.meShuffle3 = new DevExpress.XtraEditors.MemoEdit();
            this.sbOpenNext = new DevExpress.XtraEditors.SimpleButton();
            this.lblReview = new DevExpress.XtraEditors.LabelControl();
            this.gcReview = new DevExpress.XtraGrid.GridControl();
            this.gvReview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ceSynonyms = new DevExpress.XtraEditors.CheckEdit();
            this.lblWords = new DevExpress.XtraEditors.LabelControl();
            this.gcWords = new DevExpress.XtraGrid.GridControl();
            this.gvWords = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblWordIdeas = new DevExpress.XtraEditors.LabelControl();
            this.lbWordIdeas = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAccent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceNear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPairs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPairs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFamilyLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSynonyms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbWordIdeas)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.pnlFilter.Location = new System.Drawing.Point(12, 10);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFilter.Size = new System.Drawing.Size(976, 58);
            this.pnlFilter.TabIndex = 0;
            // 
            // tabWork
            // 
            this.tabWork.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.tabWork.Location = new System.Drawing.Point(12, 74);
            this.tabWork.Name = "tabWork";
            this.tabWork.SelectedTabPage = this.tpPairs;
            this.tabWork.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpPairs,
            this.tpShuffle,
            this.tpReview,
            this.tpWords});
            this.tabWork.Size = new System.Drawing.Size(976, 594);
            this.tabWork.TabIndex = 1;
            this.tabWork.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabWork_SelectedPageChanged);
            // 
            // tpPairs
            // 
            this.tpPairs.Name = "tpPairs";
            this.tpPairs.Size = new System.Drawing.Size(970, 566);
            this.tpPairs.Controls.Add(this.rgAccent);
            this.tpPairs.Controls.Add(this.ceNear);
            this.tpPairs.Controls.Add(this.lblPairs);
            this.tpPairs.Controls.Add(this.gcPairs);
            this.tpPairs.Controls.Add(this.lblFamilyLines);
            this.tpPairs.Controls.Add(this.lbFamilyLines);
            this.tpPairs.Text = "Rhyme families";
            // 
            // tpShuffle
            // 
            this.tpShuffle.Name = "tpShuffle";
            this.tpShuffle.Size = new System.Drawing.Size(970, 566);
            this.tpShuffle.Controls.Add(this.sbShuffle);
            this.tpShuffle.Controls.Add(this.lblShuffle);
            this.tpShuffle.Controls.Add(this.ceLock1);
            this.tpShuffle.Controls.Add(this.sbOpen1);
            this.tpShuffle.Controls.Add(this.meShuffle1);
            this.tpShuffle.Controls.Add(this.ceLock2);
            this.tpShuffle.Controls.Add(this.sbOpen2);
            this.tpShuffle.Controls.Add(this.meShuffle2);
            this.tpShuffle.Controls.Add(this.ceLock3);
            this.tpShuffle.Controls.Add(this.sbOpen3);
            this.tpShuffle.Controls.Add(this.meShuffle3);
            this.tpShuffle.Text = "Shuffle three";
            // 
            // tpReview
            // 
            this.tpReview.Name = "tpReview";
            this.tpReview.Size = new System.Drawing.Size(970, 566);
            this.tpReview.Controls.Add(this.sbOpenNext);
            this.tpReview.Controls.Add(this.lblReview);
            this.tpReview.Controls.Add(this.gcReview);
            this.tpReview.Text = "Review queue";
            // 
            // tpWords
            // 
            this.tpWords.Name = "tpWords";
            this.tpWords.Size = new System.Drawing.Size(970, 566);
            this.tpWords.Controls.Add(this.ceSynonyms);
            this.tpWords.Controls.Add(this.lblWords);
            this.tpWords.Controls.Add(this.gcWords);
            this.tpWords.Controls.Add(this.lblWordIdeas);
            this.tpWords.Controls.Add(this.lbWordIdeas);
            this.tpWords.Text = "Recurring words";
            // 
            // rgAccent
            // 
            this.rgAccent.Location = new System.Drawing.Point(6, 6);
            this.rgAccent.Name = "rgAccent";
            this.rgAccent.Properties.Columns = 2;
            this.rgAccent.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "UK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "US")});
            this.rgAccent.Size = new System.Drawing.Size(110, 26);
            this.rgAccent.TabIndex = 2;
            // 
            // ceNear
            // 
            this.ceNear.Location = new System.Drawing.Point(130, 9);
            this.ceNear.Name = "ceNear";
            this.ceNear.Size = new System.Drawing.Size(230, 20);
            this.ceNear.TabIndex = 3;
            this.ceNear.Properties.Caption = "Show near rhymes too";
            // 
            // lblPairs
            // 
            this.lblPairs.Location = new System.Drawing.Point(370, 11);
            this.lblPairs.Name = "lblPairs";
            this.lblPairs.TabIndex = 4;
            this.lblPairs.Text = "";
            // 
            // gcPairs
            // 
            this.gcPairs.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.gcPairs.Location = new System.Drawing.Point(6, 40);
            this.gcPairs.Name = "gcPairs";
            this.gcPairs.MainView = this.gvPairs;
            this.gcPairs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPairs});
            this.gcPairs.Size = new System.Drawing.Size(958, 300);
            this.gcPairs.TabIndex = 5;
            // 
            // gvPairs
            // 
            this.gvPairs.Name = "gvPairs";
            this.gvPairs.GridControl = this.gcPairs;
            this.gvPairs.OptionsBehavior.Editable = false;
            this.gvPairs.OptionsFind.AlwaysVisible = true;
            this.gvPairs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPairs.OptionsView.RowAutoHeight = true;
            this.gvPairs.OptionsView.ShowGroupPanel = false;
            this.gvPairs.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPairs_FocusedRowChanged);
            // 
            // lblFamilyLines
            // 
            this.lblFamilyLines.Location = new System.Drawing.Point(6, 348);
            this.lblFamilyLines.Name = "lblFamilyLines";
            this.lblFamilyLines.TabIndex = 6;
            this.lblFamilyLines.Text = "Lines in this rhyme family:";
            // 
            // lbFamilyLines
            // 
            this.lbFamilyLines.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.lbFamilyLines.Location = new System.Drawing.Point(6, 368);
            this.lbFamilyLines.Name = "lbFamilyLines";
            this.lbFamilyLines.Size = new System.Drawing.Size(958, 192);
            this.lbFamilyLines.TabIndex = 7;
            this.lbFamilyLines.DoubleClick += new System.EventHandler(this.lbFamilyLines_DoubleClick);
            // 
            // sbShuffle
            // 
            this.sbShuffle.Location = new System.Drawing.Point(6, 6);
            this.sbShuffle.Name = "sbShuffle";
            this.sbShuffle.Size = new System.Drawing.Size(120, 26);
            this.sbShuffle.TabIndex = 8;
            this.sbShuffle.Text = "Shuffle";
            this.sbShuffle.Click += new System.EventHandler(this.sbShuffle_Click);
            // 
            // lblShuffle
            // 
            this.lblShuffle.Location = new System.Drawing.Point(140, 11);
            this.lblShuffle.Name = "lblShuffle";
            this.lblShuffle.TabIndex = 9;
            this.lblShuffle.Text = "";
            // 
            // ceLock1
            // 
            this.ceLock1.Location = new System.Drawing.Point(6, 42);
            this.ceLock1.Name = "ceLock1";
            this.ceLock1.Size = new System.Drawing.Size(70, 20);
            this.ceLock1.TabIndex = 10;
            this.ceLock1.Properties.Caption = "Lock";
            // 
            // sbOpen1
            // 
            this.sbOpen1.Location = new System.Drawing.Point(86, 40);
            this.sbOpen1.Name = "sbOpen1";
            this.sbOpen1.Size = new System.Drawing.Size(80, 23);
            this.sbOpen1.TabIndex = 11;
            this.sbOpen1.Text = "Open";
            this.sbOpen1.Click += new System.EventHandler(this.sbOpen_Click);
            // 
            // meShuffle1
            // 
            this.meShuffle1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.meShuffle1.Location = new System.Drawing.Point(6, 70);
            this.meShuffle1.Name = "meShuffle1";
            this.meShuffle1.Properties.ReadOnly = true;
            this.meShuffle1.Size = new System.Drawing.Size(310, 490);
            this.meShuffle1.TabIndex = 12;
            // 
            // ceLock2
            // 
            this.ceLock2.Location = new System.Drawing.Point(328, 42);
            this.ceLock2.Name = "ceLock2";
            this.ceLock2.Size = new System.Drawing.Size(70, 20);
            this.ceLock2.TabIndex = 13;
            this.ceLock2.Properties.Caption = "Lock";
            // 
            // sbOpen2
            // 
            this.sbOpen2.Location = new System.Drawing.Point(408, 40);
            this.sbOpen2.Name = "sbOpen2";
            this.sbOpen2.Size = new System.Drawing.Size(80, 23);
            this.sbOpen2.TabIndex = 14;
            this.sbOpen2.Text = "Open";
            this.sbOpen2.Click += new System.EventHandler(this.sbOpen_Click);
            // 
            // meShuffle2
            // 
            this.meShuffle2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.meShuffle2.Location = new System.Drawing.Point(328, 70);
            this.meShuffle2.Name = "meShuffle2";
            this.meShuffle2.Properties.ReadOnly = true;
            this.meShuffle2.Size = new System.Drawing.Size(310, 490);
            this.meShuffle2.TabIndex = 15;
            // 
            // ceLock3
            // 
            this.ceLock3.Location = new System.Drawing.Point(650, 42);
            this.ceLock3.Name = "ceLock3";
            this.ceLock3.Size = new System.Drawing.Size(70, 20);
            this.ceLock3.TabIndex = 16;
            this.ceLock3.Properties.Caption = "Lock";
            // 
            // sbOpen3
            // 
            this.sbOpen3.Location = new System.Drawing.Point(730, 40);
            this.sbOpen3.Name = "sbOpen3";
            this.sbOpen3.Size = new System.Drawing.Size(80, 23);
            this.sbOpen3.TabIndex = 17;
            this.sbOpen3.Text = "Open";
            this.sbOpen3.Click += new System.EventHandler(this.sbOpen_Click);
            // 
            // meShuffle3
            // 
            this.meShuffle3.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.meShuffle3.Location = new System.Drawing.Point(650, 70);
            this.meShuffle3.Name = "meShuffle3";
            this.meShuffle3.Properties.ReadOnly = true;
            this.meShuffle3.Size = new System.Drawing.Size(310, 490);
            this.meShuffle3.TabIndex = 18;
            // 
            // sbOpenNext
            // 
            this.sbOpenNext.Location = new System.Drawing.Point(6, 6);
            this.sbOpenNext.Name = "sbOpenNext";
            this.sbOpenNext.Size = new System.Drawing.Size(170, 26);
            this.sbOpenNext.TabIndex = 19;
            this.sbOpenNext.Text = "Open the next one";
            this.sbOpenNext.Click += new System.EventHandler(this.sbOpenNext_Click);
            // 
            // lblReview
            // 
            this.lblReview.Location = new System.Drawing.Point(190, 11);
            this.lblReview.Name = "lblReview";
            this.lblReview.TabIndex = 20;
            this.lblReview.Text = "";
            // 
            // gcReview
            // 
            this.gcReview.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.gcReview.Location = new System.Drawing.Point(6, 40);
            this.gcReview.Name = "gcReview";
            this.gcReview.MainView = this.gvReview;
            this.gcReview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReview});
            this.gcReview.Size = new System.Drawing.Size(958, 520);
            this.gcReview.TabIndex = 21;
            // 
            // gvReview
            // 
            this.gvReview.Name = "gvReview";
            this.gvReview.GridControl = this.gcReview;
            this.gvReview.OptionsBehavior.Editable = false;
            this.gvReview.OptionsFind.AlwaysVisible = true;
            this.gvReview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvReview.OptionsView.RowAutoHeight = true;
            this.gvReview.OptionsView.ShowGroupPanel = false;
            this.gvReview.DoubleClick += new System.EventHandler(this.gvReview_DoubleClick);
            // 
            // ceSynonyms
            // 
            this.ceSynonyms.Location = new System.Drawing.Point(6, 9);
            this.ceSynonyms.Name = "ceSynonyms";
            this.ceSynonyms.Size = new System.Drawing.Size(190, 20);
            this.ceSynonyms.TabIndex = 22;
            this.ceSynonyms.Properties.Caption = "Count synonyms too";
            // 
            // lblWords
            // 
            this.lblWords.Location = new System.Drawing.Point(210, 11);
            this.lblWords.Name = "lblWords";
            this.lblWords.TabIndex = 23;
            this.lblWords.Text = "";
            // 
            // gcWords
            // 
            this.gcWords.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.gcWords.Location = new System.Drawing.Point(6, 36);
            this.gcWords.Name = "gcWords";
            this.gcWords.MainView = this.gvWords;
            this.gcWords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWords});
            this.gcWords.Size = new System.Drawing.Size(958, 300);
            this.gcWords.TabIndex = 24;
            // 
            // gvWords
            // 
            this.gvWords.Name = "gvWords";
            this.gvWords.GridControl = this.gcWords;
            this.gvWords.OptionsBehavior.Editable = false;
            this.gvWords.OptionsFind.AlwaysVisible = true;
            this.gvWords.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvWords.OptionsView.RowAutoHeight = true;
            this.gvWords.OptionsView.ShowGroupPanel = false;
            this.gvWords.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvWords_FocusedRowChanged);
            // 
            // lblWordIdeas
            // 
            this.lblWordIdeas.Location = new System.Drawing.Point(6, 344);
            this.lblWordIdeas.Name = "lblWordIdeas";
            this.lblWordIdeas.TabIndex = 25;
            this.lblWordIdeas.Text = "Ideas using the word (or its synonyms):";
            // 
            // lbWordIdeas
            // 
            this.lbWordIdeas.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.lbWordIdeas.Location = new System.Drawing.Point(6, 364);
            this.lbWordIdeas.Name = "lbWordIdeas";
            this.lbWordIdeas.Size = new System.Drawing.Size(958, 196);
            this.lbWordIdeas.TabIndex = 26;
            this.lbWordIdeas.DoubleClick += new System.EventHandler(this.lbWordIdeas_DoubleClick);
            // 
            // WorkbenchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.tabWork);
            this.Controls.Add(this.pnlFilter);
            this.MinimumSize = new System.Drawing.Size(800, 560);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Name = "WorkbenchForm";
            this.Text = "Workbench";
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAccent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceNear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPairs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPairs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbFamilyLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLock3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meShuffle3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSynonyms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbWordIdeas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFilter;
        private DevExpress.XtraTab.XtraTabControl tabWork;
        private DevExpress.XtraTab.XtraTabPage tpPairs;
        private DevExpress.XtraTab.XtraTabPage tpShuffle;
        private DevExpress.XtraTab.XtraTabPage tpReview;
        private DevExpress.XtraTab.XtraTabPage tpWords;
        private DevExpress.XtraEditors.RadioGroup rgAccent;
        private DevExpress.XtraEditors.CheckEdit ceNear;
        private DevExpress.XtraEditors.LabelControl lblPairs;
        private DevExpress.XtraGrid.GridControl gcPairs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPairs;
        private DevExpress.XtraEditors.LabelControl lblFamilyLines;
        private DevExpress.XtraEditors.ListBoxControl lbFamilyLines;
        private DevExpress.XtraEditors.SimpleButton sbShuffle;
        private DevExpress.XtraEditors.LabelControl lblShuffle;
        private DevExpress.XtraEditors.CheckEdit ceLock1;
        private DevExpress.XtraEditors.SimpleButton sbOpen1;
        private DevExpress.XtraEditors.MemoEdit meShuffle1;
        private DevExpress.XtraEditors.CheckEdit ceLock2;
        private DevExpress.XtraEditors.SimpleButton sbOpen2;
        private DevExpress.XtraEditors.MemoEdit meShuffle2;
        private DevExpress.XtraEditors.CheckEdit ceLock3;
        private DevExpress.XtraEditors.SimpleButton sbOpen3;
        private DevExpress.XtraEditors.MemoEdit meShuffle3;
        private DevExpress.XtraEditors.SimpleButton sbOpenNext;
        private DevExpress.XtraEditors.LabelControl lblReview;
        private DevExpress.XtraGrid.GridControl gcReview;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReview;
        private DevExpress.XtraEditors.CheckEdit ceSynonyms;
        private DevExpress.XtraEditors.LabelControl lblWords;
        private DevExpress.XtraGrid.GridControl gcWords;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWords;
        private DevExpress.XtraEditors.LabelControl lblWordIdeas;
        private DevExpress.XtraEditors.ListBoxControl lbWordIdeas;
    }
}
