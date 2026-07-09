# PROMPT TRAO ĐỔI VỚI CODEX

Đọc các tài liệu:

1. `docs/yeu-cau-moi-2026/00-READ-ME-FIRST.md`
2. `docs/yeu-cau-moi-2026/02-PHAM-VI-NGHIEN-CUU.md`
3. `docs/yeu-cau-moi-2026/tai-lieu-goc/01-YEU-CAU-BAN-GIAM-DOC.pdf`

Sau đó đọc:

- các tài liệu hiện có trong `docs` liên quan hệ thống Quản lý thiết bị;
- code backend hiện tại;
- code frontend hiện tại;
- database schema/entity hiện tại.

Bối cảnh bắt buộc:

- hệ thống cũ đã convert xong;
- tất cả version trước hiện đang được xem là ổn;
- không đánh giá lại hoặc làm lại phần cũ;
- chỉ nghiên cứu yêu cầu mới của Ban Giám đốc và cách bổ sung vào hệ thống hiện tại.

## Vòng này chỉ nghiên cứu và trao đổi

Chưa sửa code.
Chưa sửa database.
Chưa migration.
Chưa tạo bảng.
Chưa tạo API.

Hãy tạo các file sau trong:

`docs/yeu-cau-moi-2026/_codex-analysis/`

### 01-HIEN-TRANG-LIEN-QUAN.md

Chỉ ra hệ thống hiện tại đã có gì liên quan đến:

- bảo trì/bảo dưỡng;
- báo hỏng;
- sửa chữa;
- thay thế;
- thu hồi;
- điều chuyển;
- nghiệm thu.

Mỗi kết luận phải dẫn tới file code, entity, bảng, API hoặc màn hình cụ thể.

### 02-DOI-CHIEU-YEU-CAU-MOI.md

Lập bảng cho từng yêu cầu:

- yêu cầu từ tài liệu mới;
- hệ thống hiện tại đã có chưa;
- mức độ: Đã có / Có một phần / Chưa có / Cần mở rộng;
- phần cần bổ sung.

### 03-CAC-HUONG-TRIEN-KHAI.md

Đề xuất 2 hoặc 3 hướng triển khai nếu có nhiều lựa chọn.

Ví dụ:

- mở rộng phiếu hiện có;
- thêm loại phiếu mới nhưng dùng chung hạ tầng;
- thêm module quy trình riêng.

Với mỗi hướng phải có:

- cách làm;
- ưu điểm;
- nhược điểm;
- ảnh hưởng dữ liệu;
- ảnh hưởng backend;
- ảnh hưởng frontend;
- mức độ rủi ro.

Không mặc định hướng nào là đúng trước khi đối chiếu code hiện tại.

### 04-HUONG-DE-XUAT.md

Sau khi so sánh các phương án, đề xuất hướng phù hợp nhất với hệ thống hiện tại.

Phải giải thích:

- vì sao ít ảnh hưởng hệ thống cũ;
- phần nào tái sử dụng;
- phần nào thật sự phải bổ sung;
- thứ tự triển khai hợp lý.

### 05-CAU-HOI-CAN-CHOT.md

Chỉ ghi các câu hỏi thật sự cần Ban Giám đốc/người dùng chốt trước khi code.

Ưu tiên câu hỏi ảnh hưởng:

- quy trình;
- quyền duyệt;
- trạng thái;
- dữ liệu;
- biểu mẫu;
- kế toán;
- thu hồi tài sản cũ.

## Kết thúc

Sau khi tạo 5 file trên, dừng lại.

Hãy tóm tắt trong chat:

1. Hệ thống hiện tại đã đáp ứng được khoảng bao nhiêu phần yêu cầu mới.
2. Ba thay đổi lớn nhất.
3. Hướng triển khai Codex đề xuất.
4. Những câu hỏi cần tôi chốt.

Không viết code cho đến khi tôi trao đổi và xác nhận hướng làm.
