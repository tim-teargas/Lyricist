using System;
using DevExpress.Xpo;

namespace Lyricist.Data
{
    /// <summary>
    /// A song being written (table Song, created by sql/001-songs.sql). Free text in Body: lines,
    /// with optional header lines in brackets ([Verse 1], [Chorus]) that start a new section.
    /// </summary>
    public class Song : DumbAngel.NexusObject
    {
        public Song (Session session) : base (session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Title = "";
            Status = "Idea";
            Body = "";
            Notes = "";
        }

        string title;
        [Size (200)]
        public string Title
        {
            get => title;
            set => SetPropertyValue (nameof (Title), ref title, value);
        }

        string status;
        [Size (50)]
        public string Status
        {
            get => status;
            set => SetPropertyValue (nameof (Status), ref status, value);
        }

        string body;
        [Size (SizeAttribute.Unlimited)]
        public string Body
        {
            get => body;
            set => SetPropertyValue (nameof (Body), ref body, value);
        }

        string notes;
        [Size (SizeAttribute.Unlimited)]
        public string Notes
        {
            get => notes;
            set => SetPropertyValue (nameof (Notes), ref notes, value);
        }

        string progRef;
        [Size (64)]
        public string ProgRef
        {
            get => progRef;
            set => SetPropertyValue (nameof (ProgRef), ref progRef, value);
        }

        DumbAngel.Rating rating;
        public DumbAngel.Rating Rating
        {
            get => rating;
            set => SetPropertyValue (nameof (Rating), ref rating, value);
        }

        [Association ("Song-Ideas"), Aggregated]
        public XPCollection<SongLyric> Ideas => GetCollection<SongLyric> (nameof (Ideas));
    }

    /// <summary>An idea (Lyric) used by a song, in order (table SongLyric).</summary>
    public class SongLyric : XPObject
    {
        public SongLyric (Session session) : base (session) { }

        Song song;
        [Association ("Song-Ideas")]
        public Song Song
        {
            get => song;
            set => SetPropertyValue (nameof (Song), ref song, value);
        }

        DumbAngel.Lyric lyric;
        public DumbAngel.Lyric Lyric
        {
            get => lyric;
            set => SetPropertyValue (nameof (Lyric), ref lyric, value);
        }

        int position;
        public int Position
        {
            get => position;
            set => SetPropertyValue (nameof (Position), ref position, value);
        }
    }
}
