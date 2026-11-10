# BÁO CÁO BÀI TẬP LẬP TRÌNH C# .NET - KIẾN TRÚC 3 LỚP (3-TIER ARCHITECTURE)
## HỆ THỐNG QUẢN TRỊ DỊCH VỤ DOANH NGHIỆP (ENTERPRISE SERVICES MANAGEMENT)
### TỔNG HỢP BÀI TẬP CHƯƠNG 6: ADO.NET NÂNG CAO, TRANSACTIONS & KIẾN TRÚC 3 LỚP (BÀI 6.3, 6.4, 6.5)

---

## MỤC 1: THÔNG TIN SINH VIÊN
* **Họ và tên sinh viên:** Nguyễn Phúc Vượng
* **Mã sinh viên:** 24810320187
* **Lớp / Khóa:** D19QTANM1 - Lập trình .NET
* **Môn học:** Lập trình C# .NET / Lập trình Windows Forms
* **Giảng viên hướng dẫn:** Giảng viên Bộ môn Lập trình .NET - Khoa Công nghệ Thông tin
* **Link Repository GitHub (Public):** [https://github.com/vuongnp24810320187-bit/LTNET-24810320187-NguyenPhucVuong](https://github.com/vuongnp24810320187-bit/LTNET-24810320187-NguyenPhucVuong)

---

## MỤC 2: HÌNH ẢNH KẾT QUẢ CÁC MÀN HÌNH CHỨC NĂNG

> *(Sinh viên chụp ảnh màn hình ứng dụng đang chạy thực tế và nhúng vào file Markdown theo cú pháp `![Tên ảnh](đường-dẫn-ảnh)`)*

### 1. Màn hình Chính / Tổng quan hệ thống
![Màn hình chính](images/01_main_screen.png)

* **Mô tả chức năng & Kỹ thuật triển khai:**
  * **Dashboard tổng quan hiện đại:** Giao diện trung tâm thiết kế theo phong cách Flat UI với thanh Sidebar điều hướng đa phân hệ và Topbar hiển thị trạng thái kết nối CSDL SQL Server.
  * **Hệ thống thẻ thống kê (KPI Cards) thời gian thực:**
    * **Doanh thu Karaoke hôm nay:** Tổng hợp giá trị các hóa đơn phòng và dịch vụ đã thanh toán, sử dụng phương thức tối ưu `SqlCommand.ExecuteScalar()`.
    * **Phòng đang hát (Công suất phục vụ):** Đếm trực tiếp số lượng phòng có trạng thái `'Đang hát'`, cập nhật liên tục để nhân viên nắm bắt tình trạng phòng.
    * **Doanh thu Garage bảo dưỡng:** Tổng tiền các phiếu sửa chữa hoàn thành, bao gồm tiền vật tư thay thế và tiền công thợ.
    * **Tiến độ công việc dự án:** Tổng số đầu việc trên bảng Kanban đang thực hiện (`To Do`, `In Progress`, `Review`, `Done`).
  * **Điều hướng 1-click:** Cho phép người dùng chuyển nhanh đến tất cả các phân hệ quản lý: Quản lý danh mục, Thực thi nghiệp vụ đặt phòng & lập hóa đơn, Trung tâm báo cáo thống kê, và Phân hệ kiểm thử an toàn dữ liệu.

---

### 2. Màn hình Quản lý Danh mục (CRUD - Thêm/Sửa/Xóa)
![Màn hình CRUD](images/02_crud_screen.png)

* **Mô tả chức năng & Kỹ thuật triển khai:**
  * **Bộ biểu mẫu quản lý danh mục đa chức năng (chia theo TabControl):**
    * **Tab 1 - Danh mục Phòng hát:** Mã phòng, Tên phòng, Loại phòng (Thường, VIP, Party), Giá theo giờ (VNĐ), Trạng thái (Trống / Đang hát).
    * **Tab 2 - Danh mục Dịch vụ F&B:** Mã dịch vụ, Tên món ăn / thức uống, Đơn giá niêm yết, Số lượng tồn kho.
    * **Tab 3 - Danh mục Phụ tùng Garage:** Mã phụ tùng, Tên linh kiện phụ tùng, Đơn giá xuất kho, Số lượng tồn.
  * **Hỗ trợ đầy đủ bộ thao tác chuẩn CRUD:**
    * **Thêm mới (Create):** Kiểm tra tính hợp lệ dữ liệu (Validation) ở tầng BUS: tên không được rỗng, đơn giá không âm, tồn kho $\ge 0$.
    * **Cập nhật (Update):** Khi người dùng nhấp chọn bất kỳ dòng nào trên `DataGridView`, dữ liệu tự động binding đổ ngược lên các ô nhập liệu tương ứng; cho phép sửa thông tin và lưu lại nhanh chóng.
    * **Xóa (Delete):** Hộp thoại xác nhận trước khi xóa (Confirm Dialog), đồng thời bắt lỗi toàn vẹn dữ liệu (ràng buộc khóa ngoại Foreign Key) để ngăn chặn phát sinh lỗi runtime khi phòng hoặc phụ tùng đã có hóa đơn liên kết.
    * **An toàn dữ liệu:** 100% câu lệnh truy vấn tầng DAL đều sử dụng `SqlParameter` ngăn ngừa triệt để nguy cơ SQL Injection.

---

### 3. Màn hình Thực thi Nghiệp vụ chính (Đặt phòng / Lập Hóa đơn)
![Màn hình Nghiệp vụ](images/03_transaction_screen.png)

* **Mô tả chức năng & Kỹ thuật triển khai:**
  * **Nghiệp vụ 1: Đặt phòng & Quản lý Phòng hát Karaoke theo thời gian thực (Bài 6.5)**
    * **Sơ đồ phòng trực quan (Visual Room Map):** Trực quan hóa danh sách phòng bằng hệ thống card button đổi màu động (Màu xanh lá: Phòng trống sẵn sàng phục vụ; Màu đỏ cam: Phòng đang có khách hát).
    * **Mở phòng / Đặt phòng:** Khi chọn phòng trống và nhấn *"Mở Phòng Hát"*, hệ thống khởi tạo `SqlTransaction` cấp độ cô lập `RepeatableRead`:
      1. Khóa bản ghi phòng bằng `UPDLOCK, HOLDLOCK` để chống tranh chấp đồng thời (Concurrency lock).
      2. Cập nhật trạng thái phòng thành `'Đang hát'`.
      3. Tạo mới bản ghi hóa đơn trong bảng `HoaDon`, ghi nhận chính xác `GioVao` (thời điểm khách nhận phòng) và sinh mã hóa đơn tự động (`SCOPE_IDENTITY()`).
    * **Đồng hồ đếm thời gian thực (Real-time Elapsed Timer):** Sử dụng `System.Windows.Forms.Timer` đếm từng giây thời gian khách đã hát, hiển thị trực quan dạng `hh:mm:ss`.
    * **Gọi món / Dịch vụ ăn uống:**
      * Chọn món từ combobox và nhập số lượng cần gọi.
      * Hệ thống kiểm tra số lượng tồn kho khả dụng bằng khóa `UPDLOCK`. Nếu vượt quá tồn kho $\rightarrow$ Ném ngoại lệ và Rollback ngay lập tức.
      * Tự động trừ số lượng tồn kho trong bảng `DichVu` và ghi nhận chi tiết vào `ChiTietDichVu`.
    * **Thanh toán & Trả phòng:**
      * Tính toán tiền phòng chính xác dựa trên thời gian sử dụng thực tế:
        $$\text{Tiền phòng} = \text{Số giờ sử dụng} \times \text{Giá theo giờ}$$
      * Tự động tổng hợp tiền dịch vụ đồ ăn uống.
      * Chốt trạng thái hóa đơn thành `Đã thanh toán (1)`, giải phóng phòng hát trở về trạng thái `'Trống'`.
      * Toàn bộ quy trình diễn ra trọn vẹn trong một `SqlTransaction` Serializable, bảo đảm không bao giờ thất thoát doanh thu hoặc sai lệch trạng thái phòng.
  * **Nghiệp vụ 2: Tiếp nhận Xe & Lập Phiếu sửa chữa Garage (Bài 6.4)**
    * Chọn xe từ danh sách xe đã tiếp nhận, nhập nội dung sửa chữa, chọn phụ tùng và nhập tiền công thợ.
    * Nhấn *"Lập Phiếu & Xuất Xưởng"*: Hệ thống tạo phiếu sửa chữa, trừ tồn kho phụ tùng trong `SqlTransaction` và in tổng số tiền cần thanh toán.

---

### 4. Màn hình Báo cáo - Thống kê
![Màn hình Thống kê](images/04_report_screen.png)

* **Mô tả chức năng & Kỹ thuật triển khai:**
  * **Thống kê Doanh thu Tổng hợp (SqlCommand.ExecuteScalar):**
    * Áp dụng phương thức `ExecuteScalar()` để truy vấn trực tiếp các giá trị số tổng hợp (`SUM`, `COUNT`).
    * Giúp tối ưu hóa tốc độ truy vấn tối đa, giảm thiểu băng thông đường truyền giữa ứng dụng và máy chủ SQL Server.
  * **Báo cáo Top 5 Dịch vụ Karaoke Bán chạy nhất (SqlCommand.ExecuteReader):**
    * Áp dụng phương thức `ExecuteReader()` kết hợp `SqlDataReader`.
    * Đọc dữ liệu theo luồng một chiều (Forward-Only) và chỉ đọc (Read-Only), mang lại hiệu năng cao nhất trong ADO.NET khi kết xuất danh sách báo cáo mà không tiêu tốn bộ nhớ RAM.
  * **Báo cáo Top 5 Phụ tùng Garage Thay thế nhiều nhất:**
    * Thống kê tổng số lượng linh kiện đã sử dụng và tổng chi phí phát sinh (tiền phụ tùng + tiền công) theo từng loại phụ tùng.
  * **Thống kê Tiến độ Dự án (Kanban Stats):**
    * Báo cáo phân bổ số lượng đầu việc theo từng trạng thái (`To Do`, `In Progress`, `Review`, `Done`), giúp người quản lý nắm bắt tiến độ dự án kịp thời.

---

### 5. Minh họa An toàn Dữ liệu (Phòng chống SQL Injection / Báo lỗi Transaction Rollback)
![Minh họa Xử lý lỗi](images/05_error_handling.png)

* **Mô tả chức năng & Kỹ thuật triển khai:**
  * **Phân hệ 1: Kiểm thử & Phòng chống SQL Injection (SQL Injection Mitigation)**
    * Cung cấp giao diện trực quan cho phép nhập chuỗi payload tấn công phổ biến, ví dụ: `' OR '1'='1' --` hoặc `VIP' --`.
    * **Nút "Chạy Không An Toàn (Ghép chuỗi SQL)":** Minh họa câu truy vấn sai chuẩn dùng toán tử cộng chuỗi `+`. Kẻ tấn công bẻ gãy cú pháp mệnh đề `WHERE` và làm lộ toàn bộ dữ liệu trong hệ thống.
    * **Nút "Chạy An Toàn (SqlParameter ADO.NET)":** Minh họa kỹ thuật Parameterized Query chuẩn ADO.NET. Trình phân tích cú pháp SQL Server tự động coi dữ liệu đầu vào là chuỗi giá trị thuần túy (Literal Value), vô hiệu hóa hoàn toàn mọi nỗ lực tiêm mã độc SQL.
  * **Phân hệ 2: Kiểm thử Giao dịch Toàn vẹn (ACID SqlTransaction Rollback)**
    * Giả lập tình huống giao dịch gặp sự cố trong quá trình bán hàng / gọi dịch vụ (ví dụ: số lượng tồn trong kho bị thiếu do người khác vừa mua, hoặc ngắt kết nối đột ngột).
    * Khi phát hiện bất thường, khối `try...catch` lập tức kích hoạt lệnh `transaction.Rollback()`.
    * Hệ thống in ra chi tiết nhật ký kiểm toán giao dịch (Audit Log), chứng minh:
      * Hóa đơn rác không bị tạo trong bảng `HoaDon`.
      * Chi tiết dịch vụ không bị lưu dở dang vào `ChiTietDichVu`.
      * Số lượng tồn kho giữ nguyên 100% như trước khi bắt đầu giao dịch, không bị trừ âm kho.

---

## MỤC 3: CẤU TRÚC GIẢI PHÁP & MÃ NGUỒN CHUẨN 3-TIER

Repository được tổ chức theo quy chuẩn cấu trúc thư mục của đồ án môn học:

```text
├── Database/
│   ├── ScriptDB.sql                  # Script tổng hợp khởi tạo 3 CSDL và chèn dữ liệu mẫu
│   ├── KaraokeDB.sql                 # Script riêng cho CSDL Karaoke (Bài 6.5)
│   ├── GarageDB.sql                  # Script riêng cho CSDL Garage (Bài 6.4)
│   └── ProjectTaskDB.sql             # Script riêng cho CSDL Dự án & Task (Bài 6.3)
├── MultiServiceEnterpriseSystem/     # Thư mục mã nguồn Solution C# (Kiến trúc 3 lớp)
│   ├── DTO/                          # Data Transfer Objects (Các lớp thực thể dữ liệu)
│   │   ├── KaraokeDTO.cs             # PhongHatDTO, DichVuDTO, HoaDonDTO, ChiTietDichVuDTO
│   │   ├── GarageDTO.cs              # KhachHangDTO, XeDTO, PhuTungDTO, PhieuSuaChuaDTO, ChiTietSuaChuaDTO
│   │   └── ProjectTaskDTO.cs         # DuAnDTO, ThanhVienDTO, CongViecDTO
│   ├── DAL/                          # Data Access Layer (Truy vấn CSDL ADO.NET thuần)
│   │   ├── DbHelper.cs               # Quản lý kết nối, tự động phát hiện instance, transaction
│   │   ├── KaraokeDAL.cs             # Xử lý ADO.NET cho Karaoke, phòng hát, gọi món, trả phòng
│   │   ├── GarageDAL.cs              # Xử lý ADO.NET cho Garage, tiếp nhận xe, phụ tùng, hóa đơn
│   │   ├── ProjectTaskDAL.cs         # Xử lý ADO.NET cho Dự án, công việc Kanban
│   │   └── SecurityAuditDAL.cs       # Truy vấn demo SQL Injection & Transaction Rollback
│   ├── BUS/                          # Business Logic Layer (Xử lý quy tắc nghiệp vụ)
│   │   ├── KaraokeBUS.cs             # Nghiệp vụ tính tiền giờ, mở phòng, trừ kho, thanh toán
│   │   ├── GarageBUS.cs              # Nghiệp vụ lập phiếu sửa chữa, tính tiền công + phụ tùng
│   │   ├── ProjectTaskBUS.cs         # Nghiệp vụ phân công công việc, chuyển trạng thái
│   │   └── SecurityAuditBUS.cs       # Điều phối kịch bản kiểm thử bảo mật & rollback
│   ├── GUI/                          # Presentation Layer (Giao diện Windows Forms)
│   │   ├── UITheme.cs                # Bảng màu Flat UI hiện đại, styling DataGridView chuyên nghiệp
│   │   ├── frmMain.cs                # Màn hình chính Dashboard tổng quan hệ thống
│   │   ├── frmCategoryCRUD.cs        # Màn hình Quản lý Danh mục (CRUD Thêm/Sửa/Xóa)
│   │   ├── frmBookingService.cs      # Màn hình Thực thi Nghiệp vụ chính (Đặt phòng Karaoke & Garage)
│   │   ├── frmReportStats.cs         # Màn hình Báo cáo - Thống kê doanh thu & Top dịch vụ
│   │   └── frmSecurityDemo.cs        # Minh họa An toàn Dữ liệu (SQL Injection & Rollback)
│   ├── App.config                    # Cấu hình chuỗi kết nối SQL Server linh hoạt
│   ├── Program.cs                    # Điểm khởi chạy ứng dụng (Entry Point)
│   └── MultiServiceEnterpriseSystem.csproj # File cấu hình dự án .NET 10.0 Windows Forms
├── images/                           # Thư mục chứa 5 ảnh kết quả theo đúng quy chuẩn tên file
│   ├── 01_main_screen.png            # Màn hình Chính / Tổng quan hệ thống
│   ├── 02_crud_screen.png            # Màn hình Quản lý Danh mục (CRUD)
│   ├── 03_transaction_screen.png     # Màn hình Thực thi Nghiệp vụ chính
│   ├── 04_report_screen.png          # Màn hình Báo cáo - Thống kê
│   └── 05_error_handling.png         # Minh họa An toàn Dữ liệu & Xử lý lỗi
├── README.md                         # Báo cáo chi tiết bài tập
└── MultiServiceEnterpriseSystem.sln  # File Solution chuẩn Microsoft Visual Studio
```

---

## MỤC 4: HƯỚNG DẪN CÀI ĐẶT & CHẠY ỨNG DỤNG

### Bước 1: Khởi tạo Cơ sở Dữ liệu trên SQL Server
1. Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
2. Kết nối tới SQL Server của bạn (ví dụ: `(localdb)\MSSQLLocalDB` hoặc `.` hoặc `localhost` hoặc `.\SQLEXPRESS`).
3. Mở file `Database/ScriptDB.sql` trong thư mục đồ án.
4. Nhấn **Execute (F5)** để tự động tạo toàn bộ 3 cơ sở dữ liệu (`KaraokeDB`, `GarageDB`, `ProjectTaskDB`) cùng đầy đủ các bảng, ràng buộc toàn vẹn và dữ liệu mẫu chuẩn hóa.

### Bước 2: Cấu hình chuỗi kết nối trong `App.config` (nếu cần)
* Mở file `MultiServiceEnterpriseSystem/App.config`.
* Mặc định chuỗi kết nối sử dụng `(localdb)\MSSQLLocalDB`:
```xml
<connectionStrings>
  <add name="KaraokeDB" 
       connectionString="Server=(localdb)\MSSQLLocalDB;Database=KaraokeDB;Integrated Security=True;TrustServerCertificate=True" 
       providerName="Microsoft.Data.SqlClient" />
  <add name="GarageDB" 
       connectionString="Server=(localdb)\MSSQLLocalDB;Database=GarageDB;Integrated Security=True;TrustServerCertificate=True" 
       providerName="Microsoft.Data.SqlClient" />
  <add name="ProjectTaskDB" 
       connectionString="Server=(localdb)\MSSQLLocalDB;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True" 
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```
* Nếu máy tính sử dụng SQL Server có tên khác (ví dụ: `.\SQLEXPRESS` hoặc `.`): Sinh viên chỉ cần sửa giá trị thuộc tính `Server=...` cho phù hợp.

### Bước 3: Mở và Chạy Solution trên Visual Studio
1. Nhấp đúp vào file `MultiServiceEnterpriseSystem.sln` để mở toàn bộ dự án bằng **Visual Studio 2022**.
2. Nhấn tổ hợp phím **Ctrl + Shift + B** để Build Solution.
3. Nhấn **F5** (hoặc nút **Start**) để khởi chạy và trải nghiệm toàn bộ các màn hình chức năng.

---

## MỤC 5: BẢNG TỰ ĐÁNH GIÁ MỨC ĐỘ HOÀN THÀNH

| STT | Hạng mục công việc / Yêu cầu | Mức độ hoàn thành | Tự đánh giá |
| :---: | :--- | :---: | :---: |
| 1 | Khởi tạo cấu trúc thư mục Solution chuẩn Kiến trúc 3 lớp (3-Tier) | 100% | Xuất sắc |
| 2 | Thiết kế CSDL SQL Server với đầy đủ PK, FK, Check, Default, Index | 100% | Xuất sắc |
| 3 | Màn hình Chính / Tổng quan Dashboard hiển thị thẻ KPI thời gian thực | 100% | Xuất sắc |
| 4 | Màn hình Quản lý Danh mục (CRUD Thêm/Sửa/Xóa, Validation tầng BUS) | 100% | Xuất sắc |
| 5 | Màn hình Thực thi Nghiệp vụ chính (Đặt phòng Karaoke, Tính giờ, Gọi món, Lập hóa đơn Garage) | 100% | Xuất sắc |
| 6 | Giao dịch toàn vẹn ACID với `SqlTransaction` cấp độ Serializable, xử lý Rollback khi lỗi | 100% | Xuất sắc |
| 7 | Màn hình Báo cáo - Thống kê tối ưu bằng `ExecuteScalar()` và `SqlDataReader` | 100% | Xuất sắc |
| 8 | Minh họa An toàn Dữ liệu (Phòng chống SQL Injection & Demo Transaction Rollback) | 100% | Xuất sắc |
| 9 | Chuẩn bị đầy đủ 5 file ảnh chụp màn hình trong thư mục `images/` | 100% | Xuất sắc |
| 10 | Báo cáo `README.md` chi tiết, chuyên nghiệp theo đúng hướng dẫn | 100% | Xuất sắc |

