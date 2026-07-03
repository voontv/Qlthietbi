using System;

namespace QlThietBi.Models
{
    public class DonViBoPhan
    {
        public int Id { get; set; }
        public string MaDonVi { get; set; } = null!;
        public string TenDonVi { get; set; } = null!;
        public int? ParentId { get; set; }
        public string? LoaiDonVi { get; set; }
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


