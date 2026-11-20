using DevExpress.Xpo;

namespace DumbAngel   // kept for XPO's type records: see NexusObject.cs
{
    /// <summary>
    /// The prototype's shared 1-5 rating table (Oid 1 = Crap ... 5 = Excellent). Also used by
    /// prog relations (ShareTitle, SmoothnessTitle), so Lyricist only reads it.
    /// </summary>
    public class Rating : XPObject
    {
        public enum RatingEnum
        {
            Crap = 1,
            Ok = 2,
            Good = 3,
            VeryGood = 4,
            Excellent = 5
        }

        public Rating (Session session) : base (session) { }

        string title;
        public string Title
        {
            get => title;
            set => SetPropertyValue (nameof (Title), ref title, value);
        }

        string shareTitle;
        public string ShareTitle
        {
            get => shareTitle;
            set => SetPropertyValue (nameof (ShareTitle), ref shareTitle, value);
        }

        string smoothnessTitle;
        public string SmoothnessTitle
        {
            get => smoothnessTitle;
            set => SetPropertyValue (nameof (SmoothnessTitle), ref smoothnessTitle, value);
        }

        public static Rating Get (Session session, int oid) => session.GetObjectByKey<Rating> (oid);
    }
}
