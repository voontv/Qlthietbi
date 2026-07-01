using System;

namespace QlThietBi.DTO.Response
{
    public class TepDinhKemDto
    {
        public Guid Id { get; set; }
        public string DoiTuongLoai { get; set; } = null!;
        public Guid DoiTuongId { get; set; }
        public string TenFile { get; set; } = null!;
        public string DuongDan { get; set; } = null!;
        public string? LoaiFile { get; set; }
        public long? DungLuong { get; set; }
        public string? GhiChu { get; set; }
    }
}