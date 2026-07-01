using System;

namespace QlThietBi.Models
{
    public class PhieuThietBiChiTiet
    {
        public Guid Id { get; set; }
        public Guid PhieuThietBiId { get; set; }
        public Guid? CongViecId { get; set; }
        public Guid? ThongSoId { get; set; }
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
