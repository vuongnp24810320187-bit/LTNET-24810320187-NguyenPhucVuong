using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bt2_28_10
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadCourseData();
        }

        // Nạp danh sách Khóa học vào ComboBox bằng Binding Data (DisplayMember, ValueMember)
        private void LoadCourseData()
        {
            // Tạo DataTable chứa danh sách khóa học
            DataTable dtCourses = new DataTable();
            dtCourses.Columns.Add("CourseID", typeof(int));
            dtCourses.Columns.Add("CourseName", typeof(string));

            dtCourses.Rows.Add(1, "Lập trình C#");
            dtCourses.Rows.Add(2, "Lập trình Java");
            dtCourses.Rows.Add(3, "Lập trình Web");
            dtCourses.Rows.Add(4, "Cơ sở dữ liệu");
            dtCourses.Rows.Add(5, "Mạng máy tính");

            // Binding dữ liệu vào ComboBox
            cboCourse.DataSource = dtCourses;
            cboCourse.DisplayMember = "CourseName";  // Hiển thị tên khóa học
            cboCourse.ValueMember = "CourseID";       // Giá trị là mã khóa học
            cboCourse.SelectedIndex = 0;
        }

        // Nút "Đăng ký": Tổng hợp và in thông tin đăng ký lên MessageBox
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Lấy thông tin Họ tên
            string fullName = txtFullName.Text.Trim();

            // Lấy Số điện thoại từ MaskedTextBox
            string phone = mtxtPhone.Text;

            // Lấy Ngày sinh từ DateTimePicker
            string birthDate = dtpBirthDate.Value.ToString("dd/MM/yyyy");

            // Lấy Giới tính từ RadioButton
            string gender = rdoMale.Checked ? "Nam" : "Nữ";

            // Lấy Khóa học từ ComboBox
            string courseName = cboCourse.Text;
            string courseID = cboCourse.SelectedValue.ToString();

            // Lấy Dịch vụ đăng ký thêm từ CheckBox
            List<string> services = new List<string>();
            if (chkDormitory.Checked) services.Add("Ký túc xá");
            if (chkTransport.Checked) services.Add("Xe đưa đón");
            if (chkMeal.Checked) services.Add("Suất ăn");

            string serviceStr = services.Count > 0
                ? string.Join(", ", services)
                : "Không có";

            // Tổng hợp thông tin
            string info = string.Format(
                "========== THÔNG TIN ĐĂNG KÝ ==========\n" +
                "Họ tên: {0}\n" +
                "Số điện thoại: {1}\n" +
                "Ngày sinh: {2}\n" +
                "Giới tính: {3}\n" +
                "Khóa học: {4} (Mã: {5})\n" +
                "Dịch vụ thêm: {6}\n" +
                "======================================",
                fullName, phone, birthDate, gender,
                courseName, courseID, serviceStr);

            MessageBox.Show(info, "Thông tin đăng ký",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Nút "Xóa": Reset tất cả các control về mặc định
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            mtxtPhone.Clear();
            dtpBirthDate.Value = DateTime.Now;
            rdoMale.Checked = true;
            cboCourse.SelectedIndex = 0;
            chkDormitory.Checked = false;
            chkTransport.Checked = false;
            chkMeal.Checked = false;
            txtFullName.Focus();
        }
    }
}
