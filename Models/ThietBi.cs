using System;

namespace QlThietBi.Models
{
    public class ThietBi
    {
        public int Id { get; set; }
        public string MaThietBi { get; set; } = null!;
        public string? MaThietBiCu { get; set; }
        public string TenThietBi { get; set; } = null!;
        public string? SoSerial { get; set; }
        public string? Model { get; set; }
        public string? MaKeToan { get; set; }
        public string? MaThietBiCha { get; set; }
        public int NhomThietBiId { get; set; }
        public int TrangThaiId { get; set; }
        public int? TrangThaiKiemKeId { get; set; }
        public int? DonViTinhId { get; set; }
        public int? NhanHieuId { get; set; }
        public int? MauSacId { get; set; }
        public int? NuocSanXuatId { get; set; }
        public int? ChatLieuId { get; set; }
        public int? DonViCungCapId { get; set; }
        public int? PhongBanId { get; set; }
        public int? BoPhanId { get; set; }
        public int? NguoiSuDungId { get; set; }
        public DateTime? NgayMua { get; set; }
        public DateTime? NgayNhapThietBi { get; set; }
        public DateTime? NgayDuaVaoSuDung { get; set; }
        public decimal? NguyenGia { get; set; }
        public int? ThoiGianBaoHanh { get; set; }
        public DateTime? NgayHetBaoHanh { get; set; }
        public string? MaQrCode { get; set; }
        public string? ViTriLapDat { get; set; }
        public string? GhiChu { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
        public DateTime? NgayChinhSuaCuoiCung { get; set; }
        public string? MaNguoiChinhSua { get; set; }
        public string? TenNguoiChinhSua { get; set; }
    }
}


