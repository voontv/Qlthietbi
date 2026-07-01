# QLTB API Architecture & Workflow

## 1. Mục tiêu

Tài liệu này mô tả kiến trúc hiện tại của ứng dụng Quản lý thiết bị và các luồng xử lý chính của API để người khác dễ hiểu code và thiết kế.

## 2. Kiến trúc chung

### 2.1. Mô hình lớp

- Controller: nhận request, gọi business, trả response.
- Business: chứa nghiệp vụ, validation, cập nhật trạng thái và ghi lịch sử.
- DbContext: `QlThietBiContext` quản lý Entity Framework Core.
- DTO: yêu cầu và phản hồi tách biệt với model dữ liệu.
- Models: bản đồ tới bảng dữ liệu SQL Server.

### 2.2. Nguyên tắc triển khai

- Không để logic nghiệp vụ trong controller.
- Controller chỉ chịu trách nhiệm định tuyến và gọi business.
- Business dùng `async/await` cho mọi thao tác database.
- Các bảng nghiệp vụ chỉ lưu ID tham chiếu, không lưu lại tên danh mục.
- Tất cả biến động quan trọng của thiết bị phải ghi vào `LichSuThietBi`.

## 3. Các lớp business chính

### 3.1. `Businesses/ThietBi/ThietBiBusiness.cs`

Phục vụ nghiệp vụ chính liên quan thiết bị:

- CRUD danh mục dùng chung (`DmDungChung`).
- CRUD nhóm thiết bị (`NhomThietBi`).
- CRUD hồ sơ thiết bị (`ThietBi`).
- Lấy thông số theo nhóm thiết bị (`DmThongSoThietBi`).
- Tạo/cập nhật thông số thiết bị động (`ThietBiThongSo`).
- Tạo phiếu nghiệp vụ thiết bị (`PhieuThietBi`, `PhieuThietBiChiTiet`).
- Ghi lịch sử thiết bị (`LichSuThietBi`).
- Truy vấn lịch sử thiết bị.

### 3.2. `Businesses/DonViBoPhan/DonViBoPhanBusiness.cs`

Quản lý đơn vị / phòng ban / bộ phận:

- Lấy danh sách.
- Lấy chi tiết.
- Lưu/cập nhật.
- Xóa mềm (deactivate).

### 3.3. `Businesses/NguoiSuDungThietBi/NguoiSuDungThietBiBusiness.cs`

Quản lý người dùng thiết bị:

- CRUD người dùng.
- Chỉ lưu ID tham chiếu tới `DonViBoPhan`.

### 3.4. `Businesses/TepDinhKem/TepDinhKemBusiness.cs`

Quản lý tệp đính kèm:

- Lấy tệp đính kèm theo đối tượng.
- Lưu/cập nhật tệp.
- Xóa tệp.

## 4. DI và khởi tạo

### 4.1. `LibsStartup/DIConfig.cs`

Đăng ký:

- `IThietBiBusiness` -> `ThietBiBusiness`
- `IDonViBoPhanBusiness` -> `DonViBoPhanBusiness`
- `INguoiSuDungThietBiBusiness` -> `NguoiSuDungThietBiBusiness`
- `ITepDinhKemBusiness` -> `TepDinhKemBusiness`
- `IFileService` -> `FileService`

### 4.2. `AutoConfigScanner`

Quét các interface được gắn `[ImplementBy]` để đăng ký service tự động. Hiện tại một số business vẫn được đăng ký tường minh trong `DIConfig`.

## 5. API routes hiện tại

> Swagger UI hiện được phục vụ tại đường dẫn gốc `/` của ứng dụng.
>
> Header `Authorization: Bearer {token}` được hỗ trợ cho các API bảo mật.

### 5.1. Danh mục dùng chung

- `GET api/ThietBi/danh-muc/{nhomDanhMuc}`
- `POST api/ThietBi/danh-muc`
- `DELETE api/ThietBi/danh-muc/{id}`

### 5.2. Nhóm thiết bị

- `GET api/ThietBi/nhom-thiet-bi`
- `POST api/ThietBi/nhom-thiet-bi`
- `DELETE api/ThietBi/nhom-thiet-bi/{id}`

### 5.3. Hồ sơ thiết bị

- `GET api/ThietBi/thiet-bi`
- `GET api/ThietBi/thiet-bi/{id}`
- `POST api/ThietBi/thiet-bi`
- `DELETE api/ThietBi/thiet-bi/{id}`

### 5.4. Thông số thiết bị

- `GET api/ThietBi/thiet-bi/{nhomThietBiId}/thong-so`
- `POST api/ThietBi/thiet-bi/thong-so`
- `DELETE api/ThietBi/thiet-bi/thong-so/{id}`

### 5.5. Phiếu nghiệp vụ thiết bị

- `POST api/ThietBi/phieu-thiet-bi`

### 5.6. Lịch sử thiết bị

- `GET api/ThietBi/thiet-bi/{thietBiId}/lich-su`

### 5.7. Tệp đính kèm

- `GET api/TepDinhKem?doiTuongLoai=ThietBi&doiTuongId={guid}`
- `GET api/TepDinhKem/{id}`
- `POST api/TepDinhKem`
- `DELETE api/TepDinhKem/{id}`

## 6. Luồng xử lý chính

### 6.1. Lưu/cập nhật thiết bị

1. Controller gọi `thietBiBusiness.SaveDeviceAsync(request)`.
2. Business kiểm tra duplicate `MaThietBi`.
3. Nếu là cập nhật:
   - tìm thiết bị hiện tại.
   - so sánh `TrangThaiId`, `PhongBanId`, `BoPhanId`, `NguoiSuDungId`.
   - cập nhật thông tin và `NgayChinhSuaCuoiCung`.
   - nếu có thay đổi nghiệp vụ quan trọng, gọi `CreateHistoryAsync` để ghi `LichSuThietBi`.
4. Nếu là tạo mới:
   - tạo `ThietBi` mới.
   - gán `NgayKhoiTao`.
   - gọi `CreateHistoryAsync` với loại `NHAP_KHO`.
5. Lưu các thông số động:
   - `SaveDeviceParametersAsync` tạo mới hoặc cập nhật `ThietBiThongSo`.
6. Trả về `ThietBiDto` đầy đủ, gồm cả danh sách `ThongSo`.

### 6.2. Tạo phiếu nghiệp vụ

1. Controller gọi `thietBiBusiness.CreatePhieuThietBiAsync(request)`.
2. Business tìm `ThietBi` theo `ThietBiId`.
3. Tạo `PhieuThietBi` mới và chi tiết `PhieuThietBiChiTiet` nếu có.
4. Cập nhật trạng thái/thông tin thiết bị theo `LoaiPhieu`:
   - `CAP_PHAT`: cập nhật `TrangThaiId`, `PhongBanId`, `BoPhanId`, `NguoiSuDungId`.
   - `LUAN_CHUYEN`: cập nhật `PhongBanId`, `BoPhanId`, `NguoiSuDungId`.
   - `SUA_CHUA`: cập nhật trạng thái sửa chữa.
   - `BAO_TRI`: cập nhật trạng thái bảo trì.
   - `THANH_LY`: cập nhật trạng thái thanh lý.
5. Ghi `LichSuThietBi` với thông tin trước và sau biến đổi.
6. Lưu thay đổi vào database.

### 6.3. Ghi sử thay đổi thiết bị

- Mọi lần thay đổi quan trọng đều gọi `CreateHistoryAsync`.
- `LichSuThietBi` lưu:
  - `ThietBiId`
  - `LoaiNghiepVu`
  - `TrangThaiTruocId`, `TrangThaiSauId`
  - `PhongBanTruocId`, `PhongBanSauId`
  - `BoPhanTruocId`, `BoPhanSauId`
  - `NguoiSuDungTruocId`, `NguoiSuDungSauId`
  - `NoiDung`
  - `NgayPhatSinh`

## 7. Các điểm cần lưu ý

- Controller không chứa logic, chỉ nhận request và trả response.
- Business chứa logic nghiệp vụ, validation và history.
- Không lưu tên danh mục trong bảng nghiệp vụ. Chỉ dùng ID và hiển thị tên khi cần query join.
- Tài liệu này nên được bổ sung khi thêm API mới hoặc thay đổi nghiệp vụ.
