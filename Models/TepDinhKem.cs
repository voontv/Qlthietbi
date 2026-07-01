using System;

namespace QlThietBi.Models
{
    public class TepDinhKem
    {
        public Guid Id { get; set; }
        public string DoiTuongLoai { get; set; } = null!;
        public Guid DoiTuongId { get; set; }
        public string TenFile { get; set; } = null!;
        public string DuongDan { get; set; } = null!;
        public string? LoaiFile { get; set; }
        public long? DungLuong { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayKhoiTao { get; set; } = DateTime.UtcNow;
        public string? MaNguoiNhap { get; set; }
        public string? TenNguoiNhap { get; set; }
    }
}
