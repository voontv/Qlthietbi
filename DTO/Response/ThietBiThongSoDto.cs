using System;

namespace QlThietBi.DTO.Response
{
    public class ThietBiThongSoDto
    {
        public int Id { get; set; }
        public int ThietBiId { get; set; }
        public int ThongSoId { get; set; }
        public string? GiaTriText { get; set; }
        public decimal? GiaTriNumber { get; set; }
        public DateTime? GiaTriDate { get; set; }
        public bool? GiaTriBit { get; set; }
        public string? GhiChu { get; set; }
    }
}

