using System;

namespace QlThietBi.Models
{
    public class PhieuThietBiChiTiet
    {
        public int Id { get; set; }
        public int PhieuThietBiId { get; set; }
        public int? CongViecId { get; set; }
        public int? ThongSoId { get; set; }
        public string? NoiDung { get; set; }
        public string? GiaTri { get; set; }
        public decimal? ChiPhi { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }

        public PhieuThietBi? PhieuThietBi { get; set; }
    }
}


