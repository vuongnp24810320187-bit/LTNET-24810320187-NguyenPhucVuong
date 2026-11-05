-- 1. Bảng PhongBan
CREATE TABLE PhongBan (
    MaPB INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL
);

-- 2. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATETIME NULL,
    CCCD VARCHAR(20) UNIQUE NOT NULL,
    MaPB INT FOREIGN KEY REFERENCES PhongBan(MaPB),
    LuongCoBan DECIMAL(18,2) CHECK (LuongCoBan > 0),
    HeSoLuong FLOAT DEFAULT 1.0
);

-- 3. Bảng BangChamCong
CREATE TABLE BangChamCong (
    MaCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL, -- Ví dụ: 11/2026
    SoNgayCong FLOAT CHECK (SoNgayCong <= 31) DEFAULT 0,
    SoGioOT FLOAT DEFAULT 0,
    CONSTRAINT UQ_ChamCong UNIQUE(MaNV, ThangNam)
);

-- 4. Bảng BangLuong
CREATE TABLE BangLuong (
    MaLuong INT IDENTITY(1,1),
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    ThucLinh DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaNV, ThangNam)
);

-- Chèn dữ liệu mẫu phòng ban
INSERT INTO PhongBan (TenPB, MoTa) VALUES 
(N'Phòng Kỹ Thuật', N'Phát triển phần mềm'),
(N'Phòng Nhân Sự', N'Tuyển dụng và tính lương'),
(N'Phòng Kinh Doanh', N'Kinh doanh và thị trường');

-- Chèn dữ liệu mẫu nhân viên
INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES
(N'Nguyễn Văn A', '1995-05-15', '001200000001', 1, 15000000, 1.2),
(N'Trần Thị B', '1998-10-20', '001200000002', 2, 12000000, 1.0),
(N'Lê Văn C', '1992-03-08', '001200000003', 3, 10000000, 1.5);