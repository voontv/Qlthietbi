/*
    QLTB schema using int identity IDs.

    Use this for a fresh database or after intentionally dropping old GUID-based tables.
    Do not run this on the current test database without backing up/import scripts first.
*/

CREATE TABLE dbo.DmDungChung (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DmDungChung PRIMARY KEY,
    NhomDanhMuc nvarchar(50) NOT NULL,
    Ma nvarchar(50) NOT NULL,
    Ten nvarchar(250) NOT NULL,
    GhiChu nvarchar(500) NULL,
    SapXep int NULL,
    IsActive bit NOT NULL CONSTRAINT DF_DmDungChung_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_DmDungChung_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
CREATE UNIQUE INDEX IX_DmDungChung_Nhom_Ma ON dbo.DmDungChung(NhomDanhMuc, Ma);

CREATE TABLE dbo.DonViBoPhan (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DonViBoPhan PRIMARY KEY,
    MaDonVi nvarchar(50) NOT NULL,
    TenDonVi nvarchar(250) NOT NULL,
    ParentId int NULL,
    LoaiDonVi nvarchar(50) NULL,
    GhiChu nvarchar(500) NULL,
    SapXep int NULL,
    IsActive bit NOT NULL CONSTRAINT DF_DonViBoPhan_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_DonViBoPhan_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
ALTER TABLE dbo.DonViBoPhan ADD CONSTRAINT FK_DonViBoPhan_Parent FOREIGN KEY (ParentId) REFERENCES dbo.DonViBoPhan(Id);

CREATE TABLE dbo.NguoiSuDungThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NguoiSuDungThietBi PRIMARY KEY,
    MaNguoiDung nvarchar(50) NOT NULL,
    TenNguoiDung nvarchar(250) NOT NULL,
    DonViBoPhanId int NULL,
    ChucVu nvarchar(250) NULL,
    SoDienThoai nvarchar(50) NULL,
    Email nvarchar(250) NULL,
    GhiChu nvarchar(500) NULL,
    IsActive bit NOT NULL CONSTRAINT DF_NguoiSuDungThietBi_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_NguoiSuDungThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
ALTER TABLE dbo.NguoiSuDungThietBi ADD CONSTRAINT FK_NguoiSuDungThietBi_DonViBoPhan FOREIGN KEY (DonViBoPhanId) REFERENCES dbo.DonViBoPhan(Id);

CREATE TABLE dbo.NhomThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhomThietBi PRIMARY KEY,
    MaNhomThietBi nvarchar(50) NOT NULL,
    TenNhomThietBi nvarchar(250) NOT NULL,
    KyHieu nvarchar(20) NULL,
    ParentId int NULL,
    MoTa nvarchar(1000) NULL,
    GhiChu nvarchar(500) NULL,
    SapXep int NULL,
    IsActive bit NOT NULL CONSTRAINT DF_NhomThietBi_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_NhomThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
CREATE UNIQUE INDEX IX_NhomThietBi_Ma ON dbo.NhomThietBi(MaNhomThietBi);
ALTER TABLE dbo.NhomThietBi ADD CONSTRAINT FK_NhomThietBi_Parent FOREIGN KEY (ParentId) REFERENCES dbo.NhomThietBi(Id);

CREATE TABLE dbo.ThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ThietBi PRIMARY KEY,
    MaThietBi nvarchar(50) NOT NULL,
    MaThietBiCu nvarchar(50) NULL,
    TenThietBi nvarchar(500) NOT NULL,
    SoSerial nvarchar(100) NULL,
    Model nvarchar(100) NULL,
    MaKeToan nvarchar(50) NULL,
    MaThietBiCha nvarchar(50) NULL,
    NhomThietBiId int NOT NULL,
    TrangThaiId int NOT NULL,
    TrangThaiKiemKeId int NULL,
    DonViTinhId int NULL,
    NhanHieuId int NULL,
    MauSacId int NULL,
    NuocSanXuatId int NULL,
    ChatLieuId int NULL,
    DonViCungCapId int NULL,
    PhongBanId int NULL,
    BoPhanId int NULL,
    NguoiSuDungId int NULL,
    NgayMua datetime2 NULL,
    NgayNhapThietBi datetime2 NULL,
    NgayDuaVaoSuDung datetime2 NULL,
    NguyenGia decimal(18,2) NULL,
    ThoiGianBaoHanh int NULL,
    NgayHetBaoHanh datetime2 NULL,
    MaQrCode nvarchar(100) NULL,
    ViTriLapDat nvarchar(500) NULL,
    GhiChu nvarchar(1000) NULL,
    IsActive bit NOT NULL CONSTRAINT DF_ThietBi_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_ThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
CREATE UNIQUE INDEX IX_ThietBi_MaThietBi ON dbo.ThietBi(MaThietBi);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_NhomThietBi FOREIGN KEY (NhomThietBiId) REFERENCES dbo.NhomThietBi(Id);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_TrangThai FOREIGN KEY (TrangThaiId) REFERENCES dbo.DmDungChung(Id);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_PhongBan FOREIGN KEY (PhongBanId) REFERENCES dbo.DonViBoPhan(Id);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_BoPhan FOREIGN KEY (BoPhanId) REFERENCES dbo.DonViBoPhan(Id);
ALTER TABLE dbo.ThietBi ADD CONSTRAINT FK_ThietBi_NguoiSuDung FOREIGN KEY (NguoiSuDungId) REFERENCES dbo.NguoiSuDungThietBi(Id);

CREATE TABLE dbo.DmThongSoThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_DmThongSoThietBi PRIMARY KEY,
    NhomThietBiId int NOT NULL,
    MaThongSo nvarchar(50) NOT NULL,
    TenThongSo nvarchar(250) NOT NULL,
    KieuDuLieu nvarchar(50) NOT NULL,
    DonViTinhId int NULL,
    BatBuoc bit NOT NULL,
    SapXep int NULL,
    GhiChu nvarchar(500) NULL,
    IsActive bit NOT NULL CONSTRAINT DF_DmThongSoThietBi_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_DmThongSoThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
CREATE UNIQUE INDEX IX_DmThongSoThietBi_Nhom_Ma ON dbo.DmThongSoThietBi(NhomThietBiId, MaThongSo);
ALTER TABLE dbo.DmThongSoThietBi ADD CONSTRAINT FK_DmThongSoThietBi_NhomThietBi FOREIGN KEY (NhomThietBiId) REFERENCES dbo.NhomThietBi(Id);

CREATE TABLE dbo.ThietBiThongSo (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ThietBiThongSo PRIMARY KEY,
    ThietBiId int NOT NULL,
    ThongSoId int NOT NULL,
    GiaTriText nvarchar(1000) NULL,
    GiaTriNumber decimal(18,2) NULL,
    GiaTriDate datetime2 NULL,
    GiaTriBit bit NULL,
    GhiChu nvarchar(500) NULL,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_ThietBiThongSo_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL
);
CREATE UNIQUE INDEX IX_ThietBiThongSo_ThietBi_ThongSo ON dbo.ThietBiThongSo(ThietBiId, ThongSoId);
ALTER TABLE dbo.ThietBiThongSo ADD CONSTRAINT FK_ThietBiThongSo_ThietBi FOREIGN KEY (ThietBiId) REFERENCES dbo.ThietBi(Id);
ALTER TABLE dbo.ThietBiThongSo ADD CONSTRAINT FK_ThietBiThongSo_DmThongSo FOREIGN KEY (ThongSoId) REFERENCES dbo.DmThongSoThietBi(Id);

CREATE TABLE dbo.PhieuThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuThietBi PRIMARY KEY,
    SoPhieu nvarchar(50) NOT NULL,
    LoaiPhieu nvarchar(50) NOT NULL,
    NgayPhieu datetime2 NOT NULL,
    ThietBiId int NOT NULL,
    PhongBanId int NULL,
    BoPhanId int NULL,
    NguoiSuDungId int NULL,
    DonViThucHienId int NULL,
    KetLuanId int NULL,
    NoiDung nvarchar(1000) NULL,
    ChiPhi decimal(18,2) NULL,
    FileScan01 nvarchar(500) NULL,
    FileScan02 nvarchar(500) NULL,
    GhiChu nvarchar(1000) NULL,
    IsActive bit NOT NULL CONSTRAINT DF_PhieuThietBi_IsActive DEFAULT 1,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_PhieuThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL,
    NgayChinhSuaCuoiCung datetime2 NULL,
    MaNguoiChinhSua nvarchar(50) NULL,
    TenNguoiChinhSua nvarchar(250) NULL
);
ALTER TABLE dbo.PhieuThietBi ADD CONSTRAINT FK_PhieuThietBi_ThietBi FOREIGN KEY (ThietBiId) REFERENCES dbo.ThietBi(Id);

CREATE TABLE dbo.PhieuThietBiChiTiet (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuThietBiChiTiet PRIMARY KEY,
    PhieuThietBiId int NOT NULL,
    CongViecId int NULL,
    ThongSoId int NULL,
    NoiDung nvarchar(1000) NULL,
    GiaTri nvarchar(1000) NULL,
    ChiPhi decimal(18,2) NULL,
    NgayBatDau datetime2 NULL,
    NgayKetThuc datetime2 NULL,
    GhiChu nvarchar(500) NULL,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_PhieuThietBiChiTiet_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL
);
ALTER TABLE dbo.PhieuThietBiChiTiet ADD CONSTRAINT FK_PhieuThietBiChiTiet_Phieu FOREIGN KEY (PhieuThietBiId) REFERENCES dbo.PhieuThietBi(Id) ON DELETE CASCADE;

CREATE TABLE dbo.LichSuThietBi (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_LichSuThietBi PRIMARY KEY,
    ThietBiId int NOT NULL,
    LoaiNghiepVu nvarchar(50) NOT NULL,
    NghiepVuId int NULL,
    TrangThaiTruocId int NULL,
    TrangThaiSauId int NULL,
    PhongBanTruocId int NULL,
    PhongBanSauId int NULL,
    BoPhanTruocId int NULL,
    BoPhanSauId int NULL,
    NguoiSuDungTruocId int NULL,
    NguoiSuDungSauId int NULL,
    NoiDung nvarchar(1000) NULL,
    NgayPhatSinh datetime2 NULL,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_LichSuThietBi_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL
);
ALTER TABLE dbo.LichSuThietBi ADD CONSTRAINT FK_LichSuThietBi_ThietBi FOREIGN KEY (ThietBiId) REFERENCES dbo.ThietBi(Id);

CREATE TABLE dbo.TepDinhKem (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TepDinhKem PRIMARY KEY,
    DoiTuongLoai nvarchar(50) NOT NULL,
    DoiTuongId int NOT NULL,
    TenFile nvarchar(500) NOT NULL,
    DuongDan nvarchar(1000) NOT NULL,
    LoaiFile nvarchar(50) NULL,
    DungLuong bigint NULL,
    GhiChu nvarchar(500) NULL,
    NgayKhoiTao datetime2 NOT NULL CONSTRAINT DF_TepDinhKem_NgayKhoiTao DEFAULT SYSUTCDATETIME(),
    MaNguoiNhap nvarchar(50) NULL,
    TenNguoiNhap nvarchar(250) NULL
);
