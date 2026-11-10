using System;
using System.Drawing;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.BUS;

namespace MultiServiceEnterpriseSystem.GUI
{
    public class frmReportStats : Form
    {
        private readonly KaraokeBUS _karaokeBus = new();
        private readonly GarageBUS _garageBus = new();
        private readonly ProjectTaskBUS _projectBus = new();

        private Label lblTotalKaraokeRev = null!;
        private Label lblTotalGarageRev = null!;
        private Label lblTotalCombinedRev = null!;
        private Label lblTotalTasks = null!;

        private DataGridView dgvTopServices = null!;
        private DataGridView dgvTopParts = null!;
        private DataGridView dgvTaskStats = null!;

        public frmReportStats()
        {
            InitializeComponent();
            LoadReportData();
        }

        private void InitializeComponent()
        {
            this.Text = "BÁO CÁO - THỐNG KÊ DOANH THU & HIỆU SUẤT HOẠT ĐỘNG";
            this.Size = new Size(1150, 720);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.FontRegular;

            // 1. Top KPI Summary Cards
            var pnlKpis = new Panel
            {
                Dock = DockStyle.Top,
                Height = 130,
                BackColor = UITheme.Background,
                Padding = new Padding(15, 15, 15, 10)
            };

            var card1 = CreateKpiCard("💰 DOANH THU KARAOKE", "0 VNĐ", UITheme.Primary, 0);
            var card2 = CreateKpiCard("🚗 DOANH THU GARAGE", "0 VNĐ", UITheme.Success, 270);
            var card3 = CreateKpiCard("💎 TỔNG DOANH THU HỆ THỐNG", "0 VNĐ", Color.FromArgb(168, 85, 247), 540);
            var card4 = CreateKpiCard("📋 TỔNG SỐ ĐẦU CÔNG VIỆC", "0 Task", UITheme.Secondary, 810);

            lblTotalKaraokeRev = (Label)card1.Controls[1];
            lblTotalGarageRev = (Label)card2.Controls[1];
            lblTotalCombinedRev = (Label)card3.Controls[1];
            lblTotalTasks = (Label)card4.Controls[1];

            pnlKpis.Controls.AddRange(new Control[] { card1, card2, card3, card4 });
            this.Controls.Add(pnlKpis);

            // 2. Tabs Reports
            var tabReports = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.FontBold,
                Padding = new Point(15, 8)
            };

            // TAB 1: TOP DỊCH VỤ KARAOKE
            var tab1 = new TabPage("🍹 Top 5 Dịch vụ Karaoke Bán Chạy Nhất") { BackColor = UITheme.Background, Padding = new Padding(15) };
            var lblT1 = new Label { Text = "BÁO CÁO TOP 5 DỊCH VỤ BÁN CHẠY NHẤT (TRUY VẤN TỐI ƯU SQLDATAREADER)", Dock = DockStyle.Top, Height = 30, Font = UITheme.FontBold };
            dgvTopServices = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvTopServices);
            tab1.Controls.AddRange(new Control[] { dgvTopServices, lblT1 });
            tabReports.TabPages.Add(tab1);

            // TAB 2: TOP PHỤ TÙNG GARAGE
            var tab2 = new TabPage("⚙️ Top 5 Phụ Tùng Garage Thay Thế Nhiều Nhất") { BackColor = UITheme.Background, Padding = new Padding(15) };
            var lblT2 = new Label { Text = "BÁO CÁO TOP 5 PHỤ TÙNG SỬ DỤNG NHIỀU NHẤT (TRUY VẤN ADO.NET NÂNG CAO)", Dock = DockStyle.Top, Height = 30, Font = UITheme.FontBold };
            dgvTopParts = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvTopParts);
            tab2.Controls.AddRange(new Control[] { dgvTopParts, lblT2 });
            tabReports.TabPages.Add(tab2);

            // TAB 3: TIẾN ĐỘ CÔNG VIỆC DỰ ÁN
            var tab3 = new TabPage("📌 Thống Kê Tiến Độ Dự Án (Kanban)") { BackColor = UITheme.Background, Padding = new Padding(15) };
            var lblT3 = new Label { Text = "THỐNG KÊ SỐ LƯỢNG CÔNG VIỆC THEO TRẠNG THÁI (TO DO / IN PROGRESS / REVIEW / DONE)", Dock = DockStyle.Top, Height = 30, Font = UITheme.FontBold };
            dgvTaskStats = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvTaskStats);
            tab3.Controls.AddRange(new Control[] { dgvTaskStats, lblT3 });
            tabReports.TabPages.Add(tab3);

            this.Controls.Add(tabReports);
            pnlKpis.BringToFront();
        }

        private Panel CreateKpiCard(string title, string value, Color color, int left)
        {
            var card = new Panel
            {
                Location = new Point(left + 15, 10),
                Width = 250,
                Height = 95,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblT = new Label
            {
                Text = title,
                ForeColor = UITheme.TextMuted,
                Font = UITheme.FontSmall,
                Location = new Point(12, 12),
                AutoSize = true
            };

            var lblV = new Label
            {
                Text = value,
                ForeColor = color,
                Font = UITheme.FontSubheading,
                Location = new Point(12, 38),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { lblT, lblV });
            return card;
        }

        private void LoadReportData()
        {
            try
            {
                decimal kRev = _karaokeBus.GetTotalRevenue();
                decimal gRev = _garageBus.GetTotalRevenue();
                decimal totalRev = kRev + gRev;
                int taskCount = _projectBus.GetTotalTaskCount();

                lblTotalKaraokeRev.Text = $"{kRev:N0} VNĐ";
                lblTotalGarageRev.Text = $"{gRev:N0} VNĐ";
                lblTotalCombinedRev.Text = $"{totalRev:N0} VNĐ";
                lblTotalTasks.Text = $"{taskCount} Công việc";

                dgvTopServices.DataSource = _karaokeBus.GetTopServicesReport();
                dgvTopParts.DataSource = _garageBus.GetTopPartsReport();
                dgvTaskStats.DataSource = _projectBus.GetTaskStatusStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

