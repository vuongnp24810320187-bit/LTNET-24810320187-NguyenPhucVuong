using System;
using System.Drawing;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.BUS;

namespace MultiServiceEnterpriseSystem.GUI
{
    public class frmSecurityDemo : Form
    {
        private readonly SecurityAuditBUS _securityBus = new();

        // SQL Injection Controls
        private TextBox txtPayload = null!;
        private Button btnRunUnsafe = null!;
        private Button btnRunSafe = null!;
        private Label lblQueryPreview = null!;
        private DataGridView dgvInjectionResult = null!;
        private Label lblExplanation = null!;

        // Transaction Rollback Controls
        private CheckBox chkForceError = null!;
        private Button btnTestTransaction = null!;
        private RichTextBox rtbAuditLog = null!;

        public frmSecurityDemo()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "MINH HỌA AN TOÀN DỮ LIỆU: PHÒNG CHỐNG SQL INJECTION & TRANSACTION ROLLBACK";
            this.Size = new Size(1150, 740);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.FontRegular;

            var tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.FontBold,
                Padding = new Point(15, 8)
            };

            // TAB 1: PHÒNG CHỐNG SQL INJECTION
            var tabInjection = new TabPage("🛡️ Kiểm Thử 1: Phòng Chống SQL Injection")
            {
                BackColor = UITheme.Background,
                Padding = new Padding(15)
            };
            BuildInjectionUI(tabInjection);
            tabMain.TabPages.Add(tabInjection);

            // TAB 2: TRANSACTION ROLLBACK
            var tabRollback = new TabPage("🔄 Kiểm Thử 2: Báo Lỗi & Transaction Rollback (ACID)")
            {
                BackColor = UITheme.Background,
                Padding = new Padding(15)
            };
            BuildRollbackUI(tabRollback);
            tabMain.TabPages.Add(tabRollback);

            this.Controls.Add(tabMain);
        }

        private void BuildInjectionUI(TabPage tab)
        {
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 175,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblPrompt = new Label
            {
                Text = "Nhập chuỗi tìm kiếm hoặc Payload tấn công SQL Injection:",
                Font = UITheme.FontSmall,
                Location = new Point(15, 15),
                AutoSize = true
            };

            txtPayload = new TextBox
            {
                Text = "' OR '1'='1' --",
                Location = new Point(15, 38),
                Width = 520,
                Font = new Font("Consolas", 10.5F, FontStyle.Regular)
            };

            var lblQuickSelect = new Label
            {
                Text = "Payload mẫu:",
                Font = UITheme.FontSmall,
                Location = new Point(550, 15),
                AutoSize = true
            };

            var cboSamples = new ComboBox
            {
                Location = new Point(550, 38),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboSamples.Items.AddRange(new object[]
            {
                "' OR '1'='1' -- (Bypass toàn bộ WHERE)",
                "VIP' -- (Cắt ngắn câu lệnh)",
                "' UNION SELECT 1, 'Hacked', 'Hacked', 999999, 'Hacked' --",
                "Phòng VIP 01 (Chuỗi bình thường)"
            });
            cboSamples.SelectedIndex = 0;
            cboSamples.SelectedIndexChanged += (_, _) =>
            {
                string sel = cboSamples.SelectedItem?.ToString() ?? "";
                if (sel.Contains(" ("))
                {
                    txtPayload.Text = sel.Substring(0, sel.IndexOf(" ("));
                }
            };

            btnRunUnsafe = UITheme.CreateButton("❌ Chạy Không An Toàn (Ghép chuỗi SQL)", UITheme.Danger, Color.White, 310, 40);
            btnRunUnsafe.Location = new Point(15, 80);
            btnRunUnsafe.Click += (_, _) => RunUnsafeInjection();

            btnRunSafe = UITheme.CreateButton("🛡️ Chạy An Toàn (SqlParameter ADO.NET)", UITheme.Success, Color.White, 310, 40);
            btnRunSafe.Location = new Point(340, 80);
            btnRunSafe.Click += (_, _) => RunSafeInjection();

            lblQueryPreview = new Label
            {
                Text = "Câu lệnh SQL được thực thi: (Chưa thực thi)",
                Font = new Font("Consolas", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryDark,
                Location = new Point(15, 135),
                AutoSize = true
            };

            pnlTop.Controls.AddRange(new Control[] { lblPrompt, txtPayload, lblQuickSelect, cboSamples, btnRunUnsafe, btnRunSafe, lblQueryPreview });
            tab.Controls.Add(pnlTop);

            lblExplanation = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.FromArgb(254, 243, 199),
                ForeColor = Color.FromArgb(146, 64, 14),
                Padding = new Padding(12),
                Font = UITheme.FontSmall,
                Text = "💡 GIẢI THÍCH KỸ THUẬT: Khi sử dụng SqlParameter, SQL Server coi toàn bộ chuỗi nhập vào là một Literal Value (giá trị hằng) thay vì phân tích cú pháp mã lệnh thi hành. Do đó, các ký tự đặc biệt như dấu nháy đơn (') hoặc comment (--) hoàn toàn bị vô hiệu hóa, bảo vệ 100% CSDL."
            };
            tab.Controls.Add(lblExplanation);

            dgvInjectionResult = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvInjectionResult);
            tab.Controls.Add(dgvInjectionResult);
            pnlTop.BringToFront();
            lblExplanation.BringToFront();
        }

        private void BuildRollbackUI(TabPage tab)
        {
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            chkForceError = new CheckBox
            {
                Text = "⚠️ Cố ý kích hoạt lỗi ngoại lệ (Giả lập gọi món với số lượng vượt quá tồn kho khả dụng)",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = UITheme.FontBold,
                ForeColor = UITheme.Danger,
                Checked = true
            };

            btnTestTransaction = UITheme.CreateButton("⚡ Khởi Chạy Giao Dịch Kiểm Thử ACID (SqlTransaction)", UITheme.Primary, Color.White, 420, 42);
            btnTestTransaction.Location = new Point(20, 52);
            btnTestTransaction.Click += (_, _) => RunTransactionTest();

            pnlTop.Controls.AddRange(new Control[] { chkForceError, btnTestTransaction });
            tab.Controls.Add(pnlTop);

            var pnlLog = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            var lblLogHeader = new Label
            {
                Text = "NHẬT KÝ KIỂM TOÁN GIAO DỊCH REAL-TIME (TRANSACTION AUDIT LOG):",
                Dock = DockStyle.Top,
                Height = 30,
                Font = UITheme.FontBold
            };

            rtbAuditLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10.5F, FontStyle.Regular),
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(52, 211, 153),
                ReadOnly = true
            };

            pnlLog.Controls.AddRange(new Control[] { rtbAuditLog, lblLogHeader });
            tab.Controls.Add(pnlLog);
            pnlTop.BringToFront();
        }

        private void RunUnsafeInjection()
        {
            try
            {
                var (table, sql) = _securityBus.TestUnsafeSql(txtPayload.Text);
                lblQueryPreview.Text = $"[NGUY HIỂM] SQL: {sql}";
                lblQueryPreview.ForeColor = UITheme.Danger;
                dgvInjectionResult.DataSource = table;
                MessageBox.Show($"CẢNH BÁO LỖ HỔNG:\nCâu lệnh ghép chuỗi đã bị tiêm mã độc!\nKết quả trả về {table.Rows.Count} dòng dữ liệu (bao gồm cả dữ liệu không được phép xem).", "Lỗ hổng SQL Injection phát hiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblQueryPreview.Text = $"[LỖI CÚ PHÁP SQL]: {ex.Message}";
                MessageBox.Show($"Lỗi thực thi SQL Injection: {ex.Message}", "Lỗi cú pháp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunSafeInjection()
        {
            try
            {
                var (table, displaySql) = _securityBus.TestSafeSql(txtPayload.Text);
                lblQueryPreview.Text = $"[AN TOÀN] {displaySql}";
                lblQueryPreview.ForeColor = UITheme.Success;
                dgvInjectionResult.DataSource = table;
                MessageBox.Show($"AN TOÀN TUYỆT ĐỐI:\nSử dụng SqlParameter, hệ thống tìm kiếm chính xác giá trị chuỗi nhập vào.\nKết quả trả về {table.Rows.Count} dòng (không bị bypass câu lệnh WHERE).", "Phòng vệ thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunTransactionTest()
        {
            bool forceError = chkForceError.Checked;
            var (success, log) = _securityBus.TestRollbackTransaction(forceError);

            rtbAuditLog.Text = log;
            if (!success)
            {
                MessageBox.Show("GIAO DỊCH ĐÃ ĐƯỢC ROLLBACK THÀNH CÔNG!\n\nKhối catch đã phát hiện ngoại lệ và thực thi lệnh transaction.Rollback(). Toàn bộ thay đổi tạm thời trên SQL Server đã được hủy bỏ sạch sẽ, đảm bảo tính toàn vẹn ACID.", "Kiểm thử Transaction Rollback thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Giao dịch kiểm thử hoàn tất thành công (Commit) mà không có lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

