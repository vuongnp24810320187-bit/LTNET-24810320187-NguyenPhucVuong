using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.DAL
{
    public class GarageDAL
    {
        private const string DbName = "GarageDB";

        public List<KhachHangDTO> GetCustomers()
        {
            var list = new List<KhachHangDTO>();
            const string sql = "SELECT MaKH, TenKH, DienThoai, DiaChi FROM dbo.KhachHang ORDER BY MaKH DESC";
            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KhachHangDTO
                {
                    MaKH = reader.GetInt32(0),
                    TenKH = reader.GetString(1),
                    DienThoai = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    DiaChi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                });
            }
            return list;
        }

        public bool InsertCustomer(KhachHangDTO kh)
        {
            const string sql = "INSERT INTO dbo.KhachHang (TenKH, DienThoai, DiaChi) VALUES (@TenKH, @DienThoai, @DiaChi)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TenKH", kh.TenKH),
                new SqlParameter("@DienThoai", (object?)kh.DienThoai ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object?)kh.DiaChi ?? DBNull.Value)) > 0;
        }

        public bool UpdateCustomer(KhachHangDTO kh)
        {
            const string sql = "UPDATE dbo.KhachHang SET TenKH = @TenKH, DienThoai = @DienThoai, DiaChi = @DiaChi WHERE MaKH = @MaKH";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@TenKH", kh.TenKH),
                new SqlParameter("@DienThoai", (object?)kh.DienThoai ?? DBNull.Value),
                new SqlParameter("@DiaChi", (object?)kh.DiaChi ?? DBNull.Value)) > 0;
        }

        public bool DeleteCustomer(int maKH)
        {
            const string sql = "DELETE FROM dbo.KhachHang WHERE MaKH = @MaKH";
            return DbHelper.ExecuteNonQuery(DbName, sql, new SqlParameter("@MaKH", maKH)) > 0;
        }

        public List<XeDTO> GetVehicles()
        {
            var list = new List<XeDTO>();
            const string sql = @"
                SELECT x.BienSo, x.MaKH, kh.TenKH, x.HieuXe, x.NamSanXuat
                FROM dbo.Xe x
                INNER JOIN dbo.KhachHang kh ON kh.MaKH = x.MaKH
                ORDER BY x.BienSo";

            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new XeDTO
                {
                    BienSo = reader.GetString(0),
                    MaKH = reader.GetInt32(1),
                    TenKH = reader.GetString(2),
                    HieuXe = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    NamSanXuat = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                });
            }
            return list;
        }

        public bool InsertVehicle(XeDTO xe)
        {
            const string sql = "INSERT INTO dbo.Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES (@BienSo, @MaKH, @HieuXe, @NamSanXuat)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@BienSo", xe.BienSo),
                new SqlParameter("@MaKH", xe.MaKH),
                new SqlParameter("@HieuXe", (object?)xe.HieuXe ?? DBNull.Value),
                new SqlParameter("@NamSanXuat", (object?)xe.NamSanXuat ?? DBNull.Value)) > 0;
        }

        public bool DeleteVehicle(string bienSo)
        {
            const string sql = "DELETE FROM dbo.Xe WHERE BienSo = @BienSo";
            return DbHelper.ExecuteNonQuery(DbName, sql, new SqlParameter("@BienSo", bienSo)) > 0;
        }

        public List<PhuTungDTO> GetParts()
        {
            var list = new List<PhuTungDTO>();
            const string sql = "SELECT MaPhuTung, TenPhuTung, DonGia, SoLuongTon FROM dbo.PhuTung ORDER BY MaPhuTung DESC";
            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhuTungDTO
                {
                    MaPhuTung = reader.GetInt32(0),
                    TenPhuTung = reader.GetString(1),
                    DonGia = reader.GetDecimal(2),
                    SoLuongTon = reader.GetInt32(3)
                });
            }
            return list;
        }

        public bool InsertPart(PhuTungDTO pt)
        {
            const string sql = "INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (@TenPhuTung, @DonGia, @SoLuongTon)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TenPhuTung", pt.TenPhuTung),
                new SqlParameter("@DonGia", pt.DonGia),
                new SqlParameter("@SoLuongTon", pt.SoLuongTon)) > 0;
        }

        public bool UpdatePart(PhuTungDTO pt)
        {
            const string sql = "UPDATE dbo.PhuTung SET TenPhuTung = @TenPhuTung, DonGia = @DonGia, SoLuongTon = @SoLuongTon WHERE MaPhuTung = @MaPhuTung";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@MaPhuTung", pt.MaPhuTung),
                new SqlParameter("@TenPhuTung", pt.TenPhuTung),
                new SqlParameter("@DonGia", pt.DonGia),
                new SqlParameter("@SoLuongTon", pt.SoLuongTon)) > 0;
        }

        public bool DeletePart(int maPT)
        {
            const string sql = "DELETE FROM dbo.PhuTung WHERE MaPhuTung = @MaPhuTung";
            return DbHelper.ExecuteNonQuery(DbName, sql, new SqlParameter("@MaPhuTung", maPT)) > 0;
        }

        // ====================================================================
        // THỰC THI NGHIỆP VỤ: LẬP PHIẾU SỬA CHỮA & TRỪ TỒN KHO VỚI SQLTRANSACTION
        // ====================================================================
        public int CreateRepairOrder(string bienSo, string noiDung, List<ChiTietSuaChuaDTO> details)
        {
            using var conn = DbHelper.GetConnection(DbName);
            conn.Open();
            using var transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                decimal tongTien = 0;
                foreach (var item in details)
                {
                    tongTien += item.ThanhTien;
                }

                // 1. Tạo phiếu sửa chữa
                const string insertPhieuSql = @"
                    INSERT INTO dbo.PhieuSuaChua (BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai)
                    VALUES (@BienSo, GETDATE(), @NoiDung, @TongTien, N'Đã hoàn thành');
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int maPhieu;
                using (var cmd = new SqlCommand(insertPhieuSql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@BienSo", bienSo);
                    cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                    cmd.Parameters.AddWithValue("@TongTien", tongTien);
                    maPhieu = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Ghi chi tiết và trừ kho
                foreach (var item in details)
                {
                    // Kiểm tra tồn kho
                    const string checkStockSql = "SELECT SoLuongTon FROM dbo.PhuTung WITH (UPDLOCK, HOLDLOCK) WHERE MaPhuTung = @MaPT;";
                    using (var checkCmd = new SqlCommand(checkStockSql, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@MaPT", item.MaPhuTung);
                        var stockObj = checkCmd.ExecuteScalar();
                        if (stockObj == null)
                        {
                            throw new InvalidOperationException($"Phụ tùng mã {item.MaPhuTung} không tồn tại!");
                        }
                        int stock = Convert.ToInt32(stockObj);
                        if (item.SoLuong > stock)
                        {
                            throw new InvalidOperationException($"Phụ tùng '{item.TenPhuTung}' yêu cầu {item.SoLuong} nhưng chỉ còn {stock} trong kho!");
                        }
                    }

                    // Insert ChiTietSuaChua
                    const string insertDetailSql = @"
                        INSERT INTO dbo.ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong)
                        VALUES (@MaPhieu, @MaPT, @SoLuong, @TienCong);";
                    using (var insCmd = new SqlCommand(insertDetailSql, conn, transaction))
                    {
                        insCmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        insCmd.Parameters.AddWithValue("@MaPT", item.MaPhuTung);
                        insCmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                        insCmd.Parameters.AddWithValue("@TienCong", item.TienCong);
                        insCmd.ExecuteNonQuery();
                    }

                    // Trừ tồn kho
                    const string deductSql = "UPDATE dbo.PhuTung SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaPhuTung = @MaPT;";
                    using (var decCmd = new SqlCommand(deductSql, conn, transaction))
                    {
                        decCmd.Parameters.AddWithValue("@MaPT", item.MaPhuTung);
                        decCmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                        decCmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                return maPhieu;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public decimal GetTotalRevenue()
        {
            const string sql = "SELECT ISNULL(SUM(TongTien), 0) FROM dbo.PhieuSuaChua;";
            var res = DbHelper.ExecuteScalar(DbName, sql);
            return res != null && res != DBNull.Value ? Convert.ToDecimal(res) : 0;
        }

        public DataTable GetTopPartsReport()
        {
            const string sql = @"
                SELECT TOP 5 pt.TenPhuTung AS [Tên phụ tùng],
                             SUM(ct.SoLuong) AS [Số lượng đã dùng],
                             SUM(ct.SoLuong * pt.DonGia + ct.TienCong) AS [Tổng tiền phát sinh (VNĐ)]
                FROM dbo.ChiTietSuaChua ct
                INNER JOIN dbo.PhuTung pt ON pt.MaPhuTung = ct.MaPhuTung
                GROUP BY pt.TenPhuTung
                ORDER BY [Số lượng đã dùng] DESC;";

            return DbHelper.ExecuteQuery(DbName, sql);
        }
    }
}

