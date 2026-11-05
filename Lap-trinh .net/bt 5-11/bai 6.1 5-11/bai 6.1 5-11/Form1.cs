using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesSystem.BUS;
using SalesSystem.DTO;

namespace SalesSystem.GUI
{
    public partial class Form1 : Form
    {
        private SalesBUS bus = new SalesBUS();
        private List<ChiTietHoaDonDTO> gioHang = new List<ChiTietHoaDonDTO>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            LoadDanhSachSanPham();
            CapNhatGioHang();
            CapNhatThongKe();
        }

        private void LoadCombobox()
        {
            // Load Nhân viên
            cboNhanVien.DataSource = bus.GetNhanViens();
            cboNhanVien.DisplayMember = "TenNV";
            cboNhanVien.ValueMember = "MaNV";

            // Load Khách hàng
            cboKhachHang.DataSource = bus.GetKhachHangs();
            cboKhachHang.DisplayMember = "TenKH";
            cboKhachHang.ValueMember = "MaKH";
        }

        private void LoadDanhSachSanPham()
        {
            dgvSanPham.DataSource = bus.GetSanPhams();
        }

        // Tìm kiếm sản phẩm thời gian thực (SqlParameter)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = bus.SearchSanPhams(txtTimKiem.Text.Trim());
        }

        // Cập nhật điểm tích lũy khi đổi khách hàng
        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem is KhachHangDTO kh)
            {
                lblDiemTichLuy.Text = $"Điểm hiện có: {kh.DiemTichLuy}";
            }
        }

        // THÊM SẢN PHẨM VÀO GIỎ
        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;

            var sp = (SanPhamDTO)dgvSanPham.CurrentRow.DataBoundItem;
            int soLuongMua = (int)nudSoLuong.Value;

            // Kiểm tra sơ bộ tồn kho
            if (soLuongMua > sp.SoLuongTon)
            {
                MessageBox.Show($"Kho chỉ còn {sp.SoLuongTon} sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemTrongGio = gioHang.FirstOrDefault(x => x.MaSP == sp.MaSP);
            if (itemTrongGio != null)
            {
                if (itemTrongGio.SoLuong + soLuongMua > sp.SoLuongTon)
                {
                    MessageBox.Show("Tổng số lượng trong giỏ vượt quá tồn kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                itemTrongGio.SoLuong += soLuongMua;
            }
            else
            {
                gioHang.Add(new ChiTietHoaDonDTO
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    DonGia = sp.GiaBan,
                    SoLuong = soLuongMua
                });
            }

            CapNhatGioHang();
        }

        // XÓA SẢN PHẨM KHỎI GIỎ
        private void btnXoaKhoiGio_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            var item = (ChiTietHoaDonDTO)dgvGioHang.CurrentRow.DataBoundItem;
            gioHang.Remove(item);
            CapNhatGioHang();
        }

        private void CapNhatGioHang()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = gioHang;

            decimal tongTien = gioHang.Sum(x => x.ThanhTien);
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }

        // NÚT THANH TOÁN (CHẠY SqlTransaction)
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng chưa có sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show("Xác nhận thanh toán đơn hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            HoaDonDTO hoaDon = new HoaDonDTO
            {
                MaNV = (int)cboNhanVien.SelectedValue,
                MaKH = (int)cboKhachHang.SelectedValue,
                TongTien = gioHang.Sum(x => x.ThanhTien)
            };

            string error;
            bool ketQua = bus.XyLyThanhToan(hoaDon, gioHang, out error);

            if (ketQua)
            {
                MessageBox.Show("Thanh toán thành công! Dữ liệu kho & điểm đã được cập nhật.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gioHang.Clear();
                CapNhatGioHang();
                LoadDanhSachSanPham();
                LoadCombobox(); // Cập nhật lại điểm khách hàng
                CapNhatThongKe();
            }
            else
            {
                // Khi có lỗi (hết hàng, đứt kết nối...), Transaction đã Rollback
                MessageBox.Show($"Thanh toán thất bại! Đã Rollback giao dịch.\nChi tiết lỗi: {error}", "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDanhSachSanPham(); // Refresh lại dữ liệu tồn kho mới nhất
            }
        }

        // THỐNG KÊ (ExecuteScalar & SqlDataReader)
        private void CapNhatThongKe()
        {
            // ExecuteScalar
            decimal dt = bus.GetDoanhThuHomNay();
            lblDoanhThuNgay.Text = $"Doanh thu hôm nay: {dt:N0} VNĐ";

            // SqlDataReader
            lstTop5.Items.Clear();
            List<string> top5 = bus.GetTop5();
            foreach (var item in top5)
            {
                lstTop5.Items.Add(item);
            }
        }

        private void btnLamMoiThongKe_Click(object sender, EventArgs e)
        {
            CapNhatThongKe();
        }
    }
}