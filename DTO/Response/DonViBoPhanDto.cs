using System;

namespace QlThietBi.DTO.Response
{
    public class DonViBoPhanDto
    {
        public Guid Id { get; set; }
        public string MaDonVi { get; set; } = null!;
        public string TenDonVi { get; set; } = null!;
        public Guid? ParentId { get; set; }
        public string? LoaiDonVi { get; set; }
        public string? GhiChu { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; }
    }
}
