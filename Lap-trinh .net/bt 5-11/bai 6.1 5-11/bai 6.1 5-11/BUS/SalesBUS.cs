using System;
using System.Collections.Generic;
using SalesSystem.DAL;
using SalesSystem.DTO;

namespace SalesSystem.BUS
{
    public class SalesBUS
    {
        private SanPhamDAL spDAL = new SanPhamDAL();
        private KhachHangDAL khDAL = new KhachHangDAL();
        private NhanVienDAL nvDAL = new NhanVienDAL();
        private HoaDonDAL hdDAL = new HoaDonDAL();
        private ThongKeDAL tkDAL = new ThongKeDAL();

        public List<SanPhamDTO> GetSanPhams() => spDAL.LayDanhSachSanPham();
        public List<SanPhamDTO> SearchSanPhams(string kw) => spDAL.TimKiemSanPham(kw);
        public List<KhachHangDTO> GetKhachHangs() => khDAL.LayDanhSachKhachHang();
        public List<NhanVienDTO> GetNhanViens() => nvDAL.LayDanhSachNhanVien();
        public decimal GetDoanhThuHomNay() => tkDAL.TinhTongDoanhThuHomNay();
        public List<string> GetTop5() => tkDAL.LayTop5SanPhamBanChay();

        public bool XyLyThanhToan(HoaDonDTO hd, List<ChiTietHoaDonDTO> listCT, out string error)
        {
            if (listCT == null || listCT.Count == 0)
            {
                error = "Giỏ hàng hiện đang trống!";
                return false;
            }
            return hdDAL.ThanhToanHoaDon(hd, listCT, out error);
        }
    }
}