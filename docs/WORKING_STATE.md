# QLTB Working State

Last updated: 2026-07-02

## Project Context

This repository is an ASP.NET Core 7 API for the redesigned equipment management system (`QlThietBi`).

Primary architecture documents:

- `docs/QLTB_REDESIGN_REQUIREMENT.md`
- `docs/QLTB_API_ARCHITECTURE.md`
- `docs/QLTB_SQLSERVER_SCHEMA.sql`

The new design uses SQL Server and separates:

- Controllers: routing and API response only.
- Businesses: validation and business workflow.
- DTOs: request/response contracts.
- Models: EF Core entity mapping.
- `DmDungChung`: shared small catalogs.
- `NhomThietBi`: equipment groups.
- `DonViBoPhan`: unit/department/section tree.
- `ThietBi`: equipment profile.
- `ThietBiThongSo`: dynamic equipment parameter values.
- `PhieuThietBi`: equipment business slips.
- `LichSuThietBi`: equipment lifecycle/history.

## Important Local Files

- `.gitignore`: avoids committing build output, IDE state, secrets, and raw export files.
- `appsettings.example.json`: safe template.
- `appsettings.json`: ignored because it contains real connection strings/secrets.
- `docs/export.sql`: ignored because it is a raw old-system export.
- `docs/QLTB_SEED_DMDUNGCHUNG_FROM_EXPORT.sql`: converts old Oracle catalog export into `DmDungChung`.
- `docs/QLTB_SEED_NHOMTHIETBI_FROM_EXPORT.sql`: converts old equipment groups into `NhomThietBi`.
- `docs/QLTB_SEED_DONVIBOPHAN_FROM_EXPORT.sql`: converts old units/departments into `DonViBoPhan`.
- `docs/QLTB_IMPORT_THIETBI_FROM_DULIEU_DANHAP.sql`: converts imported old equipment profiles into `ThietBi` for test migration.
- `docs/QLTB_IMPORT_THIETBI_THONGSO_FROM_DULIEU_DANHAP.sql`: converts imported old equipment detail parameters into `DmThongSoThietBi` and `ThietBiThongSo`.
- `docs/QLTB_REPORT_NHOM_KHONG_XAC_DINH.sql`: separate report SQL for equipment intentionally assigned to `KHONG_XAC_DINH`.
- `docs/QLTB_FE_IMPLEMENTATION_GUIDE.md`: API/workflow guide for frontend implementation.
- `wwwroot/qltb-fe/`: static FE demo for the current BE workflows.
- `docs/WORKING_STATE.md`: this file.

## Code Changes Already Made

### `Businesses/ThietBi/ThietBiBusiness.cs`

- Fixed history creation from `PhieuThietBi` so `LichSuThietBi.NghiepVuId` points to `PhieuThietBi.Id`, not `ThietBi.Id`.
- Added handling for `LoaiPhieu = THU_HOI`:
  - sets status to `TRONG_KHO`
  - clears `PhongBanId`, `BoPhanId`, `NguoiSuDungId`
- Updates `NgayChinhSuaCuoiCung` when a slip changes the device.
- Sorted common categories, equipment groups, device lists, and parameter lists.
- `LayThietBiTheoIdAsync` no longer returns soft-deleted devices.
- `LayTrangThaiIdAsync` only accepts active status rows.

### Vietnamese Method Names

The `ThietBi` business/controller method names were renamed to Vietnamese without accents:

- `LayDanhMucDungChungAsync`
- `LuuDanhMucDungChungAsync`
- `XoaDanhMucDungChungAsync`
- `LayDanhSachNhomThietBiAsync`
- `LuuNhomThietBiAsync`
- `XoaNhomThietBiAsync`
- `LayDanhSachThietBiAsync`
- `LuuThietBiAsync`
- `LayThietBiTheoIdAsync`
- `XoaThietBiAsync`
- `LayThongSoTheoNhomThietBiAsync`
- `LuuThongSoThietBiAsync`
- `XoaThongSoThietBiAsync`
- `TaoPhieuThietBiAsync`
- `LayLichSuThietBiAsync`

Private helpers also use Vietnamese names:

- `LuuThongSoCuaThietBiAsync`
- `TaoLichSuThietBiAsync`
- `LayTrangThaiIdAsync`

API routes were intentionally kept unchanged.

### Swagger

`LibsStartup/SwaggerConfig.cs` was fixed so Swagger UI loads JSON from:

```text
/swagger/v1/swagger.json
```

instead of the broken `v1/swagger.json`.

Verified:

- `GET https://localhost:62220/swagger/v1/swagger.json` returned `200`.
- Swagger UI HTML at `/` contains `"/swagger/v1/swagger.json"`.

## Database State

Database used during this session:

```text
Data Source=200.201.222.68,1433
Initial Catalog=qlthietbi
```

Schema was created by running:

```powershell
sqlcmd -S 200.201.222.68,1433 -d qlthietbi -U sa -P <password> -C -i docs\QLTB_SQLSERVER_SCHEMA.sql
```

Tables created:

- `DmDungChung`
- `DonViBoPhan`
- `NguoiSuDungThietBi`
- `NhomThietBi`
- `ThietBi`
- `DmThongSoThietBi`
- `ThietBiThongSo`
- `PhieuThietBi`
- `PhieuThietBiChiTiet`
- `LichSuThietBi`
- `TepDinhKem`

Default equipment statuses were initially seeded from `QLTB_SQLSERVER_SCHEMA.sql`, then corrected for Vietnamese text.

## DmDungChung Conversion From Old System

Raw old-system export:

```text
docs/export.sql
```

This file is intentionally ignored by Git.

Generated conversion script:

```text
docs/QLTB_SEED_DMDUNGCHUNG_FROM_EXPORT.sql
```

It converts old `WTB_91x` and related catalog tables into `DmDungChung` using idempotent `MERGE`.

Audit values required by user:

```text
MaNguoiNhap = 0044
TenNguoiNhap = Truong Van Voon (Vietnamese: Trương Văn Voôn)
```

Inserted/updated `175` converted rows.

Groups converted into `DmDungChung`:

- `CHAT_LIEU`
- `CONG_VIEC_BTBD`
- `DON_VI_CUNG_CAP`
- `DON_VI_TINH`
- `KET_LUAN`
- `KHO`
- `LOAI_THANH_LY`
- `LY_DO_THANH_LY`
- `MAU_SAC`
- `NGUOI_PHU_TRACH`
- `NHAN_HIEU`
- `NHAT_KY_THIET_BI`
- `NHOM_DOI_TUONG`
- `NHOM_QUAN_LY`
- `NUOC_SAN_XUAT`
- `TRANG_THAI_KK`
- `TRANG_THAI_TB`

Notes:

- `TRANG_THAI_TB` currently contains both the new standard statuses (`MOI_NHAP`, `TRONG_KHO`, etc.) and old status codes (`0001` to `0008`) to support later legacy-data mapping.
- All current `DmDungChung.NgayKhoiTao` values were updated to `2026-07-01` per user request.

Verification query result:

```text
DmDungChung total after date update: 183
Converted rows with MaNguoiNhap=0044 and TenNguoiNhap=Trương Văn Voôn: 175
```

## NhomThietBi And DonViBoPhan Conversion From Old System

Generated conversion scripts:

```text
docs/QLTB_SEED_NHOMTHIETBI_FROM_EXPORT.sql
docs/QLTB_SEED_DONVIBOPHAN_FROM_EXPORT.sql
```

Both scripts are idempotent and preserve old codes for later migration mapping:

- `WTB_924_DM_NHOM_THIET_BI.MA_NHOM_THIET_BI` -> `NhomThietBi.MaNhomThietBi`
- `WTB_927_DM_BO_PHAN.MA_BO_PHAN` -> `DonViBoPhan.MaDonVi`

Audit values:

```text
MaNguoiNhap = 0044
TenNguoiNhap = Trương Văn Voôn
NgayKhoiTao = SYSDATETIME()
```

Database verification after running the scripts:

```text
NhomThietBi: 120 rows
NhomThietBi parent rows from old MA_NHOM_DOI_TUONG: 7
NhomThietBi child rows with ParentId: 113
DonViBoPhan: 134 rows
DonViBoPhan rows with ParentId: 129
DonViBoPhan old-parent placeholders: 4
DonViBoPhan rows marked as KHO: 10
```

`WTB_924_DM_NHOM_THIET_BI.MA_NHOM_DOI_TUONG` is now represented as parent `NhomThietBi` rows. Because old object-group codes like `0001` conflict with real equipment-group codes like `0001 = Ghế`, parent codes are stored as `NDT_0001`, `NDT_0002`, etc. Child equipment groups keep their original `MA_NHOM_THIET_BI` and point to these parent rows via `ParentId`. Old object-group text is no longer stored in child `MoTa`; only old free-form notes remain there.

`WTB_927_DM_BO_PHAN.MA_DON_VI_PARENT` sometimes uses four-character parent codes while `MA_BO_PHAN` is usually three characters, so the script links parents by exact code or `RIGHT(MA_DON_VI_PARENT, 3)`.

The old export did not contain real names for four parent codes: `0001`, `0002`, `0013`, `0544`. The script creates placeholder parent nodes for those with `LoaiDonVi = DON_VI_CHA_CU` and names like `Don vi cha cu 0013`. These placeholders keep the hierarchy and later migration mapping intact; update their names if a fuller old organization export is provided.

## ThietBi Import From Real Old Data

Raw old-system data file:

```text
docs/du_lieu_daNhap.sql
```

This file is intentionally ignored by Git.

Generated conversion script:

```text
docs/QLTB_IMPORT_THIETBI_FROM_DULIEU_DANHAP.sql
```

Generated history/business conversion script:

```text
docs/QLTB_IMPORT_LICHSU_THIETBI_FROM_DULIEU_DANHAP.sql
```

Generated equipment-parameter conversion script:

```text
docs/QLTB_IMPORT_THIETBI_THONGSO_FROM_DULIEU_DANHAP.sql
```

Audit fix scripts generated from the same raw file:

```text
docs/QLTB_FIX_THIETBI_AUDIT_FROM_DULIEU_DANHAP.sql
docs/QLTB_FIX_NGUOISUDUNG_AUDIT_FROM_DULIEU_DANHAP.sql
```

The audit fix blocks are also appended to the end of `docs/QLTB_IMPORT_THIETBI_FROM_DULIEU_DANHAP.sql`, so rerunning the import does not reset real equipment/user audit fields back to the migration user.

Imported source table:

```text
QLTB_LOC.WTB_00_HO_SO_THIET_BI
```

Important mapping rules:

- `WTB_00_HO_SO_THIET_BI.MA_NHOM_THIET_BI` maps to `ThietBi.NhomThietBiId`.
- `NDT_*` parent equipment groups are never assigned to `ThietBi.NhomThietBiId`.
- Equipment rows without `MA_NHOM_THIET_BI` map to child group `KHONG_XAC_DINH`.
- `MA_PHONG_BAN` and `MA_BO_PHAN` are preserved as exact old codes in `DonViBoPhan.MaDonVi`.
- Missing users from `MA_NLD_SU_DUNG/TEN_NLD_SU_DUNG` are created in `NguoiSuDungThietBi`.
- Catalog/master rows created by seed scripts use migration audit `0044 / Trương Văn Voôn`.
- Real equipment/user rows use old-system audit: `LOG_USER -> MaNguoiNhap`, `LOG_DATE -> NgayKhoiTao`, `LOG_USER_LAST -> MaNguoiChinhSua`, `LOG_DATE_LAST -> NgayChinhSuaCuoiCung`.

Database verification after running the import:

```text
ThietBi: 6540 rows
NguoiSuDungThietBi: 219 rows
Equipment mapped to KHONG_XAC_DINH group: 1446
Equipment incorrectly mapped to NDT_* parent groups: 0
ThietBi audit is distributed by old LOG_USER, for example hai.cntt, 0044, nguyen.cntt, 0043, 0564...
NguoiSuDungThietBi audit is distributed by old LOG_USER, mostly nguyen.cntt.
```

Equipment profile detail import verification:

```text
Source WTB_00_HO_SO_THIET_BI_CHI_TET rows: 281
Source rows that reference missing equipment: 29
Source duplicate device-parameter pairs: 7
DmThongSoThietBi: 13 rows
ThietBiThongSo: 249 rows
```

`WTB_00_HO_SO_THIET_BI_CHI_TET` maps as follows:

- `MA_THIET_BI` -> `ThietBi.MaThietBi`
- `MA_THONG_SO/TEN_THONG_SO` -> `DmThongSoThietBi.MaThongSo/TenThongSo` per equipment group
- `GIA_TRI` -> `ThietBiThongSo.GiaTriNumber`
- `TEN_DON_VI_TINH` -> `DmThongSoThietBi.DonViTinhId` via `DmDungChung(NhomDanhMuc='DON_VI_TINH')`
- `LOG_USER/LOG_DATE` -> real old audit on `ThietBiThongSo`

The detail import does not create placeholder equipment. These 11 old detail codes are present in detail rows but not in imported `ThietBi`, so their 29 parameter rows are not attached:

```text
MTI.0321
SCN.0002
SCN.0003
TUR.0002
TUR.0003
TUR.0004
TUR.0005
TUR.0006
TUR.0007
TUR.0010
TUR.0011
```

If a later old-system export contains those equipment profiles, rerun `docs/QLTB_IMPORT_THIETBI_FROM_DULIEU_DANHAP.sql` first, then rerun `docs/QLTB_IMPORT_THIETBI_THONGSO_FROM_DULIEU_DANHAP.sql`.

`KHONG_XAC_DINH` is intentionally kept as a valid temporary group for asset management. It should not be treated as a failed import. The separate report script is:

```text
docs/QLTB_REPORT_NHOM_KHONG_XAC_DINH.sql
```

Business/history import verification:

```text
PhieuThietBi:
- SUA_CHUA: 74
- THAY_THE: 133
- LUAN_CHUYEN: 14
- BAO_TRI: 1

PhieuThietBiChiTiet: 133
LichSuThietBi: 222
WTB_05_NHAP_KHO_THIET_BI source rows: 0
```

History import is intentionally direct SQL, not through business methods, so it does not rewrite the current equipment status/location from the old final profile. `PhieuThietBi` stores the old business record, and `LichSuThietBi.NghiepVuId` points to the imported `PhieuThietBi.Id`.

## Build/Test Status

Build command:

```powershell
dotnet build QlThietBi.sln
```

Result:

```text
0 Error(s)
```

Known warnings still present:

- `net7.0` is out of support.
- `AutoMapper 13.0.1` vulnerability warning.
- Many XML documentation warnings because `GenerateDocumentationFile=true`.

These warnings were not addressed yet.

FE/static verification:

```text
https://localhost:5078/qltb-fe/index.html -> 200
https://localhost:5078/qltb-fe/app.js -> 200
GET /api/ThietBi/thiet-bi -> 200
GET /api/ThietBi/nhom-thiet-bi -> 200
GET /api/DonViBoPhan -> 200
GET /api/NguoiSuDungThietBi -> 200
GET /api/ThietBi/danh-muc/TRANG_THAI_TB -> 200
```

The app redirects HTTP to HTTPS in `Startup`, so local manual testing should use the HTTPS URL or the launch profile HTTPS port.

## Git State Guidance

The repo previously had no `.gitignore`, so everything appeared untracked.

Now ignored:

- `bin/`
- `obj/`
- `.vs/`
- `.vscode/`
- `*.user`
- generated `*.xml`
- `appsettings.json`
- `appsettings.*.json`
- `docs/export.sql`
- `docs/du_lieu_daNhap.sql`
- temp/log/upload/database backup artifacts

Commit candidates include:

- source folders (`AutoConfig`, `Businesses`, `Controllers`, `DTO`, `Models`, etc.)
- `docs/QLTB_*`
- `docs/WORKING_STATE.md`
- `.gitignore`
- `appsettings.example.json`
- `QlThietBi.csproj`, `QlThietBi.sln`
- `Startup.cs`, `Program.cs`
- `wwwroot/` if this static frontend bundle is intentionally part of the app

Do not commit:

- `appsettings.json`
- `docs/export.sql`
- `docs/du_lieu_daNhap.sql`
- `bin/`, `obj/`, `.vs/`

## Recommended Next Steps

1. Add mapping scripts/tables or notes for old code to new `int identity` IDs.
2. Convert real equipment records after user provides the old equipment table export.
3. Add validation in `ThietBiBusiness` so IDs belong to correct `DmDungChung.NhomDanhMuc`.
4. Review whether old `TRANG_THAI_TB` codes should map to standard new statuses rather than remain as active choices.
5. Decide whether to upgrade target framework from `net7.0` to `net8.0`.
6. Review `AutoMapper` package warning.

