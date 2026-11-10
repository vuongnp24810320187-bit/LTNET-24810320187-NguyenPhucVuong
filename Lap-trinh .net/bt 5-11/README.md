# BÁO CÁO BÀI TẬP LẬP TRÌNH C# .NET - KIẾN TRÚC 3 LỚP (3-TIER ARCHITECTURE)
## HỆ THỐNG QUẢN LÝ BÁN HÀNG & NHÂN SỰ TIỀN LƯƠNG (RETAIL & HRM SYSTEM)

---

## MỤC 1: THÔNG TIN SINH VIÊN
* **Họ và tên sinh viên:** Nguyễn Phúc Vượng 
* **Mã sinh viên:** 24810320187
* **Lớp / Khóa:** D19QTANM1
* **Môn học:** Lập trình C# .NET / Lập trình Windows Form

---

## MỤC 2: HÌNH ẢNH KẾT QUẢ CÁC MÀN HÌNH CHỨC NĂNG

> **Lưu ý:** Sinh viên chụp ảnh màn hình ứng dụng thực tế đang chạy trên máy tính, đặt tên file ảnh và lưu vào thư mục `images/` theo đúng định dạng dưới đây.

### 1. Màn hình Chính / Tổng quan hệ thống
![Màn hình chính](images/01_main_screen.png)

* **Mô tả chức năng:**
  * Giao diện Dashboard trung tâm hiện đại, chuyên nghiệp với Sidebar điều hướng dạng cây phân hệ và thanh trạng thái Topbar.
  * Hiển thị các khối thẻ thống kê trực quan (KPI Cards):
    * **Doanh thu hôm nay (VNĐ):** Cập nhật thời gian thực bằng truy vấn tối ưu ADO.NET `ExecuteScalar()`.
    * **Tổng số mặt hàng kinh doanh:** Số lượng chủng loại sản phẩm hiện có trong kho.
    * **Tổng số nhân sự:** Số lượng nhân viên đang làm việc thuộc các phòng ban.
    * **Tổng quỹ lương ước tính:** Dự toán tổng chi phí chi trả lương cho toàn bộ công ty.
  * Cho phép người dùng truy cập nhanh vào tất cả các phân hệ: Quản lý Bán hàng, Danh mục Hàng hóa, Khách hàng, Quản lý Nhân sự, Chấm công ngắt kết nối (Disconnected Model), Bảng tính & Chốt lương, Báo cáo thống kê và Mô phỏng An toàn dữ liệu.

---

### 2. Màn hình Quản lý Danh mục (CRUD - Thêm/Sửa/Xóa)
![Màn hình CRUD](images/02_crud_screen.png)

* **Mô tả chức năng:**
  * Cung cấp các biểu mẫu quản lý danh mục nghiệp vụ: **Sản phẩm (Hàng hóa)**, **Khách hàng**, **Nhân viên bán hàng** và **Hồ sơ Nhân sự phòng ban**.
  * Hỗ trợ đầy đủ bộ thao tác chuẩn CRUD:
    * **Thêm mới (Create):** Kiểm tra hợp lệ dữ liệu (Validation) các trường bắt buộc, định dạng số, mã duy nhất trước khi ghi nhận vào SQL Server.
    * **Cập nhật / Sửa (Update):** Khi người dùng nhấp chọn dòng bất kỳ trên `DataGridView`, dữ liệu tự động đổ lên các ô nhập liệu tương ứng; cho phép sửa thông tin và lưu lại nhanh chóng.
    * **Xóa (Delete):** Hộp thoại xác nhận trước khi xóa (Confirm Dialog), đồng thời kiểm tra toàn vẹn dữ liệu (ràng buộc khóa ngoại Foreign Key) để ngăn chặn lỗi runtime.
    * **Tìm kiếm thông minh (Search as you type):** Tìm kiếm theo Tên sản phẩm, Tên nhân viên, Số CCCD, Phòng ban,... Sử dụng cơ chế `SqlParameter` ngăn ngừa triệt để nguy cơ SQL Injection.

---

### 3. Màn hình Thực thi Nghiệp vụ chính (Lập Hóa đơn / Bán hàng / Chấm công)
![Màn hình Nghiệp vụ](images/03_transaction_screen.png)

* **Mô tả chức năng:**
  * **Nghiệp vụ 1: Lập Hóa đơn & Bán hàng (Point of Sale - POS)**
    * Cho phép chọn Nhân viên thu ngân và Khách hàng; tự động hiển thị số điểm tích lũy hiện có của khách hàng.
    * Tìm kiếm và chọn sản phẩm trực tiếp từ lưới tồn kho; nhập số lượng và bấm "Thêm vào giỏ". Hệ thống tự động kiểm tra số lượng tồn thực tế trong kho trước khi thêm.
    * Lưới giỏ hàng tự động tính toán Thành tiền, Tổng tiền hóa đơn theo thời gian thực.
    * Cho phép xóa từng món hoặc xóa toàn bộ giỏ hàng khi khách đổi ý.
    * Khi nhấn **"Thanh toán"**, hệ thống khởi tạo một `SqlTransaction` cấp độ ACID:
      1. Tạo bản ghi mới trong bảng `HoaDon` (sinh mã tự động `IDENTITY`).
      2. Ghi nhận toàn bộ danh sách sản phẩm trong giỏ vào bảng `ChiTietHoaDon`.
      3. Tự động trừ số lượng tồn kho trong bảng `SanPham`.
      4. Tự động cộng điểm tích lũy cho Khách hàng (100.000 VNĐ tương ứng tích 1 điểm).
      5. Nếu tất cả các bước thành công $\rightarrow$ Thực hiện `Commit()`. Nếu xảy ra bất kỳ lỗi nào $\rightarrow$ Lập tức `Rollback()` toàn bộ thao tác, bảo đảm không bao giờ bị lệch kho hoặc mất dữ liệu.
  * **Nghiệp vụ 2: Chấm công ngắt kết nối (Disconnected Model - SqlDataAdapter & SqlCommandBuilder)**
    * Nhập tháng/năm làm việc (ví dụ: `11/2026`).
    * Nhấn **"Nạp bảng chấm công"**: Nạp toàn bộ dữ liệu từ SQL Server vào bộ nhớ RAM (`DataTable`) thông qua `SqlDataAdapter`. Nếu nhân viên mới chưa có dòng chấm công trong tháng, hệ thống tự động khởi tạo dữ liệu mẫu.
    * Người quản lý có thể trực tiếp nhập, sửa Số ngày công và Số giờ OT ngay trên các ô của lưới `DataGridView` mà không cần duy trì kết nối mạng liên tục với database.
    * Nhấn **"Đồng bộ / Lưu chấm công"**: Đối tượng `SqlCommandBuilder` tự sinh các lệnh `UPDATE` và đẩy toàn bộ thay đổi từ `DataTable` về lại cơ sở dữ liệu SQL Server chỉ trong một thao tác.

---

### 4. Màn hình Báo cáo - Thống kê
![Màn hình Thống kê](images/04_report_screen.png)

* **Mô tả chức năng:**
  * **Thống kê Doanh thu & Hóa đơn (ExecuteScalar):**
    * Áp dụng phương thức `SqlCommand.ExecuteScalar()` để truy vấn trực tiếp các giá trị số tổng hợp (`SUM(TongTien)`, `COUNT(MaHD)`).
    * Giúp tối ưu hóa tốc độ truy vấn tối đa, giảm thiểu băng thông đường truyền giữa ứng dụng và máy chủ CSDL.
  * **Báo cáo Top 5 Sản phẩm Bán chạy nhất (SqlDataReader):**
    * Áp dụng phương thức `SqlCommand.ExecuteReader()` kết hợp `SqlDataReader`.
    * Đọc dữ liệu theo luồng một chiều (Forward-Only) và chỉ đọc (Read-Only), mang lại hiệu năng cao nhất trong ADO.NET khi kết xuất danh sách báo cáo mà không tốn dung lượng bộ nhớ RAM.
  * **Báo cáo Bảng lương Tổng hợp:**
    * Bảng tổng hợp lương chi tiết theo công thức nghiệp vụ:
      $$\text{Thực lĩnh} = \left(\frac{\text{Lương cơ bản} \times \text{Hệ số}}{22} \times \text{Ngày công}\right) + (\text{Giờ OT} \times 50.000)$$
    * Thống kê tổng chi phí chi trả lương theo từng phòng ban và toàn công ty.

---

### 5. Minh họa An toàn Dữ liệu (Phòng chống SQL Injection / Báo lỗi Transaction Rollback)
![Minh họa Xử lý lỗi](images/05_error_handling.png)

* **Mô tả chức năng:**
  * **Tab 1: Kiểm thử & Phòng chống SQL Injection (SQL Injection Mitigation)**
    * Cung cấp giao diện trực quan cho phép nhập chuỗi payload tấn công phổ biến, ví dụ: `' OR '1'='1' --` hoặc `' UNION SELECT ...`.
    * **Nút "Thực thi Lỗ hổng (Ghép chuỗi SQL không an toàn)":** Minh họa câu truy vấn sai chuẩn `string.Format` hoặc toán tử `+`. Kẻ tấn công bẻ gãy cú pháp mệnh đề `WHERE` và làm lộ toàn bộ dữ liệu bí mật trong hệ thống.
    * **Nút "Thực thi An toàn (Sử dụng SqlParameter)":** Minh họa kỹ thuật Parameterized Query chuẩn ADO.NET. Trình phân tích cú pháp SQL Server tự động coi dữ liệu đầu vào là chuỗi giá trị thuần túy (Literal Value), vô hiệu hóa hoàn toàn mọi nỗ lực tiêm mã độc SQL.
  * **Tab 2: Kiểm thử Giao dịch Toàn vẹn (ACID SqlTransaction Rollback)**
    * Giả lập tình huống giao dịch gặp sự cố trong quá trình thanh toán đơn hàng (ví dụ: số lượng tồn trong kho bị âm do người khác vừa mua hết, kết nối mạng gián đoạn đột ngột, vi phạm khóa ngoại).
    * Khi phát hiện bất thường, khối `try...catch` lập tức kích hoạt lệnh `transaction.Rollback()`.
    * Hệ thống in ra chi tiết nhật ký giao dịch (Log), chứng minh:
      * Mã hóa đơn không được tạo trong bảng `HoaDon`.
      * Chi tiết hóa đơn không bị lưu rác vào `ChiTietHoaDon`.
      * Số lượng tồn kho và điểm khách hàng giữ nguyên 100% như trước khi bắt đầu giao dịch.

---

## MỤC 3: CẤU TRÚC GIẢI PHÁP & MÃ NGUỒN CHUẨN 3-TIER

Repository được tổ chức theo quy chuẩn cấu trúc thư mục của đồ án môn học:

```text
├── Database/
│   ├── ScriptDB.sql             # Script SQL hoàn chỉnh (Tạo CSDL & chèn dữ liệu mẫu cho SalesDB và HRMDB)
│   ├── SalesDB.sql              # Script riêng cho CSDL Quản lý bán hàng (Bài 6.1)
│   └── HRMDB.sql                # Script riêng cho CSDL Quản lý nhân sự & Tiền lương (Bài 6.2)
├── RetailAndHRMSystem/          # Thư mục mã nguồn Solution C# (Kiến trúc 3 lớp)
│   ├── DTO/                     # Data Transfer Objects (Định nghĩa các lớp thực thể dữ liệu)
│   │   ├── KhachHangDTO.cs
│   │   ├── NhanVienSalesDTO.cs
│   │   ├── SanPhamDTO.cs
│   │   ├── HoaDonDTO.cs
│   │   ├── ChiTietHoaDonDTO.cs
│   │   ├── PhongBanDTO.cs
│   │   ├── NhanSuDTO.cs
│   │   ├── ChamCongDTO.cs
│   │   └── BangLuongDTO.cs
│   ├── DAL/                     # Data Access Layer (Truy vấn CSDL ADO.NET thuần)
│   │   ├── DbHelper.cs          # Quản lý mở/đóng kết nối, transaction an toàn
│   │   ├── SanPhamDAL.cs
│   │   ├── KhachHangDAL.cs
│   │   ├── NhanVienSalesDAL.cs
│   │   ├── HoaDonDAL.cs
│   │   ├── SalesThongKeDAL.cs
│   │   ├── PhongBanDAL.cs
│   │   ├── NhanSuDAL.cs
│   │   ├── ChamCongDAL.cs       # Áp dụng SqlDataAdapter & Disconnected Model
│   │   └── BangLuongDAL.cs      # Áp dụng SqlTransaction chốt lương
│   ├── BUS/                     # Business Logic Layer (Xử lý quy tắc nghiệp vụ)
│   │   ├── SalesBUS.cs          # Nghiệp vụ bán hàng, giỏ hàng, trừ kho, tích điểm, rollback
│   │   └── HRMBUS.cs            # Nghiệp vụ tính lương, kiểm tra ngày công, chấm công
│   ├── GUI/                     # Presentation Layer (Giao diện Windows Forms)
│   │   ├── frmMain.cs           # Màn hình chính Dashboard tổng quan
│   │   ├── frmSanPham.cs        # CRUD Quản lý sản phẩm
│   │   ├── frmKhachHang.cs      # CRUD Quản lý khách hàng
│   │   ├── frmNhanVienSales.cs  # CRUD Nhân viên thu ngân
│   │   ├── frmBanHang.cs        # Nghiệp vụ Lập hóa đơn / Bán hàng POS
│   │   ├── frmBaoCaoSales.cs    # Thống kê doanh thu & Top 5 bán chạy
│   │   ├── frmNhanSu.cs         # CRUD Hồ sơ nhân sự phòng ban
│   │   ├── frmChamCong.cs       # Nghiệp vụ Chấm công ngắt kết nối
│   │   ├── frmTinhLuong.cs      # Bảng tính & Chốt lương SqlTransaction
│   │   ├── frmAnToanDuLieu.cs   # Minh họa phòng chống SQL Injection & Rollback
│   │   ├── frmConfigConnection.cs # Cấu hình linh hoạt chuỗi kết nối SQL Server
│   │   └── UITheme.cs           # Bảng màu chuẩn giao diện hiện đại Flat UI
│   ├── App.config               # File cấu hình ConnectionStrings & tham số hệ thống
│   ├── Program.cs               # Entry Point khởi chạy phần mềm
│   └── RetailAndHRMSystem.csproj
├── images/                      # Thư mục chứa 5 ảnh chụp kết quả màn hình nộp bài
│   ├── 01_main_screen.png       # 1. Ảnh màn hình Chính / Tổng quan
│   ├── 02_crud_screen.png       # 2. Ảnh màn hình Quản lý Danh mục (CRUD)
│   ├── 03_transaction_screen.png# 3. Ảnh màn hình Nghiệp vụ chính (Bán hàng / Chấm công)
│   ├── 04_report_screen.png     # 4. Ảnh màn hình Báo cáo - Thống kê
│   └── 05_error_handling.png    # 5. Ảnh màn hình Minh họa An toàn Dữ liệu
├── .gitignore                   # Loại trừ các file rác build (.vs, bin, obj, suo)
├── README.md                    # Báo cáo chi tiết đồ án nộp bài (File hiện tại)
└── RetailAndHRMSystem.sln       # File Solution mở bằng Visual Studio
```

---

## MỤC 4: HƯỚNG DẪN CÀI ĐẶT VÀ CHẠY ỨNG DỤNG

### 1. Yêu cầu môi trường
* **Hệ điều hành:** Windows 10 / Windows 11 (64-bit).
* **IDE:** Visual Studio 2022 (phiên bản Community, Professional hoặc Enterprise), tích hợp gói workload **.NET Desktop Development**.
* **Hệ quản trị CSDL:** Microsoft SQL Server 2017, 2019, 2022 hoặc **SQL Server Express / LocalDB**.
* **Nền tảng:** .NET SDK (hỗ trợ .NET 8.0 Windows Forms trở lên).

### 2. Các bước triển khai

#### Bước 1: Khởi tạo Cơ sở dữ liệu SQL Server
1. Mở công cụ **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
2. Kết nối tới máy chủ SQL Server của bạn (ví dụ: `.` hoặc `localhost` hoặc `(localdb)\MSSQLLocalDB`).
3. Mở file script `Database/ScriptDB.sql` trong thư mục đồ án.
4. Nhấn **Execute (F5)** để tự động khởi tạo 2 cơ sở dữ liệu `SalesDB` và `HRMDB`, đồng thời nạp toàn bộ dữ liệu mẫu ban đầu.

#### Bước 2: Cấu hình Chuỗi kết nối (Connection String)
Mở file `RetailAndHRMSystem/App.config` và điều chỉnh tên Server nếu máy tính của bạn sử dụng SQL Server Instance khác:

```xml
<connectionStrings>
  <!-- Nếu dùng LocalDB, đổi Data Source thành (localdb)\MSSQLLocalDB -->
  <add name="SalesDBConn" 
       connectionString="Server=.;Database=SalesDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;" 
       providerName="Microsoft.Data.SqlClient" />
  
  <add name="HRMDBConn" 
       connectionString="Server=.;Database=HRMDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;" 
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

*(Hoặc khi chạy phần mềm, người dùng có thể nhấp vào nút "⚙ Cấu hình kết nối" ngay trên góc phải màn hình chính để kiểm tra và cập nhật kết nối trực tiếp).*

#### Bước 3: Mở Solution và Chạy ứng dụng
1. Nhấp đúp vào file `RetailAndHRMSystem.sln` để mở giải pháp bằng **Visual Studio 2022**.
2. Tại thanh công cụ trên cùng, chọn cấu hình `Debug` hoặc `Release` và nền tảng `Any CPU`.
3. Nhấn phím **F5** (hoặc nút **Start**) để biên dịch và trải nghiệm toàn bộ các tính năng của hệ thống.

---

## MỤC 5: HƯỚNG DẪN KHỞI TẠO REPOSITORY PUBLIC TRÊN GITHUB

Dành cho sinh viên thực hiện nộp bài lên GitHub:

1. **Khởi tạo Git trong thư mục dự án:**
   Mở terminal (PowerShell hoặc Git Bash) tại thư mục chứa file `RetailAndHRMSystem.sln` và chạy các lệnh:
   ```bash
   git init
   git add .
   git commit -m "feat: Hoàn thiện hệ thống RetailAndHRMSystem chuẩn kiến trúc 3-Tier"
   ```

2. **Tạo Repository trên GitHub:**
   * Truy cập [https://github.com/new](https://github.com/new).
   * Đặt tên Repository (ví dụ: `LapTrinhNet-3Tier-RetailAndHRMSystem`).
   * Chọn chế độ **Public** (Bắt buộc theo yêu cầu nộp bài).
   * **Không** tích chọn *"Add a README file"* (vì dự án đã có sẵn file README.md này).
   * Nhấn nút **Create repository**.

3. **Liên kết và Đẩy mã nguồn lên GitHub:**
   ```bash
   git branch -M main
   git remote add origin https://github.com/<tai-khoan-github-cua-ban>/<ten-repo>.git
   git push -u origin main
   ```

4. **Kiểm tra kết quả:**
   * Truy cập vào trang web GitHub của Repository vừa tạo.
   * Xác nhận trang chủ hiển thị đầy đủ nội dung báo cáo `README.md` cùng 5 hình ảnh minh họa trong thư mục `images/`.
   * Sao chép đường link Repository và dán vào biểu mẫu nộp bài của giảng viên.

