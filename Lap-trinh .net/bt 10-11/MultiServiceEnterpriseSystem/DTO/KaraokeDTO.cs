using System;

namespace MultiServiceEnterpriseSystem.DTO
{
    public class PhongHatDTO
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = "Thường";
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; } = "Trống";
        public int? MaHD { get; set; }
        public DateTime? GioVao { get; set; }

        public override string ToString() => $"{TenPhong} ({LoaiPhong}) - {TrangThai}";
    }

    public class DichVuDTO
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int TonKho { get; set; }

        public override string ToString() => $"{TenDV} - {DonGia:N0} đ (Kho: {TonKho})";
    }

    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public int MaPhong { get; set; }
        public string? TenPhong { get; set; }
        public DateTime GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public bool TrangThai { get; set; } // false: đang hát, true: đã thanh toán
        public decimal TongTien => TongTienPhong + TongTienDichVu;
    }

    public class ChiTietDichVuDTO
    {
        public int MaHD { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}

