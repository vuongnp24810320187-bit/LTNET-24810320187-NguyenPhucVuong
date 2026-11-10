-- Script GarageDB (Bài 6.4)
IF DB_ID(N'GarageDB') IS NULL
BEGIN
	CREATE DATABASE GarageDB;
END;
GO

USE GarageDB;
GO

IF OBJECT_ID(N'dbo.ChiTietSuaChua', N'U') IS NULL
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

	CREATE TABLE dbo.Xe
	(
		BienSo varchar(20) NOT NULL CONSTRAINT PK_Xe PRIMARY KEY,
		MaKH int NOT NULL,
		HieuXe nvarchar(50) NULL,
		NamSanXuat int NULL,
		CONSTRAINT FK_Xe_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
		CONSTRAINT CK_Xe_NamSanXuat CHECK (NamSanXuat IS NULL OR NamSanXuat BETWEEN 1886 AND 2100)
	);

	CREATE TABLE dbo.PhuTung
	(
		MaPhuTung int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhuTung PRIMARY KEY,
		TenPhuTung nvarchar(150) NOT NULL,
		DonGia decimal(18,2) NOT NULL CONSTRAINT DF_PhuTung_DonGia DEFAULT (0),
		SoLuongTon int NOT NULL CONSTRAINT DF_PhuTung_SoLuongTon DEFAULT (0),
		CONSTRAINT CK_PhuTung_DonGia CHECK (DonGia >= 0),
		CONSTRAINT CK_PhuTung_SoLuongTon CHECK (SoLuongTon >= 0)
	);

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

