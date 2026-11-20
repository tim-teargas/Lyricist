using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Lyricist.Core;
using Lyricist.Core.Prompts;

namespace Lyricist.Desktop
{
    /// <summary>
    /// Builds a prompt from the ideas it was opened with: pick a template, adjust, edit the text
    /// freely if you like, Copy, paste into a chat with Claude. Only templates that fit what it
    /// was opened with are offered (e.g. "Continue a line" needs one idea).
    /// </summary>
    public partial class PromptForm : XtraForm
    {
        readonly AppSettings settings;
        readonly PromptContext context;
        bool building;

        public PromptForm (AppSettings settings, PromptContext context, string preferredTemplate = null)
        {
            InitializeComponent();
            this.settings = settings;
            this.context = context;

            foreach (PromptTemplate template in PromptBuilder.Templates.Where (t => t.Applies (context)))
                cbTemplate.Properties.Items.Add (template);
            cbTemplate.SelectedItem = cbTemplate.Properties.Items.Cast<PromptTemplate>()
                                          .FirstOrDefault (t => t.Name == preferredTemplate)
                                      ?? (cbTemplate.Properties.Items.Count > 0 ? cbTemplate.Properties.Items[0] : null);

            ceSource.Checked = settings.PromptIncludeSource;
            ceThemes.Checked = settings.PromptIncludeThemes;
            teScheme.Text = "AABB";

            cbTemplate.SelectedIndexChanged += (s, e) => Build();
            ceSource.CheckedChanged += (s, e) => { settings.PromptIncludeSource = ceSource.Checked; Save(); Build(); };
            ceThemes.CheckedChanged += (s, e) => { settings.PromptIncludeThemes = ceThemes.Checked; Save(); Build(); };
            teScheme.EditValueChanged += (s, e) => Build();
            seSyllables.EditValueChanged += (s, e) => Build();
            mePrompt.EditValueChanged += (s, e) => { if (!building) Count(); };

            Text = "Prompt: " + (string.IsNullOrWhiteSpace (context.Description)
                ? $"{context.Ideas.Count} idea{(context.Ideas.Count == 1 ? "" : "s")}" : context.Description);
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            Build();
        }

        PromptTemplate Template => cbTemplate.SelectedItem as PromptTemplate;

        void Build()
        {
            PromptTemplate template = Template;
            bool continueLine = template?.Name == "Continue a line";
            lblScheme.Visible = teScheme.Visible = lblSyllables.Visible = seSyllables.Visible = continueLine;
            if (template == null)
            {
                lblTemplateInfo.Text = "No template fits what this was opened with.";
                mePrompt.Text = "";
                return;
            }
            lblTemplateInfo.Text = template.Description;

            var options = new PromptOptions
            {
                IncludeSource = ceSource.Checked,
                IncludeThemes = ceThemes.Checked,
                AboutMe = settings.PromptAboutMe,
                RhymeScheme = string.IsNullOrWhiteSpace (teScheme.Text) ? "AABB" : teScheme.Text.Trim().ToUpperInvariant(),
                Syllables = Convert.ToInt32 (seSyllables.Value),
            };
            try
            {
                building = true;
                mePrompt.Text = template.Build (context, options);
            }
            catch (Exception ex)
            {
                mePrompt.Text = "Couldn't build the prompt: " + ex.Message;
            }
            finally
            {
                building = false;
            }
            Count();
        }

        void Count()
        {
            int words = Regex.Matches (mePrompt.Text ?? "", @"\S+").Count;
            lblCount.Text = $"{words:N0} words, {(mePrompt.Text ?? "").Length:N0} characters";
        }

        void Save()
        {
            try { settings.Save(); } catch { /* not worth interrupting for */ }
        }

        void sbCopy_Click (object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace (mePrompt.Text))
                return;
            Clipboard.SetText (mePrompt.Text);
            lblCount.Text = "Copied: paste it into a chat with Claude.";
        }

        void sbClose_Click (object sender, EventArgs e) => Close();
    }
}
