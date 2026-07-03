using System;

namespace QlThietBi.DTO.Request
{
    public class CreateUpdateNguoiSuDungThietBiRequest
    {
        public int? Id { get; set; }
        public string MaNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public int? DonViBoPhanId { get; set; }
        public string? ChucVu { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? GhiChu { get; set; }
        public bool IsActive { get; set; } = true;
    }
}


