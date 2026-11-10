using System.Drawing;
using System.Windows.Forms;

namespace MultiServiceEnterpriseSystem.GUI
{
    public static class UITheme
    {
        public static readonly Color Primary = Color.FromArgb(79, 70, 229);        // Indigo 600
        public static readonly Color PrimaryDark = Color.FromArgb(30, 41, 59);     // Slate 800
        public static readonly Color PrimaryLight = Color.FromArgb(238, 242, 255); // Indigo 50
        public static readonly Color Secondary = Color.FromArgb(14, 165, 233);     // Sky 500
        public static readonly Color Success = Color.FromArgb(16, 185, 129);       // Emerald 500
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);       // Amber 500
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);         // Red 500
        public static readonly Color Background = Color.FromArgb(248, 250, 252);   // Slate 50
        public static readonly Color CardBg = Color.White;
        public static readonly Color TextMain = Color.FromArgb(15, 23, 42);        // Slate 900
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);    // Slate 500
        public static readonly Color BorderColor = Color.FromArgb(226, 232, 240);  // Slate 200

        public static readonly Font FontHeading = new("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font FontSubheading = new("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font FontRegular = new("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBold = new("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontSmall = new("Segoe UI", 9F, FontStyle.Regular);

        public static void StyleGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 36;
            dgv.GridColor = BorderColor;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryDark;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBold;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.ColumnHeadersHeight = 42;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextMain;
            dgv.DefaultCellStyle.Font = FontRegular;
            dgv.DefaultCellStyle.SelectionBackColor = PrimaryLight;
            dgv.DefaultCellStyle.SelectionForeColor = Primary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
        }

        public static Button CreateButton(string text, Color backColor, Color foreColor, int width = 120, int height = 38)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = foreColor,
                Width = width,
                Height = height,
                FlatStyle = FlatStyle.Flat,
                Font = FontBold,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}

