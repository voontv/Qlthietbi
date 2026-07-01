using System;

namespace QlThietBi.Models
{
    public class NhomThietBi
    {
        public Guid Id { get; set; }
        public string MaNhomThietBi { get; set; } = null!;
        public string TenNhomThietBi { get; set; } = null!;
        public string? KyHieu { get; set; }
        public Guid? ParentId { get; set; }
        public string? MoTa { get; set; }
        public string? GhiChu { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
        public DateTime? NgayChinhSuaCuoiCung { get; set; }
        public string? MaNguoiChinhSua { get; set; }
        public string? TenNguoiChinhSua { get; set; }
    }
}
