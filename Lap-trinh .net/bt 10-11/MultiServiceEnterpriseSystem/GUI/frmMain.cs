using System;
using System.Drawing;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.BUS;

namespace MultiServiceEnterpriseSystem.GUI
{
    public class frmMain : Form
    {
        private readonly KaraokeBUS _karaokeBus = new();
        private readonly GarageBUS _garageBus = new();
        private readonly ProjectTaskBUS _projectBus = new();

        private Panel pnlSidebar = null!;
        private Panel pnlTopBar = null!;
        private Panel pnlContent = null!;

        // Stat labels on dashboard
        private Label lblKpiKaraoke = null!;
        private Label lblKpiRooms = null!;
        private Label lblKpiGarage = null!;
        private Label lblKpiTasks = null!;
        private Label lblSystemStatus = null!;

        public frmMain()
        {
            InitializeComponent();
            LoadDashboardStats();
        }

        private void InitializeComponent()
        {
            this.Text = "HỆ THỐNG QUẢN TRỊ DỊCH VỤ & NGHIỆP VỤ DOANH NGHIỆP (3-TIER ADO.NET) - BÀI 6.3, 6.4, 6.5";
            this.Size = new Size(1300, 820);
            this.MinimumSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.FontRegular;

            // 1. Sidebar (Trái)
            pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = UITheme.PrimaryDark
            };
            BuildSidebar();
            this.Controls.Add(pnlSidebar);

            // 2. TopBar (Trên)
            pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White
            };
            BuildTopBar();
            this.Controls.Add(pnlTopBar);

            // 3. Content Panel (Giữa)
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UITheme.Background,
                Padding = new Padding(25),
                AutoScroll = true
            };
            BuildDashboardContent();
            this.Controls.Add(pnlContent);
        }

        private void BuildSidebar()
        {
            // Logo Header
            var pnlLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(20, 18, 20, 18)
            };
            var lblLogo = new Label
            {
                Text = "⚡ ENTERPRISE HUB",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                Location = new Point(18, 16),
                AutoSize = true
            };
            var lblSubLogo = new Label
            {
                Text = "Hệ Thống 3-Tier ADO.NET",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = UITheme.FontSmall,
                Location = new Point(20, 46),
                AutoSize = true
            };
            pnlLogo.Controls.AddRange(new Control[] { lblLogo, lblSubLogo });
            pnlSidebar.Controls.Add(pnlLogo);

            // Menu container
            var pnlMenu = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10, 15, 10, 15)
            };

            int top = 10;
            AddMenuItem(pnlMenu, "📊 Tổng Quan Hệ Thống", ref top, (_, _) => { ShowDashboardView(); });
            AddMenuItem(pnlMenu, "📋 Quản Lý Danh Mục (CRUD)", ref top, (_, _) => { OpenForm(new frmCategoryCRUD()); });
            AddMenuItem(pnlMenu, "⚡ Thực Thi Nghiệp Vụ Chính", ref top, (_, _) => { OpenForm(new frmBookingService()); });
            AddMenuItem(pnlMenu, "📈 Báo Cáo - Thống Kê", ref top, (_, _) => { OpenForm(new frmReportStats()); });
            AddMenuItem(pnlMenu, "🛡️ Minh Họa An Toàn Dữ Liệu", ref top, (_, _) => { OpenForm(new frmSecurityDemo()); });

            // Thông tin sinh viên ghim góc dưới Sidebar
            var pnlStudentInfo = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 110,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(15)
            };
            var lblSV = new Label
            {
                Text = "Sinh viên: NGUYỄN PHÚC VƯỢNG\nMSV: 24810320187\nLớp: D19QTANM1 - Lập trình .NET\nKhoa CNTT - ĐH Điện Lực",
                ForeColor = Color.FromArgb(203, 213, 225),
                Font = UITheme.FontSmall,
                Dock = DockStyle.Fill
            };
            pnlStudentInfo.Controls.Add(lblSV);
            pnlSidebar.Controls.Add(pnlMenu);
            pnlSidebar.Controls.Add(pnlStudentInfo);
        }

        private void AddMenuItem(Panel container, string title, ref int top, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = title,
                Top = top,
                Left = 0,
                Width = 260,
                Height = 48,
                FlatStyle = FlatStyle.Flat,
                Font = UITheme.FontBold,
                ForeColor = Color.FromArgb(226, 232, 240),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (_, _) => { btn.BackColor = Color.FromArgb(51, 65, 85); btn.ForeColor = Color.White; };
            btn.MouseLeave += (_, _) => { btn.BackColor = Color.Transparent; btn.ForeColor = Color.FromArgb(226, 232, 240); };
            btn.Click += onClick;
            container.Controls.Add(btn);
            top += 54;
        }

        private void BuildTopBar()
        {
            var lblBreadcrumb = new Label
            {
                Text = "BÁO CÁO THỰC HÀNH LẬP TRÌNH C# .NET | KIẾN TRÚC 3 LỚP (3-TIER)",
                Font = UITheme.FontBold,
                ForeColor = UITheme.PrimaryDark,
                Location = new Point(25, 22),
                AutoSize = true
            };

            lblSystemStatus = new Label
            {
                Text = "🟢 Trạng thái CSDL: Sẵn sàng kết nối",
                Font = UITheme.FontSmall,
                ForeColor = UITheme.Success,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(980, 24),
                AutoSize = true
            };

            pnlTopBar.Controls.AddRange(new Control[] { lblBreadcrumb, lblSystemStatus });
        }

        private void BuildDashboardContent()
        {
            pnlContent.Controls.Clear();

            // Welcome Banner
            var pnlWelcome = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.White,
                Padding = new Padding(25, 18, 25, 18)
            };
            var lblHello = new Label
            {
                Text = "Chào mừng đến với Hệ thống Quản trị Doanh nghiệp & Dịch vụ 3-Tier",
                Font = UITheme.FontHeading,
                ForeColor = UITheme.PrimaryDark,
                AutoSize = true
            };
            var lblSubHello = new Label
            {
                Text = "Tổ chức mã nguồn chuẩn 3 lớp: DTO, DAL (Truy vấn ADO.NET), BUS (Quy tắc nghiệp vụ & Transaction), GUI (Giao diện người dùng)",
                Font = UITheme.FontRegular,
                ForeColor = UITheme.TextMuted,
                Location = new Point(25, 52),
                AutoSize = true
            };
            pnlWelcome.Controls.AddRange(new Control[] { lblHello, lblSubHello });
            pnlContent.Controls.Add(pnlWelcome);

            // KPI Grid (4 Cards)
            var pnlKpiCards = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                Padding = new Padding(0, 20, 0, 10)
            };

            var card1 = CreateKpiCard("🎤 DOANH THU KARAOKE", "0 VNĐ", "Tổng hóa đơn đã chốt", UITheme.Primary, 0);
            var card2 = CreateKpiCard("🔥 PHÒNG ĐANG HÁT", "0 Phòng", "Công suất phục vụ hiện tại", UITheme.Danger, 245);
            var card3 = CreateKpiCard("🚗 DOANH THU GARAGE", "0 VNĐ", "Phiếu bảo dưỡng hoàn thành", UITheme.Success, 490);
            var card4 = CreateKpiCard("📋 TIẾN ĐỘ CÔNG VIỆC", "0 Task", "Tổng việc trên Kanban", UITheme.Secondary, 735);

            lblKpiKaraoke = (Label)card1.Controls[1];
            lblKpiRooms = (Label)card2.Controls[1];
            lblKpiGarage = (Label)card3.Controls[1];
            lblKpiTasks = (Label)card4.Controls[1];

            pnlKpiCards.Controls.AddRange(new Control[] { card1, card2, card3, card4 });
            pnlContent.Controls.Add(pnlKpiCards);

            // Actions & Summary Section
            var pnlActions = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(25)
            };

            var lblActionTitle = new Label
            {
                Text = "TRUY CẬP NHANH CÁC MÀN HÌNH CHỨC NĂNG THEO QUY CHUẨN ĐỒ ÁN:",
                Font = UITheme.FontSubheading,
                ForeColor = UITheme.PrimaryDark,
                Dock = DockStyle.Top,
                Height = 35
            };

            var flowQuickBtns = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 90,
                Padding = new Padding(0, 10, 0, 10)
            };

            var btnNavCrud = UITheme.CreateButton("📋 Quản Lý Danh Mục (CRUD)", UITheme.Primary, Color.White, 230, 48);
            btnNavCrud.Click += (_, _) => OpenForm(new frmCategoryCRUD());

            var btnNavBooking = UITheme.CreateButton("⚡ Đặt Phòng & Lập Hóa Đơn", UITheme.Success, Color.White, 230, 48);
            btnNavBooking.Click += (_, _) => OpenForm(new frmBookingService());

            var btnNavReport = UITheme.CreateButton("📈 Báo Cáo - Thống Kê", UITheme.Warning, Color.White, 210, 48);
            btnNavReport.Click += (_, _) => OpenForm(new frmReportStats());

            var btnNavSecurity = UITheme.CreateButton("🛡️ An Toàn Dữ Liệu & Rollback", UITheme.Danger, Color.White, 240, 48);
            btnNavSecurity.Click += (_, _) => OpenForm(new frmSecurityDemo());

            flowQuickBtns.Controls.AddRange(new Control[] { btnNavCrud, btnNavBooking, btnNavReport, btnNavSecurity });

            var lblInfoSummary = new Label
            {
                Dock = DockStyle.Fill,
                Font = UITheme.FontRegular,
                ForeColor = UITheme.TextMain,
                Text = 
                    "⭐ ĐẶC TẢ CÔNG NGHỆ & KỸ THUẬT TRIỂN KHAI:\n\n" +
                    "1. KIẾN TRÚC 3 LỚP (3-TIER ARCHITECTURE):\n" +
                    "   - DTO (Data Transfer Object): Định nghĩa các thực thể dữ liệu độc lập (PhongHat, DichVu, HoaDon, KhachHang, Xe, PhuTung, DuAn, CongViec).\n" +
                    "   - DAL (Data Access Layer): Đảm nhiệm toàn bộ giao tiếp CSDL qua ADO.NET thuần (SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader).\n" +
                    "   - BUS (Business Logic Layer): Kiểm tra tính hợp lệ dữ liệu (Validation), xử lý quy tắc nghiệp vụ, tính giờ hát và kiểm soát SqlTransaction.\n" +
                    "   - GUI (Presentation Layer): Giao diện Windows Forms thiết kế trực quan, hiện đại, thân thiện người dùng.\n\n" +
                    "2. AN TOÀN DỮ LIỆU & GIAO DỊCH (ACID TRANSACTIONS):\n" +
                    "   - Phòng chống SQL Injection triệt để bằng cơ chế Parameterized Query (SqlParameter).\n" +
                    "   - Xử lý giao dịch nhiều bước (Mở phòng, trừ tồn kho, thanh toán hóa đơn) với SqlTransaction cấp độ cô lập Serializable / RepeatableRead, tự động Rollback khi xảy ra lỗi."
            };

            pnlActions.Controls.AddRange(new Control[] { lblInfoSummary, flowQuickBtns, lblActionTitle });
            pnlContent.Controls.Add(pnlActions);

            pnlWelcome.BringToFront();
            pnlKpiCards.BringToFront();
        }

        private Panel CreateKpiCard(string title, string value, string subtitle, Color accentColor, int left)
        {
            var card = new Panel
            {
                Location = new Point(left, 15),
                Width = 230,
                Height = 115,
                BackColor = Color.White,
                Padding = new Padding(18)
            };

            // Accent bar
            var bar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = accentColor
            };
            card.Controls.Add(bar);

            var lblT = new Label
            {
                Text = title,
                ForeColor = UITheme.TextMuted,
                Font = UITheme.FontSmall,
                Location = new Point(15, 12),
                AutoSize = true
            };

            var lblV = new Label
            {
                Text = value,
                ForeColor = UITheme.TextMain,
                Font = UITheme.FontSubheading,
                Location = new Point(15, 38),
                AutoSize = true
            };

            var lblSub = new Label
            {
                Text = subtitle,
                ForeColor = UITheme.TextMuted,
                Font = UITheme.FontSmall,
                Location = new Point(15, 75),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { lblT, lblV, lblSub });
            return card;
        }

        private void ShowDashboardView()
        {
            BuildDashboardContent();
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                decimal kRev = _karaokeBus.GetTotalRevenue();
                int activeRooms = _karaokeBus.GetOccupiedRoomCount();
                decimal gRev = _garageBus.GetTotalRevenue();
                int tasks = _projectBus.GetTotalTaskCount();

                if (lblKpiKaraoke != null) lblKpiKaraoke.Text = $"{kRev:N0} VNĐ";
                if (lblKpiRooms != null) lblKpiRooms.Text = $"{activeRooms} Phòng";
                if (lblKpiGarage != null) lblKpiGarage.Text = $"{gRev:N0} VNĐ";
                if (lblKpiTasks != null) lblKpiTasks.Text = $"{tasks} Task";
            }
            catch (Exception ex)
            {
                if (lblSystemStatus != null)
                {
                    lblSystemStatus.Text = $"⚠️ Chưa kết nối CSDL: {ex.Message}";
                    lblSystemStatus.ForeColor = UITheme.Warning;
                }
            }
        }

        private void OpenForm(Form frm)
        {
            frm.ShowDialog(this);
            LoadDashboardStats();
        }
    }
}

