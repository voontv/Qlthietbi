# 02 - Đối chiếu yêu cầu mới

Ngày lập: 2026-07-08

## Ghi chú

Đối chiếu dựa trên `02-PHAM-VI-NGHIEN-CUU.md`, prompt trao đổi, nguyên tắc nghiệp vụ đã chốt và hiện trạng code/schema. PDF gốc chưa trích text/OCR được trong môi trường hiện tại.

## Nguyên tắc nghiệp vụ đã chốt

- Mỗi `PhieuThietBi` chỉ xử lý một `ThietBi`.
- Không thiết kế một `PhieuThietBi` chứa nhiều thiết bị.
- `PhieuThietBiChiTiet` chỉ lưu dòng công việc, nội dung thực hiện, chi phí, thời gian và dữ liệu chi tiết của thiết bị đang nằm trên phiếu.
- Thiết bị có thể có cấu trúc cha/con. Thiết bị con vẫn là một `ThietBi` độc lập, có thể báo hỏng, sửa chữa, bảo trì, thay thế, thu hồi và có lịch sử riêng.
- Các bảng nhiều thiết bị trong tài liệu Ban Giám đốc được hiểu là báo cáo/hồ sơ tổng hợp, được tổng hợp từ nhiều thiết bị hoặc nhiều `PhieuThietBi`, không phải một phiếu nghiệp vụ chứa nhiều thiết bị.

## Hiện trạng quan hệ thiết bị cha/con

Code hiện tại đã có nền cho quan hệ thiết bị cha/con bằng mã thiết bị cha:

- Entity: `Models/ThietBi.cs`.
- Field: `ThietBi.MaThietBi string`, DB thật là `nvarchar(50)`.
- Field: `ThietBi.MaThietBiCha string?`, DB thật là `nvarchar(50)`.
- DTO request: `DTO/Request/CreateUpdateThietBiRequest.cs` có `MaThietBiCha`.
- DTO response: `DTO/Response/ThietBiDto.cs` có `MaThietBiCha`.
- API hiện có:
  - `GET api/ThietBi/thiet-bi` trả danh sách thiết bị có `MaThietBiCha`.
  - `GET api/ThietBi/thiet-bi/{id}` trả chi tiết thiết bị có `MaThietBiCha`.
  - `POST api/ThietBi/thiet-bi` tạo/cập nhật thiết bị có nhận `MaThietBiCha`.

Đánh giá: hướng này phù hợp với DB thật vì mã thiết bị là chuỗi `nvarchar(50)` và mã thiết bị cha cũng là chuỗi `nvarchar(50)`. `Id` vẫn giữ vai trò khóa kỹ thuật để truy cập/join nhanh trong hệ thống, còn `MaThietBi` và `MaThietBiCha` là mã nghiệp vụ để người dùng, dữ liệu cũ và báo cáo hiểu được.

Không đề xuất đổi `MaThietBi` thành primary key và cũng không đề xuất đổi `MaThietBiCha` sang khóa ngoại int. Nếu cần bổ sung sau này, chỉ cần thêm API tra cứu cây thiết bị theo `MaThietBiCha`, ví dụ lấy danh sách thiết bị con theo `maThietBiCha`, và trả thêm danh sách con trong màn hình chi tiết nếu FE cần.

## Hiện trạng báo cáo tổng hợp nhiều phiếu

Hiện đã có API thống kê thiết bị:

- `GET api/ThietBi/thiet-bi/thong-ke`.
- Request: `DTO/Request/QueryThongKeThietBiRequest.cs`.
- Response: `DTO/Response/ThongKeThietBiDto.cs`.
- Logic: `ThietBiBusiness.ThongKeThietBiAsync`.

API này tổng hợp theo hồ sơ thiết bị: phòng ban, bộ phận, nhóm thiết bị, trạng thái, danh sách thiết bị và tổng nguyên giá. Chưa thấy cơ chế báo cáo tổng hợp nhiều `PhieuThietBi` theo đợt/tờ trình/hồ sơ trình Ban Giám đốc.

Với báo cáo Ban Giám đốc, có hai mức:

- Báo cáo tra cứu nhanh: có thể tổng hợp trực tiếp từ `PhieuThietBi`, `ThietBi`, `LichSuThietBi` và danh mục.
- Hồ sơ/tờ trình chính thức: nên có thêm khái niệm đợt/tờ trình/báo cáo tổng hợp để gom nhiều phiếu, lưu số tờ trình, ngày trình, người lập, người duyệt, trạng thái trình, file đính kèm và danh sách phiếu/thiết bị thuộc báo cáo.

## Bảng đối chiếu

| Nhóm yêu cầu mới | Hệ thống hiện tại đã có chưa | Mức độ | Dẫn chứng hiện tại | Phần cần bổ sung |
| --- | --- | --- | --- | --- |
| Một phiếu xử lý một thiết bị | Đã đúng với thiết kế hiện tại | Đã có nền | `PhieuThietBi.ThietBiId`, `CreatePhieuThietBiRequest.ThietBiId` | Giữ nguyên tắc này khi mở rộng, không thêm danh sách thiết bị vào `PhieuThietBi` |
| Chi tiết phiếu | Đã có `PhieuThietBiChiTiet` | Đã có nền | `CongViecId`, `ThongSoId`, `NoiDung`, `GiaTri`, `ChiPhi`, `NgayBatDau`, `NgayKetThuc` | Chỉ dùng cho công việc/dữ liệu chi tiết của một thiết bị trên phiếu |
| Thiết bị cha/con | Có `MaThietBiCha` dạng `nvarchar(50)` | Có nền | `ThietBi.MaThietBiCha`, DTO request/response | Bổ sung API cây thiết bị theo `MaThietBiCha` nếu FE cần |
| Bảo trì/bảo dưỡng: ghi nhận phiếu phát sinh | Có phiếu chung `PhieuThietBi`, chi tiết `PhieuThietBiChiTiet`, `LoaiPhieu = BAO_TRI` | Có một phần | `Models/PhieuThietBi.cs`, `Models/PhieuThietBiChiTiet.cs`, `ThietBiBusiness.TaoPhieuThietBiAsync` | Chuẩn hóa loại phiếu, trạng thái phiếu, nghiệm thu và báo cáo theo từng thiết bị |
| Bảo trì/bảo dưỡng: kế hoạch định kỳ | Chưa thấy | Chưa có | Không có entity/API kế hoạch trong `Models`, `Controllers`, `DTO` | Bổ sung kế hoạch/lịch định kỳ nếu BGD yêu cầu quản lý trước phát sinh |
| Bảo trì/bảo dưỡng: dự toán và chi phí thực tế | Có `ChiPhi`, nhưng chưa tách dự toán/thực tế | Cần mở rộng | `PhieuThietBi.ChiPhi`, `PhieuThietBiChiTiet.ChiPhi` | Chốt có cần `DuToan`, `ChiPhiThucTe`, nguồn kinh phí, kế toán hay không |
| Bảo trì/bảo dưỡng: nghiệm thu hoàn thành | Có `KetLuanId`, `NgayKetThuc`, file scan | Có một phần | `PhieuThietBi.KetLuanId`, `PhieuThietBiChiTiet.NgayKetThuc`, `TepDinhKem` | Bổ sung bước nghiệm thu, người nghiệm thu, trạng thái hoàn thành, kết quả sau xử lý |
| Báo hỏng | Chưa có loại phiếu/luồng riêng | Chưa có | `ThietBiBusiness` switch chưa có `BAO_HONG` | Bổ sung luồng báo hỏng theo từng thiết bị, kiểm tra hiện trạng, liên kết tới sửa chữa/thay thế/thu hồi |
| Kiểm tra hiện trạng sau báo hỏng | Chưa thấy | Chưa có | Không có entity/API kiểm tra hiện trạng | Bổ sung bước kiểm tra, người kiểm tra, kết quả kiểm tra, đề xuất xử lý |
| Sửa chữa: phiếu sửa chữa | Có `LoaiPhieu = SUA_CHUA` và đổi trạng thái `DANG_SUA_CHUA` | Có một phần | `ThietBiBusiness.TaoPhieuThietBiAsync` | Bổ sung phê duyệt, đơn vị thực hiện, nghiệm thu, hoàn tất trạng thái |
| Sửa chữa: liên kết báo hỏng với xử lý | Chưa có báo hỏng nên chưa có liên kết | Chưa có | `PhieuThietBi` chưa có `PhieuNguonId` | Cần quan hệ phiếu nguồn - phiếu xử lý, vẫn mỗi phiếu xử lý một thiết bị |
| Thay cả tài sản/thiết bị | Chưa có | Chưa có | Không có `LoaiPhieu = THAY_THE`; không có bảng quan hệ thiết bị cũ - mới | Bổ sung quan hệ thiết bị bị thay và thiết bị thay thế, xử lý trạng thái tài sản cũ |
| Thay bộ phận/linh kiện | Chưa rõ | Chưa có | Có `ThietBi.MaThietBiCha`, nhưng chưa có nghiệp vụ thay thế con/linh kiện | Nếu linh kiện là tài sản quản lý riêng thì lưu như `ThietBi` con độc lập; nếu không thì ghi ở `PhieuThietBiChiTiet` |
| Thu hồi | Có `LoaiPhieu = THU_HOI`, cập nhật trạng thái `TRONG_KHO`, xóa phòng ban/bộ phận/người dùng hiện tại | Có một phần | `ThietBiBusiness.TaoPhieuThietBiAsync` | Bổ sung nơi nhận, người giao/nhận, tình trạng thu hồi, xử lý sau thu hồi |
| Điều chuyển | Có `LoaiPhieu = LUAN_CHUYEN`, cập nhật phòng ban/bộ phận/người sử dụng | Có một phần | `ThietBiBusiness.TaoPhieuThietBiAsync`, `LichSuThietBi` | Bổ sung xác nhận bên giao/bên nhận, ngày hiệu lực, trạng thái phiếu, biểu mẫu |
| Cấp phát | Có `LoaiPhieu = CAP_PHAT`, cập nhật trạng thái `DANG_SU_DUNG` | Có một phần | `ThietBiBusiness.TaoPhieuThietBiAsync` | Có thể tái dùng cho điều chuyển/thu hồi nếu quy trình BGD chốt |
| Nghiệm thu sửa chữa/bảo dưỡng/nâng cấp/cải tạo | Chưa có module nghiệm thu riêng | Cần mở rộng | Chỉ có `KetLuanId`, `ChiTiet.NgayKetThuc`, `FileScan` | Bổ sung bước nghiệm thu, bên tham gia, kết luận, chi phí, trạng thái sau nghiệm thu |
| Quyền duyệt | Chưa thấy | Chưa có | Auth hiện lấy user từ token; chưa có vai trò/quyền duyệt trong entity/API | Cần chốt cấp duyệt, vai trò, luồng duyệt |
| Trạng thái phiếu nghiệp vụ | Chưa có trường trạng thái phiếu | Chưa có | `PhieuThietBi` có `IsActive` nhưng không có `TrangThaiPhieuId` | Bổ sung trạng thái phiếu nếu có phê duyệt/nghiệm thu |
| Biểu mẫu/in ấn | Chưa thấy | Chưa có | Backend chưa có export mẫu | Cần danh sách mẫu biểu, dữ liệu từng mẫu, định dạng in |
| Báo cáo tổng hợp nhiều thiết bị/phiếu | Chỉ có thống kê hồ sơ thiết bị | Có một phần nhỏ | `GET api/ThietBi/thiet-bi/thong-ke` | Nếu là hồ sơ trình BGD chính thức, nên bổ sung đợt/tờ trình/báo cáo tổng hợp |
| File/chứng từ | Có nền tảng | Có một phần | `TepDinhKem`, `FileScan01`, `FileScan02` | Nên thống nhất dùng `TepDinhKem` thay vì nhiều cột file rời |
| Lịch sử tài sản | Có | Có một phần | `LichSuThietBi` lưu trạng thái/phòng ban/bộ phận/người dùng trước/sau | Mở rộng nếu cần lịch sử thay linh kiện/cấu hình/quan hệ thay thế |

## Nhận định nhanh

Tính theo nhóm nghiệp vụ lớn, hệ thống hiện đáp ứng khoảng 40-50% phần nền dữ liệu và thao tác cơ bản. Phần đang khớp tốt là nguyên tắc một phiếu xử lý một thiết bị, hồ sơ thiết bị, chi tiết công việc và lịch sử thiết bị.

Phần còn thiếu chủ yếu là quy trình quản trị: báo hỏng, phê duyệt, nghiệm thu, trạng thái phiếu, kế hoạch định kỳ, quan hệ phiếu nguồn - phiếu xử lý, thay thế thiết bị/linh kiện và báo cáo/tờ trình tổng hợp nhiều phiếu cho Ban Giám đốc.
