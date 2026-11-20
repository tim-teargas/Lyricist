using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Lyricist.Core;

namespace Lyricist.Desktop
{
    public sealed class WindowEntry
    {
        public string Key { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Maximized { get; set; }
    }

    public sealed class ColumnEntry
    {
        public string Key { get; set; }
        /// <summary>"FieldName=width;FieldName=width"</summary>
        public string Widths { get; set; }
    }

    public sealed class LayoutStore
    {
        public List<WindowEntry> Windows { get; set; } = new List<WindowEntry>();
        public List<ColumnEntry> Columns { get; set; } = new List<ColumnEntry>();
    }

    /// <summary>
    /// Window positions / sizes and grid column widths, remembered between sessions in
    /// %APPDATA%\Lyricist\layout.xml. (The main list's grid keeps its own full layout file.)
    /// </summary>
    static class WindowMemory
    {
        static readonly string path = Path.Combine (AppSettings.Folder, "layout.xml");
        static LayoutStore store = Load();

        /// <summary>Restores the window when it loads and remembers it when it closes.</summary>
        public static void Remember (Form form, string key, Point? cascade = null)
        {
            form.Load += (s, e) =>
            {
                if (Restore (form, key) && cascade.HasValue)
                    form.Location = new Point (form.Left + cascade.Value.X, form.Top + cascade.Value.Y);
            };
            form.FormClosing += (s, e) => Store (form, key);
        }

        /// <summary>False when nothing is remembered (or it was on a screen that's gone).</summary>
        public static bool Restore (Form form, string key)
        {
            WindowEntry entry = store.Windows.FirstOrDefault (w => w.Key == key);
            if (entry == null || entry.Width < 100 || entry.Height < 80)
                return false;
            var bounds = new Rectangle (entry.Left, entry.Top, entry.Width, entry.Height);
            if (!Screen.AllScreens.Any (screen => screen.WorkingArea.IntersectsWith (bounds)))
                return false;
            form.StartPosition = FormStartPosition.Manual;
            if (form.FormBorderStyle == FormBorderStyle.FixedDialog || form.FormBorderStyle == FormBorderStyle.FixedSingle)
                form.Location = bounds.Location;   // fixed-size windows: position only
            else
                form.Bounds = bounds;
            if (entry.Maximized)
                form.WindowState = FormWindowState.Maximized;
            return true;
        }

        public static void Store (Form form, string key)
        {
            if (form.WindowState == FormWindowState.Minimized && form.RestoreBounds.Width <= 0)
                return;
            Rectangle bounds = form.WindowState == FormWindowState.Normal ? form.Bounds : form.RestoreBounds;
            WindowEntry entry = store.Windows.FirstOrDefault (w => w.Key == key);
            if (entry == null)
                store.Windows.Add (entry = new WindowEntry { Key = key });
            entry.Left = bounds.Left;
            entry.Top = bounds.Top;
            entry.Width = bounds.Width;
            entry.Height = bounds.Height;
            entry.Maximized = form.WindowState == FormWindowState.Maximized;
            Save();
        }

        /// <summary>Remembers the view's current column widths (if it has columns).</summary>
        public static void StoreColumns (GridView view, string key)
        {
            if (view.Columns.Count == 0)
                return;
            string widths = string.Join (";", view.Columns.Cast<GridColumn>().Select (c => c.FieldName + "=" + c.Width));
            ColumnEntry entry = store.Columns.FirstOrDefault (c => c.Key == key);
            if (entry == null)
                store.Columns.Add (entry = new ColumnEntry { Key = key });
            entry.Widths = widths;
            Save();
        }

        /// <summary>Applies remembered widths; false when there are none (best-fit instead).</summary>
        public static bool RestoreColumns (GridView view, string key)
        {
            ColumnEntry entry = store.Columns.FirstOrDefault (c => c.Key == key);
            if (string.IsNullOrEmpty (entry?.Widths))
                return false;
            bool any = false;
            foreach (string part in entry.Widths.Split (';'))
            {
                int eq = part.LastIndexOf ('=');
                if (eq <= 0 || !int.TryParse (part.Substring (eq + 1), out int width))
                    continue;
                GridColumn column = view.Columns[part.Substring (0, eq)];
                if (column != null && width > 10)
                {
                    column.Width = width;
                    any = true;
                }
            }
            return any;
        }

        static LayoutStore Load()
        {
            try
            {
                if (File.Exists (path))
                    using (var stream = File.OpenRead (path))
                        return (LayoutStore) new XmlSerializer (typeof (LayoutStore)).Deserialize (stream) ?? new LayoutStore();
            }
            catch
            {
                // A damaged layout file just means default positions.
            }
            return new LayoutStore();
        }

        static void Save()
        {
            try
            {
                Directory.CreateDirectory (AppSettings.Folder);
                using (var stream = File.Create (path))
                    new XmlSerializer (typeof (LayoutStore)).Serialize (stream, store);
            }
            catch (Exception ex)
            {
                Ui.Log (ex);
            }
        }
    }
}
