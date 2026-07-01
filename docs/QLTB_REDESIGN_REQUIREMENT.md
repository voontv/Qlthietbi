# Thiết kế lại phần mềm Quản lý thiết bị

## 1. Mục tiêu

Thiết kế mới phần mềm quản lý thiết bị trên SQL Server theo hướng chuẩn hóa, dễ mở rộng, không phụ thuộc thiết kế Oracle cũ.  
Oracle cũ chỉ dùng làm nguồn dữ liệu để tham khảo và convert sau.

Hệ thống mới cần đáp ứng:

- Quản lý hồ sơ thiết bị.
- Quản lý danh mục.
- Quản lý thông số động theo từng nhóm thiết bị.
- Quản lý nhập mới, cấp phát, thu hồi, điều chuyển, sửa chữa, bảo trì, kiểm kê, thanh lý.
- Lưu lịch sử vòng đời thiết bị.
- Có thể mở rộng QR Code, file đính kèm, dashboard, báo cáo.
- Phù hợp kiến trúc .NET API: Controller + Business + DTO.

---

## 2. Nguyên tắc thiết kế

### 2.1. Không lưu dư tên danh mục trong bảng nghiệp vụ

Sai:

```text
MaTrangThaiTb
TenTrangThaiTb
MaPhongBan
TenPhongBan
```

Đúng:

```text
TrangThaiId
PhongBanId
```

Tên hiển thị lấy qua JOIN từ bảng danh mục.

### 2.2. Thiết bị có thông tin chung và thông tin riêng

Thông tin chung lưu ở bảng `ThietBi`.

Ví dụ:

- Mã thiết bị.
- Tên thiết bị.
- Nhóm thiết bị.
- Trạng thái.
- Đơn vị sử dụng.
- Người sử dụng.
- Ngày mua.
- Nguyên giá.
- Ghi chú.

Thông tin riêng theo từng nhóm thiết bị lưu bằng cơ chế **thông số động**.

Ví dụ:

- Máy tính: CPU, RAM, SSD, IP, MAC, License.
- Máy in: Khổ giấy, tốc độ in, loại mực.
- Máy bơm: Công suất, lưu lượng, cột áp.
- Camera: IP, độ phân giải, vị trí lắp đặt.
- Đồng hồ nước: Cỡ đồng hồ, số thân, cấp chính xác.

### 2.3. Mọi biến động phải ghi lịch sử

Khi thiết bị thay đổi trạng thái, phòng ban, người sử dụng, sửa chữa, bảo trì, điều chuyển hoặc thanh lý thì phải ghi vào bảng `LichSuThietBi`.

---

## 3. Danh mục chính

### 3.1. DmDungChung

Dùng lưu các danh mục nhỏ:

- Trạng thái thiết bị.
- Đơn vị tính.
- Nhóm quản lý.
- Nhóm đối tượng.
- Màu sắc.
- Nhãn hiệu.
- Nước sản xuất.
- Chất liệu.
- Loại thanh lý.
- Lý do thanh lý.
- Kết luận.
- Công việc bảo trì/bảo dưỡng.

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

## 4. Đơn vị, phòng ban, bộ phận

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

## 5. Người sử dụng thiết bị

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

## 6. Nhóm thiết bị

Nhóm thiết bị là thành phần quan trọng vì mỗi nhóm sẽ có bộ thông số riêng.

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

Ví dụ nhóm:

```text
MAY_TINH       - Máy tính
MAY_IN         - Máy in
MAY_BOM        - Máy bơm
CAMERA         - Camera
DONG_HO_NUOC   - Đồng hồ nước
THIET_BI_MANG  - Thiết bị mạng
```

---

## 7. Hồ sơ thiết bị

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

## 8. Thông số động theo nhóm thiết bị

### 8.1. Danh mục thông số thiết bị

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

### 8.2. Giá trị thông số của từng thiết bị

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

## 9. Ví dụ thông số theo nhóm thiết bị

### 9.1. Nhóm máy tính

Quản lý các thông tin:

- CPU.
- RAM.
- Ổ cứng SSD.
- Ổ cứng HDD.
- Hệ điều hành.
- License Windows.
- License Office.
- Tên máy tính.
- Địa chỉ IP.
- MAC Address.
- Domain/Workgroup.
- Card đồ họa.
- Kích thước màn hình.

```sql
-- Giả sử @NhomMayTinhId là Id nhóm Máy tính
INSERT INTO dbo.DmThongSoThietBi
(NhomThietBiId, MaThongSo, TenThongSo, KieuDuLieu, BatBuoc, SapXep)
VALUES
(@NhomMayTinhId, N'CPU', N'Bộ vi xử lý', N'TEXT', 1, 1),
(@NhomMayTinhId, N'RAM', N'Dung lượng RAM', N'NUMBER', 1, 2),
(@NhomMayTinhId, N'SSD', N'Dung lượng ổ cứng SSD', N'NUMBER', 0, 3),
(@NhomMayTinhId, N'HDD', N'Dung lượng ổ cứng HDD', N'NUMBER', 0, 4),
(@NhomMayTinhId, N'OS', N'Hệ điều hành', N'TEXT', 0, 5),
(@NhomMayTinhId, N'WINDOWS_LICENSE', N'License Windows', N'TEXT', 0, 6),
(@NhomMayTinhId, N'OFFICE_LICENSE', N'License Office', N'TEXT', 0, 7),
(@NhomMayTinhId, N'COMPUTER_NAME', N'Tên máy tính', N'TEXT', 0, 8),
(@NhomMayTinhId, N'IP', N'Địa chỉ IP', N'TEXT', 0, 9),
(@NhomMayTinhId, N'MAC', N'MAC Address', N'TEXT', 0, 10);
```

### 9.2. Nhóm máy in

- Công nghệ in.
- Khổ giấy.
- Tốc độ in.
- In màu/đen trắng.
- Duplex.
- Loại hộp mực.
- IP máy in.

### 9.3. Nhóm máy bơm

- Công suất.
- Điện áp.
- Lưu lượng.
- Cột áp.
- Đường kính ống.
- Vị trí lắp đặt.
- Ngày vận hành.

### 9.4. Nhóm camera

- Độ phân giải.
- IP.
- MAC.
- Vị trí lắp đặt.
- Chuẩn nén.
- Hãng đầu ghi.
- Kênh đầu ghi.

### 9.5. Nhóm đồng hồ nước

- Cỡ đồng hồ.
- Số thân.
- Cấp chính xác.
- Lưu lượng danh định.
- Năm sản xuất.
- Tem kiểm định.
- Ngày kiểm định.

---

## 10. Phiếu nghiệp vụ thiết bị

Một bảng phiếu chung, phân biệt bằng `LoaiPhieu`.

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

Loại phiếu:

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

### Chi tiết phiếu

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

## 11. Lịch sử thiết bị

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

## 12. File đính kèm

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

## 13. Trạng thái thiết bị mặc định

```sql
INSERT INTO dbo.DmDungChung (NhomDanhMuc, Ma, Ten, SapXep)
VALUES
(N'TRANG_THAI_TB', N'MOI_NHAP', N'Mới nhập', 1),
(N'TRANG_THAI_TB', N'TRONG_KHO', N'Trong kho', 2),
(N'TRANG_THAI_TB', N'DANG_SU_DUNG', N'Đang sử dụng', 3),
(N'TRANG_THAI_TB', N'DANG_SUA_CHUA', N'Đang sửa chữa', 4),
(N'TRANG_THAI_TB', N'DANG_BAO_TRI', N'Đang bảo trì/bảo dưỡng', 5),
(N'TRANG_THAI_TB', N'CHO_THANH_LY', N'Chờ thanh lý', 6),
(N'TRANG_THAI_TB', N'DA_THANH_LY', N'Đã thanh lý', 7),
(N'TRANG_THAI_TB', N'MAT_HUY', N'Mất/Hủy', 8);
```

---

## 14. Quy trình nghiệp vụ

### 14.1. Nhập thiết bị

1. Tạo hồ sơ thiết bị.
2. Gán trạng thái `MOI_NHAP` hoặc `TRONG_KHO`.
3. Nhập thông số động theo nhóm thiết bị.
4. Tạo phiếu `NHAP_KHO`.
5. Ghi lịch sử thiết bị.

### 14.2. Cấp phát thiết bị

1. Chọn thiết bị trong kho.
2. Chọn phòng ban, bộ phận, người sử dụng.
3. Tạo phiếu `CAP_PHAT`.
4. Cập nhật trạng thái `DANG_SU_DUNG`.
5. Ghi lịch sử thiết bị.

### 14.3. Điều chuyển thiết bị

1. Chọn thiết bị đang sử dụng.
2. Chọn nơi chuyển đến.
3. Tạo phiếu `LUAN_CHUYEN`.
4. Cập nhật phòng ban, bộ phận, người sử dụng mới.
5. Ghi lịch sử thiết bị.

### 14.4. Sửa chữa

1. Tạo phiếu `SUA_CHUA`.
2. Cập nhật trạng thái `DANG_SUA_CHUA`.
3. Ghi chi phí sửa chữa.
4. Khi hoàn thành, cập nhật lại trạng thái trước đó hoặc `DANG_SU_DUNG`.
5. Ghi lịch sử.

### 14.5. Bảo trì/bảo dưỡng

1. Tạo phiếu `BAO_TRI`.
2. Chọn các công việc bảo trì.
3. Nhập ngày bắt đầu, ngày kết thúc, chi phí.
4. Cập nhật lịch sử.

### 14.6. Kiểm kê

1. Tạo đợt kiểm kê.
2. Quét QR hoặc chọn thiết bị.
3. Cập nhật trạng thái kiểm kê: Đúng vị trí, Sai vị trí, Không tìm thấy, Phát sinh mới.
4. Ghi lịch sử kiểm kê.

### 14.7. Thanh lý

1. Tạo phiếu `THANH_LY`.
2. Chọn loại thanh lý, lý do thanh lý.
3. Cập nhật trạng thái `DA_THANH_LY`.
4. Ghi lịch sử.

---

## 15. Mapping từ Oracle cũ sang hệ thống mới

| Oracle cũ | SQL Server mới |
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

## 16. Yêu cầu dành cho Codex

Codex cần thực hiện theo thứ tự:

1. Tạo Entity SQL Server theo thiết kế trên.
2. Tạo DbContext.
3. Tạo DTO cho:
   - Danh mục.
   - Nhóm thiết bị.
   - Thiết bị.
   - Thông số thiết bị.
   - Phiếu thiết bị.
   - Lịch sử thiết bị.
4. Tạo Business Interface và Business Implementation.
5. Tạo Controller.
6. Tạo API:
   - CRUD danh mục.
   - CRUD nhóm thiết bị.
   - CRUD thiết bị.
   - API lấy thông số theo nhóm thiết bị.
   - API lưu thiết bị kèm thông số động.
   - API tạo phiếu nghiệp vụ.
   - API xem lịch sử thiết bị.
7. Tạo script seed dữ liệu danh mục mặc định.
8. Tạo script mapping Oracle cũ sang SQL Server mới.

Nguyên tắc code:

- Dùng async/await.
- Business xử lý nghiệp vụ, Controller chỉ gọi Business.
- Không để logic nghiệp vụ trong Controller.
- Trạng thái thiết bị không nhập tự do, phải lấy từ danh mục.
- Khi tạo phiếu nghiệp vụ phải ghi lịch sử thiết bị.
- Khi thay đổi phòng ban/người sử dụng phải ghi lịch sử thiết bị.
