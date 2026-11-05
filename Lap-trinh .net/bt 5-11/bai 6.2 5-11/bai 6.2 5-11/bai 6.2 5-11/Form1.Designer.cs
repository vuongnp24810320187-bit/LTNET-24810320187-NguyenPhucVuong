namespace HRM_PayrollSystem
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabNhanSu = new System.Windows.Forms.TabPage();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.cboTieuChiTim = new System.Windows.Forms.ComboBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.btnLamMoiNV = new System.Windows.Forms.Button();
            this.btnXoaNV = new System.Windows.Forms.Button();
            this.btnSuaNV = new System.Windows.Forms.Button();
            this.btnThemNV = new System.Windows.Forms.Button();
            this.numHeSoLuong = new System.Windows.Forms.NumericUpDown();
            this.numLuongCoBan = new System.Windows.Forms.NumericUpDown();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblHeSoLuong = new System.Windows.Forms.Label();
            this.lblLuongCoBan = new System.Windows.Forms.Label();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.tabChamCong = new System.Windows.Forms.TabPage();
            this.lblHuongDanCong = new System.Windows.Forms.Label();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();
            this.btnLuuChamCong = new System.Windows.Forms.Button();
            this.btnNapChamCong = new System.Windows.Forms.Button();
            this.txtThangNamCong = new System.Windows.Forms.TextBox();
            this.lblThangNamCong = new System.Windows.Forms.Label();
            this.tabLuong = new System.Windows.Forms.TabPage();
            this.dgvLuong = new System.Windows.Forms.DataGridView();
            this.btnChotLuong = new System.Windows.Forms.Button();
            this.btnXemLuong = new System.Windows.Forms.Button();
            this.txtThangNamLuong = new System.Windows.Forms.TextBox();
            this.lblThangNamLuong = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabNhanSu.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongCoBan)).BeginInit();
            this.tabChamCong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).BeginInit();
            this.tabLuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabNhanSu);
            this.tabControlMain.Controls.Add(this.tabChamCong);
            this.tabControlMain.Controls.Add(this.tabLuong);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1064, 621);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabNhanSu
            // 
            this.tabNhanSu.Controls.Add(this.grpDanhSach);
            this.tabNhanSu.Controls.Add(this.grpThongTin);
            this.tabNhanSu.Location = new System.Drawing.Point(4, 26);
            this.tabNhanSu.Name = "tabNhanSu";
            this.tabNhanSu.Padding = new System.Windows.Forms.Padding(3);
            this.tabNhanSu.Size = new System.Drawing.Size(1056, 591);
            this.tabNhanSu.TabIndex = 0;
            this.tabNhanSu.Text = "1. Quản lý Hồ sơ Nhân sự";
            this.tabNhanSu.UseVisualStyleBackColor = true;
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDanhSach.Controls.Add(this.dgvNhanVien);
            this.grpDanhSach.Controls.Add(this.btnTimKiem);
            this.grpDanhSach.Controls.Add(this.txtTimKiem);
            this.grpDanhSach.Controls.Add(this.cboTieuChiTim);
            this.grpDanhSach.Controls.Add(this.lblTimKiem);
            this.grpDanhSach.Location = new System.Drawing.Point(372, 6);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(676, 577);
            this.grpDanhSach.TabIndex = 1;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh sách Nhân sự";
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Location = new System.Drawing.Point(6, 68);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersWidth = 51;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(664, 503);
            this.dgvNhanVien.TabIndex = 4;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(495, 27);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 28);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(240, 29);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(249, 25);
            this.txtTimKiem.TabIndex = 2;
            // 
            // cboTieuChiTim
            // 
            this.cboTieuChiTim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTieuChiTim.FormattingEnabled = true;
            this.cboTieuChiTim.Location = new System.Drawing.Point(105, 29);
            this.cboTieuChiTim.Name = "cboTieuChiTim";
            this.cboTieuChiTim.Size = new System.Drawing.Size(129, 25);
            this.cboTieuChiTim.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(15, 32);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(67, 19);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm theo:";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.btnLamMoiNV);
            this.grpThongTin.Controls.Add(this.btnXoaNV);
            this.grpThongTin.Controls.Add(this.btnSuaNV);
            this.grpThongTin.Controls.Add(this.btnThemNV);
            this.grpThongTin.Controls.Add(this.numHeSoLuong);
            this.grpThongTin.Controls.Add(this.numLuongCoBan);
            this.grpThongTin.Controls.Add(this.cboPhongBan);
            this.grpThongTin.Controls.Add(this.dtpNgaySinh);
            this.grpThongTin.Controls.Add(this.txtCCCD);
            this.grpThongTin.Controls.Add(this.txtHoTen);
            this.grpThongTin.Controls.Add(this.txtMaNV);
            this.grpThongTin.Controls.Add(this.lblHeSoLuong);
            this.grpThongTin.Controls.Add(this.lblLuongCoBan);
            this.grpThongTin.Controls.Add(this.lblPhongBan);
            this.grpThongTin.Controls.Add(this.lblCCCD);
            this.grpThongTin.Controls.Add(this.lblNgaySinh);
            this.grpThongTin.Controls.Add(this.lblHoTen);
            this.grpThongTin.Controls.Add(this.lblMaNV);
            this.grpThongTin.Location = new System.Drawing.Point(8, 6);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(358, 577);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin Nhân sự";
            // 
            // btnLamMoiNV
            // 
            this.btnLamMoiNV.Location = new System.Drawing.Point(253, 350);
            this.btnLamMoiNV.Name = "btnLamMoiNV";
            this.btnLamMoiNV.Size = new System.Drawing.Size(85, 35);
            this.btnLamMoiNV.TabIndex = 17;
            this.btnLamMoiNV.Text = "Làm mới";
            this.btnLamMoiNV.UseVisualStyleBackColor = true;
            this.btnLamMoiNV.Click += new System.EventHandler(this.btnLamMoiNV_Click);
            // 
            // btnXoaNV
            // 
            this.btnXoaNV.Location = new System.Drawing.Point(172, 350);
            this.btnXoaNV.Name = "btnXoaNV";
            this.btnXoaNV.Size = new System.Drawing.Size(75, 35);
            this.btnXoaNV.TabIndex = 16;
            this.btnXoaNV.Text = "Xóa";
            this.btnXoaNV.UseVisualStyleBackColor = true;
            this.btnXoaNV.Click += new System.EventHandler(this.btnXoaNV_Click);
            // 
            // btnSuaNV
            // 
            this.btnSuaNV.Location = new System.Drawing.Point(91, 350);
            this.btnSuaNV.Name = "btnSuaNV";
            this.btnSuaNV.Size = new System.Drawing.Size(75, 35);
            this.btnSuaNV.TabIndex = 15;
            this.btnSuaNV.Text = "Sửa";
            this.btnSuaNV.UseVisualStyleBackColor = true;
            this.btnSuaNV.Click += new System.EventHandler(this.btnSuaNV_Click);
            // 
            // btnThemNV
            // 
            this.btnThemNV.Location = new System.Drawing.Point(10, 350);
            this.btnThemNV.Name = "btnThemNV";
            this.btnThemNV.Size = new System.Drawing.Size(75, 35);
            this.btnThemNV.TabIndex = 14;
            this.btnThemNV.Text = "Thêm";
            this.btnThemNV.UseVisualStyleBackColor = true;
            this.btnThemNV.Click += new System.EventHandler(this.btnThemNV_Click);
            // 
            // numHeSoLuong
            // 
            this.numHeSoLuong.DecimalPlaces = 2;
            this.numHeSoLuong.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.numHeSoLuong.Location = new System.Drawing.Point(120, 276);
            this.numHeSoLuong.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numHeSoLuong.Name = "numHeSoLuong";
            this.numHeSoLuong.Size = new System.Drawing.Size(218, 25);
            this.numHeSoLuong.TabIndex = 13;
            this.numHeSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numLuongCoBan
            // 
            this.numLuongCoBan.Increment = new decimal(new int[] { 500000, 0, 0, 0 });
            this.numLuongCoBan.Location = new System.Drawing.Point(120, 236);
            this.numLuongCoBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numLuongCoBan.Name = "numLuongCoBan";
            this.numLuongCoBan.Size = new System.Drawing.Size(218, 25);
            this.numLuongCoBan.TabIndex = 12;
            this.numLuongCoBan.Value = new decimal(new int[] { 10000000, 0, 0, 0 });
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongBan.FormattingEnabled = true;
            this.cboPhongBan.Location = new System.Drawing.Point(120, 196);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(218, 25);
            this.cboPhongBan.TabIndex = 11;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(120, 116);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(218, 25);
            this.dtpNgaySinh.TabIndex = 10;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(120, 156);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(218, 25);
            this.txtCCCD.TabIndex = 9;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(120, 76);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(218, 25);
            this.txtHoTen.TabIndex = 8;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(120, 36);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.ReadOnly = true;
            this.txtMaNV.Size = new System.Drawing.Size(218, 25);
            this.txtMaNV.TabIndex = 7;
            // 
            // lblHeSoLuong
            // 
            this.lblHeSoLuong.AutoSize = true;
            this.lblHeSoLuong.Location = new System.Drawing.Point(16, 278);
            this.lblHeSoLuong.Name = "lblHeSoLuong";
            this.lblHeSoLuong.Size = new System.Drawing.Size(86, 19);
            this.lblHeSoLuong.TabIndex = 6;
            this.lblHeSoLuong.Text = "Hệ số lương:";
            // 
            // lblLuongCoBan
            // 
            this.lblLuongCoBan.AutoSize = true;
            this.lblLuongCoBan.Location = new System.Drawing.Point(16, 238);
            this.lblLuongCoBan.Name = "lblLuongCoBan";
            this.lblLuongCoBan.Size = new System.Drawing.Size(95, 19);
            this.lblLuongCoBan.TabIndex = 5;
            this.lblLuongCoBan.Text = "Lương cơ bản:";
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Location = new System.Drawing.Point(16, 199);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(78, 19);
            this.lblPhongBan.TabIndex = 4;
            this.lblPhongBan.Text = "Phòng ban:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Location = new System.Drawing.Point(16, 159);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(68, 19);
            this.lblCCCD.TabIndex = 3;
            this.lblCCCD.Text = "Số CCCD:";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(16, 121);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(73, 19);
            this.lblNgaySinh.TabIndex = 2;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(16, 79);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(54, 19);
            this.lblHoTen.TabIndex = 1;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(16, 39);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(56, 19);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // tabChamCong
            // 
            this.tabChamCong.Controls.Add(this.lblHuongDanCong);
            this.tabChamCong.Controls.Add(this.dgvChamCong);
            this.tabChamCong.Controls.Add(this.btnLuuChamCong);
            this.tabChamCong.Controls.Add(this.btnNapChamCong);
            this.tabChamCong.Controls.Add(this.txtThangNamCong);
            this.tabChamCong.Controls.Add(this.lblThangNamCong);
            this.tabChamCong.Location = new System.Drawing.Point(4, 26);
            this.tabChamCong.Name = "tabChamCong";
            this.tabChamCong.Padding = new System.Windows.Forms.Padding(3);
            this.tabChamCong.Size = new System.Drawing.Size(1056, 591);
            this.tabChamCong.TabIndex = 1;
            this.tabChamCong.Text = "2. Chấm công (Disconnected Model)";
            this.tabChamCong.UseVisualStyleBackColor = true;
            // 
            // lblHuongDanCong
            // 
            this.lblHuongDanCong.AutoSize = true;
            this.lblHuongDanCong.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblHuongDanCong.Location = new System.Drawing.Point(542, 27);
            this.lblHuongDanCong.Name = "lblHuongDanCong";
            this.lblHuongDanCong.Size = new System.Drawing.Size(437, 19);
            this.lblHuongDanCong.TabIndex = 5;
            this.lblHuongDanCong.Text = "(*) Nhập trực tiếp Số Ngày Công, Giờ OT trên lưới rồi bấm Lưu Chấm Công";
            // 
            // dgvChamCong
            // 
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.AllowUserToDeleteRows = false;
            this.dgvChamCong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChamCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChamCong.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvChamCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamCong.Location = new System.Drawing.Point(12, 70);
            this.dgvChamCong.Name = "dgvChamCong";
            this.dgvChamCong.RowHeadersWidth = 51;
            this.dgvChamCong.Size = new System.Drawing.Size(1036, 513);
            this.dgvChamCong.TabIndex = 4;
            // 
            // btnLuuChamCong
            // 
            this.btnLuuChamCong.BackColor = System.Drawing.Color.LightGreen;
            this.btnLuuChamCong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuChamCong.Location = new System.Drawing.Point(380, 19);
            this.btnLuuChamCong.Name = "btnLuuChamCong";
            this.btnLuuChamCong.Size = new System.Drawing.Size(130, 35);
            this.btnLuuChamCong.TabIndex = 3;
            this.btnLuuChamCong.Text = "Lưu Chấm Công";
            this.btnLuuChamCong.UseVisualStyleBackColor = false;
            this.btnLuuChamCong.Click += new System.EventHandler(this.btnLuuChamCong_Click);
            // 
            // btnNapChamCong
            // 
            this.btnNapChamCong.Location = new System.Drawing.Point(234, 19);
            this.btnNapChamCong.Name = "btnNapChamCong";
            this.btnNapChamCong.Size = new System.Drawing.Size(140, 35);
            this.btnNapChamCong.TabIndex = 2;
            this.btnNapChamCong.Text = "Nạp Bảng Công";
            this.btnNapChamCong.UseVisualStyleBackColor = true;
            this.btnNapChamCong.Click += new System.EventHandler(this.btnNapChamCong_Click);
            // 
            // txtThangNamCong
            // 
            this.txtThangNamCong.Location = new System.Drawing.Point(118, 25);
            this.txtThangNamCong.Name = "txtThangNamCong";
            this.txtThangNamCong.Size = new System.Drawing.Size(100, 25);
            this.txtThangNamCong.TabIndex = 1;
            this.txtThangNamCong.Text = "11/2026";
            // 
            // lblThangNamCong
            // 
            this.lblThangNamCong.AutoSize = true;
            this.lblThangNamCong.Location = new System.Drawing.Point(20, 28);
            this.lblThangNamCong.Name = "lblThangNamCong";
            this.lblThangNamCong.Size = new System.Drawing.Size(86, 19);
            this.lblThangNamCong.TabIndex = 0;
            this.lblThangNamCong.Text = "Tháng/Năm:";
            // 
            // tabLuong
            // 
            this.tabLuong.Controls.Add(this.dgvLuong);
            this.tabLuong.Controls.Add(this.btnChotLuong);
            this.tabLuong.Controls.Add(this.btnXemLuong);
            this.tabLuong.Controls.Add(this.txtThangNamLuong);
            this.tabLuong.Controls.Add(this.lblThangNamLuong);
            this.tabLuong.Location = new System.Drawing.Point(4, 26);
            this.tabLuong.Name = "tabLuong";
            this.tabLuong.Padding = new System.Windows.Forms.Padding(3);
            this.tabLuong.Size = new System.Drawing.Size(1056, 591);
            this.tabLuong.TabIndex = 2;
            this.tabLuong.Text = "3. Tính & Chốt Lương (SqlTransaction)";
            this.tabLuong.UseVisualStyleBackColor = true;
            // 
            // dgvLuong
            // 
            this.dgvLuong.AllowUserToAddRows = false;
            this.dgvLuong.AllowUserToDeleteRows = false;
            this.dgvLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLuong.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLuong.Location = new System.Drawing.Point(12, 70);
            this.dgvLuong.Name = "dgvLuong";
            this.dgvLuong.ReadOnly = true;
            this.dgvLuong.RowHeadersWidth = 51;
            this.dgvLuong.Size = new System.Drawing.Size(1036, 513);
            this.dgvLuong.TabIndex = 4;
            // 
            // btnChotLuong
            // 
            this.btnChotLuong.BackColor = System.Drawing.Color.OrangeRed;
            this.btnChotLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChotLuong.ForeColor = System.Drawing.Color.White;
            this.btnChotLuong.Location = new System.Drawing.Point(375, 19);
            this.btnChotLuong.Name = "btnChotLuong";
            this.btnChotLuong.Size = new System.Drawing.Size(155, 35);
            this.btnChotLuong.TabIndex = 3;
            this.btnChotLuong.Text = "Chốt Lương Tháng";
            this.btnChotLuong.UseVisualStyleBackColor = false;
            this.btnChotLuong.Click += new System.EventHandler(this.btnChotLuong_Click);
            // 
            // btnXemLuong
            // 
            this.btnXemLuong.Location = new System.Drawing.Point(234, 19);
            this.btnXemLuong.Name = "btnXemLuong";
            this.btnXemLuong.Size = new System.Drawing.Size(135, 35);
            this.btnXemLuong.TabIndex = 2;
            this.btnXemLuong.Text = "Xem Bảng Lương";
            this.btnXemLuong.UseVisualStyleBackColor = true;
            this.btnXemLuong.Click += new System.EventHandler(this.btnXemLuong_Click);
            // 
            // txtThangNamLuong
            // 
            this.txtThangNamLuong.Location = new System.Drawing.Point(118, 25);
            this.txtThangNamLuong.Name = "txtThangNamLuong";
            this.txtThangNamLuong.Size = new System.Drawing.Size(100, 25);
            this.txtThangNamLuong.TabIndex = 1;
            this.txtThangNamLuong.Text = "11/2026";
            // 
            // lblThangNamLuong
            // 
            this.lblThangNamLuong.AutoSize = true;
            this.lblThangNamLuong.Location = new System.Drawing.Point(20, 28);
            this.lblThangNamLuong.Name = "lblThangNamLuong";
            this.lblThangNamLuong.Size = new System.Drawing.Size(86, 19);
            this.lblThangNamLuong.TabIndex = 0;
            this.lblThangNamLuong.Text = "Tháng/Năm:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 621);
            this.Controls.Add(this.tabControlMain);
            this.MinimumSize = new System.Drawing.Size(950, 550);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ & TÍNH LƯƠNG (HRM & PAYROLL)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabNhanSu.ResumeLayout(false);
            this.grpDanhSach.ResumeLayout(false);
            this.grpDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongCoBan)).EndInit();
            this.tabChamCong.ResumeLayout(false);
            this.tabChamCong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).EndInit();
            this.tabLuong.ResumeLayout(false);
            this.tabLuong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabNhanSu;
        private System.Windows.Forms.TabPage tabChamCong;
        private System.Windows.Forms.TabPage tabLuong;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblPhongBan;
        private System.Windows.Forms.Label lblLuongCoBan;
        private System.Windows.Forms.Label lblHeSoLuong;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.NumericUpDown numLuongCoBan;
        private System.Windows.Forms.NumericUpDown numHeSoLuong;
        private System.Windows.Forms.Button btnThemNV;
        private System.Windows.Forms.Button btnSuaNV;
        private System.Windows.Forms.Button btnXoaNV;
        private System.Windows.Forms.Button btnLamMoiNV;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox cboTieuChiTim;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblThangNamCong;
        private System.Windows.Forms.TextBox txtThangNamCong;
        private System.Windows.Forms.Button btnNapChamCong;
        private System.Windows.Forms.Button btnLuuChamCong;
        private System.Windows.Forms.DataGridView dgvChamCong;
        private System.Windows.Forms.Label lblHuongDanCong;
        private System.Windows.Forms.Label lblThangNamLuong;
        private System.Windows.Forms.TextBox txtThangNamLuong;
        private System.Windows.Forms.Button btnXemLuong;
        private System.Windows.Forms.Button btnChotLuong;
        private System.Windows.Forms.DataGridView dgvLuong;
    }
}