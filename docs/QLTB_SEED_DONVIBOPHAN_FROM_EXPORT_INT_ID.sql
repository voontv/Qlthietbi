-- Converted from docs/export.sql table QLTB_LOC.WTB_927_DM_BO_PHAN.
-- Keep MaDonVi as the old system code for later data migration mapping.
SET NOCOUNT ON;
IF OBJECT_ID('tempdb..#DonViBoPhanCu') IS NOT NULL
    DROP TABLE #DonViBoPhanCu;
CREATE TABLE #DonViBoPhanCu (
    MaDonVi NVARCHAR(50) NOT NULL,
    TenDonVi NVARCHAR(250) NOT NULL,
    MaDonViParent NVARCHAR(50) NULL,
    GhiChu NVARCHAR(500) NULL,
    SapXep INT NULL
);
INSERT INTO #DonViBoPhanCu (MaDonVi, TenDonVi, MaDonViParent, GhiChu, SapXep)
VALUES
    (N'000', N'Công ty CP Cấp nước Đà nẵng', N'0001', NULL, 0),
    (N'003', N'Xí Nghiệp Cấp nước Sơn Trà', N'0016', NULL, 1),
    (N'004', N'Xí Nghiệp Cấp nước Hải Châu', N'0013', NULL, 2),
    (N'005', N'Xí Nghiệp Cấp nước Ngũ Hành Sơn', N'0015', NULL, 3),
    (N'006', N'Xí Nghiệp Cấp nước Liên Chiểu', N'0014', NULL, 4),
    (N'007', N'Xí Nghiệp Cấp nước Thanh Khê', N'0017', NULL, 5),
    (N'008', N'Xí Nghiệp Cấp nước Cẩm Lệ', N'0012', NULL, 6),
    (N'009', N'Ban Hành chính Nhân sự', N'0004', NULL, 7),
    (N'010', N'Ban Tài chính-Kế toán', N'0007', NULL, 8),
    (N'011', N'Ban Kế hoạch-Kỹ thuật', N'0006', NULL, 9),
    (N'012', N'Ban KD-QHKH', NULL, NULL, 10),
    (N'014', N'Ban Truyền thông', N'0008', NULL, 11),
    (N'015', N'Trung Tâm Lab', N'0018', NULL, 12),
    (N'016', N'Trung Tâm Điều Khiển', N'0009', NULL, 13),
    (N'017', N'Khe Lạnh', N'0014', NULL, 14),
    (N'018', N'Nhà Máy nước Cầu Đỏ', N'0020', NULL, 15),
    (N'019', N'Nhà Máy nước Sân Bay', N'0021', NULL, 16),
    (N'020', N'Nhà Máy nước Phú Sơn', N'0023', NULL, 17),
    (N'021', N'Nhà Máy nước Hải Vân', N'0025', NULL, 18),
    (N'022', N'Nhà Máy nước Hòa Trung', N'0019', NULL, 19),
    (N'023', N'Nhà Máy nước Sơn Trà', N'0024', NULL, 20),
    (N'024', N'Trạm An Trạch', N'0022', NULL, 21),
    (N'025', N'Công ty Dawacon', N'0544', NULL, 22),
    (N'026', N'Kho Xuân Thiều', N'0014', NULL, 23),
    (N'027', N'Phòng Giám đốc', N'0014', NULL, 24),
    (N'028', N'Phòng giao dịch', N'0014', NULL, 25),
    (N'029', N'Phòng phó Giám đốc', N'0014', NULL, 26),
    (N'030', N'Phòng Kỹ thuật', N'0014', NULL, 27),
    (N'031', N'Kho', N'0014', NULL, 28),
    (N'032', N'Phòng Giám đốc', N'0015', NULL, 29),
    (N'033', N'Phòng Kỹ thuật', N'0015', NULL, 30),
    (N'034', N'Phòng giao dịch', N'0015', NULL, 31),
    (N'035', N'Phòng Giám đốc', N'0013', NULL, 32),
    (N'036', N'Phòng phó giám đốc', N'0013', NULL, 33),
    (N'037', N'Phòng Kỹ thuật', N'0013', NULL, 34),
    (N'038', N'Phòng giao dịch', N'0013', NULL, 35),
    (N'039', N'Kho', N'0013', NULL, 36),
    (N'040', N'Kho', N'0015', NULL, 37),
    (N'041', N'Phòng Giám đốc', N'0016', NULL, 38),
    (N'042', N'Phòng Kỹ thuật', N'0016', NULL, 39),
    (N'043', N'Phòng giao dịch', N'0016', NULL, 40),
    (N'044', N'Kho', N'0016', NULL, 41),
    (N'045', N'Phòng Giám đốc', N'0017', NULL, 42),
    (N'046', N'Phòng phó giám đốc', N'0017', NULL, 43),
    (N'047', N'Phòng Kỹ thuật', N'0017', NULL, 44),
    (N'048', N'Phòng giao dịch', N'0017', NULL, 45),
    (N'049', N'Kho', N'0017', NULL, 46),
    (N'050', N'Phòng Giám đốc', N'0012', NULL, 47),
    (N'051', N'Phòng phó giám đốc', N'0012', NULL, 48),
    (N'052', N'Phòng Kỹ thuật', N'0012', NULL, 49),
    (N'053', N'Phòng giao dịch', N'0012', NULL, 50),
    (N'054', N'Kho', N'0012', NULL, 51),
    (N'055', N'Tổ Nghiệp vụ', N'0014', NULL, 52),
    (N'056', N'Nhóm Chăm sóc', N'0017', NULL, 53),
    (N'057', N'Phòng CT HĐQT', N'0002', NULL, 54),
    (N'058', N'Phòng Phó CT HĐQT', N'0002', NULL, 55),
    (N'059', N'Phòng Tổng Giám đốc', N'0002', NULL, 56),
    (N'060', N'Phòng Phó Tổng Giám đốc (A.Quý)', N'0002', NULL, 57),
    (N'061', N'Phòng Phó Tổng Giám đốc (A.Thương)', N'0002', NULL, 58),
    (N'062', N'Phòng Phó Tổng Giám đốc (A.Nam)', N'0002', NULL, 59),
    (N'063', N'Ban Kiểm soát', N'0003', NULL, 60),
    (N'064', N'Phòng TV HĐQT', N'0002', NULL, 61),
    (N'065', N'Phòng Nghiệp vụ', N'0004', NULL, 62),
    (N'067', N'Phòng Văn thư', N'0004', NULL, 64),
    (N'068', N'Phòng Họp', N'0004', NULL, 65),
    (N'069', N'Phòng Khánh tiết', N'0004', NULL, 66),
    (N'070', N'Hội Trường', N'0004', NULL, 67),
    (N'071', N'Phòng Máy chủ', N'0004', NULL, 68),
    (N'072', N'Tổ Kỹ thuật', N'0006', NULL, 69),
    (N'073', N'Tổ Dự án', N'0006', NULL, 70),
    (N'074', N'Tổ Kế hoạch', N'0006', NULL, 71),
    (N'075', N'TT Tư vấn', N'0006', NULL, 72),
    (N'076', N'Tổ Đầu tư', N'0006', NULL, 73),
    (N'077', N'Tổ Vật tư', N'0006', NULL, 74),
    (N'078', N'Tổ Nghiệp vụ', N'0008', NULL, 75),
    (N'079', N'Tổ Giám sát', N'0008', NULL, 76),
    (N'080', N'Call Center', N'0008', NULL, 77),
    (N'081', N'Tổ Bảo vệ', N'0004', NULL, 78),
    (N'082', N'Phòng Giám đốc', N'0008', NULL, 79),
    (N'083', N'Phòng Hành chính', N'0021', NULL, 80),
    (N'084', N'Phòng Thí nghiệm', N'0021', NULL, 81),
    (N'085', N'Trạm cấp 2 mới', N'0021', NULL, 82),
    (N'086', N'Trạm cấp 2 cũ', N'0021', NULL, 83),
    (N'087', N'Nhà kho', N'0021', NULL, 84),
    (N'088', N'Cụm xử lý', N'0021', NULL, 85),
    (N'089', N'Nhà hóa chất', N'0021', NULL, 86),
    (N'090', N'Phòng họp', N'0021', NULL, 87),
    (N'091', N'Phòng Quản đốc', N'0019', NULL, 88),
    (N'092', N'TT Điều khiển', N'0019', NULL, 89),
    (N'093', N'Phòng thí nghiệm', N'0019', NULL, 90),
    (N'094', N'Phòng tổng hợp', N'0019', NULL, 91),
    (N'095', N'Phòng khác', N'0019', NULL, 92),
    (N'096', N'Hội trường', N'0017', NULL, 93),
    (N'097', N'Phòng Biên đọc', N'0017', NULL, 94),
    (N'099', N'Phòng họp', N'0015', NULL, 96),
    (N'100', N'Phòng khác', N'0015', NULL, 97),
    (N'101', N'Phòng biên đọc', N'0012', NULL, 98),
    (N'102', N'Phòng họp', N'0012', NULL, 99),
    (N'103', N'Phòng thi công', N'0013', NULL, 100),
    (N'104', N'Phòng biên đọc', N'0013', NULL, 101),
    (N'105', N'Phòng họp', N'0013', NULL, 102),
    (N'106', N'Phòng bảo vệ', N'0013', NULL, 103),
    (N'107', N'Phòng Nghiệp vụ', N'0007', NULL, 104),
    (N'108', N'Phòng Giám đốc', N'0007', NULL, 105),
    (N'109', N'Phòng Hồ sơ', N'0007', NULL, 106),
    (N'110', N'Trạm bơm 1', N'0020', NULL, 107),
    (N'111', N'Phòng Phó Giám đốc', N'0020', NULL, 108),
    (N'112', N'Phòng QL chất lượng nước', N'0020', NULL, 109),
    (N'113', N'Phòng thí nghiệm', N'0020', NULL, 110),
    (N'114', N'Phòng kỹ thuật', N'0020', NULL, 111),
    (N'115', N'Phòng khác', N'0020', NULL, 112),
    (N'116', N'Phòng Hành chính', N'0020', NULL, 113),
    (N'117', N'Nhà Hóa chất', N'0020', NULL, 114),
    (N'118', N'Nhà Clo', N'0020', NULL, 115),
    (N'119', N'Tổ sản xuất', N'0020', NULL, 116),
    (N'120', N'Trạm bơm 2', N'0020', NULL, 117),
    (N'121', N'Kho', N'0020', NULL, 118),
    (N'122', N'Phòng giám đốc', N'0020', NULL, 119),
    (N'123', N'CallCenter', N'0008', NULL, 120),
    (N'124', N'Xí Nghiệp Xây Lắp', N'0544', NULL, 121),
    (N'125', N'Biên đọc', N'0015', NULL, 122),
    (N'126', N'Biên đọc', N'0014', NULL, 123),
    (N'127', N'Biên đọc', N'0012', NULL, 124),
    (N'128', N'Biên đọc', N'0016', NULL, 125),
    (N'129', N'Biên đọc', N'0017', NULL, 126),
    (N'130', N'Biên đọc', N'0013', NULL, 127),
    (N'131', N'Kho vật tư', N'0011', NULL, 128),
    (N'132', N'Tổ Hạ tầng', N'0005', NULL, 129),
    (N'133', N'Phòng Phó Tổng Giám đốc (A. Thịnh)', N'0002', NULL, 131),
    (N'134', N'Tổ phần mềm', N'0005', NULL, 132);
MERGE dbo.DonViBoPhan AS target
USING #DonViBoPhanCu AS source
ON target.MaDonVi = source.MaDonVi
WHEN MATCHED THEN
    UPDATE SET
        TenDonVi = source.TenDonVi,
        GhiChu = source.GhiChu,
        SapXep = source.SapXep,
        LoaiDonVi = CASE WHEN source.TenDonVi LIKE N'%Kho%' THEN N'KHO' ELSE N'BO_PHAN' END,
        IsActive = 1,
        NgayChinhSuaCuoiCung = SYSDATETIME(),
        MaNguoiChinhSua = N'0044',
        TenNguoiChinhSua = N'Trương Văn Voôn'
WHEN NOT MATCHED THEN
    INSERT (MaDonVi, TenDonVi, ParentId, LoaiDonVi, GhiChu, SapXep, IsActive, NgayKhoiTao, MaNguoiNhap, TenNguoiNhap)
    VALUES (source.MaDonVi, source.TenDonVi, NULL, CASE WHEN source.TenDonVi LIKE N'%Kho%' THEN N'KHO' ELSE N'BO_PHAN' END, source.GhiChu, source.SapXep, 1, SYSDATETIME(), N'0044', N'Trương Văn Voôn');

INSERT INTO dbo.DonViBoPhan (MaDonVi, TenDonVi, ParentId, LoaiDonVi, GhiChu, SapXep, IsActive, NgayKhoiTao, MaNguoiNhap, TenNguoiNhap)
SELECT missing.MaDonViParent,
    N'Don vi cha cu ' + missing.MaDonViParent,
    NULL,
    N'DON_VI_CHA_CU',
    N'Placeholder created from old MA_DON_VI_PARENT for migration mapping.',
    NULL,
    1,
    SYSDATETIME(),
    N'0044',
    N'Trương Văn Voôn'
FROM (
    SELECT DISTINCT source.MaDonViParent
    FROM #DonViBoPhanCu source
    WHERE source.MaDonViParent IS NOT NULL
) missing
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.DonViBoPhan existing
    WHERE existing.MaDonVi = missing.MaDonViParent
        OR existing.MaDonVi = RIGHT(missing.MaDonViParent, 3)
);

UPDATE child
SET child.ParentId = parent.Id
FROM dbo.DonViBoPhan child
JOIN #DonViBoPhanCu source ON source.MaDonVi = child.MaDonVi
LEFT JOIN dbo.DonViBoPhan parent ON parent.MaDonVi = source.MaDonViParent
    OR parent.MaDonVi = RIGHT(source.MaDonViParent, 3)
WHERE source.MaDonViParent IS NOT NULL;
UPDATE child
SET child.ParentId = NULL
FROM dbo.DonViBoPhan child
JOIN #DonViBoPhanCu source ON source.MaDonVi = child.MaDonVi
WHERE source.MaDonViParent IS NULL;
DROP TABLE #DonViBoPhanCu;
GO
