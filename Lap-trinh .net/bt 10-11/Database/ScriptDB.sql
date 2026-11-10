-- ============================================================================
-- TRƯỜNG ĐẠI HỌC ĐIỆN LỰC (EPU) - KHOA CÔNG NGHỆ THÔNG TIN
-- BÀI TẬP THỰC HÀNH LẬP TRÌNH .NET (CHƯƠNG 6: ADO.NET NÂNG CAO & 3-TIER)
-- HỌ VÀ TÊN SINH VIÊN: NGUYỄN PHÚC VƯỢNG
-- MÃ SINH VIÊN       : 24810320187
-- LỚP HỌC PHẦN       : D19QTANM1 - LẬP TRÌNH .NET
-- SCRIPT TỔNG HỢP KHỞI TẠO TOÀN BỘ CSDL VÀ DỮ LIỆU MẪU CHO BÀI 6.3, 6.4, 6.5
-- ============================================================================

-------------------------------------------------------------------------------
-- PHẦN 1: CSDL KARAOKEDB (BÀI 6.5 - QUẢN LÝ PHÒNG HÁT & DỊCH VỤ KARAOKE)
-------------------------------------------------------------------------------
IF DB_ID(N'KaraokeDB') IS NULL
BEGIN
    CREATE DATABASE KaraokeDB;
END;
GO

USE KaraokeDB;
GO

-- 1.1. Bảng Phòng hát
IF OBJECT_ID(N'dbo.PhongHat', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhongHat
    (
        MaPhong int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhongHat PRIMARY KEY,
        TenPhong nvarchar(50) NOT NULL,
        LoaiPhong nvarchar(30) NULL,
        GiaTheoGio decimal(18,2) NOT NULL,
        TrangThai nvarchar(20) NOT NULL CONSTRAINT DF_PhongHat_TrangThai DEFAULT (N'Trống'),
        CONSTRAINT CK_PhongHat_GiaTheoGio CHECK (GiaTheoGio >= 0),
        CONSTRAINT CK_PhongHat_TrangThai CHECK (TrangThai IN (N'Trống', N'Đang hát'))
    );
END;
GO

-- 1.2. Bảng Danh mục Dịch vụ (Đồ ăn, Thức uống)
IF OBJECT_ID(N'dbo.DichVu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DichVu
    (
        MaDV int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DichVu PRIMARY KEY,
        TenDV nvarchar(100) NOT NULL,
        DonGia decimal(18,2) NOT NULL,
        TonKho int NOT NULL,
        CONSTRAINT CK_DichVu_DonGia CHECK (DonGia >= 0),
        CONSTRAINT CK_DichVu_TonKho CHECK (TonKho >= 0)
    );
END;
GO

-- 1.3. Bảng Hóa đơn phòng
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HoaDon
    (
        MaHD int IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
        MaPhong int NOT NULL,
        GioVao datetime NOT NULL,
        GioRa datetime NULL,
        TongTienPhong decimal(18,2) NOT NULL CONSTRAINT DF_HoaDon_TongTienPhong DEFAULT (0),
        TongTienDichVu decimal(18,2) NOT NULL CONSTRAINT DF_HoaDon_TongTienDichVu DEFAULT (0),
        TrangThai bit NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT (0), -- 0: Đang sử dụng, 1: Đã thanh toán
        CONSTRAINT FK_HoaDon_PhongHat FOREIGN KEY (MaPhong) REFERENCES dbo.PhongHat(MaPhong),
        CONSTRAINT CK_HoaDon_TongTienPhong CHECK (TongTienPhong >= 0),
        CONSTRAINT CK_HoaDon_TongTienDichVu CHECK (TongTienDichVu >= 0)
    );
END;
GO

-- 1.4. Bảng Chi tiết gọi dịch vụ
IF OBJECT_ID(N'dbo.ChiTietDichVu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ChiTietDichVu
    (
        MaHD int NOT NULL,
        MaDV int NOT NULL,
        SoLuong int NOT NULL,
        DonGia decimal(18,2) NOT NULL,
        CONSTRAINT PK_ChiTietDichVu PRIMARY KEY (MaHD, MaDV),
        CONSTRAINT FK_ChiTietDichVu_HoaDon FOREIGN KEY (MaHD) REFERENCES dbo.HoaDon(MaHD),
        CONSTRAINT FK_ChiTietDichVu_DichVu FOREIGN KEY (MaDV) REFERENCES dbo.DichVu(MaDV),
        CONSTRAINT CK_ChiTietDichVu_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_ChiTietDichVu_DonGia CHECK (DonGia >= 0)
    );
END;
GO

-- 1.5. Chèn dữ liệu mẫu KaraokeDB
IF NOT EXISTS (SELECT 1 FROM dbo.PhongHat)
BEGIN
    INSERT INTO dbo.PhongHat (TenPhong, LoaiPhong, GiaTheoGio, TrangThai) VALUES
    (N'Phòng VIP 01', N'VIP', 300000, N'Trống'),
    (N'Phòng VIP 02', N'VIP', 300000, N'Trống'),
    (N'Phòng VIP 03', N'VIP', 350000, N'Trống'),
    (N'Phòng Thường 01', N'Thường', 150000, N'Trống'),
    (N'Phòng Thường 02', N'Thường', 150000, N'Trống'),
    (N'Phòng Thường 03', N'Thường', 150000, N'Trống'),
    (N'Phòng Thường 04', N'Thường', 150000, N'Trống'),
    (N'Phòng Thường 05', N'Thường', 150000, N'Trống'),
    (N'Phòng Party 01', N'Party Lớn', 450000, N'Trống');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DichVu)
BEGIN
    INSERT INTO dbo.DichVu (TenDV, DonGia, TonKho) VALUES
    (N'Bia Heineken (Lon)', 25000, 150),
    (N'Bia Tiger Bạc (Lon)', 24000, 120),
    (N'Nước khoáng Lavie', 10000, 200),
    (N'Nước ngọt Coca-Cola', 15000, 180),
    (N'Trái cây dĩa thập cẩm', 85000, 40),
    (N'Khô bò xé sợi vắt chanh', 95000, 50),
    (N'Khô mực nướng cồn', 120000, 30),
    (N'Khoai tây chiên giòn', 45000, 60),
    (N'Hạt điều rang muối', 50000, 80);
END;
GO


-------------------------------------------------------------------------------
-- PHẦN 2: CSDL GARAGEDB (BÀI 6.4 - QUẢN LÝ DỊCH VỤ SỬA CHỮA GARAGE Ô TÔ)
-------------------------------------------------------------------------------
IF DB_ID(N'GarageDB') IS NULL
BEGIN
    CREATE DATABASE GarageDB;
END;
GO

USE GarageDB;
GO

-- 2.1. Bảng Khách hàng
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhachHang
    (
        MaKH int IDENTITY(1,1) NOT NULL CONSTRAINT PK_KhachHang PRIMARY KEY,
        TenKH nvarchar(100) NOT NULL,
        DienThoai varchar(15) NULL,
        DiaChi nvarchar(200) NULL
    );

    CREATE UNIQUE INDEX UX_KhachHang_DienThoai
        ON dbo.KhachHang(DienThoai)
        WHERE DienThoai IS NOT NULL;
END;
GO

-- 2.2. Bảng Xe
IF OBJECT_ID(N'dbo.Xe', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Xe
    (
        BienSo varchar(20) NOT NULL CONSTRAINT PK_Xe PRIMARY KEY,
        MaKH int NOT NULL,
        HieuXe nvarchar(50) NULL,
        NamSanXuat int NULL,
        CONSTRAINT FK_Xe_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
        CONSTRAINT CK_Xe_NamSanXuat CHECK (NamSanXuat IS NULL OR NamSanXuat BETWEEN 1886 AND 2100)
    );
END;
GO

-- 2.3. Bảng Phụ tùng
IF OBJECT_ID(N'dbo.PhuTung', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhuTung
    (
        MaPhuTung int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhuTung PRIMARY KEY,
        TenPhuTung nvarchar(150) NOT NULL,
        DonGia decimal(18,2) NOT NULL CONSTRAINT DF_PhuTung_DonGia DEFAULT (0),
        SoLuongTon int NOT NULL CONSTRAINT DF_PhuTung_SoLuongTon DEFAULT (0),
        CONSTRAINT CK_PhuTung_DonGia CHECK (DonGia >= 0),
        CONSTRAINT CK_PhuTung_SoLuongTon CHECK (SoLuongTon >= 0)
    );
END;
GO

-- 2.4. Bảng Phiếu sửa chữa
IF OBJECT_ID(N'dbo.PhieuSuaChua', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhieuSuaChua
    (
        MaPhieu int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuSuaChua PRIMARY KEY,
        BienSo varchar(20) NOT NULL,
        NgayNhan datetime NOT NULL CONSTRAINT DF_PhieuSuaChua_NgayNhan DEFAULT (GETDATE()),
        NoiDungSua nvarchar(max) NULL,
        TongTien decimal(18,2) NOT NULL CONSTRAINT DF_PhieuSuaChua_TongTien DEFAULT (0),
        TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_PhieuSuaChua_TrangThai DEFAULT (N'Đang sửa'),
        CONSTRAINT FK_PhieuSuaChua_Xe FOREIGN KEY (BienSo) REFERENCES dbo.Xe(BienSo),
        CONSTRAINT CK_PhieuSuaChua_TongTien CHECK (TongTien >= 0)
    );
END;
GO

-- 2.5. Bảng Chi tiết sửa chữa
IF OBJECT_ID(N'dbo.ChiTietSuaChua', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ChiTietSuaChua
    (
        MaPhieu int NOT NULL,
        MaPhuTung int NOT NULL,
        SoLuong int NOT NULL,
        TienCong decimal(18,2) NOT NULL CONSTRAINT DF_ChiTietSuaChua_TienCong DEFAULT (0),
        CONSTRAINT PK_ChiTietSuaChua PRIMARY KEY (MaPhieu, MaPhuTung),
        CONSTRAINT FK_ChiTietSuaChua_Phieu FOREIGN KEY (MaPhieu) REFERENCES dbo.PhieuSuaChua(MaPhieu),
        CONSTRAINT FK_ChiTietSuaChua_PhuTung FOREIGN KEY (MaPhuTung) REFERENCES dbo.PhuTung(MaPhuTung),
        CONSTRAINT CK_ChiTietSuaChua_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_ChiTietSuaChua_TienCong CHECK (TienCong >= 0)
    );
END;
GO

-- 2.6. Chèn dữ liệu mẫu GarageDB
IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang)
BEGIN
    INSERT INTO dbo.KhachHang (TenKH, DienThoai, DiaChi) VALUES
    (N'Trần Minh Tuấn', '0912345678', N'234 Hoàng Quốc Việt, Cầu Giấy, Hà Nội'),
    (N'Lê Thị Hương Lan', '0987654321', N'56 Nguyễn Trãi, Thanh Xuân, Hà Nội'),
    (N'Phạm Quang Dũng', '0903112233', N'12 Kim Mã, Ba Đình, Hà Nội'),
    (N'Nguyễn Phúc Vượng', '0978999888', N'187 Cầu Giấy, Hà Nội');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Xe)
BEGIN
    INSERT INTO dbo.Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES
    ('30A-888.88', 1, N'Toyota Camry 2.5Q', 2021),
    ('29B-999.99', 2, N'Mazda CX-5 2.0', 2022),
    ('30F-123.45', 3, N'Honda CR-V', 2020),
    ('29E-678.90', 4, N'Hyundai Tucson Turbo', 2023);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.PhuTung)
BEGIN
    INSERT INTO dbo.PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES
    (N'Dầu động cơ Castrol Edge 5W-30 (Can 4L)', 750000, 45),
    (N'Lọc nhớt chính hãng Denso', 180000, 80),
    (N'Lọc gió động cơ Bosch', 250000, 60),
    (N'Má phanh trước Akebono Ceramic', 950000, 35),
    (N'Bugi bạch kim Iridium NGK (Bộ 4 bugi)', 650000, 40),
    (N'Gạt mưa Silicon Bosch AeroTwin', 320000, 50),
    (N'Nước làm mát tản nhiệt Mobil', 160000, 70);
END;
GO


-------------------------------------------------------------------------------
-- PHẦN 3: CSDL PROJECTTASKDB (BÀI 6.3 - QUẢN LÝ DỰ ÁN & CÔNG VIỆC KANBAN)
-------------------------------------------------------------------------------
IF DB_ID(N'ProjectTaskDB') IS NULL
BEGIN
    CREATE DATABASE ProjectTaskDB;
END;
GO

USE ProjectTaskDB;
GO

-- 3.1. Bảng Dự án
IF OBJECT_ID(N'dbo.DuAn', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DuAn
    (
        MaDuAn int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DuAn PRIMARY KEY,
        TenDuAn nvarchar(150) NOT NULL,
        NgayBatDau datetime NULL,
        NgayKetThuc datetime NULL,
        TrangThai nvarchar(50) NULL
    );
END;
GO

-- 3.2. Bảng Thành viên
IF OBJECT_ID(N'dbo.ThanhVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ThanhVien
    (
        MaThanhVien int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ThanhVien PRIMARY KEY,
        HoTen nvarchar(100) NOT NULL,
        Email varchar(100) NOT NULL CONSTRAINT UQ_ThanhVien_Email UNIQUE,
        ChuyenMon nvarchar(100) NULL
    );
END;
GO

-- 3.3. Bảng Công việc (Task)
IF OBJECT_ID(N'dbo.CongViec', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.CongViec
    (
        MaTask int IDENTITY(1,1) NOT NULL CONSTRAINT PK_CongViec PRIMARY KEY,
        MaDuAn int NOT NULL,
        TenTask nvarchar(200) NOT NULL,
        MoTa nvarchar(max) NULL,
        HanNop datetime NULL,
        DoUuTien nvarchar(20) NULL,
        TrangThai nvarchar(50) NULL,
        CONSTRAINT FK_CongViec_DuAn FOREIGN KEY (MaDuAn) REFERENCES dbo.DuAn(MaDuAn)
    );
END;
GO

-- 3.4. Bảng Phân công
IF OBJECT_ID(N'dbo.PhanCong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhanCong
    (
        MaTask int NOT NULL,
        MaThanhVien int NOT NULL,
        NgayGiao datetime NOT NULL CONSTRAINT DF_PhanCong_NgayGiao DEFAULT (GETDATE()),
        CONSTRAINT PK_PhanCong PRIMARY KEY (MaTask, MaThanhVien),
        CONSTRAINT FK_PhanCong_CongViec FOREIGN KEY (MaTask) REFERENCES dbo.CongViec(MaTask) ON DELETE CASCADE,
        CONSTRAINT FK_PhanCong_ThanhVien FOREIGN KEY (MaThanhVien) REFERENCES dbo.ThanhVien(MaThanhVien)
    );
END;
GO

-- 3.5. Indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_CongViec_MaDuAn_TrangThai' AND object_id = OBJECT_ID(N'dbo.CongViec'))
    CREATE INDEX IX_CongViec_MaDuAn_TrangThai ON dbo.CongViec(MaDuAn, TrangThai);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_CongViec_HanNop_TrangThai' AND object_id = OBJECT_ID(N'dbo.CongViec'))
    CREATE INDEX IX_CongViec_HanNop_TrangThai ON dbo.CongViec(HanNop, TrangThai);
GO

-- 3.6. Chèn dữ liệu mẫu ProjectTaskDB
IF NOT EXISTS (SELECT 1 FROM dbo.DuAn)
BEGIN
    INSERT INTO dbo.DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) VALUES
    (N'Phát triển Hệ thống Quản trị Doanh nghiệp 3-Tier', '2026-11-01', '2026-11-30', N'Đang thực hiện'),
    (N'Nâng cấp Hạ tầng Cơ sở Dữ liệu SQL Server High-Availability', '2026-10-15', '2026-12-15', N'Đang thực hiện'),
    (N'Xây dựng Ứng dụng Quản lý Garage Thông minh', '2026-11-05', '2026-11-25', N'Đang thực hiện');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.ThanhVien)
BEGIN
    INSERT INTO dbo.ThanhVien (HoTen, Email, ChuyenMon) VALUES
    (N'Nguyễn Phúc Vượng', 'vuongnp.24810320187@epu.edu.vn', N'Software Architecture & Lead Dev'),
    (N'Trần Minh Đức', 'ductm@gmail.com', N'Backend C# .NET & Database Engineer'),
    (N'Lê Quỳnh Trang', 'tranglq@gmail.com', N'Frontend WinForms & UI/UX Designer'),
    (N'Hoàng Văn Nam', 'namhv@gmail.com', N'QA/QC & Security Specialist');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.CongViec)
BEGIN
    INSERT INTO dbo.CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai) VALUES
    (1, N'Thiết kế sơ đồ CSDL chuẩn 3NF và viết Script SQL', N'Tạo bảng, ràng buộc khóa ngoại, chỉ mục index', '2026-11-12', N'Cao', N'Done'),
    (1, N'Xây dựng tầng Data Access Layer (DAL) ADO.NET', N'Viết lớp DbHelper, xử lý SqlTransaction và parameterized query', '2026-11-15', N'Khẩn cấp', N'In Progress'),
    (1, N'Thiết kế giao diện WinForms Dashboard và Kanban Board', N'Trực quan hóa thẻ phòng, điều hướng Sidebar chuyên nghiệp', '2026-11-18', N'Trung bình', N'In Progress'),
    (1, N'Kiểm thử an toàn dữ liệu và chống SQL Injection', N'Viết kịch bản kiểm thử tiêm mã độc và rollback transaction', '2026-11-20', N'Cao', N'To Do'),
    (2, N'Tối ưu hóa Execution Plan và Indexing các bảng lớn', N'Phân tích truy vấn JOIN và chỉ mục phủ Covering Index', '2026-11-22', N'Trung bình', N'Review');
END;
GO

PRINT N'>>> ĐÃ KHỞI TẠO THÀNH CÔNG 3 CƠ SỞ DỮ LIỆU VÀ CHÈN DỮ LIỆU MẪU ĐẦY ĐỦ CHO HỆ THỐNG!';
GO
