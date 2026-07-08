using System;
using System.Collections.Generic;

namespace QlThietBi.DTO.Response
{
    public class ThongKeThietBiNhomDto
    {
        public int? Id { get; set; }
        public string? Ma { get; set; }
        public string Ten { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal TongNguyenGia { get; set; }
    }

    public class ThongKeThietBiItemDto
    {
        public int Id { get; set; }
        public string MaThietBi { get; set; } = null!;
        public string? MaThietBiCu { get; set; }
        public string TenThietBi { get; set; } = null!;
        public int NhomThietBiId { get; set; }
        public string? MaNhomThietBi { get; set; }
        public string? TenNhomThietBi { get; set; }
        public int? DanhMucThietBiId { get; set; }
        public string? MaDanhMucThietBi { get; set; }
        public string? TenDanhMucThietBi { get; set; }
        public int TrangThaiId { get; set; }
        public string? MaTrangThai { get; set; }
        public string? TenTrangThai { get; set; }
        public int? PhongBanId { get; set; }
        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }
        public int? BoPhanId { get; set; }
        public string? MaBoPhan { get; set; }
        public string? TenBoPhan { get; set; }
        public int? NguoiSuDungId { get; set; }
        public string? MaNguoiSuDung { get; set; }
        public string? TenNguoiSuDung { get; set; }
        public decimal? NguyenGia { get; set; }
        public DateTime? NgayNhapThietBi { get; set; }
        public DateTime? NgayDuaVaoSuDung { get; set; }
        public string? GhiChu { get; set; }
    }

    public class ThongKeThietBiDto
    {
        public string? MaThietBi { get; set; }
        public int? PhongBanId { get; set; }
        public int? BoPhanId { get; set; }
        public int? NhomThietBiId { get; set; }
        public int? NguoiSuDungId { get; set; }
        public string? NguoiSuDung { get; set; }
        public int? TrangThaiId { get; set; }
        public DateTime? NgayNhapTu { get; set; }
        public DateTime? NgayNhapDen { get; set; }
        public DateTime? NgayDuaVaoSuDungTu { get; set; }
        public DateTime? NgayDuaVaoSuDungDen { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongNguyenGia { get; set; }
        public IEnumerable<ThongKeThietBiNhomDto> TheoPhongBan { get; set; } = new List<ThongKeThietBiNhomDto>();
        public IEnumerable<ThongKeThietBiNhomDto> TheoBoPhan { get; set; } = new List<ThongKeThietBiNhomDto>();
        public IEnumerable<ThongKeThietBiNhomDto> TheoNhomThietBi { get; set; } = new List<ThongKeThietBiNhomDto>();
        public IEnumerable<ThongKeThietBiNhomDto> TheoTrangThai { get; set; } = new List<ThongKeThietBiNhomDto>();
        public IEnumerable<ThongKeThietBiItemDto> DanhSach { get; set; } = new List<ThongKeThietBiItemDto>();
    }
}


