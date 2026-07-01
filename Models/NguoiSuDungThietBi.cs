using System;

namespace QlThietBi.Models
{
    public class NguoiSuDungThietBi
    {
        public Guid Id { get; set; }
        public string MaNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public Guid? DonViBoPhanId { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
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
