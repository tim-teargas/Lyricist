using System.Collections.Generic;
using DumbAngel;
using Lyricist.Core.Prompts;

namespace Lyricist.Desktop
{
    /// <summary>What the other windows ask the main window to do (MainForm implements it).</summary>
    public interface ILyricHost
    {
        /// <summary>Every idea (unfiltered).</summary>
        IEnumerable<Lyric> AllLyrics { get; }

        void OpenLyric (Lyric lyric);

        /// <summary>Opens (or reuses) the Words window; onlyIfOpen: do nothing unless it's open.</summary>
        void ShowWords (string word, string line, bool onlyIfOpen);

        /// <summary>The Workbench's Related ideas tab for this idea.</summary>
        void ShowRelated (Lyric lyric);

        void ShowPrompt (PromptContext context);
    }
}
