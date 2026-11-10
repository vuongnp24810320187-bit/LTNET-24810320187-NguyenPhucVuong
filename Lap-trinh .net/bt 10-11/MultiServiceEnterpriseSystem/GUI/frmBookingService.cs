using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.BUS;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.GUI
{
    public class frmBookingService : Form
    {
        private readonly KaraokeBUS _karaokeBus = new();
        private readonly GarageBUS _garageBus = new();

        private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };

        // Phân hệ Karaoke
        private FlowLayoutPanel flowRooms = null!;
        private Label lblSelectedRoom = null!;
        private Label lblRoomRate = null!;
        private Label lblCheckInTime = null!;
        private Label lblElapsedTime = null!;
        private Label lblRoomStatus = null!;
        private Button btnOpenRoom = null!;
        private Button btnPayRoom = null!;

        private ComboBox cboServices = null!;
        private NumericUpDown numQuantity = null!;
        private Button btnAddService = null!;
        private DataGridView dgvOrderDetails = null!;

        private PhongHatDTO? _currentRoom;
        private List<DichVuDTO> _availableServices = [];

        // Phân hệ Garage
        private ComboBox cboVehicles = null!;
        private TextBox txtRepairContent = null!;
        private ComboBox cboParts = null!;
        private NumericUpDown numPartQty = null!;
        private TextBox txtLaborCost = null!;
        private Button btnAddPartToCart = null!;
        private Button btnCreateRepairBill = null!;
        private DataGridView dgvRepairCart = null!;
        private Label lblGarageTotal = null!;
        private readonly List<ChiTietSuaChuaDTO> _garageCart = [];

        public frmBookingService()
        {
            InitializeComponent();
            _timer.Tick += (_, _) => UpdateElapsedTime();
            _timer.Start();
            LoadKaraokeData();
            LoadGarageData();
        }

        private void InitializeComponent()
        {
            this.Text = "THỰC THI NGHIỆP VỤ CHÍNH: ĐẶT PHÒNG HÁT & LẬP HÓA ĐƠN DỊCH VỤ";
            this.Size = new Size(1250, 780);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.FontRegular;

            var tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.FontBold,
                Padding = new Point(15, 8)
            };

            // TAB 1: ĐẶT PHÒNG KARAOKE & ORDER DỊCH VỤ
            var tabKaraoke = new TabPage("🎤 Nghiệp vụ 1: Đặt phòng Karaoke & Order Dịch vụ")
            {
                BackColor = UITheme.Background,
                Padding = new Padding(12)
            };
            BuildKaraokeUI(tabKaraoke);
            tabMain.TabPages.Add(tabKaraoke);

            // TAB 2: LẬP PHIẾU BẢO DƯỠNG & HÓA ĐƠN GARAGE
            var tabGarage = new TabPage("🚗 Nghiệp vụ 2: Tiếp nhận Xe & Lập Hóa đơn Garage")
            {
                BackColor = UITheme.Background,
                Padding = new Padding(12)
            };
            BuildGarageUI(tabGarage);
            tabMain.TabPages.Add(tabGarage);

            this.Controls.Add(tabMain);
        }

        private void BuildKaraokeUI(TabPage tab)
        {
            // Trái: Sơ đồ phòng hát (Width: 460)
            var pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 460,
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            var lblTitleLeft = new Label
            {
                Text = "SƠ ĐỒ PHÒNG HÁT THEO THỜI GIAN THỰC",
                Font = UITheme.FontBold,
                ForeColor = UITheme.PrimaryDark,
                Dock = DockStyle.Top,
                Height = 30
            };
            var lblLegend = new Label
            {
                Text = "🟢 Xanh lá: Phòng Trống    |    🔴 Đỏ cam: Đang có khách hát",
                Font = UITheme.FontSmall,
                ForeColor = UITheme.TextMuted,
                Dock = DockStyle.Top,
                Height = 25
            };
            flowRooms = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(5)
            };
            pnlLeft.Controls.AddRange(new Control[] { flowRooms, lblLegend, lblTitleLeft });
            tab.Controls.Add(pnlLeft);

            // Phải: Chi tiết phòng và gọi món (Dock Fill)
            var pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UITheme.Background,
                Padding = new Padding(15, 0, 0, 0)
            };

            // 1. Box thông tin phòng đang chọn
            var grpRoomInfo = new GroupBox
            {
                Text = "THÔNG TIN PHÒNG HÁT ĐANG CHỌN",
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.White,
                Font = UITheme.FontBold,
                Padding = new Padding(15)
            };

            lblSelectedRoom = new Label { Text = "Phòng: [Chưa chọn]", Location = new Point(20, 30), AutoSize = true, Font = UITheme.FontSubheading, ForeColor = UITheme.Primary };
            lblRoomRate = new Label { Text = "Đơn giá: 0 đ/giờ", Location = new Point(20, 65), AutoSize = true, Font = UITheme.FontRegular };
            lblRoomStatus = new Label { Text = "Trạng thái: Trống", Location = new Point(20, 95), AutoSize = true, Font = UITheme.FontBold };

            lblCheckInTime = new Label { Text = "Giờ vào: --:--:--", Location = new Point(280, 35), AutoSize = true, Font = UITheme.FontRegular };
            lblElapsedTime = new Label { Text = "Thời gian đã hát: 00:00:00", Location = new Point(280, 65), AutoSize = true, Font = UITheme.FontBold, ForeColor = UITheme.Danger };

            btnOpenRoom = UITheme.CreateButton("🟢 Mở Phòng Hát", UITheme.Success, Color.White, 150, 42);
            btnOpenRoom.Location = new Point(540, 28);
            btnOpenRoom.Click += (_, _) => OpenSelectedRoom();

            btnPayRoom = UITheme.CreateButton("💳 Thanh Toán & Trả", UITheme.Danger, Color.White, 150, 42);
            btnPayRoom.Location = new Point(540, 80);
            btnPayRoom.Click += (_, _) => CheckoutSelectedRoom();

            grpRoomInfo.Controls.AddRange(new Control[] { lblSelectedRoom, lblRoomRate, lblRoomStatus, lblCheckInTime, lblElapsedTime, btnOpenRoom, btnPayRoom });
            pnlRight.Controls.Add(grpRoomInfo);

            // 2. Box Gọi dịch vụ (Order món)
            var grpServices = new GroupBox
            {
                Text = "GỌI DỊCH VỤ / ĐỒ ĂN & NƯỚC UỐNG (SQLTRANSACTION AN TOÀN ACID)",
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.White,
                Font = UITheme.FontBold,
                Padding = new Padding(15)
            };

            var lblS1 = new Label { Text = "Chọn món / Nước uống:", Location = new Point(20, 25), AutoSize = true, Font = UITheme.FontSmall };
            cboServices = new ComboBox { Location = new Point(20, 48), Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblS2 = new Label { Text = "Số lượng:", Location = new Point(360, 25), AutoSize = true, Font = UITheme.FontSmall };
            numQuantity = new NumericUpDown { Location = new Point(360, 48), Width = 90, Minimum = 1, Maximum = 999, Value = 1 };

            btnAddService = UITheme.CreateButton("➕ Gọi Dịch Vụ", UITheme.Primary, Color.White, 140, 36);
            btnAddService.Location = new Point(470, 42);
            btnAddService.Click += (_, _) => OrderServiceForRoom();

            grpServices.Controls.AddRange(new Control[] { lblS1, cboServices, lblS2, numQuantity, btnAddService });
            pnlRight.Controls.Add(grpServices);

            // 3. DataGridView Chi tiết dịch vụ đã gọi
            var pnlGridOrder = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            var lblGridTitle = new Label { Text = "DANH SÁCH MÓN ĂN & NƯỚC UỐNG ĐÃ GỌI CỦA PHÒNG", Dock = DockStyle.Top, Height = 25, Font = UITheme.FontBold };
            dgvOrderDetails = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvOrderDetails);
            pnlGridOrder.Controls.AddRange(new Control[] { dgvOrderDetails, lblGridTitle });
            pnlRight.Controls.Add(pnlGridOrder);

            tab.Controls.Add(pnlRight);
            grpRoomInfo.BringToFront();
            grpServices.BringToFront();
        }

        private void BuildGarageUI(TabPage tab)
        {
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 180,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblV = new Label { Text = "Chọn xe tiếp nhận:", Location = new Point(15, 15), AutoSize = true, Font = UITheme.FontSmall };
            cboVehicles = new ComboBox { Location = new Point(15, 38), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblC = new Label { Text = "Nội dung bảo dưỡng / Sửa chữa:", Location = new Point(295, 15), AutoSize = true, Font = UITheme.FontSmall };
            txtRepairContent = new TextBox { Location = new Point(295, 38), Width = 450, Text = "Bảo dưỡng định kỳ, thay dầu máy và lọc nhớt" };

            var lblP = new Label { Text = "Phụ tùng thay thế:", Location = new Point(15, 80), AutoSize = true, Font = UITheme.FontSmall };
            cboParts = new ComboBox { Location = new Point(15, 102), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblQ = new Label { Text = "Số lượng:", Location = new Point(295, 80), AutoSize = true, Font = UITheme.FontSmall };
            numPartQty = new NumericUpDown { Location = new Point(295, 102), Width = 80, Minimum = 1, Maximum = 100, Value = 1 };

            var lblLabor = new Label { Text = "Tiền công thợ (VNĐ):", Location = new Point(395, 80), AutoSize = true, Font = UITheme.FontSmall };
            txtLaborCost = new TextBox { Location = new Point(395, 102), Width = 150, Text = "150000" };

            btnAddPartToCart = UITheme.CreateButton("➕ Thêm Hạng Mục", UITheme.Primary, Color.White, 150, 36);
            btnAddPartToCart.Location = new Point(565, 96);
            btnAddPartToCart.Click += (_, _) => AddPartToRepairCart();

            pnlTop.Controls.AddRange(new Control[] { lblV, cboVehicles, lblC, txtRepairContent, lblP, cboParts, lblQ, numPartQty, lblLabor, txtLaborCost, btnAddPartToCart });
            tab.Controls.Add(pnlTop);

            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(20, 15, 20, 15)
            };

            lblGarageTotal = new Label
            {
                Text = "TỔNG TIỀN HÓA ĐƠN: 0 VNĐ",
                Font = UITheme.FontSubheading,
                ForeColor = UITheme.Danger,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            btnCreateRepairBill = UITheme.CreateButton("💾 Lập Phiếu & Xuất Xưởng (SqlTransaction)", UITheme.Success, Color.White, 320, 42);
            btnCreateRepairBill.Dock = DockStyle.Right;
            btnCreateRepairBill.Click += (_, _) => CreateGarageBill();

            pnlBottom.Controls.AddRange(new Control[] { lblGarageTotal, btnCreateRepairBill });
            tab.Controls.Add(pnlBottom);

            dgvRepairCart = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvRepairCart);
            tab.Controls.Add(dgvRepairCart);
            pnlTop.BringToFront();
            pnlBottom.BringToFront();
        }

        // ====================================================================
        // LOGIC XỬ LÝ PHÂN HỆ KARAOKE
        // ====================================================================
        private void LoadKaraokeData()
        {
            try
            {
                flowRooms.Controls.Clear();
                var rooms = _karaokeBus.GetAllRooms();
                _availableServices = _karaokeBus.GetAllServices();

                cboServices.DataSource = new List<DichVuDTO>(_availableServices);

                foreach (var room in rooms)
                {
                    var btn = new Button
                    {
                        Text = $"{room.TenPhong}\n({room.LoaiPhong})\n{room.GiaTheoGio:N0} đ/h\n[{room.TrangThai}]",
                        Width = 135,
                        Height = 110,
                        Margin = new Padding(6),
                        FlatStyle = FlatStyle.Flat,
                        Font = UITheme.FontBold,
                        ForeColor = Color.White,
                        Tag = room,
                        Cursor = Cursors.Hand
                    };
                    btn.FlatAppearance.BorderSize = 0;

                    if (room.TrangThai == "Trống")
                    {
                        btn.BackColor = UITheme.Success;
                    }
                    else
                    {
                        btn.BackColor = UITheme.Danger;
                    }

                    btn.Click += RoomButton_Click;
                    flowRooms.Controls.Add(btn);
                }

                if (_currentRoom != null)
                {
                    var reselected = rooms.FirstOrDefault(r => r.MaPhong == _currentRoom.MaPhong);
                    SelectRoom(reselected ?? rooms.FirstOrDefault());
                }
                else if (rooms.Count > 0)
                {
                    SelectRoom(rooms[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp dữ liệu Karaoke: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RoomButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is PhongHatDTO room)
            {
                SelectRoom(room);
            }
        }

        private void SelectRoom(PhongHatDTO? room)
        {
            _currentRoom = room;
            if (room == null) return;

            lblSelectedRoom.Text = $"Phòng: {room.TenPhong} ({room.LoaiPhong})";
            lblRoomRate.Text = $"Đơn giá: {room.GiaTheoGio:N0} đ/giờ";
            lblRoomStatus.Text = $"Trạng thái: {room.TrangThai}";
            lblRoomStatus.ForeColor = room.TrangThai == "Trống" ? UITheme.Success : UITheme.Danger;

            if (room.TrangThai == "Đang hát" && room.GioVao.HasValue)
            {
                lblCheckInTime.Text = $"Giờ vào: {room.GioVao.Value:HH:mm:ss (dd/MM)}";
                btnOpenRoom.Enabled = false;
                btnPayRoom.Enabled = true;
                btnAddService.Enabled = true;

                if (room.MaHD.HasValue)
                {
                    dgvOrderDetails.DataSource = _karaokeBus.GetInvoiceServices(room.MaHD.Value);
                }
            }
            else
            {
                lblCheckInTime.Text = "Giờ vào: --:--:--";
                lblElapsedTime.Text = "Thời gian đã hát: 00:00:00";
                btnOpenRoom.Enabled = true;
                btnPayRoom.Enabled = false;
                btnAddService.Enabled = false;
                dgvOrderDetails.DataSource = null;
            }
        }

        private void UpdateElapsedTime()
        {
            if (_currentRoom != null && _currentRoom.TrangThai == "Đang hát" && _currentRoom.GioVao.HasValue)
            {
                var elapsed = DateTime.Now - _currentRoom.GioVao.Value;
                lblElapsedTime.Text = $"Thời gian đã hát: {elapsed:hh\\:mm\\:ss}";
            }
        }

        private void OpenSelectedRoom()
        {
            if (_currentRoom == null) return;
            try
            {
                int invoiceId = _karaokeBus.OpenRoom(_currentRoom.MaPhong);
                MessageBox.Show($"Mở phòng '{_currentRoom.TenPhong}' thành công!\nMã hóa đơn tạo mới: #{invoiceId}", "Giao dịch thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKaraokeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi mở phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OrderServiceForRoom()
        {
            if (_currentRoom == null || !_currentRoom.MaHD.HasValue)
            {
                MessageBox.Show("Vui lòng mở phòng trước khi gọi dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboServices.SelectedItem is not DichVuDTO selectedService) return;

            try
            {
                int qty = (int)numQuantity.Value;
                _karaokeBus.OrderService(_currentRoom.MaHD.Value, selectedService.MaDV, qty, selectedService.DonGia);
                MessageBox.Show($"Đã gọi {qty} x {selectedService.TenDV} cho {_currentRoom.TenPhong}!", "Gọi món thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvOrderDetails.DataSource = _karaokeBus.GetInvoiceServices(_currentRoom.MaHD.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đặt dịch vụ: {ex.Message}", "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckoutSelectedRoom()
        {
            if (_currentRoom == null || !_currentRoom.MaHD.HasValue) return;

            var confirm = MessageBox.Show($"Xác nhận thanh toán và trả phòng cho '{_currentRoom.TenPhong}'?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = _karaokeBus.CheckoutRoom(_currentRoom.MaPhong, _currentRoom.MaHD.Value);
                string billDetail = 
                    $"==========================================\n" +
                    $"        HÓA ĐƠN THANH TOÁN KARAOKE        \n" +
                    $"==========================================\n" +
                    $"Phòng hát: {_currentRoom.TenPhong} ({_currentRoom.LoaiPhong})\n" +
                    $"Thời gian hát: {res.hours} giờ\n" +
                    $"Tiền phòng: {res.roomAmount:N0} VNĐ\n" +
                    $"Tiền dịch vụ đồ ăn/uống: {res.serviceAmount:N0} VNĐ\n" +
                    $"------------------------------------------\n" +
                    $"TỔNG THANH TOÁN: {res.totalAmount:N0} VNĐ\n" +
                    $"==========================================\n" +
                    $"(Giao dịch đã được ghi nhận an toàn bằng SqlTransaction)";

                MessageBox.Show(billDetail, "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKaraokeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thanh toán: {ex.Message}", "Lỗi Transaction Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================================================================
        // LOGIC XỬ LÝ PHÂN HỆ GARAGE
        // ====================================================================
        private void LoadGarageData()
        {
            try
            {
                cboVehicles.DataSource = _garageBus.GetAllVehicles();
                cboParts.DataSource = _garageBus.GetAllParts();
                RefreshGarageCartGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp dữ liệu Garage: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddPartToRepairCart()
        {
            if (cboParts.SelectedItem is not PhuTungDTO pt) return;
            if (!decimal.TryParse(txtLaborCost.Text.Replace(",", "").Replace(".", ""), out decimal labor))
            {
                labor = 0;
            }

            int qty = (int)numPartQty.Value;
            var item = new ChiTietSuaChuaDTO
            {
                MaPhuTung = pt.MaPhuTung,
                TenPhuTung = pt.TenPhuTung,
                SoLuong = qty,
                DonGia = pt.DonGia,
                TienCong = labor
            };
            _garageCart.Add(item);
            RefreshGarageCartGrid();
        }

        private void RefreshGarageCartGrid()
        {
            dgvRepairCart.DataSource = null;
            dgvRepairCart.DataSource = new List<ChiTietSuaChuaDTO>(_garageCart);
            decimal total = _garageCart.Sum(x => x.ThanhTien);
            lblGarageTotal.Text = $"TỔNG TIỀN HÓA ĐƠN: {total:N0} VNĐ";
        }

        private void CreateGarageBill()
        {
            if (cboVehicles.SelectedItem is not XeDTO xe)
            {
                MessageBox.Show("Vui lòng chọn xe tiếp nhận bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_garageCart.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 phụ tùng hoặc tiền công sửa chữa vào giỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maPhieu = _garageBus.CreateRepairOrder(xe.BienSo, txtRepairContent.Text.Trim(), _garageCart);
                MessageBox.Show($"Lập hóa đơn và xuất xưởng thành công cho xe {xe.BienSo}!\nMã phiếu sửa chữa: #{maPhieu}\n(Tồn kho phụ tùng đã được trừ tự động với SqlTransaction)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _garageCart.Clear();
                RefreshGarageCartGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi giao dịch: {ex.Message}", "Transaction Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

