using System;

namespace QlThietBi.DTO.Response
{
    public class TepDinhKemDto
    {
        public int Id { get; set; }
        public string DoiTuongLoai { get; set; } = null!;
        public int DoiTuongId { get; set; }
        public string TenFile { get; set; } = null!;
        public string DuongDan { get; set; } = null!;
        public string? LoaiFile { get; set; }
        public long? DungLuong { get; set; }
        public string? GhiChu { get; set; }
    }
}

