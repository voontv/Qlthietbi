using System;

namespace QlThietBi.Models
{
    public class LichSuThietBi
    {
        public Guid Id { get; set; }
        public Guid ThietBiId { get; set; }
        public string LoaiNghiepVu { get; set; } = null!;
        public Guid? NghiepVuId { get; set; }
        public Guid? TrangThaiTruocId { get; set; }
        public Guid? TrangThaiSauId { get; set; }
        public Guid? PhongBanTruocId { get; set; }
        public Guid? PhongBanSauId { get; set; }
        public Guid? BoPhanTruocId { get; set; }
        public Guid? BoPhanSauId { get; set; }
        public Guid? NguoiSuDungTruocId { get; set; }
        public Guid? NguoiSuDungSauId { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayPhatSinh { get; set; }
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
    }
}
