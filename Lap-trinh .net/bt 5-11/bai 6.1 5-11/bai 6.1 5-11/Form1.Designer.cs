namespace SalesSystem.GUI
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
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.lblDiemTichLuy = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.grbSanPham = new System.Windows.Forms.GroupBox();
            this.btnThemVaoGio = new System.Windows.Forms.Button();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.grbGioHang = new System.Windows.Forms.GroupBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnXoaKhoiGio = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.grbThongKe = new System.Windows.Forms.GroupBox();
            this.btnLamMoiThongKe = new System.Windows.Forms.Button();
            this.lstTop5 = new System.Windows.Forms.ListBox();
            this.lblTop5 = new System.Windows.Forms.Label();
            this.lblDoanhThuNgay = new System.Windows.Forms.Label();
            this.grbThongTin.SuspendLayout();
            this.grbSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.grbGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.grbThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbThongTin
            // 
            this.grbThongTin.Controls.Add(this.lblDiemTichLuy);
            this.grbThongTin.Controls.Add(this.cboKhachHang);
            this.grbThongTin.Controls.Add(this.lblKhachHang);
            this.grbThongTin.Controls.Add(this.cboNhanVien);
            this.grbThongTin.Controls.Add(this.lblNhanVien);
            this.grbThongTin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbThongTin.Location = new System.Drawing.Point(520, 12);
            this.grbThongTin.Name = "grbThongTin";
            this.grbThongTin.Size = new System.Drawing.Size(540, 95);
            this.grbThongTin.TabIndex = 0;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "1. Thông Tin Đơn Hàng";
            // 
            // lblDiemTichLuy
            // 
            this.lblDiemTichLuy.AutoSize = true;
            this.lblDiemTichLuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemTichLuy.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDiemTichLuy.Location = new System.Drawing.Point(280, 62);
            this.lblDiemTichLuy.Name = "lblDiemTichLuy";
            this.lblDiemTichLuy.Size = new System.Drawing.Size(117, 15);
            this.lblDiemTichLuy.TabIndex = 4;
            this.lblDiemTichLuy.Text = "Điểm tích lũy hiện có: 0";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(365, 27);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(160, 23);
            this.cboKhachHang.TabIndex = 3;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(280, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(73, 15);
            this.lblKhachHang.TabIndex = 2;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(95, 27);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(160, 23);
            this.cboNhanVien.TabIndex = 1;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(15, 30);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(64, 15);
            this.lblNhanVien.TabIndex = 0;
            this.lblNhanVien.Text = "Thu ngân:";
            // 
            // grbSanPham
            // 
            this.grbSanPham.Controls.Add(this.btnThemVaoGio);
            this.grbSanPham.Controls.Add(this.nudSoLuong);
            this.grbSanPham.Controls.Add(this.lblSoLuong);
            this.grbSanPham.Controls.Add(this.dgvSanPham);
            this.grbSanPham.Controls.Add(this.txtTimKiem);
            this.grbSanPham.Controls.Add(this.lblTimKiem);
            this.grbSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSanPham.Location = new System.Drawing.Point(12, 12);
            this.grbSanPham.Name = "grbSanPham";
            this.grbSanPham.Size = new System.Drawing.Size(490, 640);
            this.grbSanPham.TabIndex = 1;
            this.grbSanPham.TabStop = false;
            this.grbSanPham.Text = "2. Danh Sách Sản Phẩm (Kho)";
            // 
            // btnThemVaoGio
            // 
            this.btnThemVaoGio.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThemVaoGio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemVaoGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoGio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemVaoGio.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoGio.Location = new System.Drawing.Point(295, 595);
            this.btnThemVaoGio.Name = "btnThemVaoGio";
            this.btnThemVaoGio.Size = new System.Drawing.Size(180, 32);
            this.btnThemVaoGio.TabIndex = 5;
            this.btnThemVaoGio.Text = "Thêm vào giỏ hàng >>";
            this.btnThemVaoGio.UseVisualStyleBackColor = false;
            this.btnThemVaoGio.Click += new System.EventHandler(this.btnThemVaoGio_Click);
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(180, 601);
            this.nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(80, 23);
            this.nudSoLuong.TabIndex = 4;
            this.nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(90, 604);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(86, 15);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Số lượng mua:";
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.AllowUserToDeleteRows = false;
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Location = new System.Drawing.Point(15, 65);
            this.dgvSanPham.MultiSelect = false;
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(460, 515);
            this.dgvSanPham.TabIndex = 2;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(125, 27);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(350, 23);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(15, 30);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(99, 15);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm SP / Mã:";
            // 
            // grbGioHang
            // 
            this.grbGioHang.Controls.Add(this.btnThanhToan);
            this.grbGioHang.Controls.Add(this.lblTongTien);
            this.grbGioHang.Controls.Add(this.btnXoaKhoiGio);
            this.grbGioHang.Controls.Add(this.dgvGioHang);
            this.grbGioHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGioHang.Location = new System.Drawing.Point(520, 115);
            this.grbGioHang.Name = "grbGioHang";
            this.grbGioHang.Size = new System.Drawing.Size(540, 360);
            this.grbGioHang.TabIndex = 2;
            this.grbGioHang.TabStop = false;
            this.grbGioHang.Text = "3. Chi Tiết Giỏ Hàng";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(340, 305);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(185, 45);
            this.btnThanhToan.TabIndex = 3;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.ForeColor = System.Drawing.Color.Red;
            this.lblTongTien.Location = new System.Drawing.Point(15, 317);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(148, 21);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            // 
            // btnXoaKhoiGio
            // 
            this.btnXoaKhoiGio.BackColor = System.Drawing.Color.IndianRed;
            this.btnXoaKhoiGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaKhoiGio.ForeColor = System.Drawing.Color.White;
            this.btnXoaKhoiGio.Location = new System.Drawing.Point(425, 260);
            this.btnXoaKhoiGio.Name = "btnXoaKhoiGio";
            this.btnXoaKhoiGio.Size = new System.Drawing.Size(100, 30);
            this.btnXoaKhoiGio.TabIndex = 1;
            this.btnXoaKhoiGio.Text = "Xóa món";
            this.btnXoaKhoiGio.UseVisualStyleBackColor = false;
            this.btnXoaKhoiGio.Click += new System.EventHandler(this.btnXoaKhoiGio_Click);
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.AllowUserToDeleteRows = false;
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(15, 25);
            this.dgvGioHang.MultiSelect = false;
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.ReadOnly = true;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.Size = new System.Drawing.Size(510, 225);
            this.dgvGioHang.TabIndex = 0;
            // 
            // grbThongKe
            // 
            this.grbThongKe.Controls.Add(this.btnLamMoiThongKe);
            this.grbThongKe.Controls.Add(this.lstTop5);
            this.grbThongKe.Controls.Add(this.lblTop5);
            this.grbThongKe.Controls.Add(this.lblDoanhThuNgay);
            this.grbThongKe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbThongKe.Location = new System.Drawing.Point(520, 485);
            this.grbThongKe.Name = "grbThongKe";
            this.grbThongKe.Size = new System.Drawing.Size(540, 167);
            this.grbThongKe.TabIndex = 3;
            this.grbThongKe.TabStop = false;
            this.grbThongKe.Text = "4. Báo Cáo - Thống Kê Nhanh";
            // 
            // btnLamMoiThongKe
            // 
            this.btnLamMoiThongKe.Location = new System.Drawing.Point(395, 23);
            this.btnLamMoiThongKe.Name = "btnLamMoiThongKe";
            this.btnLamMoiThongKe.Size = new System.Drawing.Size(130, 26);
            this.btnLamMoiThongKe.TabIndex = 3;
            this.btnLamMoiThongKe.Text = "Làm mới số liệu";
            this.btnLamMoiThongKe.UseVisualStyleBackColor = true;
            this.btnLamMoiThongKe.Click += new System.EventHandler(this.btnLamMoiThongKe_Click);
            // 
            // lstTop5
            // 
            this.lstTop5.FormattingEnabled = true;
            this.lstTop5.ItemHeight = 15;
            this.lstTop5.Location = new System.Drawing.Point(195, 55);
            this.lstTop5.Name = "lstTop5";
            this.lstTop5.Size = new System.Drawing.Size(330, 94);
            this.lstTop5.TabIndex = 2;
            // 
            // lblTop5
            // 
            this.lblTop5.AutoSize = true;
            this.lblTop5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop5.Location = new System.Drawing.Point(15, 60);
            this.lblTop5.Name = "lblTop5";
            this.lblTop5.Size = new System.Drawing.Size(161, 15);
            this.lblTop5.TabIndex = 1;
            this.lblTop5.Text = "Top 5 sản phẩm bán chạy:";
            // 
            // lblDoanhThuNgay
            // 
            this.lblDoanhThuNgay.AutoSize = true;
            this.lblDoanhThuNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoanhThuNgay.ForeColor = System.Drawing.Color.Navy;
            this.lblDoanhThuNgay.Location = new System.Drawing.Point(15, 27);
            this.lblDoanhThuNgay.Name = "lblDoanhThuNgay";
            this.lblDoanhThuNgay.Size = new System.Drawing.Size(183, 17);
            this.lblDoanhThuNgay.TabIndex = 0;
            this.lblDoanhThuNgay.Text = "Doanh thu hôm nay: 0 VNĐ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1074, 664);
            this.Controls.Add(this.grbThongKe);
            this.Controls.Add(this.grbGioHang);
            this.Controls.Add(this.grbSanPham);
            this.Controls.Add(this.grbThongTin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HỆ THỐNG QUẢN LÝ BÁN HÀNG & SIÊU THỊ MINI (RETAIL SALES SYSTEM)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.grbSanPham.ResumeLayout(false);
            this.grbSanPham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.grbGioHang.ResumeLayout(false);
            this.grbGioHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.grbThongKe.ResumeLayout(false);
            this.grbThongKe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblDiemTichLuy;
        private System.Windows.Forms.GroupBox grbSanPham;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Button btnThemVaoGio;
        private System.Windows.Forms.GroupBox grbGioHang;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnXoaKhoiGio;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.GroupBox grbThongKe;
        private System.Windows.Forms.Label lblDoanhThuNgay;
        private System.Windows.Forms.ListBox lstTop5;
        private System.Windows.Forms.Label lblTop5;
        private System.Windows.Forms.Button btnLamMoiThongKe;
    }
}