using System;

namespace QlThietBi.Models
{
    public class ThietBiThongSo
    {
        public int Id { get; set; }
        public int ThietBiId { get; set; }
        public int ThongSoId { get; set; }
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


