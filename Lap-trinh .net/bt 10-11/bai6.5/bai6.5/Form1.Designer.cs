using System.Drawing;
using System.Windows.Forms;

namespace bai6._5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private TableLayoutPanel contentLayout;
        private TableLayoutPanel roomLayout;
        private Label lblRoomsTitle;
        private Label lblRoomHint;
        private FlowLayoutPanel flowRooms;
        private Button btnRoom01;
        private Button btnRoom02;
        private Button btnRoom03;
        private Button btnRoom04;
        private Button btnRoom05;
        private Button btnRoom06;
        private Button btnRoom07;
        private Button btnRoom08;
        private TableLayoutPanel orderLayout;
        private Label lblOrderTitle;
        private Label lblSelectedRoom;
        private Panel elapsedPanel;
        private Label lblElapsedCaption;
        private Label lblElapsed;
        private FlowLayoutPanel actionPanel;
        private Button btnOpenRoom;
        private Button btnPay;
        private TableLayoutPanel serviceInputLayout;
        private Label lblServiceCaption;
        private ComboBox cboService;
        private Label lblQuantityCaption;
        private NumericUpDown nudQuantity;
        private Button btnAddService;
        private DataGridView dgvServices;
        private Label lblStatus;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            contentLayout = new TableLayoutPanel();
            roomLayout = new TableLayoutPanel();
            lblRoomsTitle = new Label();
            lblRoomHint = new Label();
            flowRooms = new FlowLayoutPanel();
            btnRoom01 = new Button();
            btnRoom02 = new Button();
            btnRoom03 = new Button();
            btnRoom04 = new Button();
            btnRoom05 = new Button();
            btnRoom06 = new Button();
            btnRoom07 = new Button();
            btnRoom08 = new Button();
            orderLayout = new TableLayoutPanel();
            lblOrderTitle = new Label();
            lblSelectedRoom = new Label();
            elapsedPanel = new Panel();
            lblElapsedCaption = new Label();
            lblElapsed = new Label();
            actionPanel = new FlowLayoutPanel();
            btnOpenRoom = new Button();
            btnPay = new Button();
            serviceInputLayout = new TableLayoutPanel();
            lblServiceCaption = new Label();
            lblQuantityCaption = new Label();
            cboService = new ComboBox();
            nudQuantity = new NumericUpDown();
            btnAddService = new Button();
            dgvServices = new DataGridView();
            lblStatus = new Label();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            contentLayout.SuspendLayout();
            roomLayout.SuspendLayout();
            flowRooms.SuspendLayout();
            orderLayout.SuspendLayout();
            elapsedPanel.SuspendLayout();
            actionPanel.SuspendLayout();
            serviceInputLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.FromArgb(239, 243, 247);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(contentLayout, 0, 1);
            rootLayout.Controls.Add(lblStatus, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(12);
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            rootLayout.Size = new Size(1180, 760);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(25, 45, 66);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(15, 15);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(18, 10, 18, 10);
            headerPanel.Size = new Size(1150, 78);
            headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(18, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(384, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "KARAOKE MANAGEMENT";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(205, 218, 230);
            lblSubtitle.Location = new Point(21, 48);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(295, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Quản lý phòng hát, dịch vụ và thanh toán";
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 2;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            contentLayout.Controls.Add(roomLayout, 0, 0);
            contentLayout.Controls.Add(orderLayout, 1, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(15, 99);
            contentLayout.Name = "contentLayout";
            contentLayout.Padding = new Padding(0, 12, 0, 8);
            contentLayout.RowCount = 1;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(1150, 608);
            contentLayout.TabIndex = 1;
            // 
            // roomLayout
            // 
            roomLayout.ColumnCount = 1;
            roomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            roomLayout.Controls.Add(lblRoomsTitle, 0, 0);
            roomLayout.Controls.Add(lblRoomHint, 0, 1);
            roomLayout.Controls.Add(flowRooms, 0, 2);
            roomLayout.Dock = DockStyle.Fill;
            roomLayout.Location = new Point(3, 15);
            roomLayout.Name = "roomLayout";
            roomLayout.Padding = new Padding(0, 0, 12, 0);
            roomLayout.RowCount = 3;
            roomLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            roomLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            roomLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            roomLayout.Size = new Size(626, 582);
            roomLayout.TabIndex = 0;
            // 
            // lblRoomsTitle
            // 
            lblRoomsTitle.Dock = DockStyle.Fill;
            lblRoomsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblRoomsTitle.ForeColor = Color.FromArgb(34, 52, 70);
            lblRoomsTitle.Location = new Point(3, 0);
            lblRoomsTitle.Name = "lblRoomsTitle";
            lblRoomsTitle.Size = new Size(608, 38);
            lblRoomsTitle.TabIndex = 0;
            lblRoomsTitle.Text = "Sơ đồ phòng hát";
            lblRoomsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRoomHint
            // 
            lblRoomHint.Dock = DockStyle.Fill;
            lblRoomHint.Font = new Font("Segoe UI", 9F);
            lblRoomHint.ForeColor = Color.FromArgb(92, 108, 124);
            lblRoomHint.Location = new Point(3, 38);
            lblRoomHint.Name = "lblRoomHint";
            lblRoomHint.Size = new Size(608, 32);
            lblRoomHint.TabIndex = 1;
            lblRoomHint.Text = "Xanh: trống     Đỏ: đang hát     Chọn phòng để xem chi tiết";
            lblRoomHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowRooms
            // 
            flowRooms.AutoScroll = true;
            flowRooms.BackColor = Color.White;
            flowRooms.BorderStyle = BorderStyle.FixedSingle;
            flowRooms.Controls.Add(btnRoom01);
            flowRooms.Controls.Add(btnRoom02);
            flowRooms.Controls.Add(btnRoom03);
            flowRooms.Controls.Add(btnRoom04);
            flowRooms.Controls.Add(btnRoom05);
            flowRooms.Controls.Add(btnRoom06);
            flowRooms.Controls.Add(btnRoom07);
            flowRooms.Controls.Add(btnRoom08);
            flowRooms.Dock = DockStyle.Fill;
            flowRooms.Location = new Point(3, 73);
            flowRooms.Name = "flowRooms";
            flowRooms.Padding = new Padding(10);
            flowRooms.Size = new Size(608, 506);
            flowRooms.TabIndex = 2;
            // 
            // btnRoom01
            // 
            btnRoom01.BackColor = Color.ForestGreen;
            btnRoom01.FlatStyle = FlatStyle.Flat;
            btnRoom01.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom01.ForeColor = Color.White;
            btnRoom01.Location = new Point(18, 18);
            btnRoom01.Margin = new Padding(8);
            btnRoom01.Name = "btnRoom01";
            btnRoom01.Size = new Size(165, 112);
            btnRoom01.TabIndex = 0;
            btnRoom01.Text = "Phòng 01";
            btnRoom01.UseVisualStyleBackColor = false;
            // 
            // btnRoom02
            // 
            btnRoom02.BackColor = Color.ForestGreen;
            btnRoom02.FlatStyle = FlatStyle.Flat;
            btnRoom02.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom02.ForeColor = Color.White;
            btnRoom02.Location = new Point(199, 18);
            btnRoom02.Margin = new Padding(8);
            btnRoom02.Name = "btnRoom02";
            btnRoom02.Size = new Size(165, 112);
            btnRoom02.TabIndex = 1;
            btnRoom02.Text = "Phòng 02";
            btnRoom02.UseVisualStyleBackColor = false;
            // 
            // btnRoom03
            // 
            btnRoom03.BackColor = Color.ForestGreen;
            btnRoom03.FlatStyle = FlatStyle.Flat;
            btnRoom03.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom03.ForeColor = Color.White;
            btnRoom03.Location = new Point(380, 18);
            btnRoom03.Margin = new Padding(8);
            btnRoom03.Name = "btnRoom03";
            btnRoom03.Size = new Size(165, 112);
            btnRoom03.TabIndex = 2;
            btnRoom03.Text = "Phòng 03";
            btnRoom03.UseVisualStyleBackColor = false;
            // 
            // btnRoom04
            // 
            btnRoom04.BackColor = Color.ForestGreen;
            btnRoom04.FlatStyle = FlatStyle.Flat;
            btnRoom04.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom04.ForeColor = Color.White;
            btnRoom04.Location = new Point(18, 146);
            btnRoom04.Margin = new Padding(8);
            btnRoom04.Name = "btnRoom04";
            btnRoom04.Size = new Size(165, 112);
            btnRoom04.TabIndex = 3;
            btnRoom04.Text = "Phòng 04";
            btnRoom04.UseVisualStyleBackColor = false;
            // 
            // btnRoom05
            // 
            btnRoom05.BackColor = Color.ForestGreen;
            btnRoom05.FlatStyle = FlatStyle.Flat;
            btnRoom05.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom05.ForeColor = Color.White;
            btnRoom05.Location = new Point(199, 146);
            btnRoom05.Margin = new Padding(8);
            btnRoom05.Name = "btnRoom05";
            btnRoom05.Size = new Size(165, 112);
            btnRoom05.TabIndex = 4;
            btnRoom05.Text = "Phòng 05";
            btnRoom05.UseVisualStyleBackColor = false;
            // 
            // btnRoom06
            // 
            btnRoom06.BackColor = Color.ForestGreen;
            btnRoom06.FlatStyle = FlatStyle.Flat;
            btnRoom06.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom06.ForeColor = Color.White;
            btnRoom06.Location = new Point(380, 146);
            btnRoom06.Margin = new Padding(8);
            btnRoom06.Name = "btnRoom06";
            btnRoom06.Size = new Size(165, 112);
            btnRoom06.TabIndex = 5;
            btnRoom06.Text = "Phòng 06";
            btnRoom06.UseVisualStyleBackColor = false;
            // 
            // btnRoom07
            // 
            btnRoom07.BackColor = Color.ForestGreen;
            btnRoom07.FlatStyle = FlatStyle.Flat;
            btnRoom07.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom07.ForeColor = Color.White;
            btnRoom07.Location = new Point(18, 274);
            btnRoom07.Margin = new Padding(8);
            btnRoom07.Name = "btnRoom07";
            btnRoom07.Size = new Size(165, 112);
            btnRoom07.TabIndex = 6;
            btnRoom07.Text = "Phòng 07";
            btnRoom07.UseVisualStyleBackColor = false;
            // 
            // btnRoom08
            // 
            btnRoom08.BackColor = Color.ForestGreen;
            btnRoom08.FlatStyle = FlatStyle.Flat;
            btnRoom08.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRoom08.ForeColor = Color.White;
            btnRoom08.Location = new Point(199, 274);
            btnRoom08.Margin = new Padding(8);
            btnRoom08.Name = "btnRoom08";
            btnRoom08.Size = new Size(165, 112);
            btnRoom08.TabIndex = 7;
            btnRoom08.Text = "Phòng 08";
            btnRoom08.UseVisualStyleBackColor = false;
            // 
            // orderLayout
            // 
            orderLayout.ColumnCount = 1;
            orderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            orderLayout.Controls.Add(lblOrderTitle, 0, 0);
            orderLayout.Controls.Add(lblSelectedRoom, 0, 1);
            orderLayout.Controls.Add(elapsedPanel, 0, 2);
            orderLayout.Controls.Add(actionPanel, 0, 3);
            orderLayout.Controls.Add(serviceInputLayout, 0, 4);
            orderLayout.Controls.Add(dgvServices, 0, 5);
            orderLayout.Dock = DockStyle.Fill;
            orderLayout.Location = new Point(635, 15);
            orderLayout.Name = "orderLayout";
            orderLayout.Padding = new Padding(12, 0, 0, 0);
            orderLayout.RowCount = 6;
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            orderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            orderLayout.Size = new Size(512, 582);
            orderLayout.TabIndex = 1;
            // 
            // lblOrderTitle
            // 
            lblOrderTitle.Dock = DockStyle.Fill;
            lblOrderTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblOrderTitle.ForeColor = Color.FromArgb(34, 52, 70);
            lblOrderTitle.Location = new Point(15, 0);
            lblOrderTitle.Name = "lblOrderTitle";
            lblOrderTitle.Size = new Size(494, 38);
            lblOrderTitle.TabIndex = 0;
            lblOrderTitle.Text = "Thông tin phòng & dịch vụ";
            lblOrderTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSelectedRoom
            // 
            lblSelectedRoom.BackColor = Color.White;
            lblSelectedRoom.BorderStyle = BorderStyle.FixedSingle;
            lblSelectedRoom.Dock = DockStyle.Fill;
            lblSelectedRoom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectedRoom.ForeColor = Color.FromArgb(44, 62, 80);
            lblSelectedRoom.Location = new Point(15, 38);
            lblSelectedRoom.Name = "lblSelectedRoom";
            lblSelectedRoom.Padding = new Padding(10, 0, 0, 0);
            lblSelectedRoom.Size = new Size(494, 48);
            lblSelectedRoom.TabIndex = 1;
            lblSelectedRoom.Text = "Chưa chọn phòng";
            lblSelectedRoom.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // elapsedPanel
            // 
            elapsedPanel.Controls.Add(lblElapsedCaption);
            elapsedPanel.Controls.Add(lblElapsed);
            elapsedPanel.Dock = DockStyle.Fill;
            elapsedPanel.Location = new Point(15, 89);
            elapsedPanel.Name = "elapsedPanel";
            elapsedPanel.Size = new Size(494, 30);
            elapsedPanel.TabIndex = 2;
            // 
            // lblElapsedCaption
            // 
            lblElapsedCaption.AutoSize = true;
            lblElapsedCaption.Font = new Font("Segoe UI", 9.5F);
            lblElapsedCaption.ForeColor = Color.FromArgb(76, 91, 107);
            lblElapsedCaption.Location = new Point(2, 8);
            lblElapsedCaption.Name = "lblElapsedCaption";
            lblElapsedCaption.Size = new Size(104, 21);
            lblElapsedCaption.TabIndex = 0;
            lblElapsedCaption.Text = "Thời gian hát:";
            // 
            // lblElapsed
            // 
            lblElapsed.AutoSize = true;
            lblElapsed.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblElapsed.ForeColor = Color.FromArgb(34, 52, 70);
            lblElapsed.Location = new Point(112, 7);
            lblElapsed.Name = "lblElapsed";
            lblElapsed.Size = new Size(80, 23);
            lblElapsed.TabIndex = 1;
            lblElapsed.Text = "00:00:00";
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnOpenRoom);
            actionPanel.Controls.Add(btnPay);
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.Location = new Point(15, 125);
            actionPanel.Name = "actionPanel";
            actionPanel.Padding = new Padding(0, 4, 0, 0);
            actionPanel.Size = new Size(494, 42);
            actionPanel.TabIndex = 3;
            actionPanel.WrapContents = false;
            // 
            // btnOpenRoom
            // 
            btnOpenRoom.BackColor = Color.FromArgb(36, 132, 87);
            btnOpenRoom.Enabled = false;
            btnOpenRoom.FlatStyle = FlatStyle.Flat;
            btnOpenRoom.ForeColor = Color.White;
            btnOpenRoom.Location = new Point(3, 7);
            btnOpenRoom.Name = "btnOpenRoom";
            btnOpenRoom.Size = new Size(145, 36);
            btnOpenRoom.TabIndex = 0;
            btnOpenRoom.Text = "Mở phòng";
            btnOpenRoom.UseVisualStyleBackColor = false;
            // 
            // btnPay
            // 
            btnPay.BackColor = Color.FromArgb(35, 91, 145);
            btnPay.Enabled = false;
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.ForeColor = Color.White;
            btnPay.Location = new Point(154, 7);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(145, 36);
            btnPay.TabIndex = 1;
            btnPay.Text = "Thanh toán";
            btnPay.UseVisualStyleBackColor = false;
            // 
            // serviceInputLayout
            // 
            serviceInputLayout.ColumnCount = 3;
            serviceInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            serviceInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
            serviceInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            serviceInputLayout.Controls.Add(lblServiceCaption, 0, 0);
            serviceInputLayout.Controls.Add(lblQuantityCaption, 1, 0);
            serviceInputLayout.Controls.Add(cboService, 0, 1);
            serviceInputLayout.Controls.Add(nudQuantity, 1, 1);
            serviceInputLayout.Controls.Add(btnAddService, 2, 1);
            serviceInputLayout.Dock = DockStyle.Fill;
            serviceInputLayout.Location = new Point(15, 173);
            serviceInputLayout.Name = "serviceInputLayout";
            serviceInputLayout.Padding = new Padding(0, 4, 0, 4);
            serviceInputLayout.RowCount = 2;
            serviceInputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            serviceInputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            serviceInputLayout.Size = new Size(494, 76);
            serviceInputLayout.TabIndex = 4;
            // 
            // lblServiceCaption
            // 
            lblServiceCaption.Dock = DockStyle.Fill;
            lblServiceCaption.Font = new Font("Segoe UI", 9F);
            lblServiceCaption.ForeColor = Color.FromArgb(76, 91, 107);
            lblServiceCaption.Location = new Point(3, 4);
            lblServiceCaption.Name = "lblServiceCaption";
            lblServiceCaption.Size = new Size(288, 25);
            lblServiceCaption.TabIndex = 0;
            lblServiceCaption.Text = "Dịch vụ";
            lblServiceCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblQuantityCaption
            // 
            lblQuantityCaption.Dock = DockStyle.Fill;
            lblQuantityCaption.Font = new Font("Segoe UI", 9F);
            lblQuantityCaption.ForeColor = Color.FromArgb(76, 91, 107);
            lblQuantityCaption.Location = new Point(297, 4);
            lblQuantityCaption.Name = "lblQuantityCaption";
            lblQuantityCaption.Size = new Size(82, 25);
            lblQuantityCaption.TabIndex = 1;
            lblQuantityCaption.Text = "Số lượng";
            lblQuantityCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboService
            // 
            cboService.Dock = DockStyle.Fill;
            cboService.DropDownStyle = ComboBoxStyle.DropDownList;
            cboService.Location = new Point(3, 32);
            cboService.Name = "cboService";
            cboService.Size = new Size(288, 28);
            cboService.TabIndex = 2;
            // 
            // nudQuantity
            // 
            nudQuantity.Dock = DockStyle.Fill;
            nudQuantity.Location = new Point(297, 32);
            nudQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(82, 27);
            nudQuantity.TabIndex = 3;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddService
            // 
            btnAddService.Dock = DockStyle.Fill;
            btnAddService.Enabled = false;
            btnAddService.Location = new Point(385, 32);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new Size(106, 37);
            btnAddService.TabIndex = 4;
            btnAddService.Text = "Thêm dịch vụ";
            // 
            // dgvServices
            // 
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServices.BackgroundColor = Color.White;
            dgvServices.ColumnHeadersHeight = 29;
            dgvServices.Dock = DockStyle.Fill;
            dgvServices.Location = new Point(15, 255);
            dgvServices.Name = "dgvServices";
            dgvServices.ReadOnly = true;
            dgvServices.RowHeadersVisible = false;
            dgvServices.RowHeadersWidth = 51;
            dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServices.Size = new Size(494, 324);
            dgvServices.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.FromArgb(225, 232, 239);
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.FromArgb(48, 65, 82);
            lblStatus.Location = new Point(15, 710);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(8, 0, 8, 0);
            lblStatus.Size = new Size(1150, 38);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Khởi tạo hệ thống...";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 243, 247);
            ClientSize = new Size(1180, 760);
            Controls.Add(rootLayout);
            MinimumSize = new Size(980, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống quản lý Karaoke";
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            contentLayout.ResumeLayout(false);
            roomLayout.ResumeLayout(false);
            flowRooms.ResumeLayout(false);
            orderLayout.ResumeLayout(false);
            elapsedPanel.ResumeLayout(false);
            elapsedPanel.PerformLayout();
            actionPanel.ResumeLayout(false);
            serviceInputLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
