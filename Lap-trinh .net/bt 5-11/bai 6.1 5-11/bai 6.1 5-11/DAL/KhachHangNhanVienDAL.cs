using System.Collections.Generic;
using System.Data.SqlClient;
using SalesSystem.DTO;

namespace SalesSystem.DAL
{
    public class KhachHangDAL
    {
        public List<KhachHangDTO> LayDanhSachKhachHang()
        {
            List<KhachHangDTO> list = new List<KhachHangDTO>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM KhachHang", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhachHangDTO
                        {
                            MaKH = (int)reader["MaKH"],
                            TenKH = reader["TenKH"].ToString(),
                            DienThoai = reader["DienThoai"].ToString(),
                            DiemTichLuy = (int)reader["DiemTichLuy"]
                        });
                    }
                }
            }
            return list;
        }
    }

    public class NhanVienDAL
    {
        public List<NhanVienDTO> LayDanhSachNhanVien()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM NhanVien", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVienDTO
                        {
                            MaNV = (int)reader["MaNV"],
                            TenNV = reader["TenNV"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString(),
                            ChucVu = reader["ChucVu"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}