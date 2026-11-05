-- 1. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE,
    ChucVu NVARCHAR(50)
);

-- 2. Bảng KhachHang
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) UNIQUE,
    DiemTichLuy INT DEFAULT 0
);

-- 3. Bảng SanPham
CREATE TABLE SanPham (
    MaSP VARCHAR(20) PRIMARY KEY,
    TenSP NVARCHAR(150) NOT NULL,
    GiaBan DECIMAL(18,2) CHECK (GiaBan > 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0)
);

-- 4. Bảng HoaDon
CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaKH INT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    TongTien DECIMAL(18,2)
);

-- 5. Bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaHD INT FOREIGN KEY REFERENCES HoaDon(MaHD),
    MaSP VARCHAR(20) FOREIGN KEY REFERENCES SanPham(MaSP),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaHD, MaSP)
);

-- DỮ LIỆU MẪU ĐỂ CHẠY THỬ
INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES 
(N'Nguyễn Văn A', '0901234567', N'Thu ngân'),
(N'Trần Thị B', '0987654321', N'Quản lý');

INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES 
(N'Khách Lẻ', '0000000000', 0),
(N'Lê Văn C', '0912345678', 50),
(N'Phạm Thị D', '0934567890', 120);

INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES 
('SP01', N'Sữa tươi Vinamilk 1L', 35000, 100),
('SP02', N'Mì Hảo Hảo Tôm Chua Cay', 4500, 500),
('SP03', N'Dầu ăn Simply 1L', 65000, 50),
('SP04', N'Nước ngọt Coca Cola 330ml', 10000, 200),
('SP05', N'Bánh Chocopie Hộp 12 cái', 55000, 5);