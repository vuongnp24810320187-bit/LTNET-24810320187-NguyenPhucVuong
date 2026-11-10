using System;

namespace MultiServiceEnterpriseSystem.DTO
{
    public class DuAnDTO
    {
        public int MaDuAn { get; set; }
        public string TenDuAn { get; set; } = string.Empty;
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TrangThai { get; set; } = "Đang thực hiện";

        public override string ToString() => TenDuAn;
    }

    public class ThanhVienDTO
    {
        public int MaThanhVien { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ChuyenMon { get; set; } = string.Empty;

        public override string ToString() => $"{HoTen} ({ChuyenMon})";
    }

    public class CongViecDTO
    {
        public int MaTask { get; set; }
        public int MaDuAn { get; set; }
        public string? TenDuAn { get; set; }
        public string TenTask { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public DateTime? HanNop { get; set; }
        public string DoUuTien { get; set; } = "Trung bình";
        public string TrangThai { get; set; } = "To Do"; // To Do, In Progress, Review, Done
        public string? NguoiThucHien { get; set; }

        public override string ToString() => $"[{DoUuTien}] {TenTask} ({TrangThai})";
    }
}

