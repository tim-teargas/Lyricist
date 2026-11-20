using System;
using System.Windows.Forms;
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
