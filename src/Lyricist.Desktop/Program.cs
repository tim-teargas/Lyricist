using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault (false);

            // Anything not caught where it happens: show it, log it, carry on.
            Application.SetUnhandledExceptionMode (UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Ui.Unhandled (e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Ui.Unhandled (e.ExceptionObject as Exception);
            TaskScheduler.UnobservedTaskException += (s, e) => { Ui.Log (e.Exception); e.SetObserved(); };

            // Bigger text everywhere (DevExpress default is 8.25). Forms scale to suit (AutoScaleMode.Font).
            WindowsFormsSettings.DefaultFont = new Font (AppearanceObject.DefaultFont.FontFamily, 9.75f);

            AppSettings settings = AppSettings.Load();

            // Connect, asking for settings until it works or the user gives up.
            while (true)
            {
                if (!string.IsNullOrWhiteSpace (settings.ConnectionString))
                {
                    try
                    {
                        Database.Connect (settings.ConnectionString);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Ui.Error ("Couldn't connect to the database.", ex);
                    }
                }
                using (var form = new SettingsForm (settings))
                    if (form.ShowDialog() != DialogResult.OK)
                        return;
            }

            Application.Run (new MainForm (settings));
        }
    }
}
