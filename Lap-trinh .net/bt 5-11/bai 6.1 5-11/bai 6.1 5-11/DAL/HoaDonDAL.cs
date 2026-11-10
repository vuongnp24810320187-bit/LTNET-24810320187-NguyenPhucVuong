using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SalesSystem.DTO;

namespace SalesSystem.DAL
{
    public class HoaDonDAL
    {
        public bool ThanhToanHoaDon(HoaDonDTO hoaDon, List<ChiTietHoaDonDTO> chiTietList, out string thongBaoLoi)
        {
            thongBaoLoi = string.Empty;
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                // BẮT ĐẦU TRANSACTION ĐẢM BẢO ACID
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Chèn hóa đơn và lấy MaHD mới tạo (SCOPE_IDENTITY)
                    string sqlInsertHD = @"INSERT INTO HoaDon (NgayLap, MaNV, MaKH, TongTien) 
                                          VALUES (GETDATE(), @MaNV, @MaKH, @TongTien);
                                          SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdHD = new SqlCommand(sqlInsertHD, conn, trans);
                    cmdHD.Parameters.Add("@MaNV", SqlDbType.Int).Value = hoaDon.MaNV;
                    cmdHD.Parameters.Add("@MaKH", SqlDbType.Int).Value = (object)hoaDon.MaKH ?? DBNull.Value;
                    cmdHD.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = hoaDon.TongTien;

                    int newMaHD = Convert.ToInt32(cmdHD.ExecuteScalar());

                    // 2. Chèn ChiTietHoaDon và Trừ Tồn Kho
                    foreach (var item in chiTietList)
                    {
                        // a. Kiểm tra và Trừ kho: UPDATE nguyên tử (Atomic Update)
                        // Chỉ trừ kho nếu SoLuongTon >= SoLuong bán
                        string sqlUpdateKho = @"UPDATE SanPham 
                                               SET SoLuongTon = SoLuongTon - @SoLuong 
                                               WHERE MaSP = @MaSP AND SoLuongTon >= @SoLuong";

                        SqlCommand cmdKho = new SqlCommand(sqlUpdateKho, conn, trans);
                        cmdKho.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.SoLuong;
                        cmdKho.Parameters.Add("@MaSP", SqlDbType.VarChar).Value = item.MaSP;

                        int rowsAffected = cmdKho.ExecuteNonQuery();

                        // Nếu không dòng nào bị ảnh hưởng -> Không đủ số lượng tồn!
                        if (rowsAffected == 0)
                        {
                            throw new Exception($"Sản phẩm [{item.TenSP}] không đủ số lượng tồn trong kho!");
                        }

                        // b. Chèn vào bảng ChiTietHoaDon
                        string sqlInsertCT = @"INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) 
                                              VALUES (@MaHD, @MaSP, @SoLuong, @DonGia)";
                        SqlCommand cmdCT = new SqlCommand(sqlInsertCT, conn, trans);
                        cmdCT.Parameters.Add("@MaHD", SqlDbType.Int).Value = newMaHD;
                        cmdCT.Parameters.Add("@MaSP", SqlDbType.VarChar).Value = item.MaSP;
                        cmdCT.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.SoLuong;
                        cmdCT.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = item.DonGia;
                        cmdCT.ExecuteNonQuery();
                    }

                    // 3. Tích điểm cho khách hàng (Nếu có khách hàng: 100,000 VND = 1 điểm)
                    if (hoaDon.MaKH.HasValue && hoaDon.MaKH.Value > 1) // Bỏ qua khách lẻ (MaKH = 1)
                    {
                        int diemCong = (int)(hoaDon.TongTien / 100000);
                        if (diemCong > 0)
                        {
                            string sqlDiem = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @Diem WHERE MaKH = @MaKH";
                            SqlCommand cmdDiem = new SqlCommand(sqlDiem, conn, trans);
                            cmdDiem.Parameters.Add("@Diem", SqlDbType.Int).Value = diemCong;
                            cmdDiem.Parameters.Add("@MaKH", SqlDbType.Int).Value = hoaDon.MaKH.Value;
                            cmdDiem.ExecuteNonQuery();
                        }
                    }

                    // NẾU TẤT CẢ THÀNH CÔNG -> COMMIT
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // GẶP BẤT KỲ LỖI NÀO -> ROLLBACK NGAY LẬP TỨC
                    trans.Rollback();
                    thongBaoLoi = ex.Message;
                    return false;
                }
            }
        }
    }
}