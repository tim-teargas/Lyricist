using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DumbAngel;

namespace Lyricist.Core.Prompts
{
    /// <summary>What a prompt can be built from (whichever window it was opened in).</summary>
    public sealed class PromptContext
    {
        /// <summary>The ideas in view (a filtered list, the three shuffled, related ideas...).</summary>
        public List<Lyric> Ideas { get; set; } = new List<Lyric>();
        /// <summary>The one idea it's about, when there is one (edit window, related ideas).</summary>
        public Lyric Focus { get; set; }
        /// <summary>Where the ideas came from, for the prompt: "filtered to Love + Time", "shuffled"...</summary>
        public string Description { get; set; } = "";
        /// <summary>A song draft (its Ideas are the ideas it uses).</summary>
        public Lyricist.Data.Song Song { get; set; }
        /// <summary>The song's text with rhyme letters and syllable counts (SongAnalysis.Annotated).</summary>
        public string SongAnnotated { get; set; } = "";
        /// <summary>The song's sections and schemes, e.g. "Verse 1: ABAB · Chorus: AABB".</summary>
        public string SongSchemes { get; set; } = "";
    }

    public sealed class PromptOptions
    {
        /// <summary>Tim's Additional Info: his thoughts on how an idea could be used.</summary>
        public bool IncludeNotes { get; set; } = true;
        public bool IncludeThemes { get; set; } = true;
        /// <summary>Tim's own words about how he writes (Settings), put at the top of every prompt.</summary>
        public string AboutMe { get; set; } = "";
        // Continue a line
        public string RhymeScheme { get; set; } = "AABB";
        public int Syllables { get; set; }   // 0 = match the line
    }

    public sealed class PromptTemplate
    {
        public PromptTemplate (string name, string description, Func<PromptContext, bool> applies,
                               Func<PromptContext, PromptOptions, string> build)
        {
            Name = name;
            Description = description;
            Applies = applies;
            Build = build;
        }

        public string Name { get; }
        public string Description { get; }
        public Func<PromptContext, bool> Applies { get; }
        public Func<PromptContext, PromptOptions, string> Build { get; }
        public override string ToString() => Name;
    }

    /// <summary>
    /// Prompts to paste into a chat with Claude: the ideas written out plainly, then the task.
    /// Templates that could feed changes back ask for a fenced "lyricist" JSON block at the end, so
    /// a later "Paste result" can read it (not built yet).
    /// </summary>
    public static class PromptBuilder
    {
        public static readonly IReadOnlyList<PromptTemplate> Templates = new[]
        {
            new PromptTemplate ("Song: feedback",
                "What's working, which lines are weak (metre, forced rhymes, clichés), structure; a few alternatives, not a rewrite.",
                c => c.Song != null, SongFeedback),
            new PromptTemplate ("Song: fill the gaps",
                "Options for the lines marked ??? (or ...), fitting the rhyme scheme and syllable counts around them.",
                c => c.Song != null, SongGaps),
            new PromptTemplate ("Song: where next?",
                "Where the song could go next (verse, bridge, chorus changes) and which of its ideas fit where.",
                c => c.Song != null, SongNext),
            new PromptTemplate ("Theme mining",
                "Recurring images, tensions and points of view across the ideas, and songs they could become.",
                c => c.Ideas.Count >= 2, ThemeMining),
            new PromptTemplate ("What song holds these?",
                "A song that could hold all the ideas (made for Shuffle three).",
                c => c.Ideas.Count >= 2 && c.Ideas.Count <= 8, Collision),
            new PromptTemplate ("Concept expander",
                "Grow one idea into a song outline: title, point of view, sections, the turn.",
                c => c.Focus != null, ConceptExpander),
            new PromptTemplate ("Continue a line",
                "Options for the next lines of an idea, with a rhyme scheme and syllable count.",
                c => c.Focus != null, ContinueLine),
            new PromptTemplate ("Related ideas",
                "How an idea and the ideas related to it could work together.",
                c => c.Focus != null && c.Ideas.Count >= 1, Related),
            new PromptTemplate ("Tidy-up",
                "Suggest themes for ideas and point out near-duplicates.",
                c => c.Ideas.Count >= 2, TidyUp),
        };

        // ---------------------------------------------------------------------------------

        static string ThemeMining (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ($"Here are {c.Ideas.Count} of my lyric ideas{About (c)}. Most are one-liners I've collected; some are song concepts or titles.");
            text.AppendLine();
            Ideas (text, c.Ideas, o);
            text.AppendLine ("Please:");
            text.AppendLine ("1. Find the recurring images, tensions and points of view: what do I keep coming back to?");
            text.AppendLine ("2. Group the ideas that belong together (by idea number), and say what joins each group.");
            text.AppendLine ("3. Suggest three or four songs these groups could become: a working title, the angle, and which ideas would anchor it.");
            text.AppendLine ("4. Point out any surprising pairings: ideas that aren't obviously related but would spark off each other.");
            JsonRequest (text, "groups", "{\"groups\": [{\"name\": \"...\", \"ideas\": [12, 40], \"why\": \"...\"}]}");
            return text.ToString();
        }

        static string Collision (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ($"Here are {c.Ideas.Count} lyric ideas of mine{About (c)}. They weren't chosen to go together.");
            text.AppendLine();
            Ideas (text, c.Ideas, o);
            text.AppendLine ("What song could hold all of them? Give me two or three different answers. For each: a working title,");
            text.AppendLine ("whose voice it is and who they're talking to, the situation, where each idea would sit (verse, chorus,");
            text.AppendLine ("bridge, title line) and what changes between the start and the end. Don't write the lyrics: I'll do that.");
            return text.ToString();
        }

        static string ConceptExpander (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ("I want to grow this idea into a song:");
            text.AppendLine();
            if (o.IncludeNotes && HasNotes (c.Focus))
                text.AppendLine ("([my notes: ...] are my own thoughts on it and may include further ideas.)");
            Idea (text, c.Focus, o);
            text.AppendLine();
            List<Lyric> others = c.Ideas.Where (l => !ReferenceEquals (l, c.Focus)).ToList();
            if (others.Count > 0)
            {
                text.AppendLine ($"Other ideas of mine that might belong with it{About (c)}:");
                text.AppendLine();
                Ideas (text, others, o);
            }
            text.AppendLine ("Please suggest: a few title options; the point of view and who's being addressed; a section outline");
            text.AppendLine ("(verse / chorus / bridge, what each one does); the turn or reveal; and which of my other ideas fit where.");
            text.AppendLine ("Keep any example lines short and rough: they're prompts for me, not finished lyrics.");
            return text.ToString();
        }

        static string ContinueLine (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ("Here's a lyric idea of mine:");
            text.AppendLine();
            text.AppendLine (Quote (c.Focus.Text));
            text.AppendLine();
            if (o.IncludeNotes && !string.IsNullOrWhiteSpace (c.Focus.AdditionalInfo))
                text.AppendLine ($"(My notes on it, which may include further ideas: {Shorten (c.Focus.AdditionalInfo, 600)})").AppendLine();
            string syllables = o.Syllables > 0 ? $"about {o.Syllables} syllables a line" : "lines about as long as mine";
            text.AppendLine ($"Give me five different ways the next lines could go, using the rhyme scheme {o.RhymeScheme} (my line is the first),");
            text.AppendLine ($"{syllables}. Make them really different from each other: different directions, images and tones,");
            text.AppendLine ("not five versions of one idea. Mark the stressed syllables in CAPITALS. They're raw material I'll rewrite.");
            return text.ToString();
        }

        static string Related (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ("This is one of my lyric ideas:");
            text.AppendLine();
            Idea (text, c.Focus, o);
            text.AppendLine();
            List<Lyric> others = c.Ideas.Where (l => !ReferenceEquals (l, c.Focus)).ToList();
            text.AppendLine ($"These ideas of mine are related to it{About (c)}:");
            text.AppendLine();
            Ideas (text, others, o);
            text.AppendLine ("Which of them would work with it in one song, and how? Which pairs could be couplets or call-and-response?");
            text.AppendLine ("Is there a song in here I haven't seen? Suggest a working title and an outline for the strongest one.");
            return text.ToString();
        }

        static string TidyUp (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            text.AppendLine ($"Here are {c.Ideas.Count} of my lyric ideas{About (c)}, with the themes I've tagged them with.");
            text.AppendLine ($"My themes are: {string.Join (", ", LyricFlags.Themes.Select (f => f.Caption))}.");
            text.AppendLine();
            Ideas (text, c.Ideas, new PromptOptions { IncludeNotes = o.IncludeNotes, IncludeThemes = true, AboutMe = o.AboutMe });
            text.AppendLine ("Please:");
            text.AppendLine ("1. For ideas with no themes (or clearly wrong ones), suggest themes from my list.");
            text.AppendLine ("2. Point out near-duplicates: ideas saying much the same thing (keep the better one?).");
            text.AppendLine ("3. Suggest a new theme if several ideas share something my list doesn't cover.");
            JsonRequest (text, "suggestions",
                "{\"themes\": [{\"idea\": 12, \"add\": [\"Time / The Past\"], \"remove\": []}], \"duplicates\": [[12, 40]], \"newThemes\": [\"...\"]}");
            return text.ToString();
        }

        // ---------------------------------------------------------------------------------
        // Songs

        static StringBuilder SongIntro (PromptContext c, PromptOptions o)
        {
            var text = Start (o);
            var song = c.Song;
            text.AppendLine ($"Here's a song I'm writing: \"{(string.IsNullOrWhiteSpace (song.Title) ? "untitled" : song.Title.Trim())}\" ({song.Status}).");
            if (o.IncludeNotes && !string.IsNullOrWhiteSpace (song.Notes))
                text.AppendLine().AppendLine ($"My notes on it (may include ideas I haven't used yet): {Shorten (song.Notes, 1500)}");
            text.AppendLine();
            text.AppendLine ("Each line starts with its rhyme letter within its section (capital = perfect rhyme, small = near rhyme)");
            text.AppendLine ("and its syllable count (? = estimated). Lines in [brackets] are section headers.");
            if (!string.IsNullOrWhiteSpace (c.SongSchemes))
                text.AppendLine ($"Rhyme schemes: {c.SongSchemes}");
            text.AppendLine();
            text.AppendLine ("```");
            text.AppendLine (c.SongAnnotated);
            text.AppendLine ("```");
            text.AppendLine();
            if (c.Ideas.Count > 0)
            {
                text.AppendLine ("Ideas from my collection I've gathered for it (not all used yet):");
                text.AppendLine();
                Ideas (text, c.Ideas, o);
            }
            return text;
        }

        static string SongFeedback (PromptContext c, PromptOptions o)
        {
            var text = SongIntro (c, o);
            text.AppendLine ("Please tell me:");
            text.AppendLine ("1. What's working, and what the song seems to be about to you.");
            text.AppendLine ("2. The weakest lines and why: metre that doesn't match its partner lines (syllables / stress),");
            text.AppendLine ("   rhymes that feel forced, clichés, lines that tell rather than show.");
            text.AppendLine ("3. The structure: does the chorus land, does anything change from start to end?");
            text.AppendLine ("4. For at most five lines, two or three alternatives each, in my voice. Don't rewrite the song.");
            return text.ToString();
        }

        static string SongGaps (PromptContext c, PromptOptions o)
        {
            var text = SongIntro (c, o);
            text.AppendLine ("The lines with ??? (or ...) are gaps I haven't written yet. For each gap, give me four or five");
            text.AppendLine ("options that fit its section's rhyme scheme and roughly the syllable count of the lines around it,");
            text.AppendLine ("in the voice of the rest. Make the options really different from each other. Mark stressed");
            text.AppendLine ("syllables in CAPITALS. If there are no gaps, suggest where a line or two is missing.");
            return text.ToString();
        }

        static string SongNext (PromptContext c, PromptOptions o)
        {
            var text = SongIntro (c, o);
            text.AppendLine ("Where could this go next? Suggest two or three directions: another verse (what happens in it),");
            text.AppendLine ("a bridge or middle eight (what changes), whether the chorus should shift on its last time.");
            text.AppendLine ("Say which of my gathered ideas would fit where. Rough example lines are fine, but keep them short.");
            return text.ToString();
        }

        // ---------------------------------------------------------------------------------

        static StringBuilder Start (PromptOptions o)
        {
            var text = new StringBuilder();
            if (!string.IsNullOrWhiteSpace (o.AboutMe))
                text.AppendLine (o.AboutMe.Trim()).AppendLine();
            return text;
        }

        static string About (PromptContext c) => string.IsNullOrWhiteSpace (c.Description) ? "" : $" ({c.Description})";

        static void Ideas (StringBuilder text, IEnumerable<Lyric> ideas, PromptOptions o)
        {
            List<Lyric> list = ideas.ToList();
            if (o.IncludeNotes && list.Any (HasNotes))
                text.AppendLine ("(Where an idea has [my notes: ...], those are my own thoughts on it; they may include further ideas for how to use it.)").AppendLine();
            foreach (Lyric lyric in list)
                Idea (text, lyric, o);
            text.AppendLine();
        }

        static bool HasNotes (Lyric lyric) => !string.IsNullOrWhiteSpace (lyric.AdditionalInfo);

        /// <summary>"#123 (Very good; Time, Love; concept): text  [my notes: ...]"</summary>
        static void Idea (StringBuilder text, Lyric lyric, PromptOptions o)
        {
            var notes = new List<string>();
            if (lyric.Rating != null)
                notes.Add (lyric.Rating.Title);
            if (o.IncludeThemes)
            {
                string themes = string.Join (", ", LyricFlags.Themes.Where (f => LyricFlags.IsSet (lyric, f)).Select (f => f.ShortCaption));
                if (themes.Length > 0)
                    notes.Add (themes);
            }
            if (lyric.IsConcept) notes.Add ("concept / title");
            if (lyric.IsWellKnownPhrase == true) notes.Add ("well-known phrase");
            if (lyric.IsUsedInSong == true) notes.Add ("already used in a song");

            text.Append ($"#{lyric.Oid}");
            if (notes.Count > 0)
                text.Append ($" ({string.Join ("; ", notes)})");
            text.Append (": ").Append (Quote (lyric.Text));
            if (o.IncludeNotes && HasNotes (lyric))
                text.Append ($"   [my notes: {Shorten (lyric.AdditionalInfo, 400)}]");
            text.AppendLine();
        }

        static string Quote (string text) => (text ?? "").Trim().Replace ("\r\n", " / ").Replace ("\n", " / ");

        static string Shorten (string text, int max)
        {
            string flat = Quote (text);
            return flat.Length <= max ? flat : flat.Substring (0, max - 1) + "…";
        }

        static void JsonRequest (StringBuilder text, string what, string example)
        {
            text.AppendLine();
            text.AppendLine ($"After your answer, add the {what} as a fenced code block marked lyricist, so my app can read them, e.g.");
            text.AppendLine ("```lyricist");
            text.AppendLine (example);
            text.AppendLine ("```");
        }
    }
}
