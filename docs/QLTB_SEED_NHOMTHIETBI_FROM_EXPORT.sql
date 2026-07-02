-- Converted from docs/export.sql table QLTB_LOC.WTB_924_DM_NHOM_THIET_BI.
-- Keep MaNhomThietBi as the old system equipment-group code for later data migration mapping.
-- Old MA_NHOM_DOI_TUONG values are created as parent rows with MaNhomThietBi = NDT_<old code>.
SET NOCOUNT ON;
IF OBJECT_ID('tempdb..#NhomThietBiChaCu') IS NOT NULL
    DROP TABLE #NhomThietBiChaCu;
CREATE TABLE #NhomThietBiChaCu (
    MaNhomThietBi NVARCHAR(50) NOT NULL,
    TenNhomThietBi NVARCHAR(250) NOT NULL,
    KyHieu NVARCHAR(20) NULL,
    MoTa NVARCHAR(1000) NULL,
    SapXep INT NULL
);
INSERT INTO #NhomThietBiChaCu (MaNhomThietBi, TenNhomThietBi, KyHieu, MoTa, SapXep)
VALUES
    (N'NDT_0001', N'Thiết bị Điện', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 1),
    (N'NDT_0002', N'Thiết bị Điện tử', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 2),
    (N'NDT_0003', N'Đồ vật, dụng cụ', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 3),
    (N'NDT_0004', N'Nhà cửa - Kiến trúc', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 4),
    (N'NDT_0006', N'Máy móc và thiết bị Thi công', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 6),
    (N'NDT_0007', N'Đồng hồ điện từ', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 7),
    (N'NDT_0008', N'Khác', NULL, N'Parent group converted from old MA_NHOM_DOI_TUONG.', 8);
MERGE dbo.NhomThietBi AS target
USING #NhomThietBiChaCu AS source
ON target.MaNhomThietBi = source.MaNhomThietBi
WHEN MATCHED THEN
    UPDATE SET
        TenNhomThietBi = source.TenNhomThietBi,
        KyHieu = source.KyHieu,
        ParentId = NULL,
        MoTa = source.MoTa,
        SapXep = source.SapXep,
        IsActive = 1,
        NgayChinhSuaCuoiCung = SYSDATETIME(),
        MaNguoiChinhSua = N'0044',
        TenNguoiChinhSua = N'Trương Văn Voôn'
WHEN NOT MATCHED THEN
    INSERT (Id, MaNhomThietBi, TenNhomThietBi, KyHieu, ParentId, MoTa, SapXep, IsActive, NgayKhoiTao, MaNguoiNhap, TenNguoiNhap)
    VALUES (NEWID(), source.MaNhomThietBi, source.TenNhomThietBi, source.KyHieu, NULL, source.MoTa, source.SapXep, 1, SYSDATETIME(), N'0044', N'Trương Văn Voôn');
IF OBJECT_ID('tempdb..#NhomThietBiCu') IS NOT NULL
    DROP TABLE #NhomThietBiCu;
CREATE TABLE #NhomThietBiCu (
    MaNhomThietBi NVARCHAR(50) NOT NULL,
    TenNhomThietBi NVARCHAR(250) NOT NULL,
    KyHieu NVARCHAR(20) NULL,
    MaNhomThietBiCha NVARCHAR(50) NULL,
    MoTa NVARCHAR(1000) NULL,
    SapXep INT NULL
);
INSERT INTO #NhomThietBiCu (MaNhomThietBi, TenNhomThietBi, KyHieu, MaNhomThietBiCha, MoTa, SapXep)
VALUES
    (N'0001', N'Ghế', N'GHE', N'NDT_0003', NULL, 1),
    (N'0002', N'Bàn', N'BAN', N'NDT_0003', NULL, 2),
    (N'0003', N'Cửa', N'CUA', N'NDT_0003', NULL, 3),
    (N'0004', N'Tủ', N'TUX', N'NDT_0003', NULL, 4),
    (N'0005', N'Quạt', N'QUA', N'NDT_0001', NULL, 5),
    (N'0006', N'Điều hòa', N'DHO', N'NDT_0001', NULL, 6),
    (N'0007', N'Cân', N'CAN', N'NDT_0003', N'Old note: 123', 7),
    (N'0009', N'Bếp', N'BEP', N'NDT_0001', NULL, 9),
    (N'0010', N'Cây', N'CAY', N'NDT_0008', N'Old note: 123', 10),
    (N'0011', N'Máy Scan', N'SCN', N'NDT_0002', N'Old note: 123', 11),
    (N'0012', N'Âm ly', N'ALY', N'NDT_0001', NULL, 12),
    (N'0013', N'Bể điều nhiệt', N'BDN', N'NDT_0003', NULL, 13),
    (N'0014', N'Máy khuấy', N'MKU', N'NDT_0001', NULL, 14),
    (N'0015', N'Máy Jatest', N'MJA', N'NDT_0001', NULL, 15),
    (N'0016', N'Bảng', N'BNG', N'NDT_0003', NULL, 16),
    (N'0017', N'Bình chữa cháy', N'BCC', N'NDT_0003', NULL, 17),
    (N'0018', N'Bình nóng lạnh', N'BNN', N'NDT_0001', NULL, 18),
    (N'0019', N'Bình dưỡng khí', N'BDK', N'NDT_0003', NULL, 19),
    (N'0020', N'Thùng', N'THU', N'NDT_0003', NULL, 20),
    (N'0021', N'Bộ lọc', N'BNO', N'NDT_0003', NULL, 21),
    (N'0022', N'Chai', N'CHA', N'NDT_0003', NULL, 22),
    (N'0023', N'Bộ sạc', N'BSA', N'NDT_0001', NULL, 23),
    (N'0024', N'Bộ hiển thị', N'BHT', N'NDT_0007', NULL, 24),
    (N'0025', N'Máy hàn', N'MHA', N'NDT_0006', NULL, 25),
    (N'0026', N'Loa', N'LOA', N'NDT_0001', NULL, 26),
    (N'0027', N'Két', N'KET', N'NDT_0003', NULL, 27),
    (N'0028', N'Salong', N'SAL', N'NDT_0003', NULL, 28),
    (N'0029', N'Thang', N'THA', N'NDT_0003', NULL, 29),
    (N'0030', N'Lavabo', N'LVB', N'NDT_0004', NULL, 30),
    (N'0031', N'Xi e', N'XIE', N'NDT_0004', NULL, 31),
    (N'0032', N'Đế', N'DES', N'NDT_0003', NULL, 32),
    (N'0033', N'Nồi', N'NOI', N'NDT_0003', NULL, 33),
    (N'0035', N'Sensor', N'SEN', N'NDT_0002', NULL, 35),
    (N'0036', N'Buret', N'BUR', N'NDT_0002', NULL, 36),
    (N'0037', N'Palang', N'PAL', N'NDT_0006', NULL, 37),
    (N'0038', N'Khuôn', N'KHU', N'NDT_0003', NULL, 38),
    (N'0039', N'Trục', N'TRU', N'NDT_0006', NULL, 39),
    (N'0040', N'Hộp lồng', N'HOL', N'NDT_0003', NULL, 40),
    (N'0041', N'Giỏ', N'GIO', N'NDT_0003', NULL, 41),
    (N'0042', N'Chổi', N'CHO', N'NDT_0003', NULL, 42),
    (N'0043', N'Máy cưa', N'MCU', N'NDT_0001', NULL, 43),
    (N'0044', N'Máy hủy giấy', N'MHG', N'NDT_0001', NULL, 44),
    (N'0045', N'Máy chấm công', N'MCC', N'NDT_0002', NULL, 45),
    (N'0046', N'Camera', N'CAM', N'NDT_0002', NULL, 46),
    (N'0047', N'Máy chiếu', N'MCH', N'NDT_0002', NULL, 47),
    (N'0048', N'Màn chiếu', N'MHC', N'NDT_0003', NULL, 48),
    (N'0049', N'Vách ngăn', N'VCN', N'NDT_0003', NULL, 49),
    (N'0051', N'Động cơ', N'DCO', N'NDT_0006', NULL, 51),
    (N'0052', N'Router wifi', N'ROW', N'NDT_0002', NULL, 52),
    (N'0053', N'Thuyền', N'CNO', N'NDT_0003', NULL, 53),
    (N'0054', N'Khởi động mềm', N'KHD', N'NDT_0001', NULL, 54),
    (N'0055', N'Giá', N'GDC', N'NDT_0003', NULL, 55),
    (N'0056', N'Bộ Gateway', N'BGT', N'NDT_0007', NULL, 56),
    (N'0057', N'Cello', N'CEL', N'NDT_0007', NULL, 57),
    (N'0058', N'Bộ truyền dữ liệu', N'DTL', N'NDT_0007', NULL, 58),
    (N'0059', N'Pin', N'PIN', N'NDT_0003', NULL, 59),
    (N'0060', N'Phần mềm ứng dung', N'PME', N'NDT_0008', NULL, 60),
    (N'0061', N'Đầu phun', N'DPH', N'NDT_0003', NULL, 61),
    (N'0062', N'Nhà', N'NDB', N'NDT_0004', NULL, 62),
    (N'0063', N'Hệ thống Scada', N'SCD', N'NDT_0002', NULL, 63),
    (N'0064', N'Tivi', N'TVI', N'NDT_0002', NULL, 64),
    (N'0065', N'Khóa', N'KHO', N'NDT_0003', NULL, 65),
    (N'0066', N'Nhiệt kế', N'NKE', N'NDT_0003', NULL, 66),
    (N'0067', N'Nhiệt ẩm kế', N'NAK', N'NDT_0003', NULL, 67),
    (N'0068', N'Cáp', N'CAP', N'NDT_0003', NULL, 68),
    (N'0069', N'Đèn', N'DEN', N'NDT_0003', NULL, 69),
    (N'0070', N'Bục', N'BUC', N'NDT_0003', NULL, 70),
    (N'0071', N'Bồn', N'BON', N'NDT_0003', NULL, 71),
    (N'0072', N'Kệ', N'KEX', N'NDT_0003', NULL, 72),
    (N'0073', N'Tủ lạnh', N'TUL', N'NDT_0001', NULL, 73),
    (N'0074', N'Khung', N'KHN', N'NDT_0003', NULL, 74),
    (N'0075', N'Handheld', N'HHE', N'NDT_0002', NULL, 75),
    (N'0076', N'Đồng hồ', N'DOH', N'NDT_0003', NULL, 76),
    (N'0077', N'Máy bơm', N'BOM', N'NDT_0006', NULL, 77),
    (N'0078', N'Bộ Máy vi tính', N'MTI', N'NDT_0002', NULL, 78),
    (N'0079', N'Thiết bị cảm biến', N'TCB', N'NDT_0002', NULL, 79),
    (N'0080', N'Thiết bị mạng', N'TBM', N'NDT_0002', NULL, 80),
    (N'0081', N'Đồng hồ điện từ', N'DOT', N'NDT_0007', NULL, 81),
    (N'0082', N'Máy sưởi', N'MAY', N'NDT_0001', NULL, 82),
    (N'0083', N'Ống nghiệm', N'ONH', N'NDT_0003', NULL, 83),
    (N'0084', N'Thùng rác', N'THR', N'NDT_0003', NULL, 84),
    (N'0085', N'Nhân Test', N'NHA', N'NDT_0003', NULL, 85),
    (N'0086', N'Giá treo', N'GTD', N'NDT_0001', N'Old note: Thiết bị điện', 86),
    (N'0087', N'Màn chiếu', N'MCD', N'NDT_0001', N'Old note: Màn chiếu điện', 87),
    (N'0088', N'Pin', N'PDH', N'NDT_0007', N'Old note: Pin đồng hồ điện từ', 88),
    (N'0089', N'Máy biến tần', N'MBT', N'NDT_0006', NULL, 89),
    (N'0090', N'Máy biến áp', N'MBA', N'NDT_0001', NULL, 90),
    (N'0091', N'Máy biến tần', N'MTB', N'NDT_0001', NULL, 91),
    (N'0092', N'Máy đục', N'MDU', N'NDT_0006', NULL, 92),
    (N'0093', N'Máy In', N'MIN', N'NDT_0002', NULL, 93),
    (N'0094', N'Màn Hình', N'MHI', N'NDT_0002', NULL, 94),
    (N'0095', N'Tai nghe', N'TNG', N'NDT_0002', NULL, 95),
    (N'0096', N'Bộ lưu điện', N'UPS', N'NDT_0001', NULL, 96),
    (N'0097', N'Máy tời', N'TOI', N'NDT_0006', NULL, 97),
    (N'0098', N'Máy phát điện', N'MPD', N'NDT_0006', NULL, 98),
    (N'0099', N'Máy cắt', N'MCA', N'NDT_0006', NULL, 99),
    (N'0100', N'Máy siết', N'MSI', N'NDT_0006', NULL, 100),
    (N'0101', N'Cờ lê', N'CLE', N'NDT_0006', NULL, 101),
    (N'0102', N'Điện thoại', N'DIT', N'NDT_0002', NULL, 102),
    (N'0103', N'Bộ điều khiển', N'BKH', N'NDT_0002', NULL, 103),
    (N'0104', N'Cửa', N'CNH', N'NDT_0004', N'Old note: Cửa nhà', 104),
    (N'0105', N'Bộ đàm', N'BDA', N'NDT_0002', NULL, 105),
    (N'0106', N'Rìu', N'RIU', N'NDT_0003', NULL, 106),
    (N'0107', N'Khác', N'KHA', N'NDT_0003', NULL, 107),
    (N'0108', N'Máy Hút bụi', N'MHB', N'NDT_0001', NULL, 108),
    (N'0109', N'Bình Acquy', N'ACQ', N'NDT_0001', NULL, 109),
    (N'0110', N'Thiết bị lưu trữ', N'TBL', N'NDT_0002', NULL, 110),
    (N'0111', N'Thùng rác', N'TRA', N'NDT_0008', NULL, 111),
    (N'0112', N'Hệ thống thiết bị', N'HTH', N'NDT_0002', NULL, 112),
    (N'0113', N'Trụ', N'TRJ', N'NDT_0003', NULL, 113),
    (N'0114', N'Phần mềm ứng dụng', N'PHM', N'NDT_0002', NULL, 114),
    (N'0115', N'Máy photocopy', N'PTO', N'NDT_0002', NULL, 115),
    (N'0116', N'Bộ nguồn cấp điện', N'NGU', N'NDT_0001', NULL, 116);
MERGE dbo.NhomThietBi AS target
USING (
    SELECT
        source.MaNhomThietBi,
        source.TenNhomThietBi,
        source.KyHieu,
        parent.Id AS ParentId,
        source.MoTa,
        source.SapXep
    FROM #NhomThietBiCu source
    LEFT JOIN dbo.NhomThietBi parent ON parent.MaNhomThietBi = source.MaNhomThietBiCha
) AS source
ON target.MaNhomThietBi = source.MaNhomThietBi
WHEN MATCHED THEN
    UPDATE SET
        TenNhomThietBi = source.TenNhomThietBi,
        KyHieu = source.KyHieu,
        ParentId = source.ParentId,
        MoTa = source.MoTa,
        SapXep = source.SapXep,
        IsActive = 1,
        NgayChinhSuaCuoiCung = SYSDATETIME(),
        MaNguoiChinhSua = N'0044',
        TenNguoiChinhSua = N'Trương Văn Voôn'
WHEN NOT MATCHED THEN
    INSERT (Id, MaNhomThietBi, TenNhomThietBi, KyHieu, ParentId, MoTa, SapXep, IsActive, NgayKhoiTao, MaNguoiNhap, TenNguoiNhap)
    VALUES (NEWID(), source.MaNhomThietBi, source.TenNhomThietBi, source.KyHieu, source.ParentId, source.MoTa, source.SapXep, 1, SYSDATETIME(), N'0044', N'Trương Văn Voôn');

UPDATE child
SET child.ParentId = parent.Id
FROM dbo.NhomThietBi child
JOIN #NhomThietBiCu source ON source.MaNhomThietBi = child.MaNhomThietBi
LEFT JOIN dbo.NhomThietBi parent ON parent.MaNhomThietBi = source.MaNhomThietBiCha;
DROP TABLE #NhomThietBiCu;
DROP TABLE #NhomThietBiChaCu;
GO
