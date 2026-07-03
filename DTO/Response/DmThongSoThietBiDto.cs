using System;

namespace QlThietBi.DTO.Response
{
    public class DmThongSoThietBiDto
    {
        public int Id { get; set; }
        public int NhomThietBiId { get; set; }
        public string MaThongSo { get; set; } = null!;
        public string TenThongSo { get; set; } = null!;
        public string KieuDuLieu { get; set; } = null!;
        public int? DonViTinhId { get; set; }
        public bool BatBuoc { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; }
    }
}

