# QLTB Working State

Last updated: 2026-07-01

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

- `.gitignore`: added to avoid committing build output, IDE state, secrets, raw export files.
- `appsettings.example.json`: added as a safe template.
- `appsettings.json`: ignored because it contains real connection strings/secrets.
- `docs/export.sql`: ignored because it is a raw old-system export.
- `docs/QLTB_SEED_DMDUNGCHUNG_FROM_EXPORT.sql`: generated script that converts old Oracle catalog export into `DmDungChung`.
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
TenNguoiNhap = Trương Văn Voôn
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
- `bin/`, `obj/`, `.vs/`

## Recommended Next Steps

1. Convert `WTB_924_DM_NHOM_THIET_BI` into `NhomThietBi`.
2. Convert `WTB_927_DM_BO_PHAN` into `DonViBoPhan`.
3. Add mapping scripts/tables or notes for old code to new `Guid` IDs.
4. Add validation in `ThietBiBusiness` so IDs belong to correct `DmDungChung.NhomDanhMuc`.
5. Review whether old `TRANG_THAI_TB` codes should map to standard new statuses rather than remain as active choices.
6. Decide whether to upgrade target framework from `net7.0` to `net8.0`.
7. Review `AutoMapper` package warning.

