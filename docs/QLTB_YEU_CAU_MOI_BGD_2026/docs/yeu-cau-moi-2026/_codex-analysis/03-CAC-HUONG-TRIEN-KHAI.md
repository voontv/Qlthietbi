# 03 - Các hướng triển khai

Ngày lập: 2026-07-08

## Nguyên tắc nền cho cả 3 hướng

- `PhieuThietBi` là phiếu nghiệp vụ của một thiết bị duy nhất.
- Không thêm danh sách nhiều thiết bị vào `PhieuThietBi`.
- `PhieuThietBiChiTiet` chỉ là các dòng công việc/chi phí/thời gian/thông số chi tiết của thiết bị trên phiếu.
- Thiết bị cha/con là quan hệ giữa các bản ghi `ThietBi`, không phải quan hệ giữa các dòng chi tiết phiếu.
- Báo cáo nhiều thiết bị hoặc nhiều phiếu là lớp tổng hợp riêng, không làm thay đổi nguyên tắc một phiếu - một thiết bị.

## Hướng 1 - Mở rộng phiếu hiện có

### Cách làm

Tiếp tục dùng `PhieuThietBi` và `PhieuThietBiChiTiet` làm bảng nghiệp vụ trung tâm theo nguyên tắc một phiếu xử lý một thiết bị. Bổ sung loại phiếu/trạng thái/field cần thiết để bao phủ các nghiệp vụ:

- báo hỏng;
- kiểm tra hiện trạng;
- sửa chữa;
- bảo trì/bảo dưỡng;
- thay thế;
- thu hồi;
- điều chuyển;
- nghiệm thu.

Nếu cần báo cáo nhiều thiết bị, backend tổng hợp từ nhiều `PhieuThietBi` thay vì nhét nhiều thiết bị vào một phiếu.

### Ưu điểm

- Ít thay đổi kiến trúc hiện tại.
- Tái sử dụng `TaoPhieuThietBiAsync`, `LichSuThietBi`, danh mục, file đính kèm.
- Dữ liệu hiện có ít bị ảnh hưởng.
- FE có thể mở rộng từ màn hình phiếu chung.

### Nhược điểm

- `PhieuThietBi` có thể trở thành bảng quá rộng nếu thêm nhiều field riêng cho từng quy trình.
- Khó biểu diễn quan hệ phức tạp như báo hỏng -> kiểm tra -> sửa chữa -> nghiệm thu nếu chỉ dùng một bảng phiếu.
- Báo cáo trình Ban Giám đốc nếu cần lưu số tờ trình/trạng thái trình/file trình sẽ không đủ chỉ bằng query trực tiếp.

### Ảnh hưởng dữ liệu

Thấp đến trung bình. Có thể bổ sung danh mục loại phiếu/trạng thái phiếu, thêm một số cột hoặc bảng phụ nhỏ. Không cần thay đổi bản chất `PhieuThietBi.ThietBiId`.

### Ảnh hưởng backend

Trung bình. Cần mở rộng `CreatePhieuThietBiRequest`, `PhieuThietBiDto`, `ThietBiBusiness.TaoPhieuThietBiAsync`, thêm API truy vấn theo loại phiếu/trạng thái.

### Ảnh hưởng frontend

Trung bình. Có thể làm một màn hình phiếu nghiệp vụ dùng chung, form thay đổi theo loại phiếu.

### Rủi ro

Trung bình. Rủi ro chính là phiếu chung bị quá tải và khó bảo trì nếu quy trình BGD phức tạp.

## Hướng 2 - Dùng phiếu hiện có làm hạ tầng, thêm bảng quy trình phụ

### Cách làm

Giữ `PhieuThietBi` làm phiếu nghiệp vụ gốc cho một thiết bị. Với nghiệp vụ phức tạp, thêm bảng phụ theo ngữ nghĩa:

- bảng/bước báo hỏng hoặc mở rộng phiếu báo hỏng;
- bảng quan hệ phiếu nguồn - phiếu xử lý;
- bảng thay thế thiết bị/linh kiện;
- bảng nghiệm thu;
- bảng trạng thái/quy trình duyệt phiếu;
- bảng kế hoạch bảo trì/bảo dưỡng định kỳ nếu cần.

Các bảng phụ tham chiếu `PhieuThietBi.Id` hoặc `ThietBi.Id`, không thay đổi cách quản lý hồ sơ thiết bị hiện tại.

Với thiết bị cha/con, giữ theo DB thật: `ThietBi.MaThietBi` là mã thiết bị dạng `nvarchar(50)` và `ThietBi.MaThietBiCha` là mã thiết bị cha dạng `nvarchar(50)`. `Id` vẫn là khóa kỹ thuật để truy cập/join nhanh; không đổi `MaThietBi` thành primary key và không đổi `MaThietBiCha` sang khóa ngoại int.

Với báo cáo Ban Giám đốc:

- báo cáo tra cứu có thể tổng hợp trực tiếp từ `PhieuThietBi`, `ThietBi`, `LichSuThietBi`;
- hồ sơ/tờ trình chính thức nên có thêm bảng tổng hợp, ví dụ đợt/tờ trình/báo cáo và bảng chi tiết gom các `PhieuThietBi`.

### Ưu điểm

- Cân bằng giữa tái sử dụng và mô hình hóa đúng nghiệp vụ.
- Giữ ổn định phần convert cũ.
- Không phá nguyên tắc một phiếu - một thiết bị.
- Các nghiệp vụ phức tạp có chỗ lưu dữ liệu riêng, dễ báo cáo.
- Có thể triển khai từng phần theo ưu tiên.

### Nhược điểm

- Cần thiết kế thêm vài bảng/API.
- FE phải hiểu quan hệ giữa phiếu gốc và các phần mở rộng.
- Cần chốt rõ quy trình trước khi code để tránh tạo bảng thừa.

### Ảnh hưởng dữ liệu

Trung bình. Thêm bảng phụ nhưng ít động vào bảng cũ. Dữ liệu cũ vẫn giữ nguyên. Quan hệ cha/con tiếp tục đi theo `MaThietBiCha`, nên không phát sinh mapping từ mã thiết bị sang khóa ngoại int.

### Ảnh hưởng backend

Trung bình đến cao. Cần thêm business/service cho quy trình mới, nhưng vẫn tái sử dụng `ThietBi`, `PhieuThietBi`, `LichSuThietBi`, `TepDinhKem`.

### Ảnh hưởng frontend

Trung bình đến cao. Cần màn hình quy trình: danh sách phiếu, chi tiết phiếu, xử lý/duyệt/nghiệm thu, thay thế, báo cáo tổng hợp.

### Rủi ro

Trung bình. Rủi ro thấp hơn hướng 3 vì không viết lại module, nhưng cao hơn hướng 1 do có thêm bảng/quy trình.

## Hướng 3 - Tạo module quy trình riêng hoàn toàn

### Cách làm

Tạo module riêng cho bảo trì/sửa chữa/thay thế/thu hồi/điều chuyển/nghiệm thu. Module này có bảng quy trình, bảng bước duyệt, bảng chi tiết riêng, rồi đồng bộ kết quả về `ThietBi` và `LichSuThietBi`.

Dù làm module riêng, vẫn nên giữ nguyên tắc: một hồ sơ xử lý nghiệp vụ cụ thể chỉ gắn một thiết bị. Báo cáo nhiều thiết bị là lớp tổng hợp phía trên.

### Ưu điểm

- Mô hình hóa quy trình đầy đủ và sạch nếu BGD yêu cầu luồng phức tạp.
- Dễ thêm workflow nhiều bước, phân quyền duyệt, SLA, thông báo.
- Tách biệt nghiệp vụ mới với phần convert cũ.

### Nhược điểm

- Khối lượng lớn.
- Có nguy cơ trùng dữ liệu với `PhieuThietBi`.
- Cần nhiều thời gian phân tích, code, test.
- FE phải làm nhiều màn hình mới.

### Ảnh hưởng dữ liệu

Cao. Nhiều bảng mới, cần quy tắc đồng bộ với thiết bị hiện tại.

### Ảnh hưởng backend

Cao. Cần module mới, business mới, API mới, có thể cần phân quyền/quy trình duyệt.

### Ảnh hưởng frontend

Cao. Cần thiết kế trải nghiệm quy trình đầy đủ.

### Rủi ro

Cao. Dễ vượt phạm vi, ảnh hưởng tiến độ và có thể làm phức tạp hệ thống.

## So sánh nhanh

| Hướng | Phù hợp khi | Ưu tiên |
| --- | --- | --- |
| Hướng 1 - Mở rộng phiếu hiện có | BGD chỉ cần ghi nhận phiếu, trạng thái đơn giản, ít duyệt, báo cáo chỉ là query nhanh | Trung bình |
| Hướng 2 - Phiếu hiện có + bảng phụ | Cần quy trình rõ, báo cáo/tờ trình tổng hợp, nhưng vẫn muốn giữ nền hiện tại | Cao |
| Hướng 3 - Module riêng | Cần workflow phức tạp, nhiều vai trò, nhiều cấp duyệt, SLA, tách module hẳn | Thấp ở giai đoạn đầu |

## Nhận định riêng về báo cáo Ban Giám đốc

Không nên biến `PhieuThietBi` thành phiếu nhiều thiết bị để phục vụ báo cáo. Nếu BGD cần một biểu mẫu có nhiều thiết bị, nên xem đó là báo cáo tổng hợp.

Nếu chỉ xem/lọc/in nhanh: tổng hợp trực tiếp từ `PhieuThietBi` là đủ.

Nếu cần lưu vết một lần trình ký chính thức: nên thêm khái niệm đợt/tờ trình/báo cáo tổng hợp, có bảng chi tiết liên kết nhiều `PhieuThietBi`.
