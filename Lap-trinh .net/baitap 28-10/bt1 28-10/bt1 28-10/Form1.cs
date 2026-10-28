using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bt1_28_10
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Xử lý checkbox Hiện/Ẩn mật khẩu
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Tick chọn → Hiện mật khẩu, Bỏ chọn → Ẩn mật khẩu (*)
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // Xử lý nút Đăng nhập với Validation ErrorProvider
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            // Xóa lỗi cũ
            errorProvider.Clear();

            // Validation: Không để trống Tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                isValid = false;
            }

            // Validation: Không để trống Mật khẩu
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Mật khẩu không được để trống!");
                isValid = false;
            }

            if (isValid)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Xử lý nút Thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
