# 05 - Câu hỏi cần chốt

Ngày lập: 2026-07-08

## 1. Quy trình tổng thể

1. Các nghiệp vụ mới có đi theo một luồng chung không: báo hỏng -> kiểm tra -> đề xuất -> duyệt -> thực hiện -> nghiệm thu?
2. Bảo trì và bảo dưỡng có tách thành hai loại nghiệp vụ riêng không?
3. Sửa chữa, bảo trì, bảo dưỡng, nâng cấp, cải tạo có dùng chung quy trình nghiệm thu không?
4. Điều chuyển và thu hồi có độc lập không, hay thu hồi là bước trước/sau của điều chuyển?

## 2. Báo hỏng

1. Ai được tạo phiếu báo hỏng: người sử dụng, phòng ban, bộ phận, quản trị thiết bị?
2. Báo hỏng có cần duyệt trước khi kiểm tra hiện trạng không?
3. Sau báo hỏng có những hướng xử lý nào: sửa chữa, thay thế, thu hồi, thanh lý, không xử lý?
4. Có cần liên kết bắt buộc giữa phiếu báo hỏng và phiếu xử lý sau đó không?

## 3. Phê duyệt

1. Những nghiệp vụ nào bắt buộc duyệt?
2. Có bao nhiêu cấp duyệt?
3. Vai trò duyệt lấy từ hệ thống token ngoài hay quản lý riêng trong hệ thống QLTB?
4. Khi bị từ chối duyệt thì phiếu quay về trạng thái nào?
5. Người duyệt có được sửa nội dung phiếu không hay chỉ duyệt/từ chối?

## 4. Trạng thái phiếu

1. Danh sách trạng thái phiếu cần có là gì?
2. Trạng thái phiếu có dùng chung cho mọi loại phiếu không?
3. Khi phiếu hoàn thành, trạng thái thiết bị đổi khi nào: lúc duyệt, lúc thực hiện, hay lúc nghiệm thu?
4. Có cho phép hủy phiếu đã duyệt chưa thực hiện không?

## 5. Bảo trì/bảo dưỡng định kỳ

1. Có cần quản lý kế hoạch định kỳ không?
2. Nếu có, chu kỳ theo ngày/tháng/quý/năm hay theo số giờ vận hành?
3. Có cần nhắc việc/quá hạn không?
4. Ai lập kế hoạch, ai duyệt kế hoạch?
5. Một kế hoạch có áp dụng cho nhiều thiết bị cùng lúc không?

## 6. Sửa chữa

1. Có cần dự toán sửa chữa trước khi thực hiện không?
2. Có cần ghi đơn vị sửa chữa bên ngoài không?
3. Chi phí cần tách dự toán và chi phí thực tế không?
4. Sau sửa chữa, trạng thái thiết bị tự quay về đang sử dụng/trong kho hay người nghiệm thu chọn?

## 7. Thay thế

1. Thay thế có hai loại riêng không: thay cả thiết bị và thay linh kiện/bộ phận?
2. Thiết bị mới thay vào có bắt buộc đã tồn tại trong `ThietBi` không?
3. Linh kiện thay thế có quản lý như một tài sản/thiết bị riêng không?
4. Phần cũ sau thay thế xử lý thế nào: thu hồi, nhập kho, tái sử dụng, sửa chữa, chờ thanh lý?
5. Có cần lưu cấu hình trước/sau khi thay linh kiện không?
6. Có cần in biên bản thay thế không?

## 8. Thu hồi

1. Thu hồi về đâu: kho, phòng ban quản lý, đơn vị kỹ thuật hay vị trí khác?
2. Có cần người giao/người nhận ký xác nhận không?
3. Có cần ghi tình trạng thiết bị khi thu hồi không?
4. Sau thu hồi, thiết bị mặc định trạng thái `TRONG_KHO` hay có nhiều trạng thái khác?
5. Thu hồi có thể phát sinh từ báo hỏng/thay thế/điều chuyển không?

## 9. Điều chuyển

1. Điều chuyển cần xác nhận của những bên nào?
2. Có ngày hiệu lực điều chuyển khác ngày lập phiếu không?
3. Có cần biên bản điều chuyển không?
4. Điều chuyển có cho phép đổi cả phòng ban, bộ phận và người sử dụng cùng lúc không?

## 10. Nghiệm thu hoàn thành

1. Nghiệm thu là một phiếu riêng hay là bước cuối của phiếu sửa chữa/bảo trì/thay thế?
2. Các bên tham gia nghiệm thu gồm những ai?
3. Có cần lưu dự toán, chi phí thực tế, kết luận, đánh giá chất lượng không?
4. Có cần file biên bản nghiệm thu không?
5. Sau nghiệm thu có tự cập nhật trạng thái thiết bị không?

## 11. Biểu mẫu và báo cáo

1. Cần in những mẫu nào?
2. Mẫu do BGD cung cấp cố định hay hệ thống tự thiết kế?
3. Cần xuất PDF, Excel hay chỉ in HTML?
4. Mỗi mẫu cần chữ ký của ai?

## 12. Dữ liệu kế toán/tài sản

1. Khi thay thế hoặc thu hồi, có cần cập nhật mã kế toán/nguyên giá/khấu hao không?
2. Thiết bị cũ sau thay thế có cần chuyển trạng thái chờ thanh lý không?
3. Có cần báo cáo riêng danh sách tài sản chờ xử lý sau thay thế/thu hồi không?
4. Có tích hợp sang hệ thống kế toán/tài sản khác không?
