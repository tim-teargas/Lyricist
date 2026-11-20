using System;
using DevExpress.Xpo;

// IMPORTANT: the persistent classes keep the prototype's namespace (DumbAngel) and class names.
// XPO records each object's class by full name in the XPObjectType table ("DumbAngel.Lyric"),
// so renaming the namespace would stop existing rows from loading. The tables and columns are
// exactly the prototype's: nothing here changes the database schema.
namespace DumbAngel
{
    /// <summary>
    /// The prototype's base class: active flag and created / updated times.
    /// Non-persistent, so its members live in each derived class's own table.
    /// </summary>
    [NonPersistent]
    public abstract class NexusObject : XPObject
    {
        protected NexusObject (Session session) : base (session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DateTime now = DateTime.Now;
            IsActive = true;
            CreatedAt = now;
            UpdatedAt = now;
        }

        // The prototype set these in a Save() that hid XPO's own, so commits through a
        // UnitOfWork skipped it. OnSaving always runs.
        protected override void OnSaving()
        {
            base.OnSaving();
            if (IsDeleted)
                return;
            DateTime now = DateTime.Now;
            if (Session.IsNewObject (this))
                CreatedAt = now;
            if (!KeepUpdatedAt)
                UpdatedAt = now;
        }

        /// <summary>True while saving something that isn't an edit (e.g. a "reviewed" time stamp).</summary>
        protected virtual bool KeepUpdatedAt => false;

        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => SetPropertyValue (nameof (IsActive), ref isActive, value);
        }

        DateTime createdAt;
        public DateTime CreatedAt
        {
            get => createdAt;
            set => SetPropertyValue (nameof (CreatedAt), ref createdAt, value);
        }

        DateTime updatedAt;
        public DateTime UpdatedAt
        {
            get => updatedAt;
            set => SetPropertyValue (nameof (UpdatedAt), ref updatedAt, value);
        }

        [NonPersistent]
        public string IsActiveText => IsActive ? "Yes" : "No";

        [NonPersistent]
        public bool HasBeenSaved => !Session.IsNewObject (this);
    }
}
