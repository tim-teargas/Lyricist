using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    /// <summary>
    /// Connection string, FTP import and entry defaults. Saved to %APPDATA%\Lyricist\settings.xml.
    /// Connection string example (Windows login):
    /// XpoProvider=MSSqlServer;data source=.;integrated security=SSPI;initial catalog=DumbAngel
    /// </summary>
    public partial class SettingsForm : XtraForm
    {
        readonly AppSettings settings;

        public SettingsForm (AppSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            WindowMemory.Remember (this, "Settings");
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            meConnection.Text = settings.ConnectionString;
            teFtpHost.Text = settings.FtpHost;
            teFtpUser.Text = settings.FtpUser;
            teFtpPassword.Text = settings.FtpPassword;
            teFtpRemoteDir.Text = settings.FtpRemoteDir;
            teFtpArchiveDir.Text = settings.FtpArchiveDir;
            teDownloadDir.Text = settings.DownloadDir;
            teNewIdeaSource.Text = settings.NewIdeaSource;
            meAboutMe.Text = settings.PromptAboutMe;
            if (string.IsNullOrWhiteSpace (settings.ConnectionString))
                lblTestResult.Text = "Paste the prototype's XPO connection string, then Test.";
        }

        void sbTest_Click (object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblTestResult.Text = Database.Test (meConnection.Text.Trim());
            }
            catch (Exception ex)
            {
                lblTestResult.Text = "Failed: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void sbOK_Click (object sender, EventArgs e)
        {
            settings.ConnectionString = meConnection.Text.Trim();
            settings.FtpHost = teFtpHost.Text.Trim();
            settings.FtpUser = teFtpUser.Text.Trim();
            settings.FtpPassword = teFtpPassword.Text;
            settings.FtpRemoteDir = teFtpRemoteDir.Text.Trim();
            settings.FtpArchiveDir = teFtpArchiveDir.Text.Trim();
            settings.DownloadDir = teDownloadDir.Text.Trim();
            settings.NewIdeaSource = teNewIdeaSource.Text;
            settings.PromptAboutMe = meAboutMe.Text;
            try
            {
                settings.Save();
            }
            catch (Exception ex)
            {
                Ui.Error ("Couldn't save the settings.", ex);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        void sbCancel_Click (object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
