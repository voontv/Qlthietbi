using System;

namespace QlThietBi.DTO.Request
{
    public class CreateUpdateNhomThietBiRequest
    {
        public Guid? Id { get; set; }
        public string MaNhomThietBi { get; set; } = null!;
        public string TenNhomThietBi { get; set; } = null!;
        public string? KyHieu { get; set; }
        public Guid? ParentId { get; set; }
        public string? MoTa { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; } = true;
    }
}