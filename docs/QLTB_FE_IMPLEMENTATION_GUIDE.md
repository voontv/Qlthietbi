# QLTB FE Implementation Guide

Last updated: 2026-07-02

## Base

API base path:

```text
/api
```

JSON response/request property names are serialized as `snake_case` by the BE because `Startup.ConfigJson` uses `SnakeJsonNamingPolicy`.

Example:

```json
{
  "ma_thiet_bi": "MTI.0001",
  "ten_thiet_bi": "May tinh",
  "nhom_thiet_bi_id": "guid"
}
```

The static demo at `/qltb-fe/index.html` converts response keys to camelCase internally only for easier JavaScript rendering. A production FE can either use `snake_case` directly or normalize it in its API client.

Swagger:

```text
/swagger
```

Static FE demo:

```text
/qltb-fe/index.html
```

## Core Workflows

### 1. Dashboard / danh sach thiet bi

Call:

```http
GET /api/ThietBi/thiet-bi
```

Response is an array of `ThietBiDto`.

Important fields:

- `id`
- `ma_thiet_bi`
- `ma_thiet_bi_cu`
- `ten_thiet_bi`
- `nhom_thiet_bi_id`
- `trang_thai_id`
- `phong_ban_id`
- `bo_phan_id`
- `nguoi_su_dung_id`
- `nguyen_gia`
- `ngay_nhap_thiet_bi`
- `ngay_dua_vao_su_dung`
- `ghi_chu`
- `thong_so`

FE should load lookup lists first, then map IDs to names:

```http
GET /api/ThietBi/nhom-thiet-bi
GET /api/ThietBi/danh-muc/TRANG_THAI_TB
GET /api/DonViBoPhan
GET /api/NguoiSuDungThietBi
```

### 2. Chi tiet thiet bi

Call:

```http
GET /api/ThietBi/thiet-bi/{id}
GET /api/ThietBi/thiet-bi/{id}/lich-su
GET /api/TepDinhKem?doiTuongLoai=ThietBi&doiTuongId={id}
```

Show:

- Profile fields from `ThietBiDto`
- Dynamic parameters from `thong_so`
- Lifecycle history from `LichSuThietBiDto`
- Attachments from `TepDinhKemDto`

### 3. Them/sua thiet bi

Before rendering form:

```http
GET /api/ThietBi/nhom-thiet-bi
GET /api/ThietBi/danh-muc/TRANG_THAI_TB
GET /api/ThietBi/danh-muc/TRANG_THAI_KK
GET /api/ThietBi/danh-muc/DON_VI_TINH
GET /api/ThietBi/danh-muc/NHAN_HIEU
GET /api/ThietBi/danh-muc/MAU_SAC
GET /api/ThietBi/danh-muc/NUOC_SAN_XUAT
GET /api/ThietBi/danh-muc/CHAT_LIEU
GET /api/ThietBi/danh-muc/DON_VI_CUNG_CAP
GET /api/DonViBoPhan
GET /api/NguoiSuDungThietBi
```

When user selects an equipment group:

```http
GET /api/ThietBi/thiet-bi/{nhomThietBiId}/thong-so
```

Save:

```http
POST /api/ThietBi/thiet-bi
Content-Type: application/json
```

Minimal payload:

```json
{
  "id": null,
  "ma_thiet_bi": "MTI.9999",
  "ten_thiet_bi": "May tinh test",
  "nhom_thiet_bi_id": "guid",
  "trang_thai_id": "guid",
  "is_active": true,
  "thong_so": [
    {
      "thong_so_id": "guid",
      "gia_tri_text": null,
      "gia_tri_number": 8,
      "gia_tri_date": null,
      "gia_tri_bit": null,
      "ghi_chu": "RAM 8GB"
    }
  ]
}
```

### 4. Quan ly nhom thiet bi

List:

```http
GET /api/ThietBi/nhom-thiet-bi
```

Save:

```http
POST /api/ThietBi/nhom-thiet-bi
```

Payload:

```json
{
  "id": null,
  "ma_nhom_thiet_bi": "MAY_IN",
  "ten_nhom_thiet_bi": "May in",
  "ky_hieu": "MI",
  "parent_id": null,
  "mo_ta": "",
  "sap_xep": 1,
  "is_active": true
}
```

Important migration rule:

- Parent old object groups use code `NDT_*`.
- Real equipment must be assigned to child groups, not `NDT_*`.
- `KHONG_XAC_DINH` is a valid temporary child group for old equipment without `MA_NHOM_THIET_BI`.

### 5. Quan ly thong so theo nhom

List:

```http
GET /api/ThietBi/thiet-bi/{nhomThietBiId}/thong-so
```

Save:

```http
POST /api/ThietBi/thiet-bi/thong-so
```

Payload:

```json
{
  "id": null,
  "nhom_thiet_bi_id": "guid",
  "ma_thong_so": "RAM",
  "ten_thong_so": "RAM",
  "kieu_du_lieu": "NUMBER",
  "don_vi_tinh_id": null,
  "bat_buoc": false,
  "sap_xep": 1,
  "is_active": true,
  "ghi_chu": ""
}
```

Supported `kieu_du_lieu` values in FE should be:

- `TEXT`
- `NUMBER`
- `DATE`
- `BIT`

### 6. Tao phieu nghiep vu va lich su

Create slip:

```http
POST /api/ThietBi/phieu-thiet-bi
```

Payload:

```json
{
  "so_phieu": "LC-2026-0001",
  "loai_phieu": "LUAN_CHUYEN",
  "ngay_phieu": "2026-07-02",
  "thiet_bi_id": "guid",
  "phong_ban_id": "guid",
  "bo_phan_id": null,
  "nguoi_su_dung_id": "guid",
  "noi_dung": "Luan chuyen thiet bi",
  "chi_phi": null,
  "chi_tiets": []
}
```

Common `loai_phieu` values:

- `CAP_PHAT`
- `THU_HOI`
- `LUAN_CHUYEN`
- `SUA_CHUA`
- `BAO_TRI`
- `THAY_THE`
- `THANH_LY`

After creating a slip, BE updates current equipment state where appropriate and writes `LichSuThietBi`.

### 7. Danh muc dung chung

Load by group:

```http
GET /api/ThietBi/danh-muc/{nhomDanhMuc}
```

Common groups:

- `TRANG_THAI_TB`
- `TRANG_THAI_KK`
- `DON_VI_TINH`
- `NHAN_HIEU`
- `MAU_SAC`
- `NUOC_SAN_XUAT`
- `CHAT_LIEU`
- `DON_VI_CUNG_CAP`
- `KET_LUAN`
- `CONG_VIEC_BTBD`

Save:

```http
POST /api/ThietBi/danh-muc
```

Payload:

```json
{
  "id": null,
  "nhom_danh_muc": "DON_VI_TINH",
  "ma": "CAI",
  "ten": "Cai",
  "ghi_chu": "",
  "sap_xep": 1,
  "is_active": true
}
```

## Data Migration Notes For FE

- Equipment group `KHONG_XAC_DINH` is intentionally kept for asset management. It means the old profile had no `MA_NHOM_THIET_BI`.
- Equipment groups with code `NDT_*` are parent classification groups converted from old `MA_NHOM_DOI_TUONG`; they are useful for tree display but should not be chosen as a direct device group.
- Some old detail rows were not attached because their `MA_THIET_BI` does not exist in imported equipment profiles. See `docs/bao_cao_thiet_bi_co_van_de.xlsx`.
- The main profile import is currently considered correct for testing: `ThietBi = 6540`.

## Suggested Screens

- Device list with filters: keyword, group, status, department, unknown group only.
- Device detail: profile, dynamic parameters, history, attachments.
- Device edit form with dynamic parameter controls based on selected group.
- Group tree management.
- Parameter definition by group.
- Shared catalog management.
- Business slip creation screen for transfer, repair, maintenance, replacement, liquidation.
