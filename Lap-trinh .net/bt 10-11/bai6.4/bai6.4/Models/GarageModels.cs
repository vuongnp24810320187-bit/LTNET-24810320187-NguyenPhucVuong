namespace bai6._4.Models;

public sealed record VehicleRecord(
    string BienSo,
    int MaKH,
    string TenKH,
    string? DienThoai,
    string? DiaChi,
    string? HieuXe,
    int? NamSanXuat);

public sealed record RepairPart(int MaPhuTung, int SoLuong, decimal TienCong);
