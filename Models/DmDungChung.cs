using System;

namespace QlThietBi.Models
{
    public class DmDungChung
    {
        public int Id { get; set; }
        public string NhomDanhMuc { get; set; } = null!;
        public string Ma { get; set; } = null!;
        public string Ten { get; set; } = null!;
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


