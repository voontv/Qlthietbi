using System;
using System.Collections.Generic;

namespace QlThietBi.DTO.Request
{
    public class ThietBiThongSoValueRequest
    {
        public Guid ThongSoId { get; set; }
        public string? GiaTriText { get; set; }
        public decimal? GiaTriNumber { get; set; }
        public DateTime? GiaTriDate { get; set; }
        public bool? GiaTriBit { get; set; }
        public string? GhiChu { get; set; }
    }

    public class CreateUpdateThietBiRequest
    {
        public Guid? Id { get; set; }
        public string MaThietBi { get; set; } = null!;
        public string? MaThietBiCu { get; set; }
        public string TenThietBi { get; set; } = null!;
        public string? SoSerial { get; set; }
        public string? Model { get; set; }
        public string? MaKeToan { get; set; }
        public string? MaThietBiCha { get; set; }
        public Guid NhomThietBiId { get; set; }
        public Guid TrangThaiId { get; set; }
        public Guid? TrangThaiKiemKeId { get; set; }
        public Guid? DonViTinhId { get; set; }
        public Guid? NhanHieuId { get; set; }
        public Guid? MauSacId { get; set; }
        public Guid? NuocSanXuatId { get; set; }
        public Guid? ChatLieuId { get; set; }
        public Guid? DonViCungCapId { get; set; }
        public Guid? PhongBanId { get; set; }
        public Guid? BoPhanId { get; set; }
        public Guid? NguoiSuDungId { get; set; }
        public DateTime? NgayMua { get; set; }
        public DateTime? NgayNhapThietBi { get; set; }
        public DateTime? NgayDuaVaoSuDung { get; set; }
        public decimal? NguyenGia { get; set; }
        public int? ThoiGianBaoHanh { get; set; }
        public DateTime? NgayHetBaoHanh { get; set; }
        public string? MaQrCode { get; set; }
        public string? ViTriLapDat { get; set; }
        public string? GhiChu { get; set; }
        public bool IsActive { get; set; } = true;
        public IEnumerable<ThietBiThongSoValueRequest>? ThongSo { get; set; }
    }
}