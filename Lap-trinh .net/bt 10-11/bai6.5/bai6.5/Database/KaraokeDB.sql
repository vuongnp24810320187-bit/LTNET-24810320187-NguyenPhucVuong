IF DB_ID(N'KaraokeDB') IS NULL
BEGIN
	CREATE DATABASE KaraokeDB;
END;
GO

USE KaraokeDB;
GO

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
		TrangThai bit NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT (0),
		CONSTRAINT FK_HoaDon_PhongHat FOREIGN KEY (MaPhong) REFERENCES dbo.PhongHat(MaPhong),
		CONSTRAINT CK_HoaDon_TongTienPhong CHECK (TongTienPhong >= 0),
		CONSTRAINT CK_HoaDon_TongTienDichVu CHECK (TongTienDichVu >= 0)
	);
END;
GO

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

IF NOT EXISTS (SELECT 1 FROM dbo.PhongHat)
BEGIN
	INSERT dbo.PhongHat (TenPhong, LoaiPhong, GiaTheoGio)
	VALUES
		(N'Phòng VIP 01', N'VIP', 300000),
		(N'Phòng VIP 02', N'VIP', 300000),
		(N'Phòng Thường 01', N'Thường', 150000),
		(N'Phòng Thường 02', N'Thường', 150000),
		(N'Phòng Thường 03', N'Thường', 150000),
		(N'Phòng Thường 04', N'Thường', 150000),
		(N'Phòng Thường 05', N'Thường', 150000),
		(N'Phòng Thường 06', N'Thường', 150000);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.DichVu)
BEGIN
	INSERT dbo.DichVu (TenDV, DonGia, TonKho)
	VALUES
		(N'Bia Heineken', 25000, 100),
		(N'Nước suối', 10000, 100),
		(N'Coca-Cola', 15000, 100),
		(N'Trái cây dĩa', 75000, 30),
		(N'Khô bò', 85000, 30),
		(N'Khoai tây chiên', 50000, 30);
END;
GO
