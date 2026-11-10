using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.DAL
{
    public class KaraokeDAL
    {
        private const string DbName = "KaraokeDB";

        public List<PhongHatDTO> GetRooms()
        {
            var list = new List<PhongHatDTO>();
            const string sql = @"
                SELECT p.MaPhong, p.TenPhong, p.LoaiPhong, p.GiaTheoGio, p.TrangThai, h.MaHD, h.GioVao
                FROM dbo.PhongHat AS p
                OUTER APPLY (
                    SELECT TOP (1) MaHD, GioVao 
                    FROM dbo.HoaDon 
                    WHERE MaPhong = p.MaPhong AND TrangThai = 0 
                    ORDER BY MaHD DESC
                ) AS h
                ORDER BY p.MaPhong;";

            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhongHatDTO
                {
                    MaPhong = reader.GetInt32(0),
                    TenPhong = reader.GetString(1),
                    LoaiPhong = reader.IsDBNull(2) ? "Thường" : reader.GetString(2),
                    GiaTheoGio = reader.GetDecimal(3),
                    TrangThai = reader.GetString(4),
                    MaHD = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    GioVao = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                });
            }
            return list;
        }

        public bool InsertRoom(PhongHatDTO room)
        {
            const string sql = "INSERT INTO dbo.PhongHat (TenPhong, LoaiPhong, GiaTheoGio, TrangThai) VALUES (@TenPhong, @LoaiPhong, @GiaTheoGio, @TrangThai)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TenPhong", room.TenPhong),
                new SqlParameter("@LoaiPhong", room.LoaiPhong ?? "Thường"),
                new SqlParameter("@GiaTheoGio", room.GiaTheoGio),
                new SqlParameter("@TrangThai", room.TrangThai ?? "Trống")) > 0;
        }

        public bool UpdateRoom(PhongHatDTO room)
        {
            const string sql = "UPDATE dbo.PhongHat SET TenPhong = @TenPhong, LoaiPhong = @LoaiPhong, GiaTheoGio = @GiaTheoGio, TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@MaPhong", room.MaPhong),
                new SqlParameter("@TenPhong", room.TenPhong),
                new SqlParameter("@LoaiPhong", room.LoaiPhong ?? "Thường"),
                new SqlParameter("@GiaTheoGio", room.GiaTheoGio),
                new SqlParameter("@TrangThai", room.TrangThai ?? "Trống")) > 0;
        }

        public bool DeleteRoom(int roomId)
        {
            const string sql = "DELETE FROM dbo.PhongHat WHERE MaPhong = @MaPhong";
            return DbHelper.ExecuteNonQuery(DbName, sql, new SqlParameter("@MaPhong", roomId)) > 0;
        }

        public List<DichVuDTO> GetServices()
        {
            var list = new List<DichVuDTO>();
            const string sql = "SELECT MaDV, TenDV, DonGia, TonKho FROM dbo.DichVu ORDER BY TenDV";
            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DichVuDTO
                {
                    MaDV = reader.GetInt32(0),
                    TenDV = reader.GetString(1),
                    DonGia = reader.GetDecimal(2),
                    TonKho = reader.GetInt32(3)
                });
            }
            return list;
        }

        public bool InsertService(DichVuDTO dv)
        {
            const string sql = "INSERT INTO dbo.DichVu (TenDV, DonGia, TonKho) VALUES (@TenDV, @DonGia, @TonKho)";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@TenDV", dv.TenDV),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@TonKho", dv.TonKho)) > 0;
        }

        public bool UpdateService(DichVuDTO dv)
        {
            const string sql = "UPDATE dbo.DichVu SET TenDV = @TenDV, DonGia = @DonGia, TonKho = @TonKho WHERE MaDV = @MaDV";
            return DbHelper.ExecuteNonQuery(DbName, sql,
                new SqlParameter("@MaDV", dv.MaDV),
                new SqlParameter("@TenDV", dv.TenDV),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@TonKho", dv.TonKho)) > 0;
        }

        public bool DeleteService(int dvId)
        {
            const string sql = "DELETE FROM dbo.DichVu WHERE MaDV = @MaDV";
            return DbHelper.ExecuteNonQuery(DbName, sql, new SqlParameter("@MaDV", dvId)) > 0;
        }

        public List<ChiTietDichVuDTO> GetInvoiceServices(int invoiceId)
        {
            var list = new List<ChiTietDichVuDTO>();
            const string sql = @"
                SELECT ct.MaHD, ct.MaDV, dv.TenDV, ct.SoLuong, ct.DonGia
                FROM dbo.ChiTietDichVu AS ct
                INNER JOIN dbo.DichVu AS dv ON dv.MaDV = ct.MaDV
                WHERE ct.MaHD = @MaHD
                ORDER BY dv.TenDV;";

            using var conn = DbHelper.GetConnection(DbName);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaHD", invoiceId);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ChiTietDichVuDTO
                {
                    MaHD = reader.GetInt32(0),
                    MaDV = reader.GetInt32(1),
                    TenDV = reader.GetString(2),
                    SoLuong = reader.GetInt32(3),
                    DonGia = reader.GetDecimal(4)
                });
            }
            return list;
        }

        // ====================================================================
        // THỰC THI NGHIỆP VỤ CHÍNH: ĐẶT/MỞ PHÒNG BẰNG SQLTRANSACTION
        // ====================================================================
        public int OpenRoom(int roomId)
        {
            using var conn = DbHelper.GetConnection(DbName);
            conn.Open();
            using var transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                // Kiểm tra trạng thái phòng
                const string checkSql = "SELECT TrangThai FROM dbo.PhongHat WITH (UPDLOCK, HOLDLOCK) WHERE MaPhong = @MaPhong;";
                using (var checkCmd = new SqlCommand(checkSql, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@MaPhong", roomId);
                    var status = checkCmd.ExecuteScalar()?.ToString();
                    if (status != "Trống")
                    {
                        throw new InvalidOperationException("Phòng này hiện đang có khách hát hoặc không khả dụng!");
                    }
                }

                // Cập nhật trạng thái phòng sang Đang hát
                const string updateRoomSql = "UPDATE dbo.PhongHat SET TrangThai = N'Đang hát' WHERE MaPhong = @MaPhong;";
                using (var updateCmd = new SqlCommand(updateRoomSql, conn, transaction))
                {
                    updateCmd.Parameters.AddWithValue("@MaPhong", roomId);
                    updateCmd.ExecuteNonQuery();
                }

                // Tạo hóa đơn mới
                const string insertHdSql = @"
                    INSERT INTO dbo.HoaDon (MaPhong, GioVao, TongTienPhong, TongTienDichVu, TrangThai)
                    VALUES (@MaPhong, GETDATE(), 0, 0, 0);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";
                int newInvoiceId;
                using (var insertCmd = new SqlCommand(insertHdSql, conn, transaction))
                {
                    insertCmd.Parameters.AddWithValue("@MaPhong", roomId);
                    newInvoiceId = Convert.ToInt32(insertCmd.ExecuteScalar());
                }

                transaction.Commit();
                return newInvoiceId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ====================================================================
        // THỰC THI NGHIỆP VỤ CHÍNH: GỌI DỊCH VỤ & TRỪ KHO VỚI SQLTRANSACTION
        // ====================================================================
        public void OrderService(int invoiceId, int serviceId, int quantity, decimal unitPrice)
        {
            using var conn = DbHelper.GetConnection(DbName);
            conn.Open();
            using var transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                // Kiểm tra hóa đơn và tồn kho với khóa UPDLOCK
                const string checkStockSql = @"
                    SELECT dv.TonKho
                    FROM dbo.HoaDon AS hd WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN dbo.DichVu AS dv WITH (UPDLOCK, HOLDLOCK) ON dv.MaDV = @MaDV
                    WHERE hd.MaHD = @MaHD AND hd.TrangThai = 0;";

                using (var checkCmd = new SqlCommand(checkStockSql, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    checkCmd.Parameters.AddWithValue("@MaDV", serviceId);
                    var stockObj = checkCmd.ExecuteScalar();
                    if (stockObj == null)
                    {
                        throw new InvalidOperationException("Hóa đơn đã đóng hoặc dịch vụ không tồn tại!");
                    }
                    int stock = Convert.ToInt32(stockObj);
                    if (quantity > stock)
                    {
                        throw new InvalidOperationException($"Số lượng yêu cầu ({quantity}) vượt quá tồn kho hiện tại ({stock})!");
                    }
                }

                // Ghi nhận hoặc cộng dồn vào ChiTietDichVu
                const string upsertDetailSql = @"
                    IF EXISTS (SELECT 1 FROM dbo.ChiTietDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND MaDV = @MaDV)
                        UPDATE dbo.ChiTietDichVu 
                        SET SoLuong = SoLuong + @SoLuong, DonGia = @DonGia 
                        WHERE MaHD = @MaHD AND MaDV = @MaDV;
                    ELSE
                        INSERT INTO dbo.ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia)
                        VALUES (@MaHD, @MaDV, @SoLuong, @DonGia);";

                using (var upsertCmd = new SqlCommand(upsertDetailSql, conn, transaction))
                {
                    upsertCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    upsertCmd.Parameters.AddWithValue("@MaDV", serviceId);
                    upsertCmd.Parameters.AddWithValue("@SoLuong", quantity);
                    upsertCmd.Parameters.AddWithValue("@DonGia", unitPrice);
                    upsertCmd.ExecuteNonQuery();
                }

                // Trừ tồn kho trong bảng DichVu
                const string deductStockSql = "UPDATE dbo.DichVu SET TonKho = TonKho - @SoLuong WHERE MaDV = @MaDV;";
                using (var deductCmd = new SqlCommand(deductStockSql, conn, transaction))
                {
                    deductCmd.Parameters.AddWithValue("@MaDV", serviceId);
                    deductCmd.Parameters.AddWithValue("@SoLuong", quantity);
                    deductCmd.ExecuteNonQuery();
                }

                // Cập nhật lại tổng tiền dịch vụ trên HoaDon
                const string updateInvoiceSql = @"
                    UPDATE dbo.HoaDon
                    SET TongTienDichVu = ISNULL((SELECT SUM(SoLuong * DonGia) FROM dbo.ChiTietDichVu WHERE MaHD = @MaHD), 0)
                    WHERE MaHD = @MaHD;";
                using (var updateHdCmd = new SqlCommand(updateInvoiceSql, conn, transaction))
                {
                    updateHdCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    updateHdCmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ====================================================================
        // THỰC THI NGHIỆP VỤ CHÍNH: THANH TOÁN TRẢ PHÒNG VỚI SQLTRANSACTION
        // ====================================================================
        public (decimal roomAmount, decimal serviceAmount, decimal totalAmount, double hours) CheckoutRoom(int roomId, int invoiceId)
        {
            using var conn = DbHelper.GetConnection(DbName);
            conn.Open();
            using var transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                // Lấy giờ vào và giá phòng theo giờ
                DateTime gioVao;
                decimal giaTheoGio;
                const string infoSql = @"
                    SELECT hd.GioVao, p.GiaTheoGio
                    FROM dbo.HoaDon hd
                    INNER JOIN dbo.PhongHat p ON p.MaPhong = hd.MaPhong
                    WHERE hd.MaHD = @MaHD AND hd.TrangThai = 0;";

                using (var infoCmd = new SqlCommand(infoSql, conn, transaction))
                {
                    infoCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    using var rdr = infoCmd.ExecuteReader();
                    if (!rdr.Read())
                    {
                        throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã được thanh toán!");
                    }
                    gioVao = rdr.GetDateTime(0);
                    giaTheoGio = rdr.GetDecimal(1);
                }

                DateTime gioRa = DateTime.Now;
                TimeSpan duration = gioRa - gioVao;
                double totalHours = Math.Max(0.25, Math.Round(duration.TotalHours, 2)); // Tối thiểu 15 phút
                decimal roomAmount = Math.Round((decimal)totalHours * giaTheoGio, 0);

                // Tính tổng tiền dịch vụ
                const string sumDvSql = "SELECT ISNULL(SUM(SoLuong * DonGia), 0) FROM dbo.ChiTietDichVu WHERE MaHD = @MaHD;";
                decimal serviceAmount;
                using (var sumCmd = new SqlCommand(sumDvSql, conn, transaction))
                {
                    sumCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    serviceAmount = Convert.ToDecimal(sumCmd.ExecuteScalar());
                }

                // Cập nhật hóa đơn
                const string updateHdSql = @"
                    UPDATE dbo.HoaDon
                    SET GioRa = @GioRa, TongTienPhong = @TongTienPhong, TongTienDichVu = @TongTienDichVu, TrangThai = 1
                    WHERE MaHD = @MaHD;";
                using (var updateHdCmd = new SqlCommand(updateHdSql, conn, transaction))
                {
                    updateHdCmd.Parameters.AddWithValue("@GioRa", gioRa);
                    updateHdCmd.Parameters.AddWithValue("@TongTienPhong", roomAmount);
                    updateHdCmd.Parameters.AddWithValue("@TongTienDichVu", serviceAmount);
                    updateHdCmd.Parameters.AddWithValue("@MaHD", invoiceId);
                    updateHdCmd.ExecuteNonQuery();
                }

                // Chuyển trạng thái phòng về Trống
                const string updateRoomSql = "UPDATE dbo.PhongHat SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong;";
                using (var updateRoomCmd = new SqlCommand(updateRoomSql, conn, transaction))
                {
                    updateRoomCmd.Parameters.AddWithValue("@MaPhong", roomId);
                    updateRoomCmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return (roomAmount, serviceAmount, roomAmount + serviceAmount, totalHours);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // ====================================================================
        // BÁO CÁO THỐNG KÊ (ExecuteScalar & SqlDataReader)
        // ====================================================================
        public decimal GetTotalRevenue()
        {
            const string sql = "SELECT ISNULL(SUM(TongTienPhong + TongTienDichVu), 0) FROM dbo.HoaDon WHERE TrangThai = 1;";
            var res = DbHelper.ExecuteScalar(DbName, sql);
            return res != null && res != DBNull.Value ? Convert.ToDecimal(res) : 0;
        }

        public int GetOccupiedRoomCount()
        {
            const string sql = "SELECT COUNT(*) FROM dbo.PhongHat WHERE TrangThai = N'Đang hát';";
            var res = DbHelper.ExecuteScalar(DbName, sql);
            return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
        }

        public DataTable GetTopServicesReport()
        {
            const string sql = @"
                SELECT TOP 5 dv.TenDV AS [Tên dịch vụ], 
                             SUM(ct.SoLuong) AS [Tổng số lượng bán], 
                             SUM(ct.SoLuong * ct.DonGia) AS [Doanh thu dịch vụ (VNĐ)]
                FROM dbo.ChiTietDichVu ct
                INNER JOIN dbo.DichVu dv ON dv.MaDV = ct.MaDV
                INNER JOIN dbo.HoaDon hd ON hd.MaHD = ct.MaHD
                WHERE hd.TrangThai = 1
                GROUP BY dv.TenDV
                ORDER BY [Tổng số lượng bán] DESC;";

            return DbHelper.ExecuteQuery(DbName, sql);
        }
    }
}

