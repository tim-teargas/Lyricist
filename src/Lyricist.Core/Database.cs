using System;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

namespace Lyricist.Core
{
    /// <summary>
    /// Connects XPO to the prototype's SQL Server database.
    ///
    /// AutoCreateOption.SchemaAlreadyExists: XPO never creates or alters tables or columns.
    /// The prototype keeps working against the same database. New tables (song drafts etc.)
    /// will be added deliberately, never by XPO on its own.
    /// </summary>
    public static class Database
    {
        static readonly Type[] persistentTypes = { typeof (DumbAngel.Lyric), typeof (DumbAngel.Rating) };

        /// <summary>Sets XpoDefault.DataLayer; sessions created with new Session() then use it.</summary>
        public static void Connect (string connectionString)
        {
            XpoDefault.DataLayer = CreateDataLayer (connectionString);
            XpoDefault.Session = null;   // nothing should fall back to an implicit default session
        }

        /// <summary>Tries a connection without keeping it; returns a one-line result for the user.</summary>
        public static string Test (string connectionString)
        {
            IDataLayer layer = CreateDataLayer (connectionString);
            try
            {
                using (var session = new Session (layer))
                {
                    object count = session.Evaluate (typeof (DumbAngel.Lyric), CriteriaOperator.Parse ("Count()"), null);
                    return $"Connected: {Convert.ToInt64 (count):N0} lyric ideas.";
                }
            }
            finally
            {
                (layer as IDisposable)?.Dispose();
            }
        }

        static IDataLayer CreateDataLayer (string connectionString)
        {
            if (string.IsNullOrWhiteSpace (connectionString))
                throw new InvalidOperationException ("No database connection string has been set (Settings).");

            var dictionary = new ReflectionDictionary();
            dictionary.GetDataStoreSchema (persistentTypes);
            IDataStore store = XpoDefault.GetConnectionProvider (connectionString, AutoCreateOption.SchemaAlreadyExists);
            return new SimpleDataLayer (dictionary, store);
        }
    }
}
