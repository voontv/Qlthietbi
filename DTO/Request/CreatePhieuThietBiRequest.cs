using System;
using System.Collections.Generic;

namespace QlThietBi.DTO.Request
{
    public class CreatePhieuThietBiChiTietRequest
    {
        public int? CongViecId { get; set; }
        public int? ThongSoId { get; set; }
        public string? NoiDung { get; set; }
        public string? GiaTri { get; set; }
        public decimal? ChiPhi { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? GhiChu { get; set; }
    }

    public class CreatePhieuThietBiRequest
    {
        public string SoPhieu { get; set; } = null!;
        public string LoaiPhieu { get; set; } = null!;
        public DateTime NgayPhieu { get; set; }
        public int ThietBiId { get; set; }
        public int? PhongBanId { get; set; }
        public int? BoPhanId { get; set; }
        public int? NguoiSuDungId { get; set; }
        public int? DonViThucHienId { get; set; }
        public int? KetLuanId { get; set; }
        public string? NoiDung { get; set; }
        public decimal? ChiPhi { get; set; }
        public string? FileScan01 { get; set; }
        public string? FileScan02 { get; set; }
        public string? GhiChu { get; set; }
        public IEnumerable<CreatePhieuThietBiChiTietRequest>? ChiTiets { get; set; }
    }
}

