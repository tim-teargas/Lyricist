using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DumbAngel;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    /// <summary>
    /// One lyric idea (the prototype's frmLyricEdit), in its own window (several can be open).
    /// The form copies values into the Lyric only on OK / Apply, so Cancel leaves it untouched.
    /// </summary>
    public partial class LyricEditForm : XtraForm
    {
        const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        readonly Lyric lyric;
        readonly AppSettings settings;
        readonly Action<string, string, bool> showWords;   // (word, line, onlyIfOpen): MainForm.ShowWords

        /// <summary>True once the lyric has been saved (OK or Apply).</summary>
        public bool Saved { get; private set; }

        public LyricEditForm (Lyric lyric, AppSettings settings, Action<string, string, bool> showWords)
        {
            InitializeComponent();
            this.lyric = lyric;
            this.settings = settings;
            this.showWords = showWords;
            // Double-click a word: it's selected as usual, and looked up if the Words window is open.
            meText.DoubleClick += (s, e) => BeginInvoke ((Action) (() => SendToWords (onlyIfOpen: true)));
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            try
            {
                PopulateData();
                meText.Focus();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem loading the lyric.", ex);
            }
        }

        // ---------------------------------------------------------------------------------

        void PopulateData()
        {
            teId.Text = lyric.HasBeenSaved ? lyric.Oid.ToString() : "(new)";
            ceActive.Checked = lyric.IsActive;
            teRef.Text = lyric.UniqueRef ?? "";
            rgRating.EditValue = lyric.Rating?.Oid;
            teConceived.Text = lyric.ConceivedAt?.ToString (DateFormat) ?? "";
            meText.Text = lyric.Text;
            meSource.Text = lyric.SourceInfo;
            meAdditional.Text = lyric.AdditionalInfo;

            ceIsOriginal.CheckState = Ui.ToCheckState (lyric.IsOriginal);
            ceIsWellKnownPhrase.CheckState = Ui.ToCheckState (lyric.IsWellKnownPhrase);
            ceIsUsedInSong.CheckState = Ui.ToCheckState (lyric.IsUsedInSong);
            ceIsConcept.Checked = lyric.IsConcept;
            ceIsNotLyric.Checked = lyric.IsNotLyric;

            ceIsTime.Checked = lyric.IsTime;
            ceIsAlienation.Checked = lyric.IsAlienation;
            ceIsViolence.Checked = lyric.IsViolence;
            ceIsReligion.Checked = lyric.IsReligion;
            ceIsSlacker.Checked = lyric.IsSlacker;
            ceIsLove.Checked = lyric.IsLove;
            ceIsSociety.Checked = lyric.IsSociety;
            ceIsFriends.Checked = lyric.IsFriends;
            ceIsDrinking.Checked = lyric.IsDrinking;
            ceIsDream.Checked = lyric.IsDream;
            ceIsSeductive.Checked = lyric.IsSeductive;
            ceIsPeople.Checked = lyric.IsPeople;
            ceIsUpbeat.Checked = lyric.IsUpbeat;
            ceIsOther.Checked = lyric.IsOther;
            ceIsGeneral.Checked = lyric.IsGeneral;

            ShowInfo();
        }

        void ShowInfo()
        {
            lblInfo.Text = lyric.HasBeenSaved
                ? $"Created {lyric.CreatedAt.ToString (DateFormat)}    Updated {lyric.UpdatedAt.ToString (DateFormat)}"
                : "Not saved yet";
            teId.Text = lyric.HasBeenSaved ? lyric.Oid.ToString() : "(new)";
        }

        bool ValidateControls()
        {
            if (string.IsNullOrWhiteSpace (meText.Text))
            {
                Ui.Error ("No lyric text has been entered.");
                meText.Focus();
                return false;
            }
            if (!TryParseDate (teConceived.Text, out _))
            {
                Ui.Error ("Bad format for the conceived date (e.g. 2026-11-20 18:35:00).");
                teConceived.Focus();
                return false;
            }
            if (rgRating.EditValue == null)
            {
                Ui.Error ("No rating has been selected.");
                rgRating.Focus();
                return false;
            }
            return true;
        }

        bool SaveLyric()
        {
            if (!ValidateControls())
                return false;

            bool isNew = !lyric.HasBeenSaved;
            TryParseDate (teConceived.Text, out DateTime conceived);

            lyric.IsActive = ceActive.Checked;
            lyric.Rating = Rating.Get (lyric.Session, Convert.ToInt32 (rgRating.EditValue));
            lyric.ConceivedAt = conceived;
            lyric.Text = meText.Text;
            lyric.SourceInfo = meSource.Text;
            lyric.AdditionalInfo = meAdditional.Text;

            lyric.IsOriginal = Ui.FromCheckState (ceIsOriginal.CheckState);
            lyric.IsWellKnownPhrase = Ui.FromCheckState (ceIsWellKnownPhrase.CheckState);
            lyric.IsUsedInSong = Ui.FromCheckState (ceIsUsedInSong.CheckState);
            lyric.IsConcept = ceIsConcept.Checked;
            lyric.IsNotLyric = ceIsNotLyric.Checked;

            lyric.IsTime = ceIsTime.Checked;
            lyric.IsAlienation = ceIsAlienation.Checked;
            lyric.IsViolence = ceIsViolence.Checked;
            lyric.IsReligion = ceIsReligion.Checked;
            lyric.IsSlacker = ceIsSlacker.Checked;
            lyric.IsLove = ceIsLove.Checked;
            lyric.IsSociety = ceIsSociety.Checked;
            lyric.IsFriends = ceIsFriends.Checked;
            lyric.IsDrinking = ceIsDrinking.Checked;
            lyric.IsDream = ceIsDream.Checked;
            lyric.IsSeductive = ceIsSeductive.Checked;
            lyric.IsPeople = ceIsPeople.Checked;
            lyric.IsUpbeat = ceIsUpbeat.Checked;
            lyric.IsOther = ceIsOther.Checked;
            lyric.IsGeneral = ceIsGeneral.Checked;

            try
            {
                lyric.Save();
            }
            catch (DevExpress.Xpo.DB.Exceptions.LockingException)
            {
                Ui.Error ("This idea was changed somewhere else (the prototype?) since it was opened. "
                        + "Cancel, Refresh the list and make the change again.");
                return false;
            }

            // The "last" helpers, now remembered between sessions.
            if (isNew)
                settings.LastConceivedText = conceived.ToString (DateFormat);
            settings.LastSourceInfo = lyric.SourceInfo;
            settings.Save();

            Saved = true;
            ShowInfo();
            return true;
        }

        static bool TryParseDate (string text, out DateTime value) =>
            DateTime.TryParseExact (text.Trim(), new[] { DateFormat, "yyyy-MM-dd HH:mm", "yyyy-MM-dd" },
                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out value)
            || DateTime.TryParse (text, out value);

        static readonly System.Text.RegularExpressions.Regex wordPattern =
            new System.Text.RegularExpressions.Regex (@"[A-Za-z\u2019']+(?:-[A-Za-z\u2019']+)*");

        /// <summary>The selected word, else the word at the cursor, else the line's last word.</summary>
        string WordAtCursor()
        {
            if (meText.SelectedText.Trim().Length > 0)
                return meText.SelectedText.Trim();
            string text = meText.Text ?? "";
            int at = Math.Min (meText.SelectionStart, text.Length);
            foreach (System.Text.RegularExpressions.Match m in wordPattern.Matches (text))
                if (at >= m.Index && at <= m.Index + m.Length)
                    return m.Value;
            var all = wordPattern.Matches (CurrentLine());
            return all.Count > 0 ? all[all.Count - 1].Value : "";
        }

        /// <summary>The line of the text the cursor is on.</summary>
        string CurrentLine()
        {
            string text = meText.Text ?? "";
            int at = Math.Min (meText.SelectionStart, text.Length);
            int start = text.LastIndexOf ('\n', Math.Max (0, at - 1));
            start = at == 0 || start < 0 ? 0 : start + 1;
            int end = text.IndexOf ('\n', at);
            if (end < 0) end = text.Length;
            return text.Substring (start, end - start).Trim();
        }

        void SendToWords (bool onlyIfOpen)
        {
            string word = WordAtCursor();
            if (word.Length > 0)
                showWords (word, CurrentLine(), onlyIfOpen);
            else if (!onlyIfOpen)
                Ui.Info ("Put the cursor on a word in the text first.");
        }

        // ---------------------------------------------------------------------------------
        // Buttons

        void sbOK_Click (object sender, EventArgs e)
        {
            try
            {
                if (!SaveLyric())
                    return;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem saving the lyric.", ex);
            }
        }

        void sbApply_Click (object sender, EventArgs e)
        {
            try
            {
                SaveLyric();
            }
            catch (Exception ex)
            {
                Ui.Error ("Problem saving the lyric.", ex);
            }
        }

        void sbCancel_Click (object sender, EventArgs e) => Close();

        void sbSetNow_Click (object sender, EventArgs e) =>
            teConceived.Text = DateTime.Now.ToString (DateFormat);

        void sbUseLastDate_Click (object sender, EventArgs e)
        {
            string text = settings.LastConceivedText ?? "";
            if (text.EndsWith (" 00:00:00"))
                text = text.Substring (0, 10);
            teConceived.Text = text;
        }

        void sbMinus_Click (object sender, EventArgs e)
        {
            if (TryParseDate (teConceived.Text, out DateTime date))
                teConceived.Text = date.AddDays (-1).ToString (DateFormat);
        }

        void sbPaste_Click (object sender, EventArgs e)
        {
            string text = Clipboard.GetText().Replace ("​", "").Trim();   // iCloud adds zero-width spaces
            text = text.Replace (Environment.NewLine + Environment.NewLine, Environment.NewLine);
            meText.Text = text;
        }

        /// <summary>Everything after the cursor moves to Source Info.</summary>
        void sbSplit_Click (object sender, EventArgs e)
        {
            int at = meText.SelectionStart;
            string text = meText.Text.Substring (0, at).Trim();
            string rest = meText.Text.Substring (at).Trim();
            meText.Text = text;
            meSource.Text = string.IsNullOrWhiteSpace (meSource.Text) ? rest : meSource.Text + Environment.NewLine + rest;
        }

        void sbUseLastSource_Click (object sender, EventArgs e) =>
            meSource.Text = settings.LastSourceInfo ?? "";

        void sbUseLastFirstLine_Click (object sender, EventArgs e)
        {
            string last = settings.LastSourceInfo ?? "";
            int end = last.IndexOfAny (new[] { '\r', '\n' });
            meSource.Text = (end < 0 ? last : last.Substring (0, end)) + Environment.NewLine;
            meSource.Focus();
            meSource.SelectionStart = meSource.Text.Length;
        }

        // Both open the Words window (rhymes, syllables, synonyms) on the word at the cursor.
        void sbSynonyms_Click (object sender, EventArgs e) => SendToWords (onlyIfOpen: false);

        void sbRhymes_Click (object sender, EventArgs e) => SendToWords (onlyIfOpen: false);
    }
}
