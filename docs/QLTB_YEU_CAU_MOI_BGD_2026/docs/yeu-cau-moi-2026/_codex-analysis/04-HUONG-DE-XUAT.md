# 04 - Hướng đề xuất

Ngày lập: 2026-07-08

## Đề xuất

Đề xuất chọn hướng 2: dùng `PhieuThietBi` làm hạ tầng nghiệp vụ chung cho một thiết bị, bổ sung bảng/phần mở rộng cho các quy trình thật sự cần dữ liệu riêng.

Không nên viết lại module quản lý thiết bị hiện tại. Không nên tạo module quy trình hoàn toàn riêng ngay từ đầu nếu chưa chốt đầy đủ quy trình duyệt và biểu mẫu.

## Nguyên tắc thiết kế đã chốt

1. Mỗi `PhieuThietBi` chỉ xử lý một `ThietBi`.

2. Không thiết kế một `PhieuThietBi` chứa nhiều thiết bị.

3. `PhieuThietBiChiTiet` chỉ dùng để lưu các dòng công việc, nội dung thực hiện, chi phí, thời gian và dữ liệu chi tiết của thiết bị trên phiếu.

4. Thiết bị có thể có cấu trúc cha/con.

Một thiết bị cha có thể chứa nhiều thiết bị con. Thiết bị con vẫn là một `ThietBi` độc lập và có thể báo hỏng riêng, sửa chữa riêng, bảo trì riêng, thay thế riêng, thu hồi riêng, có lịch sử riêng.

5. Các bảng có nhiều thiết bị trong tài liệu Ban Giám đốc là báo cáo hoặc hồ sơ tổng hợp trình Ban Giám đốc.

Các báo cáo này được tổng hợp từ nhiều thiết bị hoặc nhiều `PhieuThietBi`, không phải một phiếu nghiệp vụ chứa nhiều thiết bị.

## Vì sao ít ảnh hưởng hệ thống cũ

- Hồ sơ thiết bị hiện tại nằm ở `ThietBi` và đã convert ổn.
- Danh mục, nhóm thiết bị, đơn vị, người sử dụng đã có.
- Phiếu nghiệp vụ `PhieuThietBi` đã có quan hệ một phiếu - một thiết bị qua `ThietBiId`.
- Lịch sử `LichSuThietBi` đã ghi được trạng thái/phòng ban/bộ phận/người sử dụng trước/sau.
- File đính kèm đã có `TepDinhKem`.
- Các nghiệp vụ cơ bản như cấp phát, luân chuyển, thu hồi, sửa chữa, bảo trì, thanh lý đã có điểm móc trong `ThietBiBusiness.TaoPhieuThietBiAsync`.

Vì vậy phần mới nên bám vào nền này thay vì thay đổi cấu trúc lõi.

## Phần có thể tái sử dụng

- `ThietBi`: hồ sơ tài sản/thiết bị.
- `ThietBi.MaThietBi`: mã thiết bị nghiệp vụ dạng `nvarchar(50)`.
- `ThietBi.MaThietBiCha`: mã thiết bị cha dạng `nvarchar(50)`, dùng cho quan hệ cha/con theo mã thiết bị.
- `NhomThietBi`: phân loại thiết bị.
- `DmDungChung`: trạng thái, kết luận, công việc bảo trì/bảo dưỡng, danh mục phụ.
- `DonViBoPhan`: phòng ban/bộ phận.
- `NguoiSuDungThietBi`: người sử dụng/người phụ trách.
- `PhieuThietBi`: phiếu nghiệp vụ gốc của một thiết bị.
- `PhieuThietBiChiTiet`: dòng công việc, chi phí, ngày bắt đầu/kết thúc, dữ liệu chi tiết của thiết bị trên phiếu.
- `LichSuThietBi`: lịch sử thay đổi theo từng thiết bị.
- `TepDinhKem`: chứng từ/file scan.
- Auth token hiện tại: lấy `MaNguoiDung` từ bearer token để ghi người nhập/người xử lý.
- `GET api/ThietBi/thiet-bi/thong-ke`: thống kê hồ sơ thiết bị theo phòng ban, bộ phận, nhóm, trạng thái.

## Hiện trạng thiết bị cha/con

Hiện hệ thống đã có quan hệ cha/con ở mức mã thiết bị, đúng với DB thật:

- Entity: `Models/ThietBi.cs`.
- Field: `MaThietBi string`, DB thật là `nvarchar(50)`.
- Field: `MaThietBiCha string?`, DB thật là `nvarchar(50)`.
- Request: `CreateUpdateThietBiRequest.MaThietBiCha`.
- Response: `ThietBiDto.MaThietBiCha`.
- API hiện dùng chung:
  - `POST api/ThietBi/thiet-bi` để tạo/cập nhật `MaThietBiCha`.
  - `GET api/ThietBi/thiet-bi` để trả danh sách có `MaThietBiCha`.
  - `GET api/ThietBi/thiet-bi/{id}` để trả chi tiết có `MaThietBiCha`.

Chốt thiết kế:

- `Id` vẫn là khóa kỹ thuật để truy cập/join nhanh trong hệ thống.
- `MaThietBi` là mã nghiệp vụ dạng chuỗi, dùng hiển thị, tìm kiếm, in ấn, đối chiếu dữ liệu cũ.
- `MaThietBiCha` là mã nghiệp vụ của thiết bị cha, cũng dạng chuỗi.
- Không đổi `MaThietBi` thành primary key.
- Không đổi `MaThietBiCha` sang khóa ngoại int.
- Nếu FE cần cây thiết bị, bổ sung API lấy danh sách thiết bị con theo `MaThietBiCha`.

## Phần thật sự cần bổ sung

Các phần dưới đây cần chốt trước khi code, nhưng nhiều khả năng phải bổ sung:

1. Trạng thái phiếu nghiệp vụ

Ví dụ: nháp, gửi duyệt, đã duyệt, đang thực hiện, chờ nghiệm thu, hoàn thành, hủy.

2. Báo hỏng

Cần mô hình hóa bước báo hỏng cho một thiết bị, người báo, nội dung hỏng, hình ảnh/chứng từ, kiểm tra hiện trạng, đề xuất xử lý.

3. Quan hệ phiếu nguồn - phiếu xử lý

Ví dụ: phiếu báo hỏng của thiết bị A sinh ra phiếu sửa chữa hoặc thay thế cho chính thiết bị A. Hiện `PhieuThietBi` chưa có quan hệ nguồn/xử lý như `PhieuNguonId`.

4. Nghiệm thu hoàn thành

Cần người/bên tham gia, ngày nghiệm thu, kết quả, kết luận, chi phí thực tế, file biên bản, cập nhật trạng thái thiết bị sau nghiệm thu.

5. Thay thế thiết bị/bộ phận

Nếu thay cả thiết bị, cần quan hệ thiết bị cũ - thiết bị mới và xử lý phần cũ: thu hồi, nhập kho, tái sử dụng, chờ thanh lý.

Nếu thay bộ phận/linh kiện và bộ phận đó được quản lý như tài sản riêng, nên lưu bộ phận đó là một `ThietBi` con độc lập. Nếu chỉ là vật tư/chi tiết không quản lý vòng đời riêng, có thể ghi vào `PhieuThietBiChiTiet`.

6. Kế hoạch bảo trì/bảo dưỡng định kỳ

Nếu BGD yêu cầu quản lý định kỳ, cần thêm kế hoạch/lịch nhắc/tần suất. Nếu chỉ ghi nhận phát sinh thì chưa cần ngay.

7. Quyền duyệt

Hiện mới xác định user từ token. Cần chốt vai trò/cấp duyệt trước khi thiết kế.

8. Báo cáo/tờ trình tổng hợp Ban Giám đốc

Hiện chỉ có thống kê hồ sơ thiết bị, chưa có báo cáo tổng hợp nhiều phiếu theo đợt/tờ trình.

Nếu chỉ cần xem nhanh: tổng hợp trực tiếp từ `PhieuThietBi` là đủ.

Nếu cần lưu một hồ sơ trình chính thức: nên bổ sung khái niệm đợt/tờ trình/báo cáo tổng hợp, có bảng chi tiết liên kết nhiều `PhieuThietBi`.

## Thứ tự triển khai hợp lý

### Bước 1 - Chốt nghiệp vụ

Chốt các câu hỏi trong `05-CAU-HOI-CAN-CHOT.md`, đặc biệt:

- quy trình báo hỏng;
- quy trình duyệt;
- trạng thái phiếu;
- nghiệm thu;
- thay thế và xử lý tài sản cũ;
- thiết bị cha/con hiển thị và tra cứu theo `MaThietBiCha`;
- báo cáo BGD là query/in nhanh hay là tờ trình cần lưu vết;
- biểu mẫu cần in.

### Bước 2 - Chuẩn hóa danh mục nghiệp vụ

Chuẩn hóa danh sách:

- loại phiếu;
- trạng thái phiếu;
- kết luận;
- công việc bảo trì/bảo dưỡng;
- hướng xử lý sau báo hỏng;
- trạng thái sau thu hồi/thay thế.

### Bước 3 - Thiết kế mở rộng dữ liệu

Thiết kế tối thiểu:

- giữ `PhieuThietBi` làm phiếu gốc của một thiết bị;
- giữ `PhieuThietBiChiTiet` cho dòng công việc/chi phí/thời gian;
- bổ sung trạng thái phiếu;
- bổ sung quan hệ phiếu nguồn nếu cần;
- bổ sung bảng nghiệm thu nếu BGD yêu cầu dữ liệu riêng;
- bổ sung bảng thay thế nếu cần theo dõi thiết bị/bộ phận cũ - mới;
- bổ sung API cây thiết bị theo `MaThietBiCha` nếu FE cần;
- bổ sung bảng báo cáo/tờ trình tổng hợp nếu cần lưu vết trình BGD.

### Bước 4 - Backend API

Ưu tiên API theo quy trình:

- tạo báo hỏng cho một thiết bị;
- kiểm tra/đề xuất xử lý;
- tạo sửa chữa/bảo trì/thay thế/thu hồi/điều chuyển cho một thiết bị;
- duyệt phiếu;
- nghiệm thu hoàn thành;
- lấy lịch sử quy trình theo thiết bị;
- lấy cây thiết bị cha/con theo `MaThietBiCha` nếu có;
- lập/xem báo cáo tổng hợp nhiều phiếu nếu cần.

### Bước 5 - Frontend

Làm theo luồng người dùng:

- dashboard công việc/phiếu;
- màn hình báo hỏng;
- màn hình xử lý phiếu;
- màn hình nghiệm thu;
- màn hình thay thế/thu hồi;
- màn hình lịch sử thiết bị;
- màn hình cây thiết bị cha/con nếu cần;
- màn hình báo cáo/tờ trình tổng hợp Ban Giám đốc nếu cần lưu vết.

## Mức đáp ứng hiện tại

Ước lượng hệ thống hiện tại đáp ứng khoảng 40-50% nền kỹ thuật của yêu cầu mới:

- đã có nền hồ sơ, phiếu một thiết bị, chi tiết công việc, lịch sử, danh mục, file;
- đã có quan hệ thiết bị cha/con bằng `MaThietBiCha nvarchar(50)`;
- đã có thống kê hồ sơ thiết bị;
- chưa có đầy đủ workflow, phê duyệt, nghiệm thu, báo hỏng, thay thế và xử lý tài sản cũ;
- chưa có báo cáo/tờ trình tổng hợp nhiều phiếu trình Ban Giám đốc.

## Kết luận đề xuất

Nên mở rộng có kiểm soát trên nền `PhieuThietBi`, không viết lại. Giữ chặt nguyên tắc một phiếu xử lý một thiết bị. Với các nghiệp vụ phức tạp, thêm bảng phụ thay vì nhồi tất cả vào phiếu chung.

Các biểu mẫu nhiều thiết bị nên được xem là báo cáo/tờ trình tổng hợp từ nhiều `PhieuThietBi`. Nếu chỉ cần xem/in nhanh thì query trực tiếp. Nếu cần lưu vết trình ký thì thêm bảng đợt/tờ trình/báo cáo tổng hợp. Cách này giữ ổn định dữ liệu đã convert, giảm rủi ro, và vẫn đủ đường để mở rộng theo yêu cầu BGD.
