using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DumbAngel;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    /// <summary>
    /// A row of flag checkboxes (kinds and themes) plus Clear. Ticked flags must ALL be set on an
    /// idea for it to show (Love + Time = ideas tagged with both). Built in code from LyricFlags,
    /// so new flags appear by themselves; the boxes wrap to the control's width.
    /// </summary>
    [System.ComponentModel.DesignerCategory ("Code")]
    public sealed class FlagFilterControl : XtraUserControl
    {
        readonly FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true };
        readonly List<(LyricFlag flag, CheckEdit box)> boxes = new List<(LyricFlag, CheckEdit)>();
        bool clearing;

        /// <summary>Raised when a box is ticked / unticked or Clear is pressed.</summary>
        public event EventHandler FilterChanged;

        public FlagFilterControl()
        {
            Controls.Add (flow);
            foreach (LyricFlag flag in LyricFlags.All)
            {
                var box = new CheckEdit
                {
                    Properties = { Caption = flag.ShortCaption },
                    ToolTip = flag.Caption,
                    Width = 104,
                    Margin = new Padding (2, 2, 2, 0),
                };
                box.CheckedChanged += (s, e) => { if (!clearing) Changed(); };
                boxes.Add ((flag, box));
                flow.Controls.Add (box);
            }
            var clear = new SimpleButton { Text = "Clear", Width = 70, Margin = new Padding (8, 1, 2, 0) };
            clear.Click += (s, e) => Clear();
            flow.Controls.Add (clear);
        }

        public IEnumerable<LyricFlag> Ticked => boxes.Where (b => b.box.Checked).Select (b => b.flag);

        public bool Any => boxes.Any (b => b.box.Checked);

        /// <summary>True when the lyric has every ticked flag (and always true when none are ticked).</summary>
        public bool Matches (Lyric lyric) => Ticked.All (flag => LyricFlags.IsSet (lyric, flag));

        /// <summary>e.g. "Love + Time" (empty when nothing is ticked).</summary>
        public string Description => string.Join (" + ", Ticked.Select (f => f.ShortCaption));

        public void Clear()
        {
            if (!Any)
                return;
            clearing = true;
            try
            {
                foreach (var b in boxes)
                    b.box.Checked = false;
            }
            finally
            {
                clearing = false;
            }
            Changed();
        }

        void Changed() => FilterChanged?.Invoke (this, EventArgs.Empty);
    }
}
