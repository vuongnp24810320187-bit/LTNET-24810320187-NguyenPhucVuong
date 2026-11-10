using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bai6._4.Data;
using bai6._4.Models;
using Microsoft.Data.SqlClient;

namespace bai6._4
{
    public partial class Form1 : Form
    {
        private readonly GarageService _garageService;
        private readonly DataTable _repairCart = new();
        private string _invoiceText = string.Empty;

        public Form1()
        {
            InitializeComponent();
            _garageService = new GarageService();
            InitializeRepairCart();
            WireEvents();
        }

        private void WireEvents()
        {
            Load += Form1_Load;
            btnFindVehicle.Click += BtnFindVehicle_Click;
            btnSaveVehicle.Click += BtnSaveVehicle_Click;
            btnDeleteVehicle.Click += BtnDeleteVehicle_Click;
            btnDeleteCustomer.Click += BtnDeleteCustomer_Click;
            dgvVehicles.CellClick += DgvVehicles_CellClick;
            dgvCustomers.CellClick += DgvCustomers_CellClick;
            btnSavePart.Click += BtnSavePart_Click;
            btnDeletePart.Click += BtnDeletePart_Click;
            btnClearPart.Click += BtnClearPart_Click;
            dgvParts.CellClick += DgvParts_CellClick;
            btnAddRepairPart.Click += BtnAddRepairPart_Click;
            btnRemoveRepairPart.Click += BtnRemoveRepairPart_Click;
            btnCreateRepair.Click += BtnCreateRepair_Click;
            btnRefreshOrders.Click += BtnRefreshOrders_Click;
            btnMarkPaid.Click += BtnMarkPaid_Click;
            btnInvoice.Click += BtnInvoice_Click;
            btnPrintInvoice.Click += BtnPrintInvoice_Click;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void InitializeRepairCart()
        {
            _repairCart.Columns.Add("MaPhuTung", typeof(int));
            _repairCart.Columns.Add("TenPhuTung", typeof(string));
            _repairCart.Columns.Add("SoLuong", typeof(int));
            _repairCart.Columns.Add("DonGia", typeof(decimal));
            _repairCart.Columns.Add("TienCong", typeof(decimal));
            _repairCart.Columns.Add("ThanhTien", typeof(decimal));
            _repairCart.PrimaryKey = new[] { _repairCart.Columns["MaPhuTung"]! };
            dgvRepairParts.DataSource = _repairCart;
            dgvRepairParts.Columns["MaPhuTung"].HeaderText = "Mã phụ tùng";
            dgvRepairParts.Columns["TenPhuTung"].HeaderText = "Tên phụ tùng";
            dgvRepairParts.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvRepairParts.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvRepairParts.Columns["TienCong"].HeaderText = "Tiền công";
            dgvRepairParts.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvRepairParts.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvRepairParts.Columns["TienCong"].DefaultCellStyle.Format = "N0";
            dgvRepairParts.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                RefreshAllData();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void RefreshAllData()
        {
            dgvCustomers.DataSource = _garageService.GetCustomers();
            dgvVehicles.DataSource = _garageService.GetVehicles();
            RefreshParts();
            RefreshOrders();
            ConfigureGridHeaders();
        }

        private void ConfigureGridHeaders()
        {
            SetHeader(dgvCustomers, "MaKH", "Mã chủ xe");
            SetHeader(dgvCustomers, "TenKH", "Tên chủ xe");
            SetHeader(dgvCustomers, "DienThoai", "Điện thoại");
            SetHeader(dgvCustomers, "DiaChi", "Địa chỉ");
            SetHeader(dgvVehicles, "BienSo", "Biển số");
            SetHeader(dgvVehicles, "MaKH", "Mã chủ xe");
            SetHeader(dgvVehicles, "TenKH", "Tên chủ xe");
            SetHeader(dgvVehicles, "DienThoai", "Điện thoại");
            SetHeader(dgvVehicles, "HieuXe", "Hiệu xe");
            SetHeader(dgvVehicles, "NamSanXuat", "Năm SX");
            SetHeader(dgvParts, "MaPhuTung", "Mã phụ tùng");
            SetHeader(dgvParts, "TenPhuTung", "Tên phụ tùng");
            SetHeader(dgvParts, "DonGia", "Đơn giá");
            SetHeader(dgvParts, "SoLuongTon", "Tồn kho");
            SetHeader(dgvOrders, "MaPhieu", "Mã phiếu");
            SetHeader(dgvOrders, "BienSo", "Biển số");
            SetHeader(dgvOrders, "TenKH", "Chủ xe");
            SetHeader(dgvOrders, "NgayNhan", "Ngày nhận");
            SetHeader(dgvOrders, "NoiDungSua", "Nội dung sửa");
            SetHeader(dgvOrders, "TongTien", "Tổng tiền");
            SetHeader(dgvOrders, "TrangThai", "Trạng thái");
            if (dgvOrders.Columns.Contains("NgayNhan"))
            {
                dgvOrders.Columns["NgayNhan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvOrders.Columns.Contains("TongTien"))
            {
                dgvOrders.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
            if (dgvParts.Columns.Contains("DonGia"))
            {
                dgvParts.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
        }

        private static void SetHeader(DataGridView grid, string columnName, string header)
        {
            if (grid.Columns.Contains(columnName))
            {
                grid.Columns[columnName].HeaderText = header;
            }
        }

        private void RefreshParts()
        {
            var parts = _garageService.GetParts();
            dgvParts.DataSource = parts;
            cmbRepairPart.DataSource = parts.Copy();
            cmbRepairPart.DisplayMember = "TenPhuTung";
            cmbRepairPart.ValueMember = "MaPhuTung";
        }

        private void RefreshOrders()
        {
            dgvOrders.DataSource = _garageService.GetRepairOrders();
        }

        private void BtnFindVehicle_Click(object? sender, EventArgs e)
        {
            try
            {
                var plate = txtPlate.Text.Trim();
                if (plate.Length == 0)
                {
                    throw new InvalidOperationException("Vui lòng nhập biển số xe cần tra cứu.");
                }

                var vehicle = _garageService.FindVehicle(plate);
                if (vehicle is null)
                {
                    txtCustomerId.Clear();
                    txtCustomerName.Clear();
                    txtPhone.Clear();
                    txtAddress.Clear();
                    txtMake.Clear();
                    nudYear.Value = 2020;
                    MessageBox.Show("Chưa có biển số này. Nhập thông tin chủ xe và xe mới.", "Tra cứu xe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DisplayVehicle(vehicle);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void DisplayVehicle(VehicleRecord vehicle)
        {
            txtPlate.Text = vehicle.BienSo;
            txtCustomerId.Text = vehicle.MaKH.ToString();
            txtCustomerName.Text = vehicle.TenKH;
            txtPhone.Text = vehicle.DienThoai ?? string.Empty;
            txtAddress.Text = vehicle.DiaChi ?? string.Empty;
            txtMake.Text = vehicle.HieuXe ?? string.Empty;
            nudYear.Value = vehicle.NamSanXuat is int year ? Math.Clamp(year, (int)nudYear.Minimum, (int)nudYear.Maximum) : 2020;
        }

        private void BtnSaveVehicle_Click(object? sender, EventArgs e)
        {
            try
            {
                var plate = txtPlate.Text.Trim();
                var name = txtCustomerName.Text.Trim();
                if (plate.Length == 0 || name.Length == 0)
                {
                    throw new InvalidOperationException("Biển số xe và tên chủ xe là bắt buộc.");
                }

                int? customerId = int.TryParse(txtCustomerId.Text, out var id) ? id : null;
                var savedCustomerId = _garageService.SaveCustomer(customerId, name, txtPhone.Text, txtAddress.Text);
                _garageService.SaveVehicle(plate, savedCustomerId, txtMake.Text, (int)nudYear.Value);
                txtCustomerId.Text = savedCustomerId.ToString();
                RefreshAllData();
                MessageBox.Show("Đã lưu thông tin chủ xe và xe.", "Garage", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnDeleteVehicle_Click(object? sender, EventArgs e)
        {
            try
            {
                var plate = txtPlate.Text.Trim();
                if (plate.Length == 0 || MessageBox.Show($"Xóa xe {plate}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                _garageService.DeleteVehicle(plate);
                RefreshAllData();
                ClearVehicleFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnDeleteCustomer_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCustomerId.Text, out var customerId))
                {
                    throw new InvalidOperationException("Chọn chủ xe trong danh sách trước khi xóa.");
                }
                if (MessageBox.Show("Xóa chủ xe này? Chỉ xóa được khi chủ xe không còn xe liên kết.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                _garageService.DeleteCustomer(customerId);
                RefreshAllData();
                ClearVehicleFields();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void DgvVehicles_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            try
            {
                var plate = Convert.ToString(dgvVehicles.Rows[e.RowIndex].Cells["BienSo"].Value);
                if (!string.IsNullOrWhiteSpace(plate) && _garageService.FindVehicle(plate) is VehicleRecord vehicle)
                {
                    DisplayVehicle(vehicle);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void DgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var row = dgvCustomers.Rows[e.RowIndex];
            txtCustomerId.Text = Convert.ToString(row.Cells["MaKH"].Value) ?? string.Empty;
            txtCustomerName.Text = Convert.ToString(row.Cells["TenKH"].Value) ?? string.Empty;
            txtPhone.Text = Convert.ToString(row.Cells["DienThoai"].Value) ?? string.Empty;
            txtAddress.Text = Convert.ToString(row.Cells["DiaChi"].Value) ?? string.Empty;
        }

        private void ClearVehicleFields()
        {
            txtCustomerId.Clear();
            txtPlate.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtMake.Clear();
            nudYear.Value = 2020;
        }

        private void BtnSavePart_Click(object? sender, EventArgs e)
        {
            try
            {
                var name = txtPartName.Text.Trim();
                if (name.Length == 0)
                {
                    throw new InvalidOperationException("Tên phụ tùng là bắt buộc.");
                }
                int? partId = int.TryParse(txtPartId.Text, out var id) ? id : null;
                _garageService.SavePart(partId, name, nudPartPrice.Value, (int)nudPartStock.Value);
                RefreshParts();
                ClearPartFields();
                ConfigureGridHeaders();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnDeletePart_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtPartId.Text, out var partId))
                {
                    throw new InvalidOperationException("Chọn phụ tùng trong danh sách trước khi xóa.");
                }
                if (MessageBox.Show("Xóa phụ tùng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                _garageService.DeletePart(partId);
                RefreshParts();
                ClearPartFields();
                ConfigureGridHeaders();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnClearPart_Click(object? sender, EventArgs e)
        {
            ClearPartFields();
        }

        private void DgvParts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var row = dgvParts.Rows[e.RowIndex];
            txtPartId.Text = Convert.ToString(row.Cells["MaPhuTung"].Value) ?? string.Empty;
            txtPartName.Text = Convert.ToString(row.Cells["TenPhuTung"].Value) ?? string.Empty;
            nudPartPrice.Value = Math.Clamp(Convert.ToDecimal(row.Cells["DonGia"].Value), nudPartPrice.Minimum, nudPartPrice.Maximum);
            nudPartStock.Value = Math.Clamp(Convert.ToDecimal(row.Cells["SoLuongTon"].Value), nudPartStock.Minimum, nudPartStock.Maximum);
        }

        private void ClearPartFields()
        {
            txtPartId.Clear();
            txtPartName.Clear();
            nudPartPrice.Value = 0;
            nudPartStock.Value = 0;
        }

        private void BtnAddRepairPart_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cmbRepairPart.SelectedItem is not DataRowView selectedPart)
                {
                    throw new InvalidOperationException("Chọn phụ tùng cần thay thế.");
                }
                var partId = Convert.ToInt32(selectedPart["MaPhuTung"]);
                var name = Convert.ToString(selectedPart["TenPhuTung"]) ?? string.Empty;
                var price = Convert.ToDecimal(selectedPart["DonGia"]);
                var stock = Convert.ToInt32(selectedPart["SoLuongTon"]);
                var quantity = (int)nudRepairQty.Value;
                var existing = _repairCart.Rows.Find(partId);
                if (existing is not null)
                {
                    quantity += Convert.ToInt32(existing["SoLuong"]);
                }
                if (quantity > stock)
                {
                    throw new InvalidOperationException($"Tồn kho chỉ còn {stock}.");
                }

                if (existing is null)
                {
                    existing = _repairCart.NewRow();
                    existing["MaPhuTung"] = partId;
                    existing["TenPhuTung"] = name;
                    existing["SoLuong"] = quantity;
                    existing["DonGia"] = price;
                    existing["TienCong"] = nudLabor.Value;
                    _repairCart.Rows.Add(existing);
                }
                else
                {
                    existing["SoLuong"] = quantity;
                    existing["TienCong"] = Convert.ToDecimal(existing["TienCong"]) + nudLabor.Value;
                }
                existing["ThanhTien"] = quantity * price + Convert.ToDecimal(existing["TienCong"]);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnRemoveRepairPart_Click(object? sender, EventArgs e)
        {
            if (dgvRepairParts.CurrentRow is not null && !dgvRepairParts.CurrentRow.IsNewRow)
            {
                _repairCart.Rows.RemoveAt(dgvRepairParts.CurrentRow.Index);
            }
        }

        private void BtnCreateRepair_Click(object? sender, EventArgs e)
        {
            try
            {
                var plate = txtRepairPlate.Text.Trim();
                if (plate.Length == 0)
                {
                    throw new InvalidOperationException("Nhập biển số xe.");
                }
                if (_garageService.FindVehicle(plate) is null)
                {
                    throw new InvalidOperationException("Biển số xe chưa được tiếp nhận trong hệ thống.");
                }
                if (_repairCart.Rows.Count == 0)
                {
                    throw new InvalidOperationException("Thêm ít nhất một phụ tùng trước khi xác nhận phiếu.");
                }
                var parts = _repairCart.Rows.Cast<DataRow>()
                    .Select(row => new RepairPart(Convert.ToInt32(row["MaPhuTung"]), Convert.ToInt32(row["SoLuong"]), Convert.ToDecimal(row["TienCong"])))
                    .ToList();
                var repairId = _garageService.CreateRepair(plate, txtRepairDescription.Text, parts);
                _repairCart.Clear();
                RefreshParts();
                RefreshOrders();
                ConfigureGridHeaders();
                txtRepairPlate.Clear();
                txtRepairDescription.Clear();
                MessageBox.Show($"Đã tạo phiếu sửa chữa #{repairId}. Tồn kho đã được cập nhật.", "Garage", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnRefreshOrders_Click(object? sender, EventArgs e)
        {
            try
            {
                RefreshOrders();
                ConfigureGridHeaders();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnMarkPaid_Click(object? sender, EventArgs e)
        {
            try
            {
                var repairId = GetSelectedRepairId();
                _garageService.MarkAsPaid(repairId);
                RefreshOrders();
                ConfigureGridHeaders();
                MessageBox.Show("Phiếu đã được đánh dấu thanh toán.", "Thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnInvoice_Click(object? sender, EventArgs e)
        {
            try
            {
                LoadInvoice(GetSelectedRepairId());
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void BtnPrintInvoice_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_invoiceText))
                {
                    LoadInvoice(GetSelectedRepairId());
                }
                printPreviewDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private int GetSelectedRepairId()
        {
            if (dgvOrders.CurrentRow is null || !int.TryParse(Convert.ToString(dgvOrders.CurrentRow.Cells["MaPhieu"].Value), out var repairId))
            {
                throw new InvalidOperationException("Chọn phiếu sửa chữa trong danh sách.");
            }
            return repairId;
        }

        private void LoadInvoice(int repairId)
        {
            var invoice = _garageService.GetInvoice(repairId);
            if (invoice.Rows.Count == 0)
            {
                throw new InvalidOperationException("Không tìm thấy phiếu sửa chữa.");
            }

            var first = invoice.Rows[0];
            var text = new StringBuilder();
            text.AppendLine("                 HÓA ĐƠN DỊCH VỤ GARAGE");
            text.AppendLine(new string('=', 78));
            text.AppendLine($"Mã phiếu: {first["MaPhieu"]}     Ngày nhận: {Convert.ToDateTime(first["NgayNhan"]):dd/MM/yyyy HH:mm}");
            text.AppendLine($"Biển số: {first["BienSo"]}     Chủ xe: {first["TenKH"]}");
            text.AppendLine($"Điện thoại: {CellText(first, "DienThoai")}     Địa chỉ: {CellText(first, "DiaChi")}");
            text.AppendLine($"Nội dung sửa: {CellText(first, "NoiDungSua")}");
            text.AppendLine($"Trạng thái: {first["TrangThai"]}");
            text.AppendLine(new string('-', 78));
            text.AppendLine("Phụ tùng                 SL       Đơn giá       Tiền công       Thành tiền");
            text.AppendLine(new string('-', 78));
            foreach (DataRow row in invoice.Rows)
            {
                if (row.IsNull("TenPhuTung"))
                {
                    continue;
                }
                text.AppendLine($"{Convert.ToString(row["TenPhuTung"]),-24} {Convert.ToInt32(row["SoLuong"]),3} {Convert.ToDecimal(row["DonGia"]),14:N0} {Convert.ToDecimal(row["TienCong"]),14:N0} {Convert.ToDecimal(row["ThanhTien"]),15:N0}");
            }
            text.AppendLine(new string('-', 78));
            text.AppendLine($"TỔNG CỘNG: {Convert.ToDecimal(first["TongTien"]):N0} VNĐ");
            text.AppendLine("Xin cảm ơn quý khách!");
            _invoiceText = text.ToString();
            txtInvoicePreview.Text = _invoiceText;
        }

        private static string CellText(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? string.Empty : Convert.ToString(row[columnName]) ?? string.Empty;
        }

        private void PrintDocument_PrintPage(object? sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            using var font = new Font("Consolas", 9F);
            e.Graphics.DrawString(_invoiceText, font, Brushes.Black, e.MarginBounds);
            e.HasMorePages = false;
        }

        private static void ShowError(Exception exception)
        {
            var message = exception is SqlException
                ? $"Lỗi SQL Server: {exception.Message}"
                : exception.Message;
            MessageBox.Show(message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
