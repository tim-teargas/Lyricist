namespace Lyricist.Desktop
{
    partial class WordsForm
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
            this.lblWord = new DevExpress.XtraEditors.LabelControl();
            this.teWord = new DevExpress.XtraEditors.TextEdit();
            this.sbLookUp = new DevExpress.XtraEditors.SimpleButton();
            this.sbBack = new DevExpress.XtraEditors.SimpleButton();
            this.rgAccent = new DevExpress.XtraEditors.RadioGroup();
            this.ceRare = new DevExpress.XtraEditors.CheckEdit();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.teLine = new DevExpress.XtraEditors.TextEdit();
            this.meLineScan = new DevExpress.XtraEditors.MemoEdit();
            this.tabResults = new DevExpress.XtraTab.XtraTabControl();
            this.tpRhymes = new DevExpress.XtraTab.XtraTabPage();
            this.tpSynonyms = new DevExpress.XtraTab.XtraTabPage();
            this.tpIdeas = new DevExpress.XtraTab.XtraTabPage();
            this.lblPerfect = new DevExpress.XtraEditors.LabelControl();
            this.lblNear = new DevExpress.XtraEditors.LabelControl();
            this.lblVowel = new DevExpress.XtraEditors.LabelControl();
            this.lbPerfect = new DevExpress.XtraEditors.ListBoxControl();
            this.lbNear = new DevExpress.XtraEditors.ListBoxControl();
            this.lbVowel = new DevExpress.XtraEditors.ListBoxControl();
            this.meSynonyms = new DevExpress.XtraEditors.MemoEdit();
            this.lbIdeas = new DevExpress.XtraEditors.ListBoxControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.teWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAccent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meLineScan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbPerfect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbNear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbVowel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meSynonyms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbIdeas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWord
            // 
            this.lblWord.Location = new System.Drawing.Point(12, 15);
            this.lblWord.Name = "lblWord";
            this.lblWord.TabIndex = 0;
            this.lblWord.Text = "Word:";
            // 
            // teWord
            // 
            this.teWord.Location = new System.Drawing.Point(60, 12);
            this.teWord.Name = "teWord";
            this.teWord.Size = new System.Drawing.Size(200, 20);
            this.teWord.TabIndex = 1;
            // 
            // sbLookUp
            // 
            this.sbLookUp.Location = new System.Drawing.Point(266, 10);
            this.sbLookUp.Name = "sbLookUp";
            this.sbLookUp.Size = new System.Drawing.Size(80, 23);
            this.sbLookUp.TabIndex = 2;
            this.sbLookUp.Text = "Look up";
            this.sbLookUp.Click += new System.EventHandler(this.sbLookUp_Click);
            // 
            // sbBack
            // 
            this.sbBack.Location = new System.Drawing.Point(352, 10);
            this.sbBack.Name = "sbBack";
            this.sbBack.Size = new System.Drawing.Size(60, 23);
            this.sbBack.TabIndex = 3;
            this.sbBack.Text = "Back";
            this.sbBack.Click += new System.EventHandler(this.sbBack_Click);
            // 
            // rgAccent
            // 
            this.rgAccent.Location = new System.Drawing.Point(420, 8);
            this.rgAccent.Name = "rgAccent";
            this.rgAccent.Properties.Columns = 2;
            this.rgAccent.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "UK"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "US")});
            this.rgAccent.Size = new System.Drawing.Size(110, 26);
            this.rgAccent.TabIndex = 4;
            // 
            // ceRare
            // 
            this.ceRare.Location = new System.Drawing.Point(540, 12);
            this.ceRare.Name = "ceRare";
            this.ceRare.Size = new System.Drawing.Size(160, 20);
            this.ceRare.TabIndex = 5;
            this.ceRare.Properties.Caption = "Include rare words";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.lblInfo.Location = new System.Drawing.Point(12, 44);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "";
            // 
            // lblLine
            // 
            this.lblLine.Location = new System.Drawing.Point(12, 71);
            this.lblLine.Name = "lblLine";
            this.lblLine.TabIndex = 7;
            this.lblLine.Text = "Line:";
            // 
            // teLine
            // 
            this.teLine.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teLine.Location = new System.Drawing.Point(60, 68);
            this.teLine.Name = "teLine";
            this.teLine.Size = new System.Drawing.Size(688, 20);
            this.teLine.TabIndex = 8;
            // 
            // meLineScan
            // 
            this.meLineScan.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.meLineScan.Location = new System.Drawing.Point(60, 94);
            this.meLineScan.Name = "meLineScan";
            this.meLineScan.Properties.ReadOnly = true;
            this.meLineScan.Size = new System.Drawing.Size(688, 44);
            this.meLineScan.TabIndex = 9;
            // 
            // tabResults
            // 
            this.tabResults.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.tabResults.Location = new System.Drawing.Point(12, 146);
            this.tabResults.Name = "tabResults";
            this.tabResults.SelectedTabPage = this.tpRhymes;
            this.tabResults.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpRhymes,
            this.tpSynonyms,
            this.tpIdeas});
            this.tabResults.Size = new System.Drawing.Size(736, 378);
            this.tabResults.TabIndex = 10;
            // 
            // tpRhymes
            // 
            this.tpRhymes.Name = "tpRhymes";
            this.tpRhymes.Size = new System.Drawing.Size(730, 350);
            this.tpRhymes.Controls.Add(this.lblPerfect);
            this.tpRhymes.Controls.Add(this.lblNear);
            this.tpRhymes.Controls.Add(this.lblVowel);
            this.tpRhymes.Controls.Add(this.lbPerfect);
            this.tpRhymes.Controls.Add(this.lbNear);
            this.tpRhymes.Controls.Add(this.lbVowel);
            this.tpRhymes.Text = "Rhymes";
            // 
            // tpSynonyms
            // 
            this.tpSynonyms.Name = "tpSynonyms";
            this.tpSynonyms.Size = new System.Drawing.Size(730, 350);
            this.tpSynonyms.Controls.Add(this.meSynonyms);
            this.tpSynonyms.Text = "Synonyms";
            // 
            // tpIdeas
            // 
            this.tpIdeas.Name = "tpIdeas";
            this.tpIdeas.Size = new System.Drawing.Size(730, 350);
            this.tpIdeas.Controls.Add(this.lbIdeas);
            this.tpIdeas.Text = "In your ideas";
            // 
            // lblPerfect
            // 
            this.lblPerfect.Location = new System.Drawing.Point(6, 6);
            this.lblPerfect.Name = "lblPerfect";
            this.lblPerfect.TabIndex = 11;
            this.lblPerfect.Text = "Perfect";
            // 
            // lblNear
            // 
            this.lblNear.Location = new System.Drawing.Point(250, 6);
            this.lblNear.Name = "lblNear";
            this.lblNear.TabIndex = 12;
            this.lblNear.Text = "Near";
            // 
            // lblVowel
            // 
            this.lblVowel.Location = new System.Drawing.Point(494, 6);
            this.lblVowel.Name = "lblVowel";
            this.lblVowel.TabIndex = 13;
            this.lblVowel.Text = "Vowel only";
            // 
            // lbPerfect
            // 
            this.lbPerfect.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.lbPerfect.Location = new System.Drawing.Point(6, 24);
            this.lbPerfect.Name = "lbPerfect";
            this.lbPerfect.Size = new System.Drawing.Size(236, 320);
            this.lbPerfect.TabIndex = 14;
            this.lbPerfect.DoubleClick += new System.EventHandler(this.lbRhyme_DoubleClick);
            // 
            // lbNear
            // 
            this.lbNear.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.lbNear.Location = new System.Drawing.Point(250, 24);
            this.lbNear.Name = "lbNear";
            this.lbNear.Size = new System.Drawing.Size(236, 320);
            this.lbNear.TabIndex = 15;
            this.lbNear.DoubleClick += new System.EventHandler(this.lbRhyme_DoubleClick);
            // 
            // lbVowel
            // 
            this.lbVowel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left);
            this.lbVowel.Location = new System.Drawing.Point(494, 24);
            this.lbVowel.Name = "lbVowel";
            this.lbVowel.Size = new System.Drawing.Size(230, 320);
            this.lbVowel.TabIndex = 16;
            this.lbVowel.DoubleClick += new System.EventHandler(this.lbRhyme_DoubleClick);
            // 
            // meSynonyms
            // 
            this.meSynonyms.Location = new System.Drawing.Point(0, 0);
            this.meSynonyms.Name = "meSynonyms";
            this.meSynonyms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meSynonyms.Properties.ReadOnly = true;
            this.meSynonyms.Size = new System.Drawing.Size(730, 350);
            this.meSynonyms.TabIndex = 17;
            this.meSynonyms.DoubleClick += new System.EventHandler(this.meSynonyms_DoubleClick);
            // 
            // lbIdeas
            // 
            this.lbIdeas.Location = new System.Drawing.Point(0, 0);
            this.lbIdeas.Name = "lbIdeas";
            this.lbIdeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIdeas.Size = new System.Drawing.Size(730, 350);
            this.lbIdeas.TabIndex = 18;
            this.lbIdeas.DoubleClick += new System.EventHandler(this.lbIdeas_DoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblStatus.Location = new System.Drawing.Point(12, 532);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "";
            // 
            // WordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 556);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tabResults);
            this.Controls.Add(this.meLineScan);
            this.Controls.Add(this.teLine);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.ceRare);
            this.Controls.Add(this.rgAccent);
            this.Controls.Add(this.sbBack);
            this.Controls.Add(this.sbLookUp);
            this.Controls.Add(this.teWord);
            this.Controls.Add(this.lblWord);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Name = "WordsForm";
            this.Text = "Words";
            ((System.ComponentModel.ISupportInitialize)(this.teWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgAccent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRare.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meLineScan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbPerfect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbNear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbVowel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meSynonyms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbIdeas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblWord;
        private DevExpress.XtraEditors.TextEdit teWord;
        private DevExpress.XtraEditors.SimpleButton sbLookUp;
        private DevExpress.XtraEditors.SimpleButton sbBack;
        private DevExpress.XtraEditors.RadioGroup rgAccent;
        private DevExpress.XtraEditors.CheckEdit ceRare;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.TextEdit teLine;
        private DevExpress.XtraEditors.MemoEdit meLineScan;
        private DevExpress.XtraTab.XtraTabControl tabResults;
        private DevExpress.XtraTab.XtraTabPage tpRhymes;
        private DevExpress.XtraTab.XtraTabPage tpSynonyms;
        private DevExpress.XtraTab.XtraTabPage tpIdeas;
        private DevExpress.XtraEditors.LabelControl lblPerfect;
        private DevExpress.XtraEditors.LabelControl lblNear;
        private DevExpress.XtraEditors.LabelControl lblVowel;
        private DevExpress.XtraEditors.ListBoxControl lbPerfect;
        private DevExpress.XtraEditors.ListBoxControl lbNear;
        private DevExpress.XtraEditors.ListBoxControl lbVowel;
        private DevExpress.XtraEditors.MemoEdit meSynonyms;
        private DevExpress.XtraEditors.ListBoxControl lbIdeas;
        private DevExpress.XtraEditors.LabelControl lblStatus;
    }
}
