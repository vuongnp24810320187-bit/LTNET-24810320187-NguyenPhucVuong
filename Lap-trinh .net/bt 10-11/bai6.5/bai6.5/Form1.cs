using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace bai6._5
{
    public partial class Form1 : Form
    {
        private readonly KaraokeRepository _repository = new();
        private readonly System.Windows.Forms.Timer _elapsedTimer = new() { Interval = 1000 };
        private readonly HashSet<Button> _boundRoomButtons = [];
        private FlowLayoutPanel? _roomPanel;
        private DataGridView? _servicesGrid;
        private ComboBox? _servicePicker;
        private NumericUpDown? _quantityPicker;
        private Button? _openRoomButton;
        private Button? _addServiceButton;
        private Button? _payButton;
        private Label? _selectedRoomLabel;
        private Label? _elapsedLabel;
        private Label? _statusLabel;
        private IReadOnlyList<KaraokeRoom> _rooms = [];
        private DataTable? _serviceCatalog;
        private DataTable? _serviceRows;
        private KaraokeRoom? _selectedRoom;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            FormClosed += Form1_FormClosed;
            _elapsedTimer.Tick += (_, _) => UpdateElapsedTime();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            BindDesignerControls();
            if (!DesignerControlsReady())
            {
                SetStatus("Thêm các control theo tên quy ước trong hướng dẫn ở phần kết quả triển khai.");
                return;
            }

            _openRoomButton!.Click += OpenRoomButton_Click;
            _addServiceButton!.Click += AddServiceButton_Click;
            _payButton!.Click += PayButton_Click;
            _elapsedTimer.Start();
            ReloadData();
        }

        private void BindDesignerControls()
        {
            _roomPanel = FindControl<FlowLayoutPanel>("flowRooms");
            _servicesGrid = FindControl<DataGridView>("dgvServices");
            _servicePicker = FindControl<ComboBox>("cboService");
            _quantityPicker = FindControl<NumericUpDown>("nudQuantity");
            _openRoomButton = FindControl<Button>("btnOpenRoom");
            _addServiceButton = FindControl<Button>("btnAddService");
            _payButton = FindControl<Button>("btnPay");
            _selectedRoomLabel = FindControl<Label>("lblSelectedRoom");
            _elapsedLabel = FindControl<Label>("lblElapsed");
            _statusLabel = FindControl<Label>("lblStatus");

            if (_roomPanel is not null)
            {
                foreach (var button in _roomPanel.Controls.OfType<Button>()
                             .Where(control => control.Name.StartsWith("btnRoom", StringComparison.OrdinalIgnoreCase)))
                {
                    if (_boundRoomButtons.Add(button))
                    {
                        button.Click += RoomButton_Click;
                    }
                }
            }

            if (_servicesGrid is not null)
            {
                _servicesGrid.AutoGenerateColumns = true;
                _servicesGrid.AllowUserToAddRows = false;
                _servicesGrid.ReadOnly = true;
                _servicesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (_servicePicker is not null)
            {
                _servicePicker.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private bool DesignerControlsReady() =>
            _roomPanel is not null &&
            _servicesGrid is not null &&
            _servicePicker is not null &&
            _quantityPicker is not null &&
            _openRoomButton is not null &&
            _addServiceButton is not null &&
            _payButton is not null &&
            _selectedRoomLabel is not null &&
            _elapsedLabel is not null &&
            _statusLabel is not null &&
            _boundRoomButtons.Count > 0;

        private T? FindControl<T>(string name) where T : Control =>
            Controls.Find(name, true).OfType<T>().FirstOrDefault();

        private void ReloadData(int? roomToSelect = null)
        {
            try
            {
                _rooms = _repository.GetRooms();
                _serviceCatalog = _repository.GetServiceCatalog();
                _servicePicker!.DataSource = _serviceCatalog;
                _servicePicker.DisplayMember = "TenDV";
                _servicePicker.ValueMember = "MaDV";
                PopulateRoomButtons();

                var room = roomToSelect.HasValue
                    ? _rooms.FirstOrDefault(item => item.Id == roomToSelect.Value)
                    : _selectedRoom is null
                        ? null
                        : _rooms.FirstOrDefault(item => item.Id == _selectedRoom.Id);
                SelectRoom(room);

                if (_rooms.Count > _boundRoomButtons.Count)
                {
                    SetStatus($"Có {_rooms.Count} phòng nhưng chỉ có {_boundRoomButtons.Count} nút phòng trong Designer.");
                }
                else
                {
                    SetStatus("Đã kết nối KaraokeDB.");
                }
            }
            catch (Exception ex)
            {
                SetStatus($"Không thể tải dữ liệu: {ex.Message}");
            }
        }

        private void PopulateRoomButtons()
        {
            var buttons = _boundRoomButtons.OrderBy(button => button.Name, StringComparer.OrdinalIgnoreCase).ToArray();
            for (var index = 0; index < buttons.Length; index++)
            {
                var button = buttons[index];
                if (index >= _rooms.Count)
                {
                    button.Visible = false;
                    button.Tag = null;
                    continue;
                }

                var room = _rooms[index];
                button.Visible = true;
                button.Tag = room;
                button.Text = $"{room.Name}\r\n{room.Status}\r\n{room.HourlyRate:N0} đ/giờ";
                button.BackColor = room.IsOccupied ? Color.IndianRed : Color.ForestGreen;
                button.ForeColor = Color.White;
                button.UseVisualStyleBackColor = false;
            }
        }

        private void RoomButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button { Tag: KaraokeRoom room })
            {
                SelectRoom(room);
            }
        }

        private void SelectRoom(KaraokeRoom? room)
        {
            _selectedRoom = room;
            if (room is null)
            {
                _selectedRoomLabel!.Text = "Chưa chọn phòng";
                _elapsedLabel!.Text = "00:00:00";
                _serviceRows = null;
                _servicesGrid!.DataSource = null;
                _openRoomButton!.Enabled = false;
                _addServiceButton!.Enabled = false;
                _payButton!.Enabled = false;
                return;
            }

            _selectedRoomLabel!.Text = $"{room.Name} - {room.Status} ({room.HourlyRate:N0} đ/giờ)";
            _openRoomButton!.Enabled = !room.IsOccupied;
            _addServiceButton!.Enabled = room.IsOccupied && room.InvoiceId.HasValue;
            _payButton!.Enabled = room.IsOccupied && room.InvoiceId.HasValue;

            if (room.IsOccupied && room.InvoiceId.HasValue)
            {
                _serviceRows = _repository.GetInvoiceServices(room.InvoiceId.Value);
                if (!_serviceRows.Columns.Contains("ThanhTien"))
                {
                    _serviceRows.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
                }

                _servicesGrid!.DataSource = _serviceRows;
                if (_servicesGrid.Columns.Contains("MaHD"))
                {
                    _servicesGrid.Columns["MaHD"].Visible = false;
                }

                if (_servicesGrid.Columns.Contains("MaDV"))
                {
                    _servicesGrid.Columns["MaDV"].Visible = false;
                }

                _servicesGrid.Columns["TenDV"].HeaderText = "Dịch vụ";
                _servicesGrid.Columns["SoLuong"].HeaderText = "Số lượng";
                _servicesGrid.Columns["DonGia"].HeaderText = "Đơn giá";
                _servicesGrid.Columns["ThanhTien"].HeaderText = "Thành tiền";
            }
            else
            {
                _serviceRows = null;
                _servicesGrid!.DataSource = null;
            }

            UpdateElapsedTime();
        }

        private void OpenRoomButton_Click(object? sender, EventArgs e)
        {
            if (_selectedRoom is null || _selectedRoom.IsOccupied)
            {
                return;
            }

            try
            {
                _repository.OpenRoom(_selectedRoom.Id);
                ReloadData(_selectedRoom.Id);
                SetStatus("Đã mở phòng và tạo hóa đơn.");
            }
            catch (Exception ex)
            {
                SetStatus($"Không thể mở phòng: {ex.Message}");
                ReloadData(_selectedRoom.Id);
            }
        }

        private void AddServiceButton_Click(object? sender, EventArgs e)
        {
            if (_selectedRoom?.InvoiceId is not int invoiceId || _serviceRows is null ||
                _serviceCatalog is null || _servicePicker?.SelectedValue is null)
            {
                return;
            }

            var serviceId = Convert.ToInt32(_servicePicker.SelectedValue);
            var catalogRow = _serviceCatalog.Rows.Cast<DataRow>()
                .FirstOrDefault(row => row.Field<int>("MaDV") == serviceId);
            if (catalogRow is null)
            {
                return;
            }

            var quantity = decimal.ToInt32(_quantityPicker!.Value);
            var existingRow = _serviceRows.Rows.Cast<DataRow>()
                .FirstOrDefault(row => row.RowState != DataRowState.Deleted && row.Field<int>("MaDV") == serviceId);
            var currentQuantity = existingRow is null ? 0 : existingRow.Field<int>("SoLuong");
            var newTotalQuantity = currentQuantity + quantity;
            if (newTotalQuantity > catalogRow.Field<int>("TonKho"))
            {
                SetStatus("Số lượng yêu cầu vượt quá tồn kho hiện tại.");
                return;
            }

            decimal unitPrice;
            if (existingRow is null)
            {
                var newRow = _serviceRows.NewRow();
                newRow["MaHD"] = invoiceId;
                newRow["MaDV"] = serviceId;
                newRow["TenDV"] = catalogRow.Field<string>("TenDV") ?? string.Empty;
                newRow["SoLuong"] = newTotalQuantity;
                unitPrice = catalogRow.Field<decimal>("DonGia");
                newRow["DonGia"] = unitPrice;
                _serviceRows.Rows.Add(newRow);
            }
            else
            {
                existingRow["SoLuong"] = newTotalQuantity;
                unitPrice = existingRow.Field<decimal>("DonGia");
            }

            try
            {
                _repository.SaveInvoiceService(invoiceId, serviceId, newTotalQuantity, unitPrice);
                SetStatus("Đã thêm dịch vụ vào hóa đơn.");
            }
            catch (Exception ex)
            {
                ReloadData(_selectedRoom.Id);
                SetStatus($"Không thể thêm dịch vụ: {ex.Message}");
            }
        }

        private void PayButton_Click(object? sender, EventArgs e)
        {
            if (_selectedRoom?.InvoiceId is not int invoiceId || _serviceRows is null)
            {
                return;
            }

            if (MessageBox.Show(
                    $"Thanh toán phòng {_selectedRoom.Name} và giải phóng phòng?",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var roomId = _selectedRoom.Id;
            try
            {
                var summary = _repository.SettleInvoice(roomId, invoiceId, _serviceRows);
                MessageBox.Show(
                    $"Thời gian: {summary.Hours:N2} giờ\r\n" +
                    $"Tiền phòng: {summary.RoomTotal:N0} đ\r\n" +
                    $"Tiền dịch vụ: {summary.ServiceTotal:N0} đ\r\n" +
                    $"Tổng thanh toán: {summary.GrandTotal:N0} đ",
                    "Thanh toán hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ReloadData(roomId);
            }
            catch (Exception ex)
            {
                SetStatus($"Thanh toán thất bại; giao dịch đã được hoàn tác: {ex.Message}");
                ReloadData(roomId);
            }
        }

        private void UpdateElapsedTime()
        {
            if (_selectedRoom?.StartedAt is DateTime startedAt && _selectedRoom.IsOccupied)
            {
                var elapsed = DateTime.Now - startedAt;
                _elapsedLabel!.Text = elapsed < TimeSpan.Zero ? "00:00:00" : elapsed.ToString("c");
            }
            else if (_elapsedLabel is not null)
            {
                _elapsedLabel.Text = "00:00:00";
            }
        }

        private void SetStatus(string message)
        {
            if (_statusLabel is not null)
            {
                _statusLabel.Text = message;
            }
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _elapsedTimer.Stop();
            _elapsedTimer.Dispose();
        }
    }
}
