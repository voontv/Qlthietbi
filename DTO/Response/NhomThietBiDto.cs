using System;

namespace QlThietBi.DTO.Response
{
    public class NhomThietBiDto
    {
        public Guid Id { get; set; }
        public string MaNhomThietBi { get; set; } = null!;
        public string TenNhomThietBi { get; set; } = null!;
        public string? KyHieu { get; set; }
        public Guid? ParentId { get; set; }
        public string? MoTa { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; }
    }
}
