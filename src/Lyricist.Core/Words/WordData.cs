using System;
using System.IO;
using System.Threading.Tasks;

namespace Lyricist.Core.Words
{
    /// <summary>
    /// The shared word data, loaded once on first use (thread-safe). The files live in the
    /// WordData folder next to the exe.
    /// </summary>
    public static class WordData
    {
        public static string Folder => Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "WordData");

        static readonly Lazy<PronouncingDictionary> dictionary =
            new Lazy<PronouncingDictionary> (() => PronouncingDictionary.Load (Folder));

        static readonly Lazy<Thesaurus> thesaurus =
            new Lazy<Thesaurus> (() => Thesaurus.Load (Path.Combine (Folder, "th_en_US_v2.dat.gz")));

        static readonly Lazy<Rhymer> ukRhymer = new Lazy<Rhymer> (() => new Rhymer (Dictionary, Accent.UK));
        static readonly Lazy<Rhymer> usRhymer = new Lazy<Rhymer> (() => new Rhymer (Dictionary, Accent.US));

        public static PronouncingDictionary Dictionary => dictionary.Value;
        public static Thesaurus Thesaurus => thesaurus.Value;
        public static Rhymer RhymerFor (Accent accent) => accent == Accent.UK ? ukRhymer.Value : usRhymer.Value;

        /// <summary>True once the dictionary, thesaurus and this accent's rhymes are in memory.</summary>
        public static bool IsLoaded (Accent accent) =>
            thesaurus.IsValueCreated && (accent == Accent.UK ? ukRhymer : usRhymer).IsValueCreated;

        /// <summary>Loads everything for one accent in the background (a few seconds), so the first lookup is quick.</summary>
        public static Task Preload (Accent accent) => Task.Run (() =>
        {
            RhymerFor (accent);
            var unused = Thesaurus;
        });
    }
}
