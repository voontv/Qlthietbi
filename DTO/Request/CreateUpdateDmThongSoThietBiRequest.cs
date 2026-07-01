using System;

namespace QlThietBi.DTO.Request
{
    public class CreateUpdateDmThongSoThietBiRequest
    {
        public Guid? Id { get; set; }
        public Guid NhomThietBiId { get; set; }
        public string MaThongSo { get; set; } = null!;
        public string TenThongSo { get; set; } = null!;
        public string KieuDuLieu { get; set; } = null!;
        public Guid? DonViTinhId { get; set; }
        public bool BatBuoc { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; } = true;
        public string? GhiChu { get; set; }
    }
}