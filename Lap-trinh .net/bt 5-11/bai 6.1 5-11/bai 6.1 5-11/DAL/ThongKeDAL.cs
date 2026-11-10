using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalesSystem.DAL
{
    public class ThongKeDAL
    {
        // 1. Dùng ExecuteScalar tính tổng doanh thu hôm nay
        public decimal TinhTongDoanhThuHomNay()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string query = "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE CAST(NgayLap AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        // 2. Dùng SqlDataReader đọc Top 5 sản phẩm bán chạy nhất
        public List<string> LayTop5SanPhamBanChay()
        {
            List<string> result = new List<string>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string query = @"SELECT TOP 5 sp.TenSP, SUM(ct.SoLuong) AS TongBan
                                 FROM ChiTietHoaDon ct
                                 JOIN SanPham sp ON ct.MaSP = sp.MaSP
                                 GROUP BY sp.TenSP
                                 ORDER BY TongBan DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add($"{reader["TenSP"]} - Đã bán: {reader["TongBan"]}");
                    }
                }
            }
            return result;
        }
    }
}