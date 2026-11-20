using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Lyricist.Desktop
{
    /// <summary>Small shared helpers (the prototype's Explorer3 equivalents).</summary>
    static class Ui
    {
        public static void Error (string message, Exception ex = null)
        {
            if (ex != null)
                Log (ex);
            string text = ex == null ? message : message + Environment.NewLine + Environment.NewLine + ex.Message;
            XtraMessageBox.Show (text, "Lyricist", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string LogPath => System.IO.Path.Combine (Core.AppSettings.Folder, "errors.log");

        /// <summary>Appends an exception (with its stack trace) to %APPDATA%\Lyricist\errors.log.</summary>
        public static void Log (Exception ex)
        {
            try
            {
                System.IO.Directory.CreateDirectory (Core.AppSettings.Folder);
                System.IO.File.AppendAllText (LogPath,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}");
            }
            catch
            {
                // Logging must never throw.
            }
        }

        /// <summary>The last resort for anything not caught where it happened.</summary>
        public static void Unhandled (Exception ex)
        {
            if (ex == null)
                return;
            Log (ex);
            try
            {
                XtraMessageBox.Show ("Something went wrong:" + Environment.NewLine + Environment.NewLine
                                     + ex.GetType().Name + ": " + ex.Message + Environment.NewLine + Environment.NewLine
                                     + "Details are in " + LogPath + ". Lyricist will try to carry on.",
                                     "Lyricist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                // No UI left to show it on.
            }
        }

        public static void Info (string message) =>
            XtraMessageBox.Show (message, "Lyricist", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static bool Confirm (string message) =>
            XtraMessageBox.Show (message, "Lyricist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        /// <summary>Opens a URL or file with its default program (works on .NET Framework and .NET).</summary>
        public static void Open (string target) =>
            Process.Start (new ProcessStartInfo (target) { UseShellExecute = true });

        public static CheckState ToCheckState (bool? value) =>
            value == null ? CheckState.Indeterminate : value.Value ? CheckState.Checked : CheckState.Unchecked;

        public static bool? FromCheckState (CheckState state) =>
            state == CheckState.Indeterminate ? (bool?) null : state == CheckState.Checked;
    }
}
