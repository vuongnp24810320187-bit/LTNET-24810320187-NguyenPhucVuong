using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using bai6._4.Models;
using Microsoft.Data.SqlClient;

namespace bai6._4.Data;

public sealed class GarageService
{
    private readonly string _connectionString;

    public GarageService()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        using var settings = JsonDocument.Parse(File.ReadAllText(settingsPath));
        _connectionString = settings.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("GarageDB")
            .GetString() ?? throw new InvalidOperationException("Thiếu chuỗi kết nối GarageDB.");
    }

    public DataTable GetCustomers()
    {
        return Query("SELECT MaKH, TenKH, DienThoai, DiaChi FROM dbo.KhachHang ORDER BY TenKH;");
    }

    public int SaveCustomer(int? customerId, string name, string? phone, string? address)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = name.Trim();
        command.Parameters.Add("@DienThoai", SqlDbType.VarChar, 15).Value = DbValue(phone);
        command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = DbValue(address);

        if (customerId is int id)
        {
            command.CommandText = "UPDATE dbo.KhachHang SET TenKH = @TenKH, DienThoai = @DienThoai, DiaChi = @DiaChi WHERE MaKH = @MaKH;";
            command.Parameters.Add("@MaKH", SqlDbType.Int).Value = id;
            if (command.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("Không tìm thấy chủ xe cần cập nhật.");
            }
            return id;
        }

        command.CommandText = "INSERT dbo.KhachHang (TenKH, DienThoai, DiaChi) OUTPUT INSERTED.MaKH VALUES (@TenKH, @DienThoai, @DiaChi);";
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public void DeleteCustomer(int customerId)
    {
        Execute("DELETE FROM dbo.KhachHang WHERE MaKH = @MaKH;", command => command.Parameters.Add("@MaKH", SqlDbType.Int).Value = customerId);
    }

    public DataTable GetVehicles()
    {
        return Query("SELECT x.BienSo, x.MaKH, k.TenKH, k.DienThoai, x.HieuXe, x.NamSanXuat FROM dbo.Xe x JOIN dbo.KhachHang k ON k.MaKH = x.MaKH ORDER BY x.BienSo;");
    }

    public VehicleRecord? FindVehicle(string plate)
    {
        using var connection = OpenConnection();
        using var command = new SqlCommand("SELECT x.BienSo, x.MaKH, k.TenKH, k.DienThoai, k.DiaChi, x.HieuXe, x.NamSanXuat FROM dbo.Xe x JOIN dbo.KhachHang k ON k.MaKH = x.MaKH WHERE x.BienSo = @BienSo;", connection);
        command.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = plate.Trim();
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new VehicleRecord(
            reader.GetString(0),
            reader.GetInt32(1),
            reader.GetString(2),
            reader.IsDBNull(3) ? null : reader.GetString(3),
            reader.IsDBNull(4) ? null : reader.GetString(4),
            reader.IsDBNull(5) ? null : reader.GetString(5),
            reader.IsDBNull(6) ? null : reader.GetInt32(6));
    }

    public void SaveVehicle(string plate, int customerId, string? make, int? year)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = plate.Trim();
        command.Parameters.Add("@MaKH", SqlDbType.Int).Value = customerId;
        command.Parameters.Add("@HieuXe", SqlDbType.NVarChar, 50).Value = DbValue(make);
        command.Parameters.Add("@NamSanXuat", SqlDbType.Int).Value = year is int value ? value : DBNull.Value;
        command.CommandText = "IF EXISTS (SELECT 1 FROM dbo.Xe WHERE BienSo = @BienSo) UPDATE dbo.Xe SET MaKH = @MaKH, HieuXe = @HieuXe, NamSanXuat = @NamSanXuat WHERE BienSo = @BienSo ELSE INSERT dbo.Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES (@BienSo, @MaKH, @HieuXe, @NamSanXuat);";
        command.ExecuteNonQuery();
    }

    public void DeleteVehicle(string plate)
    {
        Execute("DELETE FROM dbo.Xe WHERE BienSo = @BienSo;", command => command.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = plate.Trim());
    }

    public DataTable GetParts()
    {
        return Query("SELECT MaPhuTung, TenPhuTung, DonGia, SoLuongTon FROM dbo.PhuTung ORDER BY TenPhuTung;");
    }

    public void SavePart(int? partId, string name, decimal price, int stock)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.Parameters.Add("@TenPhuTung", SqlDbType.NVarChar, 150).Value = name.Trim();
        var priceParameter = command.Parameters.Add("@DonGia", SqlDbType.Decimal);
        priceParameter.Precision = 18;
        priceParameter.Scale = 2;
        priceParameter.Value = price;
        command.Parameters.Add("@SoLuongTon", SqlDbType.Int).Value = stock;

        if (partId is int id)
        {
            command.CommandText = "UPDATE dbo.PhuTung SET TenPhuTung = @TenPhuTung, DonGia = @DonGia, SoLuongTon = @SoLuongTon WHERE MaPhuTung = @MaPhuTung;";
            command.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = id;
        }
        else
        {
            command.CommandText = "INSERT dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (@TenPhuTung, @DonGia, @SoLuongTon);";
        }
        command.ExecuteNonQuery();
    }

    public void DeletePart(int partId)
    {
        Execute("DELETE FROM dbo.PhuTung WHERE MaPhuTung = @MaPhuTung;", command => command.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = partId);
    }

    public DataTable GetRepairOrders()
    {
        return Query("SELECT p.MaPhieu, p.BienSo, k.TenKH, p.NgayNhan, p.NoiDungSua, p.TongTien, p.TrangThai FROM dbo.PhieuSuaChua p JOIN dbo.Xe x ON x.BienSo = p.BienSo JOIN dbo.KhachHang k ON k.MaKH = x.MaKH ORDER BY p.NgayNhan DESC;");
    }

    public int CreateRepair(string plate, string description, IReadOnlyCollection<RepairPart> parts)
    {
        if (parts.Count == 0)
        {
            throw new InvalidOperationException("Phiếu sửa chữa cần có ít nhất một phụ tùng.");
        }

        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            decimal total = 0;
            foreach (var part in parts)
            {
                if (part.SoLuong <= 0 || part.TienCong < 0)
                {
                    throw new InvalidOperationException("Số lượng phải lớn hơn 0 và tiền công không được âm.");
                }

                using var priceCommand = new SqlCommand("SELECT DonGia FROM dbo.PhuTung WITH (UPDLOCK, HOLDLOCK) WHERE MaPhuTung = @MaPhuTung AND SoLuongTon >= @SoLuong;", connection, transaction);
                priceCommand.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = part.MaPhuTung;
                priceCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = part.SoLuong;
                var priceResult = priceCommand.ExecuteScalar();
                if (priceResult is null)
                {
                    throw new InvalidOperationException($"Phụ tùng mã {part.MaPhuTung} không tồn tại hoặc không đủ số lượng trong kho.");
                }
                total += Convert.ToDecimal(priceResult) * part.SoLuong + part.TienCong;
            }

            using var orderCommand = new SqlCommand("INSERT dbo.PhieuSuaChua (BienSo, NoiDungSua, TongTien, TrangThai) OUTPUT INSERTED.MaPhieu VALUES (@BienSo, @NoiDungSua, @TongTien, N'Đã hoàn thành');", connection, transaction);
            orderCommand.Parameters.Add("@BienSo", SqlDbType.VarChar, 20).Value = plate.Trim();
            orderCommand.Parameters.Add("@NoiDungSua", SqlDbType.NVarChar, -1).Value = DbValue(description);
            var totalParameter = orderCommand.Parameters.Add("@TongTien", SqlDbType.Decimal);
            totalParameter.Precision = 18;
            totalParameter.Scale = 2;
            totalParameter.Value = total;
            var repairId = Convert.ToInt32(orderCommand.ExecuteScalar());

            foreach (var part in parts)
            {
                using var detailCommand = new SqlCommand("INSERT dbo.ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong) VALUES (@MaPhieu, @MaPhuTung, @SoLuong, @TienCong); UPDATE dbo.PhuTung SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaPhuTung = @MaPhuTung AND SoLuongTon >= @SoLuong; IF @@ROWCOUNT = 0 THROW 51000, N'Không đủ số lượng phụ tùng trong kho.', 1;", connection, transaction);
                detailCommand.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = repairId;
                detailCommand.Parameters.Add("@MaPhuTung", SqlDbType.Int).Value = part.MaPhuTung;
                detailCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = part.SoLuong;
                var laborParameter = detailCommand.Parameters.Add("@TienCong", SqlDbType.Decimal);
                laborParameter.Precision = 18;
                laborParameter.Scale = 2;
                laborParameter.Value = part.TienCong;
                detailCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return repairId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public DataTable GetInvoice(int repairId)
    {
        return Query("SELECT p.MaPhieu, p.BienSo, k.TenKH, k.DienThoai, k.DiaChi, p.NgayNhan, p.NoiDungSua, p.TongTien, p.TrangThai, pt.TenPhuTung, ct.SoLuong, pt.DonGia, ct.TienCong, (ct.SoLuong * pt.DonGia + ct.TienCong) AS ThanhTien FROM dbo.PhieuSuaChua p JOIN dbo.Xe x ON x.BienSo = p.BienSo JOIN dbo.KhachHang k ON k.MaKH = x.MaKH LEFT JOIN dbo.ChiTietSuaChua ct ON ct.MaPhieu = p.MaPhieu LEFT JOIN dbo.PhuTung pt ON pt.MaPhuTung = ct.MaPhuTung WHERE p.MaPhieu = @MaPhieu ORDER BY pt.TenPhuTung;", command => command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = repairId);
    }

    public void MarkAsPaid(int repairId)
    {
        using var connection = OpenConnection();
        using var command = new SqlCommand("UPDATE dbo.PhieuSuaChua SET TrangThai = N'Đã thanh toán' WHERE MaPhieu = @MaPhieu AND TrangThai = N'Đã hoàn thành';", connection);
        command.Parameters.Add("@MaPhieu", SqlDbType.Int).Value = repairId;
        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Không tìm thấy phiếu đã hoàn thành để thanh toán.");
        }
    }

    private DataTable Query(string sql, Action<SqlCommand>? configure = null)
    {
        using var connection = OpenConnection();
        using var command = new SqlCommand(sql, connection);
        configure?.Invoke(command);
        using var adapter = new SqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private void Execute(string sql, Action<SqlCommand> configure)
    {
        using var connection = OpenConnection();
        using var command = new SqlCommand(sql, connection);
        configure(command);
        command.ExecuteNonQuery();
    }

    private SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }

    private static object DbValue(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();
    }
}
