using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MultiServiceEnterpriseSystem.DAL
{
    public class SecurityAuditDAL
    {
        private const string DbName = "KaraokeDB";

        // ====================================================================
        // PHẦN 1: MINH HỌA LỖ HỔNG SQL INJECTION (UNSAFE VS SAFE)
        // ====================================================================

        // 1.1. Chạy Không an toàn: Ghép chuỗi thô (Dễ bị bypass WHERE)
        public (DataTable table, string executedQuery) ExecuteUnsafeSearch(string inputKeyword)
        {
            // Lỗ hổng nguy hiểm: Nối chuỗi trực tiếp!
            string sql = "SELECT MaPhong, TenPhong, LoaiPhong, GiaTheoGio, TrangThai FROM dbo.PhongHat WHERE TenPhong = '" + inputKeyword + "'";
            var dt = DbHelper.ExecuteQuery(DbName, sql);
            return (dt, sql);
        }

        // 1.2. Chạy An toàn: Sử dụng SqlParameter chuẩn ADO.NET
        public (DataTable table, string executedQuery) ExecuteSafeSearch(string inputKeyword)
        {
            const string sql = "SELECT MaPhong, TenPhong, LoaiPhong, GiaTheoGio, TrangThai FROM dbo.PhongHat WHERE TenPhong = @Keyword";
            var param = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50) { Value = inputKeyword };
            var dt = DbHelper.ExecuteQuery(DbName, sql, param);
            string displayQuery = $"SELECT MaPhong, TenPhong, LoaiPhong, GiaTheoGio, TrangThai FROM dbo.PhongHat WHERE TenPhong = @Keyword [Param @Keyword = N'{inputKeyword}']";
            return (dt, displayQuery);
        }

        // ====================================================================
        // PHẦN 2: MINH HỌA GIAO DỊCH TOÀN VẸN (ACID SQLTRANSACTION ROLLBACK)
        // Giả lập giao dịch gọi dịch vụ với số lượng vượt quá tồn kho để kích hoạt Rollback
        // ====================================================================
        public (bool success, string auditLog) SimulateTransactionRollback(bool forceFailure)
        {
            var log = new System.Text.StringBuilder();
            log.AppendLine($"[{DateTime.Now:HH:mm:ss}] BẮT ĐẦU GIAO DỊCH KIỂM THỬ ACID (SqlTransaction)...");

            using var conn = DbHelper.GetConnection(DbName);
            conn.Open();
            using var trans = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                // Bước 1: Giả lập tạo hóa đơn tạm
                const string insertHd = "INSERT INTO dbo.HoaDon (MaPhong, GioVao, TongTienPhong, TongTienDichVu, TrangThai) VALUES (1, GETDATE(), 0, 0, 0); SELECT CAST(SCOPE_IDENTITY() AS int);";
                int tempHdId;
                using (var cmd1 = new SqlCommand(insertHd, conn, trans))
                {
                    tempHdId = Convert.ToInt32(cmd1.ExecuteScalar());
                    log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [Bước 1: Thành công] Đã tạo Hóa đơn thử nghiệm ID: #{tempHdId}");
                }

                // Bước 2: Kiểm tra tồn kho dịch vụ ID 1
                int currentStock;
                const string checkStock = "SELECT TonKho FROM dbo.DichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaDV = 1;";
                using (var cmd2 = new SqlCommand(checkStock, conn, trans))
                {
                    currentStock = Convert.ToInt32(cmd2.ExecuteScalar());
                    log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [Bước 2: Kiểm tra] Dịch vụ #1 hiện có tồn kho: {currentStock} sản phẩm.");
                }

                if (forceFailure)
                {
                    int requestQuantity = currentStock + 999;
                    log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [Kích hoạt sự cố] Khách yêu cầu: {requestQuantity} sản phẩm (vượt quá tồn kho {currentStock})!");
                    throw new InvalidOperationException($"Số lượng đặt hàng ({requestQuantity}) vượt quá tồn kho khả dụng ({currentStock})!");
                }

                // Nếu không lỗi thì ghi bình thường
                trans.Commit();
                log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [HOÀN TẤT] Transaction đã COMMIT thành công vào SQL Server!");
                return (true, log.ToString());
            }
            catch (Exception ex)
            {
                trans.Rollback();
                log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [PHÁT HIỆN LỖI]: {ex.Message}");
                log.AppendLine($"[{DateTime.Now:HH:mm:ss}] >>> KÍCH HOẠT LỆNH: transaction.Rollback()");
                log.AppendLine($"[{DateTime.Now:HH:mm:ss}] [KẾT QUẢ ROLLBACK]: Toàn bộ dữ liệu (Hóa đơn rác #{0}, cập nhật tồn kho) đã được phục hồi nguyên trạng 100%!");
                return (false, log.ToString());
            }
        }
    }
}

