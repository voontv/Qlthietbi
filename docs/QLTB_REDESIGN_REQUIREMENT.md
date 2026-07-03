# Thi?t k? l?i ph?n m?m Qu?n l� thi?t b?

## 1. M?c ti�u

Thi?t k? m?i ph?n m?m qu?n l� thi?t b? tr�n SQL Server theo hu?ng chu?n h�a, d? m? r?ng, kh�ng ph? thu?c thi?t k? Oracle cu.  
Oracle cu ch? d�ng l�m ngu?n d? li?u d? tham kh?o v� convert sau.

H? th?ng m?i c?n d�p ?ng:

- Qu?n l� h? so thi?t b?.
- Qu?n l� danh m?c.
- Qu?n l� th�ng s? d?ng theo t?ng nh�m thi?t b?.
- Qu?n l� nh?p m?i, c?p ph�t, thu h?i, di?u chuy?n, s?a ch?a, b?o tr�, ki?m k�, thanh l�.
- Luu l?ch s? v�ng d?i thi?t b?.
- C� th? m? r?ng QR Code, file d�nh k�m, dashboard, b�o c�o.
- Ph� h?p ki?n tr�c .NET API: Controller + Business + DTO.

---

## 2. Nguy�n t?c thi?t k?

### 2.1. Kh�ng luu du t�n danh m?c trong b?ng nghi?p v?

Sai:

```text
MaTrangThaiTb
TenTrangThaiTb
MaPhongBan
TenPhongBan
```

��ng:

```text
TrangThaiId
PhongBanId
```

T�n hi?n th? l?y qua JOIN t? b?ng danh m?c.

### 2.2. Thi?t b? c� th�ng tin chung v� th�ng tin ri�ng

Th�ng tin chung luu ? b?ng `ThietBi`.

V� d?:

- M� thi?t b?.
- T�n thi?t b?.
- Nh�m thi?t b?.
- Tr?ng th�i.
- �on v? s? d?ng.
- Ngu?i s? d?ng.
- Ng�y mua.
- Nguy�n gi�.
- Ghi ch�.

Th�ng tin ri�ng theo t?ng nh�m thi?t b? luu b?ng co ch? **th�ng s? d?ng**.

V� d?:

- M�y t�nh: CPU, RAM, SSD, IP, MAC, License.
- M�y in: Kh? gi?y, t?c d? in, lo?i m?c.
- M�y bom: C�ng su?t, luu lu?ng, c?t �p.
- Camera: IP, d? ph�n gi?i, v? tr� l?p d?t.
- �?ng h? nu?c: C? d?ng h?, s? th�n, c?p ch�nh x�c.

### 2.3. M?i bi?n d?ng ph?i ghi l?ch s?

Khi thi?t b? thay d?i tr?ng th�i, ph�ng ban, ngu?i s? d?ng, s?a ch?a, b?o tr�, di?u chuy?n ho?c thanh l� th� ph?i ghi v�o b?ng `LichSuThietBi`.

---

## 3. Danh m?c ch�nh

### 3.1. DmDungChung

D�ng luu c�c danh m?c nh?:

- Tr?ng th�i thi?t b?.
- �on v? t�nh.
- Nh�m qu?n l�.
- Nh�m d?i tu?ng.
- M�u s?c.
- Nh�n hi?u.
- Nu?c s?n xu?t.
- Ch?t li?u.
- Lo?i thanh l�.
- L� do thanh l�.
- K?t lu?n.
- C�ng vi?c b?o tr�/b?o du?ng.

```sql
CREATE TABLE dbo.DmDungChung (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    NhomDanhMuc NVARCHAR(50) NOT NULL,
    Ma NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(250) NOT NULL,
    GhiChu NVARCHAR(500) NULL,
    SapXep INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);

CREATE UNIQUE INDEX IX_DmDungChung_Nhom_Ma
ON dbo.DmDungChung(NhomDanhMuc, Ma);
```

---

## 4. �on v?, ph�ng ban, b? ph?n

```sql
CREATE TABLE dbo.DonViBoPhan (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    MaDonVi NVARCHAR(50) NOT NULL,
    TenDonVi NVARCHAR(250) NOT NULL,
    ParentId UNIQUEIDENTIFIER NULL,
    LoaiDonVi NVARCHAR(50) NULL, -- DON_VI, PHONG_BAN, BO_PHAN, KHO
    GhiChu NVARCHAR(500) NULL,
    SapXep INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);
```

---

## 5. Ngu?i s? d?ng thi?t b?

```sql
CREATE TABLE dbo.NguoiSuDungThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    MaNguoiDung NVARCHAR(50) NOT NULL,
    TenNguoiDung NVARCHAR(250) NOT NULL,
    DonViBoPhanId UNIQUEIDENTIFIER NULL,
    ChucVu NVARCHAR(250) NULL,
    SoDienThoai NVARCHAR(50) NULL,
    Email NVARCHAR(250) NULL,
    GhiChu NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);
```

---

## 6. Nh�m thi?t b?

Nh�m thi?t b? l� th�nh ph?n quan tr?ng v� m?i nh�m s? c� b? th�ng s? ri�ng.

```sql
CREATE TABLE dbo.NhomThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    MaNhomThietBi NVARCHAR(50) NOT NULL,
    TenNhomThietBi NVARCHAR(250) NOT NULL,
    KyHieu NVARCHAR(20) NULL,
    ParentId UNIQUEIDENTIFIER NULL,
    MoTa NVARCHAR(1000) NULL,
    SapXep INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);

CREATE UNIQUE INDEX IX_NhomThietBi_Ma
ON dbo.NhomThietBi(MaNhomThietBi);
```

V� d? nh�m:

```text
MAY_TINH       - M�y t�nh
MAY_IN         - M�y in
MAY_BOM        - M�y bom
CAMERA         - Camera
DONG_HO_NUOC   - �?ng h? nu?c
THIET_BI_MANG  - Thi?t b? m?ng
```

---

## 7. H? so thi?t b?

```sql
CREATE TABLE dbo.ThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    MaThietBi NVARCHAR(50) NOT NULL,
    MaThietBiCu NVARCHAR(50) NULL,
    TenThietBi NVARCHAR(500) NOT NULL,

    SoSerial NVARCHAR(100) NULL,
    Model NVARCHAR(100) NULL,
    MaKeToan NVARCHAR(50) NULL,
    MaThietBiCha NVARCHAR(50) NULL,

    NhomThietBiId UNIQUEIDENTIFIER NOT NULL,
    TrangThaiId UNIQUEIDENTIFIER NOT NULL,
    TrangThaiKiemKeId UNIQUEIDENTIFIER NULL,

    DonViTinhId UNIQUEIDENTIFIER NULL,
    NhanHieuId UNIQUEIDENTIFIER NULL,
    MauSacId UNIQUEIDENTIFIER NULL,
    NuocSanXuatId UNIQUEIDENTIFIER NULL,
    ChatLieuId UNIQUEIDENTIFIER NULL,
    DonViCungCapId UNIQUEIDENTIFIER NULL,

    PhongBanId UNIQUEIDENTIFIER NULL,
    BoPhanId UNIQUEIDENTIFIER NULL,
    NguoiSuDungId UNIQUEIDENTIFIER NULL,

    NgayMua DATE NULL,
    NgayNhapThietBi DATE NULL,
    NgayDuaVaoSuDung DATE NULL,
    NguyenGia DECIMAL(18,2) NULL,
    ThoiGianBaoHanh INT NULL,
    NgayHetBaoHanh DATE NULL,

    MaQrCode NVARCHAR(100) NULL,
    ViTriLapDat NVARCHAR(500) NULL,

    GhiChu NVARCHAR(1000) NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);

CREATE UNIQUE INDEX IX_ThietBi_MaThietBi
ON dbo.ThietBi(MaThietBi);
```

---

## 8. Th�ng s? d?ng theo nh�m thi?t b?

### 8.1. Danh m?c th�ng s? thi?t b?

```sql
CREATE TABLE dbo.DmThongSoThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    NhomThietBiId UNIQUEIDENTIFIER NOT NULL,

    MaThongSo NVARCHAR(50) NOT NULL,
    TenThongSo NVARCHAR(250) NOT NULL,

    KieuDuLieu NVARCHAR(50) NOT NULL, -- TEXT, NUMBER, DATE, BOOLEAN, SELECT
    DonViTinhId UNIQUEIDENTIFIER NULL,

    BatBuoc BIT NOT NULL DEFAULT 0,
    SapXep INT NULL,
    GhiChu NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);

CREATE UNIQUE INDEX IX_DmThongSoThietBi_Nhom_Ma
ON dbo.DmThongSoThietBi(NhomThietBiId, MaThongSo);
```

### 8.2. Gi� tr? th�ng s? c?a t?ng thi?t b?

```sql
CREATE TABLE dbo.ThietBiThongSo (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ThietBiId UNIQUEIDENTIFIER NOT NULL,
    ThongSoId UNIQUEIDENTIFIER NOT NULL,

    GiaTriText NVARCHAR(1000) NULL,
    GiaTriNumber DECIMAL(18,4) NULL,
    GiaTriDate DATE NULL,
    GiaTriBit BIT NULL,

    GhiChu NVARCHAR(500) NULL,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL
);

CREATE UNIQUE INDEX IX_ThietBiThongSo_ThietBi_ThongSo
ON dbo.ThietBiThongSo(ThietBiId, ThongSoId);
```

---

## 9. V� d? th�ng s? theo nh�m thi?t b?

### 9.1. Nh�m m�y t�nh

Qu?n l� c�c th�ng tin:

- CPU.
- RAM.
- ? c?ng SSD.
- ? c?ng HDD.
- H? di?u h�nh.
- License Windows.
- License Office.
- T�n m�y t�nh.
- �?a ch? IP.
- MAC Address.
- Domain/Workgroup.
- Card d? h?a.
- K�ch thu?c m�n h�nh.

```sql
-- Gi? s? @NhomMayTinhId l� Id nh�m M�y t�nh
INSERT INTO dbo.DmThongSoThietBi
(NhomThietBiId, MaThongSo, TenThongSo, KieuDuLieu, BatBuoc, SapXep)
VALUES
(@NhomMayTinhId, N'CPU', N'B? vi x? l�', N'TEXT', 1, 1),
(@NhomMayTinhId, N'RAM', N'Dung lu?ng RAM', N'NUMBER', 1, 2),
(@NhomMayTinhId, N'SSD', N'Dung lu?ng ? c?ng SSD', N'NUMBER', 0, 3),
(@NhomMayTinhId, N'HDD', N'Dung lu?ng ? c?ng HDD', N'NUMBER', 0, 4),
(@NhomMayTinhId, N'OS', N'H? di?u h�nh', N'TEXT', 0, 5),
(@NhomMayTinhId, N'WINDOWS_LICENSE', N'License Windows', N'TEXT', 0, 6),
(@NhomMayTinhId, N'OFFICE_LICENSE', N'License Office', N'TEXT', 0, 7),
(@NhomMayTinhId, N'COMPUTER_NAME', N'T�n m�y t�nh', N'TEXT', 0, 8),
(@NhomMayTinhId, N'IP', N'�?a ch? IP', N'TEXT', 0, 9),
(@NhomMayTinhId, N'MAC', N'MAC Address', N'TEXT', 0, 10);
```

### 9.2. Nh�m m�y in

- C�ng ngh? in.
- Kh? gi?y.
- T?c d? in.
- In m�u/den tr?ng.
- Duplex.
- Lo?i h?p m?c.
- IP m�y in.

### 9.3. Nh�m m�y bom

- C�ng su?t.
- �i?n �p.
- Luu lu?ng.
- C?t �p.
- �u?ng k�nh ?ng.
- V? tr� l?p d?t.
- Ng�y v?n h�nh.

### 9.4. Nh�m camera

- �? ph�n gi?i.
- IP.
- MAC.
- V? tr� l?p d?t.
- Chu?n n�n.
- H�ng d?u ghi.
- K�nh d?u ghi.

### 9.5. Nh�m d?ng h? nu?c

- C? d?ng h?.
- S? th�n.
- C?p ch�nh x�c.
- Luu lu?ng danh d?nh.
- Nam s?n xu?t.
- Tem ki?m d?nh.
- Ng�y ki?m d?nh.

---

## 10. Phi?u nghi?p v? thi?t b?

M?t b?ng phi?u chung, ph�n bi?t b?ng `LoaiPhieu`.

```sql
CREATE TABLE dbo.PhieuThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    SoPhieu NVARCHAR(50) NOT NULL,
    LoaiPhieu NVARCHAR(50) NOT NULL,
    NgayPhieu DATE NOT NULL,

    ThietBiId UNIQUEIDENTIFIER NOT NULL,

    PhongBanId UNIQUEIDENTIFIER NULL,
    BoPhanId UNIQUEIDENTIFIER NULL,
    NguoiSuDungId UNIQUEIDENTIFIER NULL,

    DonViThucHienId UNIQUEIDENTIFIER NULL,
    KetLuanId UNIQUEIDENTIFIER NULL,

    NoiDung NVARCHAR(1000) NULL,
    ChiPhi DECIMAL(18,2) NULL,

    FileScan01 NVARCHAR(500) NULL,
    FileScan02 NVARCHAR(500) NULL,

    GhiChu NVARCHAR(1000) NULL,
    IsActive BIT NOT NULL DEFAULT 1,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL,
    NgayChinhSuaCuoiCung DATETIME2 NULL,
    MaNguoiChinhSua NVARCHAR(50) NULL,
    TenNguoiChinhSua NVARCHAR(250) NULL
);
```

Lo?i phi?u:

```text
NHAP_KHO
CAP_PHAT
THU_HOI
LUAN_CHUYEN
SUA_CHUA
BAO_TRI
THAY_THE
KIEM_KE
THANH_LY
```

### Chi ti?t phi?u

```sql
CREATE TABLE dbo.PhieuThietBiChiTiet (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    PhieuThietBiId UNIQUEIDENTIFIER NOT NULL,

    CongViecId UNIQUEIDENTIFIER NULL,
    ThongSoId UNIQUEIDENTIFIER NULL,

    NoiDung NVARCHAR(1000) NULL,
    GiaTri NVARCHAR(1000) NULL,
    ChiPhi DECIMAL(18,2) NULL,

    NgayBatDau DATE NULL,
    NgayKetThuc DATE NULL,

    GhiChu NVARCHAR(500) NULL,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL
);
```

---

## 11. L?ch s? thi?t b?

```sql
CREATE TABLE dbo.LichSuThietBi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ThietBiId UNIQUEIDENTIFIER NOT NULL,

    LoaiNghiepVu NVARCHAR(50) NOT NULL,
    NghiepVuId UNIQUEIDENTIFIER NULL,

    TrangThaiTruocId UNIQUEIDENTIFIER NULL,
    TrangThaiSauId UNIQUEIDENTIFIER NULL,

    PhongBanTruocId UNIQUEIDENTIFIER NULL,
    PhongBanSauId UNIQUEIDENTIFIER NULL,

    BoPhanTruocId UNIQUEIDENTIFIER NULL,
    BoPhanSauId UNIQUEIDENTIFIER NULL,

    NguoiSuDungTruocId UNIQUEIDENTIFIER NULL,
    NguoiSuDungSauId UNIQUEIDENTIFIER NULL,

    NoiDung NVARCHAR(1000) NULL,
    NgayPhatSinh DATE NULL,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL
);
```

---

## 12. File d�nh k�m

```sql
CREATE TABLE dbo.TepDinhKem (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    DoiTuongLoai NVARCHAR(50) NOT NULL, -- THIET_BI, PHIEU_THIET_BI
    DoiTuongId UNIQUEIDENTIFIER NOT NULL,

    TenFile NVARCHAR(500) NOT NULL,
    DuongDan NVARCHAR(1000) NOT NULL,
    LoaiFile NVARCHAR(50) NULL,
    DungLuong BIGINT NULL,

    GhiChu NVARCHAR(500) NULL,

    NgayKhoiTao DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaNguoiNhap NVARCHAR(50) NULL,
    TenNguoiNhap NVARCHAR(250) NULL
);
```

---

## 13. Tr?ng th�i thi?t b? m?c d?nh

```sql
INSERT INTO dbo.DmDungChung (NhomDanhMuc, Ma, Ten, SapXep)
VALUES
(N'TRANG_THAI_TB', N'MOI_NHAP', N'M?i nh?p', 1),
(N'TRANG_THAI_TB', N'TRONG_KHO', N'Trong kho', 2),
(N'TRANG_THAI_TB', N'DANG_SU_DUNG', N'�ang s? d?ng', 3),
(N'TRANG_THAI_TB', N'DANG_SUA_CHUA', N'�ang s?a ch?a', 4),
(N'TRANG_THAI_TB', N'DANG_BAO_TRI', N'�ang b?o tr�/b?o du?ng', 5),
(N'TRANG_THAI_TB', N'CHO_THANH_LY', N'Ch? thanh l�', 6),
(N'TRANG_THAI_TB', N'DA_THANH_LY', N'�� thanh l�', 7),
(N'TRANG_THAI_TB', N'MAT_HUY', N'M?t/H?y', 8);
```

---

## 14. Quy tr�nh nghi?p v?

### 14.1. Nh?p thi?t b?

1. T?o h? so thi?t b?.
2. G�n tr?ng th�i `MOI_NHAP` ho?c `TRONG_KHO`.
3. Nh?p th�ng s? d?ng theo nh�m thi?t b?.
4. T?o phi?u `NHAP_KHO`.
5. Ghi l?ch s? thi?t b?.

### 14.2. C?p ph�t thi?t b?

1. Ch?n thi?t b? trong kho.
2. Ch?n ph�ng ban, b? ph?n, ngu?i s? d?ng.
3. T?o phi?u `CAP_PHAT`.
4. C?p nh?t tr?ng th�i `DANG_SU_DUNG`.
5. Ghi l?ch s? thi?t b?.

### 14.3. �i?u chuy?n thi?t b?

1. Ch?n thi?t b? dang s? d?ng.
2. Ch?n noi chuy?n d?n.
3. T?o phi?u `LUAN_CHUYEN`.
4. C?p nh?t ph�ng ban, b? ph?n, ngu?i s? d?ng m?i.
5. Ghi l?ch s? thi?t b?.

### 14.4. S?a ch?a

1. T?o phi?u `SUA_CHUA`.
2. C?p nh?t tr?ng th�i `DANG_SUA_CHUA`.
3. Ghi chi ph� s?a ch?a.
4. Khi ho�n th�nh, c?p nh?t l?i tr?ng th�i tru?c d� ho?c `DANG_SU_DUNG`.
5. Ghi l?ch s?.

### 14.5. B?o tr�/b?o du?ng

1. T?o phi?u `BAO_TRI`.
2. Ch?n c�c c�ng vi?c b?o tr�.
3. Nh?p ng�y b?t d?u, ng�y k?t th�c, chi ph�.
4. C?p nh?t l?ch s?.

### 14.6. Ki?m k�

1. T?o d?t ki?m k�.
2. Qu�t QR ho?c ch?n thi?t b?.
3. C?p nh?t tr?ng th�i ki?m k�: ��ng v? tr�, Sai v? tr�, Kh�ng t�m th?y, Ph�t sinh m?i.
4. Ghi l?ch s? ki?m k�.

### 14.7. Thanh l�

1. T?o phi?u `THANH_LY`.
2. Ch?n lo?i thanh l�, l� do thanh l�.
3. C?p nh?t tr?ng th�i `DA_THANH_LY`.
4. Ghi l?ch s?.

---

## 15. Mapping t? Oracle cu sang h? th?ng m?i

| Oracle cu | SQL Server m?i |
|---|---|
| WTB_00_HO_SO_THIET_BI | ThietBi |
| WTB_00_HO_SO_THIET_BI_CHI_TET | ThietBiThongSo |
| WTB_01_SUA_CHUA_THIET_BI | PhieuThietBi, LoaiPhieu = SUA_CHUA |
| WTB_02_THAY_THE_THIET_BI | PhieuThietBi, LoaiPhieu = THAY_THE |
| WTB_02_THAY_THE_THIET_BI_CTIET | PhieuThietBiChiTiet |
| WTB_03_LUAN_CHUYEN_THIET_BI | PhieuThietBi, LoaiPhieu = LUAN_CHUYEN |
| WTB_04_BAO_TRI_BAO_DUONG | PhieuThietBi, LoaiPhieu = BAO_TRI |
| WTB_04_BAO_TRI_BDUONG_CHI_TIET | PhieuThietBiChiTiet |
| WTB_05_NHAP_KHO_THIET_BI | PhieuThietBi, LoaiPhieu = NHAP_KHO |
| WTB_06_THANH_LY_THIET_BI_BO | PhieuThietBi, LoaiPhieu = THANH_LY |
| WTB_91x, WTB_92x | DmDungChung, NhomThietBi, DonViBoPhan |

---

## 16. Y�u c?u d�nh cho Codex

Codex c?n th?c hi?n theo th? t?:

1. T?o Entity SQL Server theo thi?t k? tr�n.
2. T?o DbContext.
3. T?o DTO cho:
   - Danh m?c.
   - Nh�m thi?t b?.
   - Thi?t b?.
   - Th�ng s? thi?t b?.
   - Phi?u thi?t b?.
   - L?ch s? thi?t b?.
4. T?o Business Interface v� Business Implementation.
5. T?o Controller.
6. T?o API:
   - CRUD danh m?c.
   - CRUD nh�m thi?t b?.
   - CRUD thi?t b?.
   - API l?y th�ng s? theo nh�m thi?t b?.
   - API luu thi?t b? k�m th�ng s? d?ng.
   - API t?o phi?u nghi?p v?.
   - API xem l?ch s? thi?t b?.
7. T?o script seed d? li?u danh m?c m?c d?nh.
8. T?o script mapping Oracle cu sang SQL Server m?i.

Nguy�n t?c code:

- D�ng async/await.
- Business x? l� nghi?p v?, Controller ch? g?i Business.
- Kh�ng d? logic nghi?p v? trong Controller.
- Tr?ng th�i thi?t b? kh�ng nh?p t? do, ph?i l?y t? danh m?c.
- Khi t?o phi?u nghi?p v? ph?i ghi l?ch s? thi?t b?.
- Khi thay d?i ph�ng ban/ngu?i s? d?ng ph?i ghi l?ch s? thi?t b?.
