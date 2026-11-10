using System.Data;
using Microsoft.Data.SqlClient;

namespace bai6._5;

internal sealed class KaraokeRepository
{
    private const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KaraokeDB;Integrated Security=true;TrustServerCertificate=true";

    public IReadOnlyList<KaraokeRoom> GetRooms()
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(
            "SELECT p.MaPhong, p.TenPhong, p.LoaiPhong, p.GiaTheoGio, p.TrangThai, h.MaHD, h.GioVao " +
            "FROM dbo.PhongHat AS p " +
            "OUTER APPLY (SELECT TOP (1) MaHD, GioVao FROM dbo.HoaDon WHERE MaPhong = p.MaPhong AND TrangThai = 0 ORDER BY MaHD DESC) AS h " +
            "ORDER BY p.MaPhong;", connection);

        connection.Open();
        using var reader = command.ExecuteReader();
        var rooms = new List<KaraokeRoom>();
        while (reader.Read())
        {
            rooms.Add(new KaraokeRoom(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                reader.GetDecimal(3),
                reader.GetString(4),
                reader.IsDBNull(5) ? null : reader.GetInt32(5),
                reader.IsDBNull(6) ? null : reader.GetDateTime(6)));
        }

        return rooms;
    }

    public DataTable GetServiceCatalog()
    {
        using var connection = new SqlConnection(ConnectionString);
        using var adapter = new SqlDataAdapter(
            "SELECT MaDV, TenDV, DonGia, TonKho FROM dbo.DichVu ORDER BY TenDV;", connection);
        var table = new DataTable("DichVu");
        adapter.Fill(table);
        return table;
    }

    public DataTable GetInvoiceServices(int invoiceId)
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(
            "SELECT ct.MaHD, ct.MaDV, dv.TenDV, ct.SoLuong, ct.DonGia " +
            "FROM dbo.ChiTietDichVu AS ct INNER JOIN dbo.DichVu AS dv ON dv.MaDV = ct.MaDV " +
            "WHERE ct.MaHD = @MaHD ORDER BY dv.TenDV;", connection);
        command.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;

        using var adapter = new SqlDataAdapter(command);
        var table = new DataTable("ChiTietDichVu");
        adapter.Fill(table);
        return table;
    }

    public void SaveInvoiceService(int invoiceId, int serviceId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0 || unitPrice < 0)
        {
            throw new InvalidOperationException("Số lượng dịch vụ phải lớn hơn 0 và đơn giá không được âm.");
        }

        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        using (var availabilityCommand = new SqlCommand(
            "SELECT dv.TonKho " +
            "FROM dbo.HoaDon AS hd WITH (UPDLOCK, HOLDLOCK) " +
            "INNER JOIN dbo.DichVu AS dv WITH (UPDLOCK, HOLDLOCK) ON dv.MaDV = @MaDV " +
            "WHERE hd.MaHD = @MaHD AND hd.TrangThai = 0;",
            connection, transaction))
        {
            availabilityCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;
            availabilityCommand.Parameters.Add("@MaDV", SqlDbType.Int).Value = serviceId;
            using var reader = availabilityCommand.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Hóa đơn không còn mở hoặc dịch vụ không tồn tại.");
            }

            var stock = reader.GetInt32(0);
            if (quantity > stock)
            {
                throw new InvalidOperationException("Số lượng yêu cầu vượt quá tồn kho hiện tại.");
            }
        }

        using (var saveCommand = new SqlCommand(
            "IF EXISTS (SELECT 1 FROM dbo.ChiTietDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND MaDV = @MaDV) " +
            "UPDATE dbo.ChiTietDichVu SET SoLuong = @SoLuong WHERE MaHD = @MaHD AND MaDV = @MaDV; " +
            "ELSE INSERT dbo.ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) VALUES (@MaHD, @MaDV, @SoLuong, @DonGia);",
            connection, transaction))
        {
            saveCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;
            saveCommand.Parameters.Add("@MaDV", SqlDbType.Int).Value = serviceId;
            saveCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = quantity;
            AddMoneyParameter(saveCommand, "@DonGia", unitPrice);
            saveCommand.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    public int OpenRoom(int roomId)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        using (var updateRoom = new SqlCommand(
            "UPDATE dbo.PhongHat SET TrangThai = N'Đang hát' " +
            "WHERE MaPhong = @MaPhong AND TrangThai = N'Trống' " +
            "AND NOT EXISTS (SELECT 1 FROM dbo.HoaDon WHERE MaPhong = @MaPhong AND TrangThai = 0);",
            connection, transaction))
        {
            updateRoom.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
            if (updateRoom.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException("Phòng không còn trống hoặc đã có hóa đơn đang mở.");
            }
        }

        int invoiceId;
        using (var createInvoice = new SqlCommand(
            "INSERT dbo.HoaDon (MaPhong, GioVao, TrangThai) " +
            "OUTPUT INSERTED.MaHD VALUES (@MaPhong, GETDATE(), 0);",
            connection, transaction))
        {
            createInvoice.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
            invoiceId = (int)createInvoice.ExecuteScalar()!;
        }

        transaction.Commit();
        return invoiceId;
    }

    public SettlementSummary SettleInvoice(int roomId, int invoiceId, DataTable services)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

        DateTime startedAt;
        using (var invoiceCommand = new SqlCommand(
            "SELECT GioVao FROM dbo.HoaDon WITH (UPDLOCK, HOLDLOCK) " +
            "WHERE MaHD = @MaHD AND MaPhong = @MaPhong AND TrangThai = 0;",
            connection, transaction))
        {
            invoiceCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;
            invoiceCommand.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
            var value = invoiceCommand.ExecuteScalar();
            if (value is not DateTime invoiceStartedAt)
            {
                throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã được thanh toán.");
            }

            startedAt = invoiceStartedAt;
        }

        var items = new List<ServiceOrder>();
        foreach (DataRow row in services.Rows)
        {
            if (row.RowState == DataRowState.Deleted)
            {
                continue;
            }

            var serviceId = Convert.ToInt32(row["MaDV"]);
            var quantity = Convert.ToInt32(row["SoLuong"]);
            var unitPrice = Convert.ToDecimal(row["DonGia"]);
            if (quantity <= 0 || unitPrice < 0)
            {
                throw new InvalidOperationException("Số lượng dịch vụ phải lớn hơn 0 và đơn giá không được âm.");
            }

            items.Add(new ServiceOrder(serviceId, quantity, unitPrice));
        }

        if (items.Select(item => item.ServiceId).Distinct().Count() != items.Count)
        {
            throw new InvalidOperationException("Danh sách dịch vụ có mã dịch vụ bị trùng.");
        }

        var serviceTotal = decimal.Round(
            items.Sum(item => item.Quantity * item.UnitPrice), 2, MidpointRounding.AwayFromZero);
        var endedAt = GetServerTime(connection, transaction);
        var hours = Math.Max(0, (decimal)(endedAt - startedAt).TotalHours);

        decimal hourlyRate;
        using (var roomCommand = new SqlCommand(
            "SELECT GiaTheoGio FROM dbo.PhongHat WITH (UPDLOCK, HOLDLOCK) WHERE MaPhong = @MaPhong AND TrangThai = N'Đang hát';",
            connection, transaction))
        {
            roomCommand.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
            var value = roomCommand.ExecuteScalar();
            if (value is not decimal rate)
            {
                throw new InvalidOperationException("Phòng không ở trạng thái đang hát.");
            }

            hourlyRate = rate;
        }

        var roomTotal = decimal.Round(hours * hourlyRate, 2, MidpointRounding.AwayFromZero);
        foreach (var item in items)
        {
            using var stockCommand = new SqlCommand(
                "UPDATE dbo.DichVu SET TonKho = TonKho - @SoLuong " +
                "WHERE MaDV = @MaDV AND TonKho >= @SoLuong;",
                connection, transaction);
            stockCommand.Parameters.Add("@MaDV", SqlDbType.Int).Value = item.ServiceId;
            stockCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.Quantity;
            if (stockCommand.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException($"Dịch vụ mã {item.ServiceId} không đủ tồn kho.");
            }
        }

        foreach (var item in items)
        {
            using var detailCommand = new SqlCommand(
                "IF EXISTS (SELECT 1 FROM dbo.ChiTietDichVu WITH (UPDLOCK, HOLDLOCK) WHERE MaHD = @MaHD AND MaDV = @MaDV) " +
                "UPDATE dbo.ChiTietDichVu SET SoLuong = @SoLuong, DonGia = @DonGia WHERE MaHD = @MaHD AND MaDV = @MaDV; " +
                "ELSE INSERT dbo.ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) VALUES (@MaHD, @MaDV, @SoLuong, @DonGia);",
                connection, transaction);
            detailCommand.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;
            detailCommand.Parameters.Add("@MaDV", SqlDbType.Int).Value = item.ServiceId;
            detailCommand.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.Quantity;
            detailCommand.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = item.UnitPrice;
            detailCommand.Parameters["@DonGia"].Precision = 18;
            detailCommand.Parameters["@DonGia"].Scale = 2;
            detailCommand.ExecuteNonQuery();
        }

        using (var updateInvoice = new SqlCommand(
            "UPDATE dbo.HoaDon SET GioRa = @GioRa, TongTienPhong = @TienPhong, " +
            "TongTienDichVu = @TienDichVu, TrangThai = 1 WHERE MaHD = @MaHD AND TrangThai = 0;",
            connection, transaction))
        {
            updateInvoice.Parameters.Add("@GioRa", SqlDbType.DateTime).Value = endedAt;
            AddMoneyParameter(updateInvoice, "@TienPhong", roomTotal);
            AddMoneyParameter(updateInvoice, "@TienDichVu", serviceTotal);
            updateInvoice.Parameters.Add("@MaHD", SqlDbType.Int).Value = invoiceId;
            if (updateInvoice.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException("Không thể cập nhật hóa đơn.");
            }
        }

        using (var updateRoom = new SqlCommand(
            "UPDATE dbo.PhongHat SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong AND TrangThai = N'Đang hát';",
            connection, transaction))
        {
            updateRoom.Parameters.Add("@MaPhong", SqlDbType.Int).Value = roomId;
            if (updateRoom.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException("Không thể cập nhật trạng thái phòng.");
            }
        }

        transaction.Commit();
        return new SettlementSummary(endedAt, hours, roomTotal, serviceTotal, roomTotal + serviceTotal);
    }

    private static DateTime GetServerTime(SqlConnection connection, SqlTransaction transaction)
    {
        using var command = new SqlCommand("SELECT GETDATE();", connection, transaction);
        return (DateTime)command.ExecuteScalar()!;
    }

    private static void AddMoneyParameter(SqlCommand command, string name, decimal value)
    {
        var parameter = command.Parameters.Add(name, SqlDbType.Decimal);
        parameter.Precision = 18;
        parameter.Scale = 2;
        parameter.Value = value;
    }

    private sealed record ServiceOrder(int ServiceId, int Quantity, decimal UnitPrice);
}

internal sealed record KaraokeRoom(
    int Id,
    string Name,
    string Type,
    decimal HourlyRate,
    string Status,
    int? InvoiceId,
    DateTime? StartedAt)
{
    public bool IsOccupied => Status == "Đang hát";
}

internal sealed record SettlementSummary(
    DateTime EndedAt,
    decimal Hours,
    decimal RoomTotal,
    decimal ServiceTotal,
    decimal GrandTotal);
