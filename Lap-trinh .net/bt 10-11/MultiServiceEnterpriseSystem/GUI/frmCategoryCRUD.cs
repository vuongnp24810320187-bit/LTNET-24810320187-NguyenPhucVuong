using System;
using System.Drawing;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.BUS;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.GUI
{
    public class frmCategoryCRUD : Form
    {
        private readonly KaraokeBUS _karaokeBus = new();
        private readonly GarageBUS _garageBus = new();

        private TabControl tabCategories = null!;

        // Tab 1: Phòng hát
        private DataGridView dgvRooms = null!;
        private TextBox txtRoomId = null!, txtRoomName = null!, txtHourlyRate = null!;
        private ComboBox cboRoomType = null!, cboRoomStatus = null!;

        // Tab 2: Dịch vụ Karaoke
        private DataGridView dgvServices = null!;
        private TextBox txtServiceId = null!, txtServiceName = null!, txtServicePrice = null!, txtServiceStock = null!;

        // Tab 3: Phụ tùng Garage
        private DataGridView dgvParts = null!;
        private TextBox txtPartId = null!, txtPartName = null!, txtPartPrice = null!, txtPartStock = null!;

        public frmCategoryCRUD()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "QUẢN LÝ DANH MỤC HỆ THỐNG (CRUD)";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = UITheme.Background;
            this.Font = UITheme.FontRegular;

            // Header Banner
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ DANH MỤC DỊCH VỤ & HÀNG HÓA (CRUD)",
                Font = UITheme.FontSubheading,
                ForeColor = UITheme.Primary,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // Tab Control
            tabCategories = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = UITheme.FontBold,
                Padding = new Point(15, 8)
            };

            // TAB 1: PHÒNG HÁT
            var tabRooms = new TabPage("🎤 Danh mục Phòng hát") { BackColor = UITheme.Background, Padding = new Padding(15) };
            BuildRoomsTab(tabRooms);
            tabCategories.TabPages.Add(tabRooms);

            // TAB 2: DỊCH VỤ KARAOKE
            var tabServices = new TabPage("🍹 Danh mục Dịch vụ Đồ ăn & Uống") { BackColor = UITheme.Background, Padding = new Padding(15) };
            BuildServicesTab(tabServices);
            tabCategories.TabPages.Add(tabServices);

            // TAB 3: PHỤ TÙNG GARAGE
            var tabParts = new TabPage("⚙️ Danh mục Phụ tùng Garage") { BackColor = UITheme.Background, Padding = new Padding(15) };
            BuildPartsTab(tabParts);
            tabCategories.TabPages.Add(tabParts);

            this.Controls.Add(tabCategories);
            pnlHeader.BringToFront();
        }

        private void BuildRoomsTab(TabPage tab)
        {
            var pnlForm = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lbl1 = new Label { Text = "Mã phòng:", Location = new Point(15, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtRoomId = new TextBox { Location = new Point(15, 42), Width = 90, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };

            var lbl2 = new Label { Text = "Tên phòng hát (*):", Location = new Point(125, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtRoomName = new TextBox { Location = new Point(125, 42), Width = 200 };

            var lbl3 = new Label { Text = "Loại phòng:", Location = new Point(345, 20), AutoSize = true, Font = UITheme.FontSmall };
            cboRoomType = new ComboBox { Location = new Point(345, 42), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            cboRoomType.Items.AddRange(new object[] { "Thường", "VIP", "Party Lớn" });
            cboRoomType.SelectedIndex = 0;

            var lbl4 = new Label { Text = "Giá theo giờ (VNĐ):", Location = new Point(505, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtHourlyRate = new TextBox { Location = new Point(505, 42), Width = 150, Text = "150000" };

            var lbl5 = new Label { Text = "Trạng thái:", Location = new Point(675, 20), AutoSize = true, Font = UITheme.FontSmall };
            cboRoomStatus = new ComboBox { Location = new Point(675, 42), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
            cboRoomStatus.Items.AddRange(new object[] { "Trống", "Đang hát" });
            cboRoomStatus.SelectedIndex = 0;

            // Buttons
            var btnAdd = UITheme.CreateButton("➕ Thêm mới", UITheme.Success, Color.White, 110);
            btnAdd.Location = new Point(15, 95);
            btnAdd.Click += (_, _) => AddRoom();

            var btnEdit = UITheme.CreateButton("✏️ Cập nhật", UITheme.Primary, Color.White, 110);
            btnEdit.Location = new Point(135, 95);
            btnEdit.Click += (_, _) => UpdateRoom();

            var btnDelete = UITheme.CreateButton("🗑️ Xóa bỏ", UITheme.Danger, Color.White, 110);
            btnDelete.Location = new Point(255, 95);
            btnDelete.Click += (_, _) => DeleteRoom();

            var btnClear = UITheme.CreateButton("🔄 Làm mới", UITheme.TextMuted, Color.White, 110);
            btnClear.Location = new Point(375, 95);
            btnClear.Click += (_, _) => ClearRoomForm();

            pnlForm.Controls.AddRange(new Control[] { lbl1, txtRoomId, lbl2, txtRoomName, lbl3, cboRoomType, lbl4, txtHourlyRate, lbl5, cboRoomStatus, btnAdd, btnEdit, btnDelete, btnClear });
            tab.Controls.Add(pnlForm);

            // DataGridView
            dgvRooms = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvRooms);
            dgvRooms.CellClick += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvRooms.Rows[e.RowIndex].DataBoundItem is PhongHatDTO room)
                {
                    txtRoomId.Text = room.MaPhong.ToString();
                    txtRoomName.Text = room.TenPhong;
                    cboRoomType.SelectedItem = room.LoaiPhong;
                    txtHourlyRate.Text = room.GiaTheoGio.ToString("N0");
                    cboRoomStatus.SelectedItem = room.TrangThai;
                }
            };

            var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 15, 0, 0) };
            pnlGrid.Controls.Add(dgvRooms);
            tab.Controls.Add(pnlGrid);
            pnlForm.BringToFront();
        }

        private void BuildServicesTab(TabPage tab)
        {
            var pnlForm = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lbl1 = new Label { Text = "Mã DV:", Location = new Point(15, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtServiceId = new TextBox { Location = new Point(15, 42), Width = 90, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };

            var lbl2 = new Label { Text = "Tên dịch vụ / Món ăn (*):", Location = new Point(125, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtServiceName = new TextBox { Location = new Point(125, 42), Width = 260 };

            var lbl3 = new Label { Text = "Đơn giá (VNĐ):", Location = new Point(405, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtServicePrice = new TextBox { Location = new Point(405, 42), Width = 150, Text = "25000" };

            var lbl4 = new Label { Text = "Tồn kho:", Location = new Point(575, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtServiceStock = new TextBox { Location = new Point(575, 42), Width = 120, Text = "100" };

            var btnAdd = UITheme.CreateButton("➕ Thêm mới", UITheme.Success, Color.White, 110);
            btnAdd.Location = new Point(15, 95);
            btnAdd.Click += (_, _) => AddService();

            var btnEdit = UITheme.CreateButton("✏️ Cập nhật", UITheme.Primary, Color.White, 110);
            btnEdit.Location = new Point(135, 95);
            btnEdit.Click += (_, _) => UpdateService();

            var btnDelete = UITheme.CreateButton("🗑️ Xóa bỏ", UITheme.Danger, Color.White, 110);
            btnDelete.Location = new Point(255, 95);
            btnDelete.Click += (_, _) => DeleteService();

            var btnClear = UITheme.CreateButton("🔄 Làm mới", UITheme.TextMuted, Color.White, 110);
            btnClear.Location = new Point(375, 95);
            btnClear.Click += (_, _) => ClearServiceForm();

            pnlForm.Controls.AddRange(new Control[] { lbl1, txtServiceId, lbl2, txtServiceName, lbl3, txtServicePrice, lbl4, txtServiceStock, btnAdd, btnEdit, btnDelete, btnClear });
            tab.Controls.Add(pnlForm);

            dgvServices = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvServices);
            dgvServices.CellClick += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvServices.Rows[e.RowIndex].DataBoundItem is DichVuDTO dv)
                {
                    txtServiceId.Text = dv.MaDV.ToString();
                    txtServiceName.Text = dv.TenDV;
                    txtServicePrice.Text = dv.DonGia.ToString("N0");
                    txtServiceStock.Text = dv.TonKho.ToString();
                }
            };

            var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 15, 0, 0) };
            pnlGrid.Controls.Add(dgvServices);
            tab.Controls.Add(pnlGrid);
            pnlForm.BringToFront();
        }

        private void BuildPartsTab(TabPage tab)
        {
            var pnlForm = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lbl1 = new Label { Text = "Mã PT:", Location = new Point(15, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtPartId = new TextBox { Location = new Point(15, 42), Width = 90, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };

            var lbl2 = new Label { Text = "Tên phụ tùng xe (*):", Location = new Point(125, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtPartName = new TextBox { Location = new Point(125, 42), Width = 260 };

            var lbl3 = new Label { Text = "Đơn giá (VNĐ):", Location = new Point(405, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtPartPrice = new TextBox { Location = new Point(405, 42), Width = 150, Text = "500000" };

            var lbl4 = new Label { Text = "Số lượng tồn kho:", Location = new Point(575, 20), AutoSize = true, Font = UITheme.FontSmall };
            txtPartStock = new TextBox { Location = new Point(575, 42), Width = 140, Text = "50" };

            var btnAdd = UITheme.CreateButton("➕ Thêm mới", UITheme.Success, Color.White, 110);
            btnAdd.Location = new Point(15, 95);
            btnAdd.Click += (_, _) => AddPart();

            var btnEdit = UITheme.CreateButton("✏️ Cập nhật", UITheme.Primary, Color.White, 110);
            btnEdit.Location = new Point(135, 95);
            btnEdit.Click += (_, _) => UpdatePart();

            var btnDelete = UITheme.CreateButton("🗑️ Xóa bỏ", UITheme.Danger, Color.White, 110);
            btnDelete.Location = new Point(255, 95);
            btnDelete.Click += (_, _) => DeletePart();

            var btnClear = UITheme.CreateButton("🔄 Làm mới", UITheme.TextMuted, Color.White, 110);
            btnClear.Location = new Point(375, 95);
            btnClear.Click += (_, _) => ClearPartForm();

            pnlForm.Controls.AddRange(new Control[] { lbl1, txtPartId, lbl2, txtPartName, lbl3, txtPartPrice, lbl4, txtPartStock, btnAdd, btnEdit, btnDelete, btnClear });
            tab.Controls.Add(pnlForm);

            dgvParts = new DataGridView { Dock = DockStyle.Fill };
            UITheme.StyleGridView(dgvParts);
            dgvParts.CellClick += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvParts.Rows[e.RowIndex].DataBoundItem is PhuTungDTO pt)
                {
                    txtPartId.Text = pt.MaPhuTung.ToString();
                    txtPartName.Text = pt.TenPhuTung;
                    txtPartPrice.Text = pt.DonGia.ToString("N0");
                    txtPartStock.Text = pt.SoLuongTon.ToString();
                }
            };

            var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 15, 0, 0) };
            pnlGrid.Controls.Add(dgvParts);
            tab.Controls.Add(pnlGrid);
            pnlForm.BringToFront();
        }

        private void LoadData()
        {
            try
            {
                dgvRooms.DataSource = _karaokeBus.GetAllRooms();
                dgvServices.DataSource = _karaokeBus.GetAllServices();
                dgvParts.DataSource = _garageBus.GetAllParts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Room CRUD
        private void AddRoom()
        {
            try
            {
                decimal rate = decimal.Parse(txtHourlyRate.Text.Replace(",", "").Replace(".", ""));
                var room = new PhongHatDTO
                {
                    TenPhong = txtRoomName.Text.Trim(),
                    LoaiPhong = cboRoomType.SelectedItem?.ToString() ?? "Thường",
                    GiaTheoGio = rate,
                    TrangThai = cboRoomStatus.SelectedItem?.ToString() ?? "Trống"
                };
                if (_karaokeBus.AddRoom(room))
                {
                    MessageBox.Show("Thêm phòng hát thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearRoomForm();
                    dgvRooms.DataSource = _karaokeBus.GetAllRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRoom()
        {
            try
            {
                if (!int.TryParse(txtRoomId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal rate = decimal.Parse(txtHourlyRate.Text.Replace(",", "").Replace(".", ""));
                var room = new PhongHatDTO
                {
                    MaPhong = id,
                    TenPhong = txtRoomName.Text.Trim(),
                    LoaiPhong = cboRoomType.SelectedItem?.ToString() ?? "Thường",
                    GiaTheoGio = rate,
                    TrangThai = cboRoomStatus.SelectedItem?.ToString() ?? "Trống"
                };
                if (_karaokeBus.UpdateRoom(room))
                {
                    MessageBox.Show("Cập nhật phòng hát thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearRoomForm();
                    dgvRooms.DataSource = _karaokeBus.GetAllRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRoom()
        {
            try
            {
                if (!int.TryParse(txtRoomId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn phòng cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng hát này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _karaokeBus.DeleteRoom(id);
                    MessageBox.Show("Đã xóa phòng hát!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearRoomForm();
                    dgvRooms.DataSource = _karaokeBus.GetAllRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa phòng (có thể đang có hóa đơn liên kết): {ex.Message}", "Lỗi toàn vẹn dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearRoomForm()
        {
            txtRoomId.Clear();
            txtRoomName.Clear();
            txtHourlyRate.Text = "150000";
            cboRoomType.SelectedIndex = 0;
            cboRoomStatus.SelectedIndex = 0;
        }

        // Service CRUD
        private void AddService()
        {
            try
            {
                decimal price = decimal.Parse(txtServicePrice.Text.Replace(",", "").Replace(".", ""));
                int stock = int.Parse(txtServiceStock.Text);
                var dv = new DichVuDTO { TenDV = txtServiceName.Text.Trim(), DonGia = price, TonKho = stock };
                if (_karaokeBus.AddService(dv))
                {
                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearServiceForm();
                    dgvServices.DataSource = _karaokeBus.GetAllServices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateService()
        {
            try
            {
                if (!int.TryParse(txtServiceId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal price = decimal.Parse(txtServicePrice.Text.Replace(",", "").Replace(".", ""));
                int stock = int.Parse(txtServiceStock.Text);
                var dv = new DichVuDTO { MaDV = id, TenDV = txtServiceName.Text.Trim(), DonGia = price, TonKho = stock };
                if (_karaokeBus.UpdateService(dv))
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearServiceForm();
                    dgvServices.DataSource = _karaokeBus.GetAllServices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteService()
        {
            try
            {
                if (!int.TryParse(txtServiceId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _karaokeBus.DeleteService(id);
                    MessageBox.Show("Đã xóa dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearServiceForm();
                    dgvServices.DataSource = _karaokeBus.GetAllServices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearServiceForm()
        {
            txtServiceId.Clear();
            txtServiceName.Clear();
            txtServicePrice.Text = "25000";
            txtServiceStock.Text = "100";
        }

        // Part CRUD
        private void AddPart()
        {
            try
            {
                decimal price = decimal.Parse(txtPartPrice.Text.Replace(",", "").Replace(".", ""));
                int stock = int.Parse(txtPartStock.Text);
                var pt = new PhuTungDTO { TenPhuTung = txtPartName.Text.Trim(), DonGia = price, SoLuongTon = stock };
                if (_garageBus.AddPart(pt))
                {
                    MessageBox.Show("Thêm phụ tùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPartForm();
                    dgvParts.DataSource = _garageBus.GetAllParts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdatePart()
        {
            try
            {
                if (!int.TryParse(txtPartId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn phụ tùng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal price = decimal.Parse(txtPartPrice.Text.Replace(",", "").Replace(".", ""));
                int stock = int.Parse(txtPartStock.Text);
                var pt = new PhuTungDTO { MaPhuTung = id, TenPhuTung = txtPartName.Text.Trim(), DonGia = price, SoLuongTon = stock };
                if (_garageBus.UpdatePart(pt))
                {
                    MessageBox.Show("Cập nhật phụ tùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPartForm();
                    dgvParts.DataSource = _garageBus.GetAllParts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeletePart()
        {
            try
            {
                if (!int.TryParse(txtPartId.Text, out int id))
                {
                    MessageBox.Show("Vui lòng chọn phụ tùng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phụ tùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _garageBus.DeletePart(id);
                    MessageBox.Show("Đã xóa phụ tùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPartForm();
                    dgvParts.DataSource = _garageBus.GetAllParts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa phụ tùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPartForm()
        {
            txtPartId.Clear();
            txtPartName.Clear();
            txtPartPrice.Text = "500000";
            txtPartStock.Text = "50";
        }
    }
}

