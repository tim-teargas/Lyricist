using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DumbAngel;

#pragma warning disable SYSLIB0014   // FtpWebRequest is obsolete on .NET 6+, but still works.

namespace Lyricist.Core
{
    public sealed class ImportResult
    {
        public int Found { get; set; }
        public int Imported { get; set; }
        public int Duplicates { get; set; }
        public int Failed { get; set; }
        public List<string> Messages { get; } = new List<string>();

        public string Summary()
        {
            string text = $"{Found} file(s) found: {Imported} imported, {Duplicates} already in the database, {Failed} failed.";
            if (Messages.Count > 0)
                text += Environment.NewLine + Environment.NewLine + string.Join (Environment.NewLine, Messages);
            return text;
        }
    }

    /// <summary>
    /// The prototype's Import: lyric ideas saved as XML files in an FTP folder.
    /// New: a file whose Ref is already in the database is skipped (the old TODO), missing
    /// elements don't stop a file, and imported files can be moved to an archive folder.
    ///
    /// File format (one idea per file):
    /// &lt;Lyric&gt;&lt;Ref/&gt;&lt;Text/&gt;&lt;SourceInfo/&gt;&lt;AdditionalInfo/&gt;&lt;IsOriginal/&gt;... &lt;ConceivedAt/&gt;&lt;/Lyric&gt;
    /// </summary>
    public static class LyricImporter
    {
        public static ImportResult ImportFromFtp (AppSettings settings, Session session)
        {
            if (string.IsNullOrWhiteSpace (settings.FtpHost))
                throw new InvalidOperationException ("No FTP host has been set (Settings).");

            var result = new ImportResult();
            Directory.CreateDirectory (settings.DownloadFolder);

            foreach (string file in ListFiles (settings))
            {
                if (!file.EndsWith (".xml", StringComparison.OrdinalIgnoreCase))
                    continue;
                result.Found++;
                try
                {
                    string localPath = Path.Combine (settings.DownloadFolder, Path.GetFileName (file));
                    Download (settings, file, localPath);

                    if (ImportFile (localPath, session))
                        result.Imported++;
                    else
                        result.Duplicates++;

                    if (!string.IsNullOrWhiteSpace (settings.FtpArchiveDir))
                        Archive (settings, file, result);
                }
                catch (Exception ex)
                {
                    result.Failed++;
                    result.Messages.Add ($"{file}: {ex.Message}");
                }
            }
            return result;
        }

        /// <summary>Imports one XML file. False = its Ref is already in the database.</summary>
        public static bool ImportFile (string path, Session session)
        {
            var doc = new XmlDocument();
            doc.Load (path);

            string uniqueRef = ReadText (doc, "Ref");
            if (!string.IsNullOrWhiteSpace (uniqueRef)
                && session.FindObject<Lyric> (CriteriaOperator.Parse ("UniqueRef = ?", uniqueRef)) != null)
                return false;

            var lyric = new Lyric (session)
            {
                UniqueRef = uniqueRef,
                Text = ReadText (doc, "Text"),
                SourceInfo = ReadText (doc, "SourceInfo"),
                AdditionalInfo = ReadText (doc, "AdditionalInfo"),
                IsOriginal = ReadBool (doc, "IsOriginal"),
                IsWellKnownPhrase = ReadBool (doc, "IsWellKnownPhrase"),
                IsUsedInSong = ReadBool (doc, "IsUsedInSong"),
                IsConcept = ReadBool (doc, "IsConcept") ?? false,
                IsNotLyric = ReadBool (doc, "IsNotLyric") ?? false,
                IsTime = ReadBool (doc, "IsTime") ?? false,
                IsAlienation = ReadBool (doc, "IsAlienation") ?? false,
                IsViolence = ReadBool (doc, "IsViolence") ?? false,
                IsReligion = ReadBool (doc, "IsReligion") ?? false,
                IsSlacker = ReadBool (doc, "IsSlacker") ?? false,
                IsLove = ReadBool (doc, "IsLove") ?? false,
                IsSociety = ReadBool (doc, "IsSociety") ?? false,
                IsFriends = ReadBool (doc, "IsFriends") ?? false,
                IsDrinking = ReadBool (doc, "IsDrinking") ?? false,
                IsDream = ReadBool (doc, "IsDream") ?? false,
                IsSeductive = ReadBool (doc, "IsSeductive") ?? false,
                IsPeople = ReadBool (doc, "IsPeople") ?? false,
                IsUpbeat = ReadBool (doc, "IsUpbeat") ?? false,
                IsOther = ReadBool (doc, "IsOther") ?? false,
                IsGeneral = ReadBool (doc, "IsGeneral") ?? false,
                ConceivedAt = ReadDate (doc, "ConceivedAt"),
            };

            string rating = ReadText (doc, "Rating");
            if (int.TryParse (rating, out int ratingOid) && ratingOid >= 1 && ratingOid <= 5)
                lyric.Rating = Rating.Get (session, ratingOid);

            lyric.Save();
            return true;
        }

        // ---------------------------------------------------------------------------------

        static string ReadText (XmlDocument doc, string name) =>
            doc.SelectSingleNode ("/Lyric/" + name)?.InnerText ?? "";

        static bool? ReadBool (XmlDocument doc, string name) =>
            bool.TryParse (ReadText (doc, name).Trim(), out bool value) ? value : (bool?) null;

        static DateTime? ReadDate (XmlDocument doc, string name)
        {
            string text = ReadText (doc, name).Trim();
            if (DateTime.TryParse (text, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
                return value;
            if (DateTime.TryParse (text, out value))
                return value;
            return null;
        }

        static FtpWebRequest Request (AppSettings settings, string path, string method)
        {
            var request = (FtpWebRequest) WebRequest.Create (new Uri ("ftp://" + settings.FtpHost + "/" + path));
            request.Credentials = new NetworkCredential (settings.FtpUser, settings.FtpPassword);
            request.Method = method;
            request.UseBinary = true;
            request.Proxy = null;
            request.KeepAlive = false;
            request.UsePassive = false;   // as the prototype (its server needed active mode)
            return request;
        }

        static List<string> ListFiles (AppSettings settings)
        {
            var files = new List<string>();
            var request = Request (settings, settings.FtpRemoteDir.Trim ('/'), WebRequestMethods.Ftp.ListDirectory);
            using (var response = request.GetResponse())
            using (var reader = new StreamReader (response.GetResponseStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length > 0)
                        files.Add (Path.GetFileName (line));   // some servers list "dir/name"
                }
            }
            return files;
        }

        static void Download (AppSettings settings, string file, string localPath)
        {
            var request = Request (settings, settings.FtpRemoteDir.Trim ('/') + "/" + file, WebRequestMethods.Ftp.DownloadFile);
            using (var response = request.GetResponse())
            using (var source = response.GetResponseStream())
            using (var target = File.Create (localPath))
                source.CopyTo (target);
        }

        static void Archive (AppSettings settings, string file, ImportResult result)
        {
            try
            {
                var request = Request (settings, settings.FtpRemoteDir.Trim ('/') + "/" + file, WebRequestMethods.Ftp.Rename);
                request.RenameTo = "/" + settings.FtpArchiveDir.Trim ('/') + "/" + file;
                using (request.GetResponse()) { }
            }
            catch (Exception ex)
            {
                result.Messages.Add ($"{file}: imported, but not moved to the archive folder ({ex.Message}).");
            }
        }
    }
}
