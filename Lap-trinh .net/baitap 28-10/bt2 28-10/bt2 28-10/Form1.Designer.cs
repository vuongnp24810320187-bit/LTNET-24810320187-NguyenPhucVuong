namespace bt2_28_10
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.mtxtPhone = new System.Windows.Forms.MaskedTextBox();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.grpGender = new System.Windows.Forms.GroupBox();
            this.rdoMale = new System.Windows.Forms.RadioButton();
            this.rdoFemale = new System.Windows.Forms.RadioButton();
            this.lblCourse = new System.Windows.Forms.Label();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.grpServices = new System.Windows.Forms.GroupBox();
            this.chkDormitory = new System.Windows.Forms.CheckBox();
            this.chkTransport = new System.Windows.Forms.CheckBox();
            this.chkMeal = new System.Windows.Forms.CheckBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.grpGender.SuspendLayout();
            this.grpServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(120, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đăng Ký Khóa Học";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFullName.Location = new System.Drawing.Point(20, 60);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(63, 17);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "Họ tên:";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFullName.Location = new System.Drawing.Point(140, 57);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(280, 23);
            this.txtFullName.TabIndex = 2;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPhone.Location = new System.Drawing.Point(20, 100);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(103, 17);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Số điện thoại:";
            // 
            // mtxtPhone
            // 
            this.mtxtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.mtxtPhone.Location = new System.Drawing.Point(140, 97);
            this.mtxtPhone.Mask = "(000) 000-0000";
            this.mtxtPhone.Name = "mtxtPhone";
            this.mtxtPhone.Size = new System.Drawing.Size(280, 23);
            this.mtxtPhone.TabIndex = 4;
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblBirthDate.Location = new System.Drawing.Point(20, 140);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(78, 17);
            this.lblBirthDate.TabIndex = 5;
            this.lblBirthDate.Text = "Ngày sinh:";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.CustomFormat = "dd/MM/yyyy";
            this.dtpBirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthDate.Location = new System.Drawing.Point(140, 137);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(280, 23);
            this.dtpBirthDate.TabIndex = 6;
            // 
            // grpGender
            // 
            this.grpGender.Controls.Add(this.rdoMale);
            this.grpGender.Controls.Add(this.rdoFemale);
            this.grpGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.grpGender.Location = new System.Drawing.Point(20, 175);
            this.grpGender.Name = "grpGender";
            this.grpGender.Size = new System.Drawing.Size(200, 55);
            this.grpGender.TabIndex = 7;
            this.grpGender.TabStop = false;
            this.grpGender.Text = "Giới tính";
            // 
            // rdoMale
            // 
            this.rdoMale.AutoSize = true;
            this.rdoMale.Checked = true;
            this.rdoMale.Location = new System.Drawing.Point(15, 25);
            this.rdoMale.Name = "rdoMale";
            this.rdoMale.Size = new System.Drawing.Size(56, 21);
            this.rdoMale.TabIndex = 0;
            this.rdoMale.TabStop = true;
            this.rdoMale.Text = "Nam";
            this.rdoMale.UseVisualStyleBackColor = true;
            // 
            // rdoFemale
            // 
            this.rdoFemale.AutoSize = true;
            this.rdoFemale.Location = new System.Drawing.Point(100, 25);
            this.rdoFemale.Name = "rdoFemale";
            this.rdoFemale.Size = new System.Drawing.Size(47, 21);
            this.rdoFemale.TabIndex = 1;
            this.rdoFemale.Text = "Nữ";
            this.rdoFemale.UseVisualStyleBackColor = true;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCourse.Location = new System.Drawing.Point(230, 195);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(72, 17);
            this.lblCourse.TabIndex = 8;
            this.lblCourse.Text = "Khóa học:";
            // 
            // cboCourse
            // 
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboCourse.FormattingEnabled = true;
            this.cboCourse.Location = new System.Drawing.Point(310, 192);
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Size = new System.Drawing.Size(140, 24);
            this.cboCourse.TabIndex = 9;
            // 
            // grpServices
            // 
            this.grpServices.Controls.Add(this.chkDormitory);
            this.grpServices.Controls.Add(this.chkTransport);
            this.grpServices.Controls.Add(this.chkMeal);
            this.grpServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.grpServices.Location = new System.Drawing.Point(20, 240);
            this.grpServices.Name = "grpServices";
            this.grpServices.Size = new System.Drawing.Size(430, 55);
            this.grpServices.TabIndex = 10;
            this.grpServices.TabStop = false;
            this.grpServices.Text = "Dịch vụ đăng ký thêm";
            // 
            // chkDormitory
            // 
            this.chkDormitory.AutoSize = true;
            this.chkDormitory.Location = new System.Drawing.Point(15, 25);
            this.chkDormitory.Name = "chkDormitory";
            this.chkDormitory.Size = new System.Drawing.Size(91, 21);
            this.chkDormitory.TabIndex = 0;
            this.chkDormitory.Text = "Ký túc xá";
            this.chkDormitory.UseVisualStyleBackColor = true;
            // 
            // chkTransport
            // 
            this.chkTransport.AutoSize = true;
            this.chkTransport.Location = new System.Drawing.Point(150, 25);
            this.chkTransport.Name = "chkTransport";
            this.chkTransport.Size = new System.Drawing.Size(107, 21);
            this.chkTransport.TabIndex = 1;
            this.chkTransport.Text = "Xe đưa đón";
            this.chkTransport.UseVisualStyleBackColor = true;
            // 
            // chkMeal
            // 
            this.chkMeal.AutoSize = true;
            this.chkMeal.Location = new System.Drawing.Point(300, 25);
            this.chkMeal.Name = "chkMeal";
            this.chkMeal.Size = new System.Drawing.Size(95, 21);
            this.chkMeal.TabIndex = 2;
            this.chkMeal.Text = "Suất ăn";
            this.chkMeal.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.Location = new System.Drawing.Point(120, 310);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(110, 35);
            this.btnRegister.TabIndex = 11;
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClear.Location = new System.Drawing.Point(250, 310);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 35);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Xóa";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 365);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.mtxtPhone);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.grpGender);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.cboCourse);
            this.Controls.Add(this.grpServices);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnClear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký Khóa Học";
            this.grpGender.ResumeLayout(false);
            this.grpGender.PerformLayout();
            this.grpServices.ResumeLayout(false);
            this.grpServices.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.MaskedTextBox mtxtPhone;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.GroupBox grpGender;
        private System.Windows.Forms.RadioButton rdoMale;
        private System.Windows.Forms.RadioButton rdoFemale;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.GroupBox grpServices;
        private System.Windows.Forms.CheckBox chkDormitory;
        private System.Windows.Forms.CheckBox chkTransport;
        private System.Windows.Forms.CheckBox chkMeal;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnClear;
    }
}
