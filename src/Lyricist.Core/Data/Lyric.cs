using System;
using DevExpress.Xpo;

namespace DumbAngel   // kept for XPO's type records: see NexusObject.cs
{
    /// <summary>
    /// One lyric idea: the prototype's Lyric table, column for column.
    /// The Oid is the reference; UniqueRef is the Ref from imported XML files (used to skip
    /// duplicates on import).
    /// </summary>
    public class Lyric : NexusObject
    {
        public Lyric (Session session) : base (session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Text = "";
            SourceInfo = "";
            AdditionalInfo = "";
            LastReviewedAt = DateTime.Now;
        }

        // Not persisted (private field, no attribute): set only while MarkReviewed saves.
        bool reviewOnly;
        protected override bool KeepUpdatedAt => reviewOnly;

        /// <summary>Stamps LastReviewedAt and saves it, without counting as an edit (UpdatedAt stays).</summary>
        public void MarkReviewed()
        {
            reviewOnly = true;
            try
            {
                LastReviewedAt = DateTime.Now;
                Save();
            }
            finally
            {
                reviewOnly = false;
            }
        }

        string uniqueRef;
        public string UniqueRef
        {
            get => uniqueRef;
            set => SetPropertyValue (nameof (UniqueRef), ref uniqueRef, value);
        }

        string text;
        public string Text
        {
            get => text;
            set => SetPropertyValue (nameof (Text), ref text, value);
        }

        string sourceInfo;
        public string SourceInfo
        {
            get => sourceInfo;
            set => SetPropertyValue (nameof (SourceInfo), ref sourceInfo, value);
        }

        string additionalInfo;
        public string AdditionalInfo
        {
            get => additionalInfo;
            set => SetPropertyValue (nameof (AdditionalInfo), ref additionalInfo, value);
        }

        bool? isOriginal;
        public bool? IsOriginal
        {
            get => isOriginal;
            set => SetPropertyValue (nameof (IsOriginal), ref isOriginal, value);
        }

        bool? isWellKnownPhrase;
        public bool? IsWellKnownPhrase
        {
            get => isWellKnownPhrase;
            set => SetPropertyValue (nameof (IsWellKnownPhrase), ref isWellKnownPhrase, value);
        }

        bool? isUsedInSong;
        public bool? IsUsedInSong
        {
            get => isUsedInSong;
            set => SetPropertyValue (nameof (IsUsedInSong), ref isUsedInSong, value);
        }

        bool isConcept;
        public bool IsConcept
        {
            get => isConcept;
            set => SetPropertyValue (nameof (IsConcept), ref isConcept, value);
        }

        bool isNotLyric;
        public bool IsNotLyric
        {
            get => isNotLyric;
            set => SetPropertyValue (nameof (IsNotLyric), ref isNotLyric, value);
        }

        // Themes (15)
        bool isTime;
        public bool IsTime
        {
            get => isTime;
            set => SetPropertyValue (nameof (IsTime), ref isTime, value);
        }

        bool isAlienation;
        public bool IsAlienation
        {
            get => isAlienation;
            set => SetPropertyValue (nameof (IsAlienation), ref isAlienation, value);
        }

        bool isViolence;
        public bool IsViolence
        {
            get => isViolence;
            set => SetPropertyValue (nameof (IsViolence), ref isViolence, value);
        }

        bool isReligion;
        public bool IsReligion
        {
            get => isReligion;
            set => SetPropertyValue (nameof (IsReligion), ref isReligion, value);
        }

        bool isSlacker;
        public bool IsSlacker
        {
            get => isSlacker;
            set => SetPropertyValue (nameof (IsSlacker), ref isSlacker, value);
        }

        bool isLove;
        public bool IsLove
        {
            get => isLove;
            set => SetPropertyValue (nameof (IsLove), ref isLove, value);
        }

        bool isSociety;
        public bool IsSociety
        {
            get => isSociety;
            set => SetPropertyValue (nameof (IsSociety), ref isSociety, value);
        }

        bool isFriends;
        public bool IsFriends
        {
            get => isFriends;
            set => SetPropertyValue (nameof (IsFriends), ref isFriends, value);
        }

        bool isDrinking;
        public bool IsDrinking
        {
            get => isDrinking;
            set => SetPropertyValue (nameof (IsDrinking), ref isDrinking, value);
        }

        bool isDream;
        public bool IsDream
        {
            get => isDream;
            set => SetPropertyValue (nameof (IsDream), ref isDream, value);
        }

        bool isSeductive;
        public bool IsSeductive
        {
            get => isSeductive;
            set => SetPropertyValue (nameof (IsSeductive), ref isSeductive, value);
        }

        bool isPeople;
        public bool IsPeople
        {
            get => isPeople;
            set => SetPropertyValue (nameof (IsPeople), ref isPeople, value);
        }

        bool isUpbeat;
        public bool IsUpbeat
        {
            get => isUpbeat;
            set => SetPropertyValue (nameof (IsUpbeat), ref isUpbeat, value);
        }

        bool isOther;
        public bool IsOther
        {
            get => isOther;
            set => SetPropertyValue (nameof (IsOther), ref isOther, value);
        }

        bool isGeneral;
        public bool IsGeneral
        {
            get => isGeneral;
            set => SetPropertyValue (nameof (IsGeneral), ref isGeneral, value);
        }

        Rating rating;
        public Rating Rating
        {
            get => rating;
            set => SetPropertyValue (nameof (Rating), ref rating, value);
        }

        DateTime? conceivedAt;
        public DateTime? ConceivedAt
        {
            get => conceivedAt;
            set => SetPropertyValue (nameof (ConceivedAt), ref conceivedAt, value);
        }

        DateTime lastReviewedAt;
        public DateTime LastReviewedAt
        {
            get => lastReviewedAt;
            set => SetPropertyValue (nameof (LastReviewedAt), ref lastReviewedAt, value);
        }
    }
}
