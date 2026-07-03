using System;

namespace QlThietBi.DTO.Request
{
    public class CreateUpdateDonViBoPhanRequest
    {
        public int? Id { get; set; }
        public string MaDonVi { get; set; } = null!;
        public string TenDonVi { get; set; } = null!;
        public int? ParentId { get; set; }
        public string? LoaiDonVi { get; set; }
        public string? GhiChu { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; } = true;
    }
}


