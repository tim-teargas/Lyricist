# Lyricist

Tim's lyric ideas: capture, and (to come) a songwriting workbench. A Windows desktop app on the
DumbAngel prototype's SQL Server database, using XPO and DevExpress 21.2 WinForms controls.

## Build and run

1. Open `Lyricist.sln` in Visual Studio.
2. Check `Directory.Build.props`:
   - `LyricistTargetFramework`: `net10.0-windows`, or `net48` if DevExpress 21.2 misbehaves on .NET 10.
   - `DevExpressRoot`: where DevExpress 21.2 is installed (the `Framework` / `NetCore` folder under it is
     picked automatically; set `DevExpressBin` to point at a folder directly).
3. Run `Lyricist.Desktop`. The first time, Settings opens: paste the prototype's XPO connection string
   (e.g. `XpoProvider=MSSqlServer;data source=.;integrated security=SSPI;initial catalog=DumbAngel`),
   press Test, then OK.

Settings live in `%APPDATA%\Lyricist\settings.xml` (never in the repo).

## Database scripts

Lyricist never changes the database by itself. New tables come as scripts in `sql/`, run once by hand
(e.g. in SQL Server Management Studio) against the DumbAngel database. Each is safe to run twice.

- `sql/001-songs.sql` (v0.5.0): `Song` and `SongLyric`, for song drafts.

## Safety

Lyricist never changes the database schema (XPO `AutoCreateOption.SchemaAlreadyExists`) and keeps the
prototype's class names, so the prototype keeps working alongside it.

See `docs/HANDOVER.md` for the design and what's next.
