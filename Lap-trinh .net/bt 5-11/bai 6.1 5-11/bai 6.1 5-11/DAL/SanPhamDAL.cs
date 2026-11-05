using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SalesSystem.DTO;

namespace SalesSystem.DAL
{
    public class SanPhamDAL
    {
        public List<SanPhamDTO> LayDanhSachSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string query = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPhamDTO
                        {
                            MaSP = reader["MaSP"].ToString(),
                            TenSP = reader["TenSP"].ToString(),
                            GiaBan = (decimal)reader["GiaBan"],
                            SoLuongTon = (int)reader["SoLuongTon"]
                        });
                    }
                }
            }
            return list;
        }

        // Chống SQL Injection bằng SqlParameter với ký tự đại diện %
        public List<SanPhamDTO> TimKiemSanPham(string keyword)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                string query = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham " +
                               "WHERE MaSP LIKE @Keyword OR TenSP LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = "%" + keyword + "%";

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPhamDTO
                        {
                            MaSP = reader["MaSP"].ToString(),
                            TenSP = reader["TenSP"].ToString(),
                            GiaBan = (decimal)reader["GiaBan"],
                            SoLuongTon = (int)reader["SoLuongTon"]
                        });
                    }
                }
            }
            return list;
        }
    }
}