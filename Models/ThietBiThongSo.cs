using System;

namespace QlThietBi.Models
{
    public class ThietBiThongSo
    {
        public Guid Id { get; set; }
        public Guid ThietBiId { get; set; }
        public Guid ThongSoId { get; set; }
        public string? GiaTriText { get; set; }
        public decimal? GiaTriNumber { get; set; }
        public DateTime? GiaTriDate { get; set; }
        public bool? GiaTriBit { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
    }
}
