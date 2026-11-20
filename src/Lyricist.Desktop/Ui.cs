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
            string text = ex == null ? message : message + Environment.NewLine + Environment.NewLine + ex.Message;
            XtraMessageBox.Show (text, "Lyricist", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
