# Lyricist: where we are (handover for a new session)

Read this first. Designed with Tim on 20 Nov 2026 (the conversation started as blue-sky thinking about
lyrics and vocals for DumbAngel, repo tim-teargas/DumbAngel).

## Working with Tim

- Wants things built, not just planned; likes concise answers.
- C# / .NET / XPO / DevExpress developer; no Python. Builds and runs on his Windows machine: nothing
  here can be compiled in the cloud session (no .NET SDK, no DevExpress), so read code carefully.
- Dislikes DevExpress LayoutControl: plain controls placed with Location / Size / Anchor, so he can
  move things in the designer.
- His prototype app (the .NET Framework "DumbAngel" prototype, WinForms + DevExpress) still edits the
  same tables; he'll retire its lyric screens once Lyricist works.

## The data (unchanged prototype tables)

- `Lyric` (NexusObject : XPObject): Oid (the reference), UniqueRef (only the Ref of imported XML
  files: used to skip duplicates), Text, SourceInfo, AdditionalInfo, IsOriginal / IsWellKnownPhrase /
  IsUsedInSong (bool?, "not sure" allowed), IsConcept, IsNotLyric, 15 theme bools (Time / The Past,
  Alienation / Isolation, Violence / Death, Religion, Slacker, Love / Relationships, Society /
  Politics, Friends, Drinking, Dream / Narco / Wonder, Seductive World / Glamour, Specific People,
  Upbeat, Other, General Use Phrase), Rating (-> Rating table, Oid 1-5 Crap..Excellent), ConceivedAt,
  LastReviewedAt, + NexusObject's IsActive, CreatedAt, UpdatedAt. **1,063 ideas** (Oids reach ~4,100:
  SQL Server identity-cache jumps after crashes, since fixed by a server setting; gaps are normal).
- The persistent classes stay in **namespace DumbAngel** with the same names: XPO stores the class
  name in XPObjectType ("DumbAngel.Lyric"). XPO runs with **SchemaAlreadyExists**: it never alters
  the schema. New tables (song drafts) are to be added deliberately.
- `Rating` is shared with the prototype's prog relations: read only.
- The prototype's Notes (ctlNoteButton) aren't used for lyrics: ignore.
- `LyricFlags` (Core) is the one list of the flags with their captions.

## Solution

- `Directory.Build.props`: `LyricistTargetFramework` (net10.0-windows; net48 if DevExpress 21.2 has
  trouble on .NET 10: Tim is limited to DevExpress **21.2**), `DevExpressRoot` (local install; the
  targets file picks `Framework` or `NetCore`). DevExpress assemblies are referenced by HintPath.
- `Lyricist.Core`: XPO classes, `Database` (connect / test), `AppSettings`
  (`%APPDATA%\Lyricist\settings.xml`: connection string, FTP login, "last" entry values),
  `LyricImporter` (FTP XML import, prototype format, duplicate Refs skipped, optional archive move).
- `Lyricist.Desktop` (exe "Lyricist"): `MainForm` = the prototype's list (same visible columns, every
  other flag in the Column Chooser, layout saved to `%APPDATA%\Lyricist\grid-layout.xml`; New, Import,
  Export xlsx, Settings, Refresh; double-click / right-click Edit, Delete = XPO deferred delete as
  before). `LyricEditForm` = the prototype's edit form without tabs / LayoutControl (Set Now, Use Last,
  Minus, Paste (strips iCloud zero-width spaces), Split, Use last, Use last first line; Rhymes /
  Synonyms still open RhymeZone / thesaurus.com). `SettingsForm`.
- XPO free licence: if ever moving to current XPO (25.1+), register the free offer before
  31 Dec 2026 (devexpress.com/xpo-orm-free).

## Status

- **v0.1.0 (20 Nov 2026)**: the runnable capture app above. DevExpress 21.2 is installed in
  `D:\Program Files (x86)\DevExpress 21.2\Components\Bin` (Framework / NetCore / Standard; for .NET
  builds Data and Xpo are in Standard). Builds on .NET 10, but XtraForm crashes at start-up there
  (FormIconHelper type initializer, NullReferenceException: 21.2 predates .NET 10), so the target is
  **net48**. Tested by Tim 20 Nov: builds and runs, reads all 1,063 ideas. Staying on 4.8 (DevExpress
  21.2 is all he has a licence for).

- **v0.2.0 (20 Nov 2026, evening)**: the word panel (stage 1's second half). Not yet built by Tim.
  - Core `Words/`: `PronouncingDictionary` (CMU for US; Britfone for UK, else CMU made British:
    non-rhotic, LOT ɒ, GOAT əʊ, NURSE ɜː; BATH can't be told apart so keeps æ), shared IPA-like
    `Phone` symbols with stress -1 / 0 / 1 / 2; `Rhymer` per accent (perfect = same from the last
    primary-stressed vowel; near = same vowels + close consonants: same kind, one extra, or + s/z;
    vowel only = assonance; weak unstressed vowels after the stress count as one); `Scansion`
    (syllables, DUM / Dum / da patterns, line totals; unknown words estimated, marked ?);
    `Thesaurus` (MyThes / WordNet); `WordData` (lazy, thread-safe, `Preload`, `IsLoaded`). Data in
    `src/Lyricist.Core/WordData` (gzipped, ~5 MB, licences + README), copied next to the exe.
    The rules were tried in Python first: UK "car" rhymes with "spa", US "father" with "bother".
  - Desktop: `WordsForm` (one modeless window: word box, Look up, Back, UK / US, Include rare words
    (rare = not in Britfone's 16k), info line, Line box with scansion, tabs Rhymes (Perfect / Near /
    Vowel only lists, double-click = look up), Synonyms (double-click a word = look up), In your
    ideas (lines whose last word rhymes perfect / near; double-click opens the idea)). Accent, rare
    words and window bounds saved in settings. Edit forms are now **modeless** (several open at
    once, one per lyric; closing refreshes the list); their Rhymes / Synonyms buttons open the Words
    window on the word at the cursor + its line; double-clicking a word looks it up when the Words
    window is open. List: Words... button.

## Plan (agreed 20 Nov)

1. **Capture** (done, v0.1.0) and the **word panel** (v0.2.0, above).
2. **Workbench window**: rhyme pairs across the whole collection (ready-made couplets), "shuffle
   three" collisions with filters, review queue (oldest LastReviewedAt), recurring words.
3. **Song drafts** (new tables): sections (verse / chorus / bridge), ideas pulled in by Oid,
   rhyme-scheme letters and syllable / stress per line worked out automatically, a rhyme-scheme
   reference (explain schemes and name songs that use them; never store other songs' lyrics).
4. **Prompts without the API**: templates (theme mining, collisions, concept expander, continue a
   line, tidy-up) build a prompt from a filtered selection -> Copy -> Tim pastes into a Claude chat ->
   optional "Paste result" reads a structured block back (suggested tags, groups, outlines), each
   change approved. Tim doesn't want the API route.
5. **DumbAngel link**: Lyricist reads DA's prog library (`Documents\DumbAngel2`, JSON) read only; a
   localhost link (DA listens, one JSON message per line) for: audition / run / loop a prog, show it
   in the Progs tab (Lyricist -> DA) and what's playing now: prog, chord, bar / beat, scene, project
   name (DA -> Lyricist). A lyric **prompter** lives in Lyricist (second screen), following DA.
   Anything DA must show comes as files in `DumbAngel2\Songs\` or over the link: DA never talks to
   the database.
6. **Phone capture** (not urgent): a page in Tim's TV logger Blazor app (tm.profusion-media.co.uk,
   Blazor on a Windows host, used on his iPhone); ideas saved on the phone first, posted to a small
   upload endpoint with a secret key (kept apart from his commercial API), written as XML (with a new
   GUID Ref) into the FTP folder the Import reads. Ask Tim whether the TV app has a server-side
   project (endpoint there) or is static files (separate tiny ASP.NET endpoint). Native MAUI on
   iPhone ruled out (needs a Mac or the paid Apple licence). iCloud Notes route dropped.

## Parked: vocal ideas for DumbAngel's audio side (internal synth architecture)

- **Layered retuned vocal** (Tim's proof of concept): live vocal in; up to 3 delayed copies (e.g.
  1 beat apart), each retuned to a DA output / harmony-source voice **at the time it plays**, so an
  echoed phrase lands on the next chord's voicing (Tim: "much more like how real singers would sing";
  his test sounded great but was a configuration nightmare and heavy on CPU). In one plugin: pitch
  analysed once (layers reuse the delayed pitch track), no routing, automatic ducking of layers
  under the lead, octave / formant shifts so his under-an-octave comfortable range carries.
- **Singing synths**: Cubase 15's Omnivocal renders only from notes + lyrics entered in advance (Tim
  checked: no live MIDI); one note per syllable, so Lyricist could show syllable counts and copy lines
  split into syllables; DA take bundles' .out.mid (a track per output) could feed Omnivocal tracks.
  Live options: Plogue Alter/Ego (free; type lyrics, each note sings the next syllable: DA outputs
  could drive it), Dreamtonics Vocoflex (real-time voice transformation of Tim's own voice).
- Tim's gear: TC VoiceLive Rack (MIDI notes harmony), TC Perform-VK, a couple of vocoders.
