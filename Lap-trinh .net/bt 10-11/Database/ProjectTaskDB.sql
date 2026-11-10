-- Script ProjectTaskDB (Bài 6.3)
IF DB_ID(N'ProjectTaskDB') IS NULL
BEGIN
	CREATE DATABASE ProjectTaskDB;
END;
GO

USE ProjectTaskDB;
GO

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

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_CongViec_MaDuAn_TrangThai' AND object_id = OBJECT_ID(N'dbo.CongViec'))
	CREATE INDEX IX_CongViec_MaDuAn_TrangThai ON dbo.CongViec(MaDuAn, TrangThai);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_CongViec_HanNop_TrangThai' AND object_id = OBJECT_ID(N'dbo.CongViec'))
	CREATE INDEX IX_CongViec_HanNop_TrangThai ON dbo.CongViec(HanNop, TrangThai);
GO

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

