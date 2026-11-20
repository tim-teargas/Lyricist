namespace Lyricist.Desktop
{
    partial class PromptForm
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
            this.lblTemplate = new DevExpress.XtraEditors.LabelControl();
            this.cbTemplate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTemplateInfo = new DevExpress.XtraEditors.LabelControl();
            this.ceSource = new DevExpress.XtraEditors.CheckEdit();
            this.ceThemes = new DevExpress.XtraEditors.CheckEdit();
            this.lblScheme = new DevExpress.XtraEditors.LabelControl();
            this.teScheme = new DevExpress.XtraEditors.TextEdit();
            this.lblSyllables = new DevExpress.XtraEditors.LabelControl();
            this.seSyllables = new DevExpress.XtraEditors.SpinEdit();
            this.mePrompt = new DevExpress.XtraEditors.MemoEdit();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.sbCopy = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceThemes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teScheme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSyllables.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mePrompt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTemplate
            // 
            this.lblTemplate.Location = new System.Drawing.Point(12, 15);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.TabIndex = 0;
            this.lblTemplate.Text = "Template:";
            // 
            // cbTemplate
            // 
            this.cbTemplate.Location = new System.Drawing.Point(80, 12);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbTemplate.Size = new System.Drawing.Size(260, 20);
            this.cbTemplate.TabIndex = 1;
            // 
            // lblTemplateInfo
            // 
            this.lblTemplateInfo.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.lblTemplateInfo.Location = new System.Drawing.Point(352, 15);
            this.lblTemplateInfo.Name = "lblTemplateInfo";
            this.lblTemplateInfo.TabIndex = 2;
            this.lblTemplateInfo.Text = "";
            // 
            // ceSource
            // 
            this.ceSource.Location = new System.Drawing.Point(80, 42);
            this.ceSource.Name = "ceSource";
            this.ceSource.Size = new System.Drawing.Size(160, 20);
            this.ceSource.TabIndex = 3;
            this.ceSource.Properties.Caption = "Include source info";
            // 
            // ceThemes
            // 
            this.ceThemes.Location = new System.Drawing.Point(246, 42);
            this.ceThemes.Name = "ceThemes";
            this.ceThemes.Size = new System.Drawing.Size(140, 20);
            this.ceThemes.TabIndex = 4;
            this.ceThemes.Properties.Caption = "Include themes";
            // 
            // lblScheme
            // 
            this.lblScheme.Location = new System.Drawing.Point(400, 45);
            this.lblScheme.Name = "lblScheme";
            this.lblScheme.TabIndex = 5;
            this.lblScheme.Text = "Rhyme scheme:";
            // 
            // teScheme
            // 
            this.teScheme.Location = new System.Drawing.Point(490, 42);
            this.teScheme.Name = "teScheme";
            this.teScheme.Size = new System.Drawing.Size(70, 20);
            this.teScheme.TabIndex = 6;
            // 
            // lblSyllables
            // 
            this.lblSyllables.Location = new System.Drawing.Point(574, 45);
            this.lblSyllables.Name = "lblSyllables";
            this.lblSyllables.TabIndex = 7;
            this.lblSyllables.Text = "Syllables (0 = as mine):";
            // 
            // seSyllables
            // 
            this.seSyllables.Location = new System.Drawing.Point(720, 42);
            this.seSyllables.Name = "seSyllables";
            this.seSyllables.Properties.MinValue = new decimal(new int[] {0, 0, 0, 0});
            this.seSyllables.Properties.MaxValue = new decimal(new int[] {24, 0, 0, 0});
            this.seSyllables.Properties.IsFloatValue = false;
            this.seSyllables.Size = new System.Drawing.Size(60, 20);
            this.seSyllables.TabIndex = 8;
            // 
            // mePrompt
            // 
            this.mePrompt.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.mePrompt.Location = new System.Drawing.Point(12, 72);
            this.mePrompt.Name = "mePrompt";
            this.mePrompt.Size = new System.Drawing.Size(796, 520);
            this.mePrompt.TabIndex = 9;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblCount.Location = new System.Drawing.Point(12, 606);
            this.lblCount.Name = "lblCount";
            this.lblCount.TabIndex = 10;
            this.lblCount.Text = "";
            // 
            // sbCopy
            // 
            this.sbCopy.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbCopy.Location = new System.Drawing.Point(622, 600);
            this.sbCopy.Name = "sbCopy";
            this.sbCopy.Size = new System.Drawing.Size(90, 23);
            this.sbCopy.TabIndex = 11;
            this.sbCopy.Text = "Copy";
            this.sbCopy.Click += new System.EventHandler(this.sbCopy_Click);
            // 
            // sbClose
            // 
            this.sbClose.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbClose.Location = new System.Drawing.Point(718, 600);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(90, 23);
            this.sbClose.TabIndex = 12;
            this.sbClose.Text = "Close";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // PromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 635);
            this.Controls.Add(this.sbClose);
            this.Controls.Add(this.sbCopy);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.mePrompt);
            this.Controls.Add(this.seSyllables);
            this.Controls.Add(this.lblSyllables);
            this.Controls.Add(this.teScheme);
            this.Controls.Add(this.lblScheme);
            this.Controls.Add(this.ceThemes);
            this.Controls.Add(this.ceSource);
            this.Controls.Add(this.lblTemplateInfo);
            this.Controls.Add(this.cbTemplate);
            this.Controls.Add(this.lblTemplate);
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "PromptForm";
            this.Text = "Prompt";
            ((System.ComponentModel.ISupportInitialize)(this.cbTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceThemes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teScheme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seSyllables.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mePrompt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTemplate;
        private DevExpress.XtraEditors.ComboBoxEdit cbTemplate;
        private DevExpress.XtraEditors.LabelControl lblTemplateInfo;
        private DevExpress.XtraEditors.CheckEdit ceSource;
        private DevExpress.XtraEditors.CheckEdit ceThemes;
        private DevExpress.XtraEditors.LabelControl lblScheme;
        private DevExpress.XtraEditors.TextEdit teScheme;
        private DevExpress.XtraEditors.LabelControl lblSyllables;
        private DevExpress.XtraEditors.SpinEdit seSyllables;
        private DevExpress.XtraEditors.MemoEdit mePrompt;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.SimpleButton sbCopy;
        private DevExpress.XtraEditors.SimpleButton sbClose;
    }
}
