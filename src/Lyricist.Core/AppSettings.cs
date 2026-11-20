using System;
using System.IO;
using System.Xml.Serialization;

namespace Lyricist.Core
{
    /// <summary>
    /// Local settings, kept in %APPDATA%\Lyricist\settings.xml (never in the repo: it holds the
    /// connection string and the FTP login).
    /// </summary>
    public sealed class AppSettings
    {
        // Database
        public string ConnectionString { get; set; } = "";

        // FTP import (ideas captured elsewhere, as XML files)
        public string FtpHost { get; set; } = "";
        public string FtpUser { get; set; } = "";
        public string FtpPassword { get; set; } = "";
        public string FtpRemoteDir { get; set; } = "lyricfiles";
        /// <summary>Optional: imported files are moved here (path from the FTP root). Empty = leave them.</summary>
        public string FtpArchiveDir { get; set; } = "";
        /// <summary>Where downloaded files are kept on this PC. Empty = %APPDATA%\Lyricist\Downloads.</summary>
        public string DownloadDir { get; set; } = "";

        // Entry helpers (the prototype forgot these when it closed)
        public string NewIdeaSource { get; set; } = "";
        public string LastConceivedText { get; set; } = "";
        public string LastSourceInfo { get; set; } = "";

        // Words window
        public string Accent { get; set; } = "UK";   // "UK" / "US"
        public bool IncludeRareWords { get; set; }
        /// <summary>"left,top,width,height"; empty = beside the main window.</summary>
        public string WordsWindowBounds { get; set; } = "";

        // ---------------------------------------------------------------------------------

        public static string Folder =>
            Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "Lyricist");

        public static string FilePath => Path.Combine (Folder, "settings.xml");

        public string DownloadFolder =>
            string.IsNullOrWhiteSpace (DownloadDir) ? Path.Combine (Folder, "Downloads") : DownloadDir;

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists (FilePath))
                    using (var stream = File.OpenRead (FilePath))
                        return (AppSettings) new XmlSerializer (typeof (AppSettings)).Deserialize (stream);
            }
            catch
            {
                // Unreadable settings: start from defaults rather than refuse to open.
            }
            return new AppSettings();
        }

        public void Save()
        {
            Directory.CreateDirectory (Folder);
            string temp = FilePath + ".tmp";
            using (var stream = File.Create (temp))
                new XmlSerializer (typeof (AppSettings)).Serialize (stream, this);
            if (File.Exists (FilePath))
                File.Delete (FilePath);
            File.Move (temp, FilePath);
        }
    }
}
