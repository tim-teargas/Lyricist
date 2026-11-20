namespace Lyricist.Desktop
{
    partial class SettingsForm
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
            this.lblConnection = new DevExpress.XtraEditors.LabelControl();
            this.meConnection = new DevExpress.XtraEditors.MemoEdit();
            this.sbTest = new DevExpress.XtraEditors.SimpleButton();
            this.lblTestResult = new DevExpress.XtraEditors.LabelControl();
            this.lblFtpHost = new DevExpress.XtraEditors.LabelControl();
            this.teFtpHost = new DevExpress.XtraEditors.TextEdit();
            this.lblFtpUser = new DevExpress.XtraEditors.LabelControl();
            this.teFtpUser = new DevExpress.XtraEditors.TextEdit();
            this.lblFtpPassword = new DevExpress.XtraEditors.LabelControl();
            this.teFtpPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblFtpRemoteDir = new DevExpress.XtraEditors.LabelControl();
            this.teFtpRemoteDir = new DevExpress.XtraEditors.TextEdit();
            this.lblFtpArchiveDir = new DevExpress.XtraEditors.LabelControl();
            this.teFtpArchiveDir = new DevExpress.XtraEditors.TextEdit();
            this.lblDownloadDir = new DevExpress.XtraEditors.LabelControl();
            this.teDownloadDir = new DevExpress.XtraEditors.TextEdit();
            this.lblNewIdeaSource = new DevExpress.XtraEditors.LabelControl();
            this.teNewIdeaSource = new DevExpress.XtraEditors.TextEdit();
            this.lblAboutMe = new DevExpress.XtraEditors.LabelControl();
            this.meAboutMe = new DevExpress.XtraEditors.MemoEdit();
            this.sbOK = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.meConnection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpHost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpRemoteDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpArchiveDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDownloadDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewIdeaSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meAboutMe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConnection
            // 
            this.lblConnection.Location = new System.Drawing.Point(12, 15);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.TabIndex = 0;
            this.lblConnection.Text = "Database connection string (XPO):";
            // 
            // meConnection
            // 
            this.meConnection.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.meConnection.Location = new System.Drawing.Point(12, 34);
            this.meConnection.Name = "meConnection";
            this.meConnection.Size = new System.Drawing.Size(556, 60);
            this.meConnection.TabIndex = 1;
            // 
            // sbTest
            // 
            this.sbTest.Location = new System.Drawing.Point(12, 100);
            this.sbTest.Name = "sbTest";
            this.sbTest.Size = new System.Drawing.Size(120, 23);
            this.sbTest.TabIndex = 2;
            this.sbTest.Text = "Test connection";
            this.sbTest.Click += new System.EventHandler(this.sbTest_Click);
            // 
            // lblTestResult
            // 
            this.lblTestResult.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.lblTestResult.Location = new System.Drawing.Point(140, 105);
            this.lblTestResult.Name = "lblTestResult";
            this.lblTestResult.TabIndex = 3;
            this.lblTestResult.Text = "";
            // 
            // lblFtpHost
            // 
            this.lblFtpHost.Location = new System.Drawing.Point(12, 145);
            this.lblFtpHost.Name = "lblFtpHost";
            this.lblFtpHost.TabIndex = 4;
            this.lblFtpHost.Text = "FTP host:";
            // 
            // teFtpHost
            // 
            this.teFtpHost.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teFtpHost.Location = new System.Drawing.Point(150, 142);
            this.teFtpHost.Name = "teFtpHost";
            this.teFtpHost.Size = new System.Drawing.Size(418, 20);
            this.teFtpHost.TabIndex = 5;
            // 
            // lblFtpUser
            // 
            this.lblFtpUser.Location = new System.Drawing.Point(12, 171);
            this.lblFtpUser.Name = "lblFtpUser";
            this.lblFtpUser.TabIndex = 6;
            this.lblFtpUser.Text = "FTP user:";
            // 
            // teFtpUser
            // 
            this.teFtpUser.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teFtpUser.Location = new System.Drawing.Point(150, 168);
            this.teFtpUser.Name = "teFtpUser";
            this.teFtpUser.Size = new System.Drawing.Size(418, 20);
            this.teFtpUser.TabIndex = 7;
            // 
            // lblFtpPassword
            // 
            this.lblFtpPassword.Location = new System.Drawing.Point(12, 197);
            this.lblFtpPassword.Name = "lblFtpPassword";
            this.lblFtpPassword.TabIndex = 8;
            this.lblFtpPassword.Text = "FTP password:";
            // 
            // teFtpPassword
            // 
            this.teFtpPassword.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teFtpPassword.Location = new System.Drawing.Point(150, 194);
            this.teFtpPassword.Name = "teFtpPassword";
            this.teFtpPassword.Properties.UseSystemPasswordChar = true;
            this.teFtpPassword.Size = new System.Drawing.Size(418, 20);
            this.teFtpPassword.TabIndex = 9;
            // 
            // lblFtpRemoteDir
            // 
            this.lblFtpRemoteDir.Location = new System.Drawing.Point(12, 223);
            this.lblFtpRemoteDir.Name = "lblFtpRemoteDir";
            this.lblFtpRemoteDir.TabIndex = 10;
            this.lblFtpRemoteDir.Text = "FTP folder:";
            // 
            // teFtpRemoteDir
            // 
            this.teFtpRemoteDir.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teFtpRemoteDir.Location = new System.Drawing.Point(150, 220);
            this.teFtpRemoteDir.Name = "teFtpRemoteDir";
            this.teFtpRemoteDir.Size = new System.Drawing.Size(418, 20);
            this.teFtpRemoteDir.TabIndex = 11;
            // 
            // lblFtpArchiveDir
            // 
            this.lblFtpArchiveDir.Location = new System.Drawing.Point(12, 249);
            this.lblFtpArchiveDir.Name = "lblFtpArchiveDir";
            this.lblFtpArchiveDir.TabIndex = 12;
            this.lblFtpArchiveDir.Text = "Archive folder (optional):";
            // 
            // teFtpArchiveDir
            // 
            this.teFtpArchiveDir.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teFtpArchiveDir.Location = new System.Drawing.Point(150, 246);
            this.teFtpArchiveDir.Name = "teFtpArchiveDir";
            this.teFtpArchiveDir.Size = new System.Drawing.Size(418, 20);
            this.teFtpArchiveDir.TabIndex = 13;
            // 
            // lblDownloadDir
            // 
            this.lblDownloadDir.Location = new System.Drawing.Point(12, 275);
            this.lblDownloadDir.Name = "lblDownloadDir";
            this.lblDownloadDir.TabIndex = 14;
            this.lblDownloadDir.Text = "Download folder (optional):";
            // 
            // teDownloadDir
            // 
            this.teDownloadDir.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teDownloadDir.Location = new System.Drawing.Point(150, 272);
            this.teDownloadDir.Name = "teDownloadDir";
            this.teDownloadDir.Size = new System.Drawing.Size(418, 20);
            this.teDownloadDir.TabIndex = 15;
            // 
            // lblNewIdeaSource
            // 
            this.lblNewIdeaSource.Location = new System.Drawing.Point(12, 315);
            this.lblNewIdeaSource.Name = "lblNewIdeaSource";
            this.lblNewIdeaSource.TabIndex = 16;
            this.lblNewIdeaSource.Text = "New idea's Source Info:";
            // 
            // teNewIdeaSource
            // 
            this.teNewIdeaSource.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.teNewIdeaSource.Location = new System.Drawing.Point(150, 312);
            this.teNewIdeaSource.Name = "teNewIdeaSource";
            this.teNewIdeaSource.Size = new System.Drawing.Size(418, 20);
            this.teNewIdeaSource.TabIndex = 17;
            // 
            // lblAboutMe
            // 
            this.lblAboutMe.Location = new System.Drawing.Point(12, 345);
            this.lblAboutMe.Name = "lblAboutMe";
            this.lblAboutMe.TabIndex = 18;
            this.lblAboutMe.Text = "About me and how I write (goes at the top of every prompt; optional):";
            // 
            // meAboutMe
            // 
            this.meAboutMe.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.meAboutMe.Location = new System.Drawing.Point(12, 364);
            this.meAboutMe.Name = "meAboutMe";
            this.meAboutMe.Size = new System.Drawing.Size(556, 90);
            this.meAboutMe.TabIndex = 19;
            // 
            // sbOK
            // 
            this.sbOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbOK.Location = new System.Drawing.Point(382, 470);
            this.sbOK.Name = "sbOK";
            this.sbOK.Size = new System.Drawing.Size(90, 23);
            this.sbOK.TabIndex = 20;
            this.sbOK.Text = "OK";
            this.sbOK.Click += new System.EventHandler(this.sbOK_Click);
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.sbCancel.Location = new System.Drawing.Point(478, 470);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(90, 23);
            this.sbCancel.TabIndex = 21;
            this.sbCancel.Text = "Cancel";
            this.sbCancel.Click += new System.EventHandler(this.sbCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 505);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.sbOK);
            this.Controls.Add(this.meAboutMe);
            this.Controls.Add(this.lblAboutMe);
            this.Controls.Add(this.teNewIdeaSource);
            this.Controls.Add(this.lblNewIdeaSource);
            this.Controls.Add(this.teDownloadDir);
            this.Controls.Add(this.lblDownloadDir);
            this.Controls.Add(this.teFtpArchiveDir);
            this.Controls.Add(this.lblFtpArchiveDir);
            this.Controls.Add(this.teFtpRemoteDir);
            this.Controls.Add(this.lblFtpRemoteDir);
            this.Controls.Add(this.teFtpPassword);
            this.Controls.Add(this.lblFtpPassword);
            this.Controls.Add(this.teFtpUser);
            this.Controls.Add(this.lblFtpUser);
            this.Controls.Add(this.teFtpHost);
            this.Controls.Add(this.lblFtpHost);
            this.Controls.Add(this.lblTestResult);
            this.Controls.Add(this.sbTest);
            this.Controls.Add(this.meConnection);
            this.Controls.Add(this.lblConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "SettingsForm";
            this.Text = "Lyricist settings";
            ((System.ComponentModel.ISupportInitialize)(this.meConnection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpHost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpRemoteDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFtpArchiveDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDownloadDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teNewIdeaSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meAboutMe.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblConnection;
        private DevExpress.XtraEditors.MemoEdit meConnection;
        private DevExpress.XtraEditors.SimpleButton sbTest;
        private DevExpress.XtraEditors.LabelControl lblTestResult;
        private DevExpress.XtraEditors.LabelControl lblFtpHost;
        private DevExpress.XtraEditors.TextEdit teFtpHost;
        private DevExpress.XtraEditors.LabelControl lblFtpUser;
        private DevExpress.XtraEditors.TextEdit teFtpUser;
        private DevExpress.XtraEditors.LabelControl lblFtpPassword;
        private DevExpress.XtraEditors.TextEdit teFtpPassword;
        private DevExpress.XtraEditors.LabelControl lblFtpRemoteDir;
        private DevExpress.XtraEditors.TextEdit teFtpRemoteDir;
        private DevExpress.XtraEditors.LabelControl lblFtpArchiveDir;
        private DevExpress.XtraEditors.TextEdit teFtpArchiveDir;
        private DevExpress.XtraEditors.LabelControl lblDownloadDir;
        private DevExpress.XtraEditors.TextEdit teDownloadDir;
        private DevExpress.XtraEditors.LabelControl lblNewIdeaSource;
        private DevExpress.XtraEditors.TextEdit teNewIdeaSource;
        private DevExpress.XtraEditors.LabelControl lblAboutMe;
        private DevExpress.XtraEditors.MemoEdit meAboutMe;
        private DevExpress.XtraEditors.SimpleButton sbOK;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
    }
}
