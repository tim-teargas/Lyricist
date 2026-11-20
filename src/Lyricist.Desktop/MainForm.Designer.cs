namespace Lyricist.Desktop
{
    partial class MainForm
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
            this.sbNew = new DevExpress.XtraEditors.SimpleButton();
            this.sbImport = new DevExpress.XtraEditors.SimpleButton();
            this.sbExport = new DevExpress.XtraEditors.SimpleButton();
            this.sbSettings = new DevExpress.XtraEditors.SimpleButton();
            this.sbWords = new DevExpress.XtraEditors.SimpleButton();
            this.sbWorkbench = new DevExpress.XtraEditors.SimpleButton();
            this.sbPrompt = new DevExpress.XtraEditors.SimpleButton();
            this.sbRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.pnlFilters = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sbNew
            // 
            this.sbNew.Location = new System.Drawing.Point(12, 12);
            this.sbNew.Name = "sbNew";
            this.sbNew.Size = new System.Drawing.Size(120, 23);
            this.sbNew.TabIndex = 0;
            this.sbNew.Text = "New";
            this.sbNew.Click += new System.EventHandler(this.sbNew_Click);
            // 
            // sbImport
            // 
            this.sbImport.Location = new System.Drawing.Point(138, 12);
            this.sbImport.Name = "sbImport";
            this.sbImport.Size = new System.Drawing.Size(90, 23);
            this.sbImport.TabIndex = 1;
            this.sbImport.Text = "Import";
            this.sbImport.Click += new System.EventHandler(this.sbImport_Click);
            // 
            // sbExport
            // 
            this.sbExport.Location = new System.Drawing.Point(234, 12);
            this.sbExport.Name = "sbExport";
            this.sbExport.Size = new System.Drawing.Size(90, 23);
            this.sbExport.TabIndex = 2;
            this.sbExport.Text = "Export";
            this.sbExport.Click += new System.EventHandler(this.sbExport_Click);
            // 
            // sbSettings
            // 
            this.sbSettings.Location = new System.Drawing.Point(330, 12);
            this.sbSettings.Name = "sbSettings";
            this.sbSettings.Size = new System.Drawing.Size(90, 23);
            this.sbSettings.TabIndex = 3;
            this.sbSettings.Text = "Settings...";
            this.sbSettings.Click += new System.EventHandler(this.sbSettings_Click);
            // 
            // sbWords
            // 
            this.sbWords.Location = new System.Drawing.Point(426, 12);
            this.sbWords.Name = "sbWords";
            this.sbWords.Size = new System.Drawing.Size(90, 23);
            this.sbWords.TabIndex = 4;
            this.sbWords.Text = "Words...";
            this.sbWords.Click += new System.EventHandler(this.sbWords_Click);
            // 
            // sbWorkbench
            // 
            this.sbWorkbench.Location = new System.Drawing.Point(522, 12);
            this.sbWorkbench.Name = "sbWorkbench";
            this.sbWorkbench.Size = new System.Drawing.Size(100, 23);
            this.sbWorkbench.TabIndex = 5;
            this.sbWorkbench.Text = "Workbench...";
            this.sbWorkbench.Click += new System.EventHandler(this.sbWorkbench_Click);
            // 
            // sbPrompt
            // 
            this.sbPrompt.Location = new System.Drawing.Point(628, 12);
            this.sbPrompt.Name = "sbPrompt";
            this.sbPrompt.Size = new System.Drawing.Size(90, 23);
            this.sbPrompt.TabIndex = 6;
            this.sbPrompt.Text = "Prompt...";
            this.sbPrompt.Click += new System.EventHandler(this.sbPrompt_Click);
            // 
            // sbRefresh
            // 
            this.sbRefresh.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.sbRefresh.Location = new System.Drawing.Point(1068, 12);
            this.sbRefresh.Name = "sbRefresh";
            this.sbRefresh.Size = new System.Drawing.Size(100, 23);
            this.sbRefresh.TabIndex = 7;
            this.sbRefresh.Text = "Refresh";
            this.sbRefresh.Click += new System.EventHandler(this.sbRefresh_Click);
            // 
            // pnlFilters
            // 
            this.pnlFilters.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.pnlFilters.Location = new System.Drawing.Point(12, 42);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFilters.Size = new System.Drawing.Size(1156, 58);
            this.pnlFilters.TabIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.gridControl1.Location = new System.Drawing.Point(12, 104);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Size = new System.Drawing.Size(1156, 544);
            this.gridControl1.TabIndex = 9;
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            this.gridView1.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler(this.gridView1_CustomRowFilter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 660);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.sbRefresh);
            this.Controls.Add(this.sbPrompt);
            this.Controls.Add(this.sbWorkbench);
            this.Controls.Add(this.sbWords);
            this.Controls.Add(this.sbSettings);
            this.Controls.Add(this.sbExport);
            this.Controls.Add(this.sbImport);
            this.Controls.Add(this.sbNew);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.Text = "Lyricist";
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbNew;
        private DevExpress.XtraEditors.SimpleButton sbImport;
        private DevExpress.XtraEditors.SimpleButton sbExport;
        private DevExpress.XtraEditors.SimpleButton sbSettings;
        private DevExpress.XtraEditors.SimpleButton sbWords;
        private DevExpress.XtraEditors.SimpleButton sbWorkbench;
        private DevExpress.XtraEditors.SimpleButton sbPrompt;
        private DevExpress.XtraEditors.SimpleButton sbRefresh;
        private DevExpress.XtraEditors.PanelControl pnlFilters;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
