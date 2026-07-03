using System;

namespace QlThietBi.Models
{
    public class LichSuThietBi
    {
        public int Id { get; set; }
        public int ThietBiId { get; set; }
        public string LoaiNghiepVu { get; set; } = null!;
        public int? NghiepVuId { get; set; }
        public int? TrangThaiTruocId { get; set; }
        public int? TrangThaiSauId { get; set; }
        public int? PhongBanTruocId { get; set; }
        public int? PhongBanSauId { get; set; }
        public int? BoPhanTruocId { get; set; }
        public int? BoPhanSauId { get; set; }
        public int? NguoiSuDungTruocId { get; set; }
        public int? NguoiSuDungSauId { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayPhatSinh { get; set; }
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
    }
}


