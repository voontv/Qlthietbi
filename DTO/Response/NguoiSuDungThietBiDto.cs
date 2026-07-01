using System;

namespace QlThietBi.DTO.Response
{
    public class NguoiSuDungThietBiDto
    {
        public Guid Id { get; set; }
        public string MaNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public Guid? DonViBoPhanId { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? GhiChu { get; set; }
        public bool IsActive { get; set; }
    }
}
