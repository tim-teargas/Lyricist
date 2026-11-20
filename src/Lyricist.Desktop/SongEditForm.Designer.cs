namespace Lyricist.Desktop
{
    partial class SongEditForm
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.teTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.cbStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblSchemes = new DevExpress.XtraEditors.LabelControl();
            this.meBody = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.meNotes = new DevExpress.XtraEditors.MemoEdit();
            this.gcLines = new DevExpress.XtraGrid.GridControl();
            this.gvLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblIdeas = new DevExpress.XtraEditors.LabelControl();
            this.lbIdeas = new DevExpress.XtraEditors.ListBoxControl();
            this.sbInsertIdea = new DevExpress.XtraEditors.SimpleButton();
            this.sbRemoveIdea = new DevExpress.XtraEditors.SimpleButton();
            this.sbWords = new DevExpress.XtraEditors.SimpleButton();
            this.sbPrompt = new DevExpress.XtraEditors.SimpleButton();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.teTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meBody.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbIdeas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            // 
            // teTitle
            // 
            this.teTitle.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teTitle.Location = new System.Drawing.Point(60, 12);
            this.teTitle.Name = "teTitle";
            this.teTitle.Size = new System.Drawing.Size(420, 20);
            this.teTitle.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblStatus.Location = new System.Drawing.Point(496, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // cbStatus
            // 
            this.cbStatus.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.cbStatus.Location = new System.Drawing.Point(544, 12);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Properties.Items.AddRange(new object[] {
            "Idea",
            "Drafting",
            "Finished",
            "Parked"});
            this.cbStatus.Size = new System.Drawing.Size(130, 20);
            this.cbStatus.TabIndex = 3;
            // 
            // lblSchemes
            // 
            this.lblSchemes.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.lblSchemes.Location = new System.Drawing.Point(12, 42);
            this.lblSchemes.Name = "lblSchemes";
            this.lblSchemes.TabIndex = 4;
            this.lblSchemes.Text = "";
            // 
            // meBody
            // 
            this.meBody.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.meBody.Location = new System.Drawing.Point(12, 64);
            this.meBody.Name = "meBody";
            this.meBody.Properties.WordWrap = false;
            this.meBody.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.meBody.Size = new System.Drawing.Size(662, 560);
            this.meBody.TabIndex = 5;
            // 
            // lblNotes
            // 
            this.lblNotes.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblNotes.Location = new System.Drawing.Point(12, 632);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.TabIndex = 6;
            this.lblNotes.Text = "My notes on the song:";
            // 
            // meNotes
            // 
            this.meNotes.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.meNotes.Location = new System.Drawing.Point(12, 650);
            this.meNotes.Name = "meNotes";
            this.meNotes.Size = new System.Drawing.Size(662, 60);
            this.meNotes.TabIndex = 7;
            // 
            // gcLines
            // 
            this.gcLines.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right);
            this.gcLines.Location = new System.Drawing.Point(684, 12);
            this.gcLines.Name = "gcLines";
            this.gcLines.MainView = this.gvLines;
            this.gcLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLines});
            this.gcLines.Size = new System.Drawing.Size(404, 410);
            this.gcLines.TabIndex = 8;
            // 
            // gvLines
            // 
            this.gvLines.Name = "gvLines";
            this.gvLines.GridControl = this.gcLines;
            this.gvLines.OptionsBehavior.Editable = false;
            this.gvLines.OptionsFind.AlwaysVisible = true;
            this.gvLines.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLines.OptionsView.RowAutoHeight = true;
            this.gvLines.OptionsView.ShowGroupPanel = false;
            this.gvLines.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvLines_RowClick);
            // 
            // lblIdeas
            // 
            this.lblIdeas.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.lblIdeas.Location = new System.Drawing.Point(684, 430);
            this.lblIdeas.Name = "lblIdeas";
            this.lblIdeas.TabIndex = 9;
            this.lblIdeas.Text = "Ideas gathered (right-click an idea in the list: Add to song):";
            // 
            // lbIdeas
            // 
            this.lbIdeas.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.lbIdeas.Location = new System.Drawing.Point(684, 450);
            this.lbIdeas.Name = "lbIdeas";
            this.lbIdeas.Size = new System.Drawing.Size(404, 190);
            this.lbIdeas.TabIndex = 10;
            this.lbIdeas.DoubleClick += new System.EventHandler(this.lbIdeas_DoubleClick);
            // 
            // sbInsertIdea
            // 
            this.sbInsertIdea.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbInsertIdea.Location = new System.Drawing.Point(684, 646);
            this.sbInsertIdea.Name = "sbInsertIdea";
            this.sbInsertIdea.Size = new System.Drawing.Size(130, 23);
            this.sbInsertIdea.TabIndex = 11;
            this.sbInsertIdea.Text = "Insert at cursor";
            this.sbInsertIdea.Click += new System.EventHandler(this.sbInsertIdea_Click);
            // 
            // sbRemoveIdea
            // 
            this.sbRemoveIdea.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbRemoveIdea.Location = new System.Drawing.Point(820, 646);
            this.sbRemoveIdea.Name = "sbRemoveIdea";
            this.sbRemoveIdea.Size = new System.Drawing.Size(90, 23);
            this.sbRemoveIdea.TabIndex = 12;
            this.sbRemoveIdea.Text = "Remove";
            this.sbRemoveIdea.Click += new System.EventHandler(this.sbRemoveIdea_Click);
            // 
            // sbWords
            // 
            this.sbWords.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbWords.Location = new System.Drawing.Point(684, 686);
            this.sbWords.Name = "sbWords";
            this.sbWords.Size = new System.Drawing.Size(90, 23);
            this.sbWords.TabIndex = 13;
            this.sbWords.Text = "Words...";
            this.sbWords.Click += new System.EventHandler(this.sbWords_Click);
            // 
            // sbPrompt
            // 
            this.sbPrompt.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbPrompt.Location = new System.Drawing.Point(780, 686);
            this.sbPrompt.Name = "sbPrompt";
            this.sbPrompt.Size = new System.Drawing.Size(90, 23);
            this.sbPrompt.TabIndex = 14;
            this.sbPrompt.Text = "Prompt...";
            this.sbPrompt.Click += new System.EventHandler(this.sbPrompt_Click);
            // 
            // sbSave
            // 
            this.sbSave.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbSave.Location = new System.Drawing.Point(902, 686);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(90, 23);
            this.sbSave.TabIndex = 15;
            this.sbSave.Text = "Save";
            this.sbSave.Click += new System.EventHandler(this.sbSave_Click);
            // 
            // sbClose
            // 
            this.sbClose.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbClose.Location = new System.Drawing.Point(998, 686);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(90, 23);
            this.sbClose.TabIndex = 16;
            this.sbClose.Text = "Close";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // SongEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.Controls.Add(this.sbClose);
            this.Controls.Add(this.sbSave);
            this.Controls.Add(this.sbPrompt);
            this.Controls.Add(this.sbWords);
            this.Controls.Add(this.sbRemoveIdea);
            this.Controls.Add(this.sbInsertIdea);
            this.Controls.Add(this.lbIdeas);
            this.Controls.Add(this.lblIdeas);
            this.Controls.Add(this.gcLines);
            this.Controls.Add(this.meNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.meBody);
            this.Controls.Add(this.lblSchemes);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.teTitle);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Name = "SongEditForm";
            this.Text = "Song";
            ((System.ComponentModel.ISupportInitialize)(this.teTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meBody.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbIdeas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.TextEdit teTitle;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.ComboBoxEdit cbStatus;
        private DevExpress.XtraEditors.LabelControl lblSchemes;
        private DevExpress.XtraEditors.MemoEdit meBody;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.MemoEdit meNotes;
        private DevExpress.XtraGrid.GridControl gcLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLines;
        private DevExpress.XtraEditors.LabelControl lblIdeas;
        private DevExpress.XtraEditors.ListBoxControl lbIdeas;
        private DevExpress.XtraEditors.SimpleButton sbInsertIdea;
        private DevExpress.XtraEditors.SimpleButton sbRemoveIdea;
        private DevExpress.XtraEditors.SimpleButton sbWords;
        private DevExpress.XtraEditors.SimpleButton sbPrompt;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraEditors.SimpleButton sbClose;
    }
}
