using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HRM_PayrollSystem
{
    public partial class Form1 : Form
    {
        private string strConn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HRMDB;Integrated Security=True;TrustServerCertificate=True";
        // Đối tượng ngắt kết nối dùng cho Tab Chấm Công
        private SqlDataAdapter daChamCong;
        private DataTable dtChamCong;
        private SqlCommandBuilder cmdBuilder;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboBoxPhongBan();
            cboTieuChiTim.Items.AddRange(new string[] { "Tên", "Phòng ban", "CCCD" });
            cboTieuChiTim.SelectedIndex = 0;

            LoadDanhSachNhanVien();
        }

        #region 1. CHỨC NĂNG QUẢN LÝ NHÂN SỰ (3-TIER CRUD & SEARCH)

        private void LoadComboBoxPhongBan()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPB, TenPB FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboPhongBan.DataSource = dt;
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";
            }
        }

        private void LoadDanhSachNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, 
                                      pb.TenPB, nv.MaPB, nv.LuongCoBan, nv.HeSoLuong 
                               FROM NhanVien nv 
                               JOIN PhongBan pb ON nv.MaPB = pb.MaPB";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhanVien.DataSource = dt;

                // Ẩn cột MaPB không cần hiện
                if (dgvNhanVien.Columns.Contains("MaPB"))
                    dgvNhanVien.Columns["MaPB"].Visible = false;
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên và CCCD!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) 
                                     VALUES (@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPB", cboPhongBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                    cmd.Parameters.AddWithValue("@HeSoLuong", (double)numHeSoLuong.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadDanhSachNhanVien();
                    btnLamMoiNV_Click(null, null);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Chọn nhân viên cần sửa từ danh sách!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE NhanVien 
                                     SET HoTen = @HoTen, NgaySinh = @NgaySinh, CCCD = @CCCD, 
                                         MaPB = @MaPB, LuongCoBan = @LuongCoBan, HeSoLuong = @HeSoLuong 
                                     WHERE MaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", int.Parse(txtMaNV.Text));
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPB", cboPhongBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                    cmd.Parameters.AddWithValue("@HeSoLuong", (double)numHeSoLuong.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                    LoadDanhSachNhanVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaNV", int.Parse(txtMaNV.Text));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Xóa nhân viên thành công!");
                        LoadDanhSachNhanVien();
                        btnLamMoiNV_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể xóa (có thể đã tồn tại dữ liệu lương/chấm công): " + ex.Message);
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string sql = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, 
                                      pb.TenPB, nv.MaPB, nv.LuongCoBan, nv.HeSoLuong 
                               FROM NhanVien nv 
                               JOIN PhongBan pb ON nv.MaPB = pb.MaPB WHERE ";

                string tieuChi = cboTieuChiTim.SelectedItem.ToString();
                if (tieuChi == "Tên") sql += "nv.HoTen LIKE @keyword";
                else if (tieuChi == "Phòng ban") sql += "pb.TenPB LIKE @keyword";
                else if (tieuChi == "CCCD") sql += "nv.CCCD LIKE @keyword";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + txtTimKiem.Text.Trim() + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhanVien.DataSource = dt;
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                cboPhongBan.SelectedValue = row.Cells["MaPB"].Value;
                numLuongCoBan.Value = Convert.ToDecimal(row.Cells["LuongCoBan"].Value);
                numHeSoLuong.Value = Convert.ToDecimal(row.Cells["HeSoLuong"].Value);
            }
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtCCCD.Clear();
            numLuongCoBan.Value = 10000000;
            numHeSoLuong.Value = 1.0M;
            dtpNgaySinh.Value = DateTime.Now;
            LoadDanhSachNhanVien();
        }

        #endregion

        #region 2. CHẤM CÔNG NGẮT KẾT NỐI (DISCONNECTED MODEL)

        private void btnNapChamCong_Click(object sender, EventArgs e)
        {
            string thangNam = txtThangNamCong.Text.Trim();
            if (string.IsNullOrEmpty(thangNam))
            {
                MessageBox.Show("Vui lòng nhập Tháng/Năm (vd: 11/2026)");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                // 1. Tự động thêm bản ghi chấm công nếu nhân viên chưa có trong tháng được chọn
                string sqlInit = @"INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT)
                                   SELECT MaNV, @ThangNam, 0, 0 
                                   FROM NhanVien 
                                   WHERE MaNV NOT IN (SELECT MaNV FROM BangChamCong WHERE ThangNam = @ThangNam)";
                SqlCommand cmdInit = new SqlCommand(sqlInit, conn);
                cmdInit.Parameters.AddWithValue("@ThangNam", thangNam);
                cmdInit.ExecuteNonQuery();

                // 2. Sử dụng SqlDataAdapter để nạp vào DataTable (Disconnected Model)
                string selectSql = @"SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT 
                                     FROM BangChamCong 
                                     WHERE ThangNam = @ThangNam";
                SqlCommand cmdSelect = new SqlCommand(selectSql, conn);
                cmdSelect.Parameters.AddWithValue("@ThangNam", thangNam);

                daChamCong = new SqlDataAdapter(cmdSelect);
                cmdBuilder = new SqlCommandBuilder(daChamCong);

                dtChamCong = new DataTable();
                daChamCong.Fill(dtChamCong);

                dgvChamCong.DataSource = dtChamCong;

                // Khóa không cho sửa MaCong, MaNV, ThangNam trực tiếp trên lưới
                dgvChamCong.Columns["MaCong"].ReadOnly = true;
                dgvChamCong.Columns["MaNV"].ReadOnly = true;
                dgvChamCong.Columns["ThangNam"].ReadOnly = true;
            }
        }

        private void btnLuuChamCong_Click(object sender, EventArgs e)
        {
            if (daChamCong == null || dtChamCong == null)
            {
                MessageBox.Show("Chưa có dữ liệu để lưu!");
                return;
            }

            try
            {
                // Cấp lại chuỗi kết nối còn sống trước khi Update
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    daChamCong.SelectCommand.Connection = conn;
                    cmdBuilder = new SqlCommandBuilder(daChamCong);

                    // Đồng bộ toàn bộ DataTable về SQL Server
                    daChamCong.Update(dtChamCong);
                }

                MessageBox.Show("Đã đồng bộ dữ liệu chấm công thành công vào Database!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu bảng chấm công: " + ex.Message);
            }
        }

        #endregion

        #region 3. TÍNH LƯƠNG & CHỐT LƯƠNG (SQLTRANSACTION)

        private void btnXemLuong_Click(object sender, EventArgs e)
        {
            string thangNam = txtThangNamLuong.Text.Trim();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                // Tính toán trực tiếp theo công thức
                string sql = @"SELECT nv.MaNV, nv.HoTen, cc.ThangNam, cc.SoNgayCong, cc.SoGioOT,
                               ROUND(((nv.LuongCoBan * CAST(nv.HeSoLuong AS DECIMAL(18,2)) / 22) * CAST(cc.SoNgayCong AS DECIMAL(18,2))) 
                                     + (CAST(cc.SoGioOT AS DECIMAL(18,2)) * 50000), 2) AS ThucLinh
                               FROM BangChamCong cc
                               JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                               WHERE cc.ThangNam = @ThangNam";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ThangNam", thangNam);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLuong.DataSource = dt;
            }
        }

        private void btnChotLuong_Click(object sender, EventArgs e)
        {
            string thangNam = txtThangNamLuong.Text.Trim();
            if (string.IsNullOrEmpty(thangNam))
            {
                MessageBox.Show("Vui lòng nhập Tháng/Năm để chốt lương!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                // Bắt đầu một SqlTransaction
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Bước 1: Xóa bảng lương cũ của tháng này (nếu đã từng chốt trước đó)
                    string sqlDelete = "DELETE FROM BangLuong WHERE ThangNam = @ThangNam";
                    SqlCommand cmdDelete = new SqlCommand(sqlDelete, conn, transaction);
                    cmdDelete.Parameters.AddWithValue("@ThangNam", thangNam);
                    cmdDelete.ExecuteNonQuery();

                    // Bước 2: Tính toán và Ghi nhận toàn bộ dữ liệu vào BangLuong
                    string sqlInsert = @"
                        INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh)
                        SELECT nv.MaNV, cc.ThangNam,
                               ROUND(((nv.LuongCoBan * CAST(nv.HeSoLuong AS DECIMAL(18,2)) / 22) * CAST(cc.SoNgayCong AS DECIMAL(18,2))) 
                                     + (CAST(cc.SoGioOT AS DECIMAL(18,2)) * 50000), 2) AS ThucLinh
                        FROM BangChamCong cc
                        JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                        WHERE cc.ThangNam = @ThangNam";

                    SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction);
                    cmdInsert.Parameters.AddWithValue("@ThangNam", thangNam);
                    int rowsAffected = cmdInsert.ExecuteNonQuery();

                    // Xác nhận lưu toàn bộ thay đổi
                    transaction.Commit();

                    MessageBox.Show($"Chốt lương tháng {thangNam} thành công cho {rowsAffected} nhân viên!", "Thành công");

                    // Hiển thị lại bảng lương vừa chốt
                    btnXemLuong_Click(null, null);
                }
                catch (Exception ex)
                {
                    // Rollback nếu có bất kỳ lỗi nào xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi! Hoàn tác toàn bộ giao dịch. Chi tiết: " + ex.Message);
                }
            }
        }

        #endregion
    }
}