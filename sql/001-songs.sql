-- Lyricist: song drafts (v0.5.0)
-- Adds two NEW tables and their XPObjectType rows. Nothing existing is changed.
-- Safe to run more than once. Run it against the DumbAngel database (the one Lyricist connects to),
-- e.g. in SSMS: open this file, pick the database, Execute.

SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- A song being written: free text (Body), with optional [Verse] / [Chorus] header lines.
IF OBJECT_ID (N'dbo.Song', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Song
    (
        OID                 int IDENTITY (1, 1) NOT NULL,
        Title               nvarchar (200) NULL,
        Status              nvarchar (50) NULL,      -- Idea / Drafting / Finished (free text)
        Body                nvarchar (max) NULL,     -- the lyric itself
        Notes               nvarchar (max) NULL,     -- thoughts about the song
        ProgRef             nvarchar (64) NULL,      -- a DumbAngel prog (later)
        Rating              int NULL,                -- -> Rating (1-5)
        IsActive            bit NULL,
        CreatedAt           datetime NULL,
        UpdatedAt           datetime NULL,
        OptimisticLockField int NULL,
        GCRecord            int NULL,
        ObjectType          int NULL,
        CONSTRAINT PK_Song PRIMARY KEY CLUSTERED (OID)
    );
    CREATE INDEX iGCRecord_Song ON dbo.Song (GCRecord);
    CREATE INDEX iRating_Song ON dbo.Song (Rating);
    CREATE INDEX iObjectType_Song ON dbo.Song (ObjectType);
END;

-- The ideas (Lyric rows) a song uses, in order.
IF OBJECT_ID (N'dbo.SongLyric', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SongLyric
    (
        OID                 int IDENTITY (1, 1) NOT NULL,
        Song                int NULL,                -- -> Song
        Lyric               int NULL,                -- -> Lyric
        Position            int NULL,
        OptimisticLockField int NULL,
        GCRecord            int NULL,
        ObjectType          int NULL,
        CONSTRAINT PK_SongLyric PRIMARY KEY CLUSTERED (OID)
    );
    CREATE INDEX iGCRecord_SongLyric ON dbo.SongLyric (GCRecord);
    CREATE INDEX iSong_SongLyric ON dbo.SongLyric (Song);
    CREATE INDEX iLyric_SongLyric ON dbo.SongLyric (Lyric);
    CREATE INDEX iObjectType_SongLyric ON dbo.SongLyric (ObjectType);
END;

-- XPO records each class in XPObjectType; add Lyricist's two so it never needs to.
IF NOT EXISTS (SELECT 1 FROM dbo.XPObjectType WHERE TypeName = N'Lyricist.Data.Song')
    INSERT INTO dbo.XPObjectType (TypeName, AssemblyName) VALUES (N'Lyricist.Data.Song', N'Lyricist.Core');
IF NOT EXISTS (SELECT 1 FROM dbo.XPObjectType WHERE TypeName = N'Lyricist.Data.SongLyric')
    INSERT INTO dbo.XPObjectType (TypeName, AssemblyName) VALUES (N'Lyricist.Data.SongLyric', N'Lyricist.Core');

COMMIT TRANSACTION;

SELECT N'Song' AS [Table], COUNT (*) AS [Rows] FROM dbo.Song
UNION ALL SELECT N'SongLyric', COUNT (*) FROM dbo.SongLyric;
