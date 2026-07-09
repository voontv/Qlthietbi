# 01 - Hiện trạng liên quan

Ngày lập: 2026-07-08

## Ghi chú nguồn

Đã đọc:

- `docs/QLTB_YEU_CAU_MOI_BGD_2026/docs/yeu-cau-moi-2026/00-READ-ME-FIRST.md`
- `docs/QLTB_YEU_CAU_MOI_BGD_2026/docs/yeu-cau-moi-2026/02-PHAM-VI-NGHIEN-CUU.md`
- `docs/QLTB_YEU_CAU_MOI_BGD_2026/docs/yeu-cau-moi-2026/03-PROMPT-TRAO-DOI-VOI-CODEX.md`
- tài liệu hệ thống hiện tại trong `docs`
- code backend, frontend, entity và schema hiện tại

Hạn chế: file PDF gốc `tai-lieu-goc/01-YEU-CAU-BAN-GIAM-DOC.pdf` hiện là dạng ảnh/nén, môi trường không có `pdftotext`, OCR hoặc thư viện PDF để trích nội dung trực tiếp. Phần phân tích dưới đây bám theo phạm vi yêu cầu đã được tóm tắt trong các file Markdown của gói yêu cầu mới và đối chiếu với hệ thống hiện tại.

## 1. Nền tảng hiện tại đã có

Hệ thống hiện tại đã có lõi quản lý thiết bị và nghiệp vụ phát sinh xoay quanh bảng `ThietBi`.

Các phần liên quan:

- Hồ sơ thiết bị: `Models/ThietBi.cs`, `DTO/Request/CreateUpdateThietBiRequest.cs`, `DTO/Response/ThietBiDto.cs`.
- Phiếu nghiệp vụ dùng chung: `Models/PhieuThietBi.cs`, `Models/PhieuThietBiChiTiet.cs`, `DTO/Request/CreatePhieuThietBiRequest.cs`, `DTO/Response/PhieuThietBiDto.cs`.
- Lịch sử thiết bị: `Models/LichSuThietBi.cs`, `DTO/Response/LichSuThietBiDto.cs`.
- Danh mục dùng chung: `Models/DmDungChung.cs`.
- Đơn vị/phòng ban/bộ phận: `Models/DonViBoPhan.cs`.
- Người sử dụng thiết bị: `Models/NguoiSuDungThietBi.cs`.
- Thông số động theo nhóm thiết bị: `Models/DmThongSoThietBi.cs`, `Models/ThietBiThongSo.cs`.
- File đính kèm: `Models/TepDinhKem.cs`, `Controllers/TepDinhKemController.cs`.

Schema liên quan nằm trong `docs/QLTB_SCHEMA_INT_ID.sql`.

API chính:

- `GET /api/ThietBi/thiet-bi`
- `GET /api/ThietBi/thiet-bi/thong-ke`
- `GET /api/ThietBi/thiet-bi/{id}`
- `POST /api/ThietBi/thiet-bi`
- `POST /api/ThietBi/phieu-thiet-bi`
- `GET /api/ThietBi/thiet-bi/{thietBiId}/lich-su`
- `GET /api/TepDinhKem?doiTuongLoai=...&doiTuongId=...`

Frontend demo hiện có trong `wwwroot/qltb-fe/index.html`, `wwwroot/qltb-fe/app.js`, `wwwroot/qltb-fe/style.css`. Màn hình hiện tại chủ yếu là tra cứu, thống kê, xem nhóm, xem thông số nhóm, xem danh mục; chưa có form đầy đủ cho các quy trình nghiệp vụ mới.

## 2. Bảo trì / bảo dưỡng

Hiện trạng: có một phần.

Đã có:

- Hạ tầng phiếu chung `PhieuThietBi` có `LoaiPhieu`, `NgayPhieu`, `ThietBiId`, `DonViThucHienId`, `KetLuanId`, `NoiDung`, `ChiPhi`, `FileScan01`, `FileScan02`, `GhiChu`.
- Chi tiết phiếu `PhieuThietBiChiTiet` có `CongViecId`, `ThongSoId`, `NoiDung`, `GiaTri`, `ChiPhi`, `NgayBatDau`, `NgayKetThuc`, `GhiChu`.
- Danh mục công việc bảo trì/bảo dưỡng có nhóm `CONG_VIEC_BTBD` trong `ThietBiController`.
- Danh mục kết luận có nhóm `KET_LUAN` trong `ThietBiController`.
- Trong `ThietBiBusiness.TaoPhieuThietBiAsync`, `LoaiPhieu = BAO_TRI` cập nhật trạng thái thiết bị sang mã trạng thái `DANG_BAO_TRI`.
- Sau khi tạo phiếu, hệ thống ghi `LichSuThietBi`.

Chưa thấy:

- kế hoạch bảo trì/bảo dưỡng định kỳ;
- lịch dự kiến/lịch nhắc;
- quy trình lập, kiểm tra, duyệt;
- phân biệt bảo trì và bảo dưỡng nếu Ban Giám đốc muốn tách nghiệp vụ;
- nghiệm thu hoàn thành có trạng thái riêng;
- dự toán so với chi phí thực tế rõ ràng;
- màn hình FE tạo/theo dõi bảo trì/bảo dưỡng.

Kết luận: hạ tầng lưu phiếu và chi tiết công việc đã có, nhưng quy trình bảo trì/bảo dưỡng đầy đủ mới chỉ có phần ghi nhận phiếu và đổi trạng thái.

## 3. Báo hỏng

Hiện trạng: chưa có rõ.

Đã có phần có thể tái sử dụng:

- `PhieuThietBi` có thể lưu một loại phiếu mới nếu thêm `LoaiPhieu = BAO_HONG`.
- `NoiDung`, `GhiChu`, `FileScan01`, `FileScan02` có thể lưu mô tả và chứng từ báo hỏng.
- `LichSuThietBi` có thể ghi lịch sử phát sinh.

Chưa thấy:

- entity/bảng riêng cho báo hỏng;
- API báo hỏng riêng;
- luồng người sử dụng báo hỏng;
- bước kiểm tra hiện trạng;
- quyết định sau báo hỏng: sửa chữa, thay thế, thu hồi, thanh lý, không xử lý;
- trạng thái phiếu báo hỏng;
- liên kết rõ giữa phiếu báo hỏng và phiếu xử lý sau đó.

Kết luận: báo hỏng là phần thiếu lớn nhất nếu tài liệu BGD yêu cầu luồng riêng.

## 4. Sửa chữa

Hiện trạng: có một phần.

Đã có:

- `PhieuThietBi` có thể lưu phiếu sửa chữa bằng `LoaiPhieu = SUA_CHUA`.
- `PhieuThietBiChiTiet` lưu nội dung công việc, chi phí, ngày bắt đầu/kết thúc.
- `DonViThucHienId`, `KetLuanId`, `ChiPhi` hỗ trợ đơn vị thực hiện, kết luận, chi phí.
- `ThietBiBusiness.TaoPhieuThietBiAsync` đổi trạng thái thiết bị sang `DANG_SUA_CHUA` khi `LoaiPhieu = SUA_CHUA`.
- `LichSuThietBi` ghi trạng thái/phòng ban/bộ phận/người sử dụng trước/sau.

Chưa thấy:

- luồng đề xuất sửa chữa sau báo hỏng;
- phê duyệt trước khi sửa;
- trạng thái phiếu sửa chữa;
- nghiệm thu sửa chữa hoàn thành;
- ngày hoàn thành thực tế ở cấp phiếu;
- phân tách dự toán và chi phí thực tế.

Kết luận: có nền lưu sửa chữa, nhưng chưa đủ quy trình quản trị.

## 5. Thay thế

Hiện trạng: chưa có rõ.

Đã có phần liên quan:

- `ThietBi.MaThietBiCha` có thể biểu diễn quan hệ cha/con ở mức hồ sơ thiết bị, nhưng chưa phải quan hệ thay thế.
- `PhieuThietBiChiTiet.ThongSoId` và `GiaTri` có thể ghi một số thay đổi thông số sau xử lý.
- `LichSuThietBi` ghi lịch sử trạng thái/đơn vị/người dùng.

Chưa thấy:

- `LoaiPhieu = THAY_THE` trong switch xử lý của `ThietBiBusiness.TaoPhieuThietBiAsync`;
- bảng/quan hệ thiết bị cũ - thiết bị mới;
- thay cả tài sản/thiết bị;
- thay bộ phận/linh kiện;
- thu hồi bộ phận cũ;
- nhập kho/tái sử dụng/chờ thanh lý phần cũ;
- lịch sử cấu hình trước/sau của thiết bị.

Kết luận: thay thế cần bổ sung đáng kể, đặc biệt phần quan hệ cái cũ - cái mới và xử lý tài sản/bộ phận cũ.

## 6. Thu hồi

Hiện trạng: có một phần.

Đã có:

- `LoaiPhieu = THU_HOI` trong `ThietBiBusiness.TaoPhieuThietBiAsync`.
- Khi thu hồi, hệ thống đổi trạng thái về `TRONG_KHO`, đồng thời set `PhongBanId`, `BoPhanId`, `NguoiSuDungId` về `null`.
- Có ghi `LichSuThietBi`.
- `PhieuThietBi` có thể lưu nội dung, ngày phiếu, ghi chú, file scan.

Chưa thấy:

- nơi tiếp nhận thu hồi/kho tiếp nhận rõ ràng;
- người bàn giao/người nhận;
- tình trạng thực tế khi thu hồi;
- trạng thái phiếu thu hồi;
- quy trình xác nhận hai bên;
- xử lý sau thu hồi: tái sử dụng, sửa chữa, thanh lý, lưu kho.

Kết luận: hiện có thao tác thu hồi kỹ thuật ở mức cập nhật trạng thái thiết bị, chưa đủ luồng bàn giao/kiểm soát.

## 7. Điều chuyển

Hiện trạng: có một phần.

Đã có:

- `LoaiPhieu = LUAN_CHUYEN` trong `ThietBiBusiness.TaoPhieuThietBiAsync`.
- Khi luân chuyển, hệ thống cập nhật `PhongBanId`, `BoPhanId`, `NguoiSuDungId`.
- Có lịch sử trước/sau trong `LichSuThietBi`.
- API thống kê/tìm kiếm theo phòng ban, bộ phận, người sử dụng.

Chưa thấy:

- bên giao/bên nhận;
- xác nhận của các bên;
- trạng thái phiếu điều chuyển;
- thời điểm hiệu lực so với thời điểm lập phiếu;
- biểu mẫu điều chuyển/in biên bản;
- phân biệt điều chuyển và thu hồi rồi cấp phát.

Kết luận: có lõi cập nhật vị trí sử dụng, nhưng chưa có quy trình xác nhận điều chuyển.

## 8. Nghiệm thu hoàn thành

Hiện trạng: có một phần rất mỏng.

Đã có:

- `PhieuThietBi.KetLuanId` tham chiếu logic tới danh mục `KET_LUAN`.
- `PhieuThietBiChiTiet.NgayBatDau`, `NgayKetThuc`, `NoiDung`, `ChiPhi`, `GiaTri`.
- File scan có thể đính kèm qua `FileScan01`, `FileScan02` hoặc bảng `TepDinhKem`.

Chưa thấy:

- phiếu nghiệm thu riêng;
- trạng thái nghiệm thu;
- danh sách bên tham gia nghiệm thu;
- người nghiệm thu/người đề nghị/người duyệt;
- dự toán và chi phí thực tế tách bạch;
- kết quả sau xử lý cập nhật trạng thái thiết bị về hoạt động bình thường;
- API/màn hình nghiệm thu.

Kết luận: dữ liệu kết luận và chi tiết công việc có thể tái sử dụng, nhưng quy trình nghiệm thu hoàn thành chưa có.

## 9. Kết luận hiện trạng

Hệ thống hiện tại có nền tảng tốt để mở rộng:

- hồ sơ thiết bị;
- phiếu nghiệp vụ dùng chung;
- chi tiết phiếu;
- lịch sử thiết bị;
- danh mục dùng chung;
- file đính kèm;
- phòng ban/bộ phận/người sử dụng;
- trạng thái thiết bị.

Nhưng các yêu cầu mới của BGD thiên về quy trình, phê duyệt, trạng thái phiếu, nghiệm thu, quan hệ thay thế và xử lý tài sản/bộ phận cũ. Đây là các phần hiện chưa được mô hình hóa đầy đủ.
