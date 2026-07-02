-- Bao cao rieng cho thiet bi chua co MA_NHOM_THIET_BI trong du lieu cu.
-- Day khong phai loi import: thiet bi duoc giu lai va gan tam vao nhom KHONG_XAC_DINH
-- de phuc vu quan ly tai san, sau nay nghiep vu co the phan loai lai.

SET NOCOUNT ON;

SELECT
    tb.MaThietBi,
    tb.TenThietBi,
    tb.MaThietBiCu,
    tb.MaKeToan,
    tb.NguyenGia,
    tb.NgayNhapThietBi,
    tb.NgayDuaVaoSuDung,
    pb.MaDonVi AS MaPhongBan,
    pb.TenDonVi AS TenPhongBan,
    bp.MaDonVi AS MaBoPhan,
    bp.TenDonVi AS TenBoPhan,
    nsd.MaNguoiDung,
    nsd.TenNguoiDung,
    tb.MaNguoiNhap,
    tb.NgayKhoiTao,
    tb.GhiChu
FROM dbo.ThietBi tb
INNER JOIN dbo.NhomThietBi ntb ON ntb.Id = tb.NhomThietBiId
LEFT JOIN dbo.DonViBoPhan pb ON pb.Id = tb.PhongBanId
LEFT JOIN dbo.DonViBoPhan bp ON bp.Id = tb.BoPhanId
LEFT JOIN dbo.NguoiSuDungThietBi nsd ON nsd.Id = tb.NguoiSuDungId
WHERE ntb.MaNhomThietBi = N'KHONG_XAC_DINH'
ORDER BY tb.MaThietBi;

SELECT COUNT(*) AS SoLuongThietBiNhomKhongXacDinh
FROM dbo.ThietBi tb
INNER JOIN dbo.NhomThietBi ntb ON ntb.Id = tb.NhomThietBiId
WHERE ntb.MaNhomThietBi = N'KHONG_XAC_DINH';
