using System;

namespace QlThietBi.DTO.Response
{
    public class ThietBiThongSoDto
    {
        public Guid Id { get; set; }
        public Guid ThietBiId { get; set; }
        public Guid ThongSoId { get; set; }
        public string? GiaTriText { get; set; }
        public decimal? GiaTriNumber { get; set; }
        public DateTime? GiaTriDate { get; set; }
        public bool? GiaTriBit { get; set; }
        public string? GhiChu { get; set; }
    }
}