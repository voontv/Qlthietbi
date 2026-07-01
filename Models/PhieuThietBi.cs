using System;

namespace QlThietBi.Models
{
    public class PhieuThietBi
    {
        public Guid Id { get; set; }
        public string SoPhieu { get; set; } = null!;
        public string LoaiPhieu { get; set; } = null!;
        public DateTime NgayPhieu { get; set; }
        public Guid ThietBiId { get; set; }
        public Guid? PhongBanId { get; set; }
        public Guid? BoPhanId { get; set; }
        public Guid? NguoiSuDungId { get; set; }
        public Guid? DonViThucHienId { get; set; }
        public Guid? KetLuanId { get; set; }
        public string? NoiDung { get; set; }
        public decimal? ChiPhi { get; set; }
        public string? FileScan01 { get; set; }
        public string? FileScan02 { get; set; }
        public string? GhiChu { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
        public DateTime? NgayChinhSuaCuoiCung { get; set; }
        public string? MaNguoiChinhSua { get; set; }
        public string? TenNguoiChinhSua { get; set; }

        public ICollection<PhieuThietBiChiTiet> ChiTiets { get; set; } = new List<PhieuThietBiChiTiet>();
    }
}
