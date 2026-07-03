using System;

namespace QlThietBi.Models
{
    public class DmThongSoThietBi
    {
        public int Id { get; set; }
        public int NhomThietBiId { get; set; }
        public string MaThongSo { get; set; } = null!;
        public string TenThongSo { get; set; } = null!;
        public string KieuDuLieu { get; set; } = null!;
        public int? DonViTinhId { get; set; }
        public bool BatBuoc { get; set; }
        public int? SapXep { get; set; }
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


