using System;
using Microsoft.AspNetCore.Mvc;

namespace QlThietBi.DTO.Request
{
    public class QueryThongKeThietBiRequest
    {
        [FromQuery(Name = "ma_thiet_bi")]
        public string? MaThietBi { get; set; }

        [FromQuery(Name = "phong_ban_id")]
        public int? PhongBanId { get; set; }

        [FromQuery(Name = "bo_phan_id")]
        public int? BoPhanId { get; set; }

        [FromQuery(Name = "nhom_thiet_bi_id")]
        public int? NhomThietBiId { get; set; }

        [FromQuery(Name = "nguoi_su_dung_id")]
        public int? NguoiSuDungId { get; set; }

        [FromQuery(Name = "nguoi_su_dung")]
        public string? NguoiSuDung { get; set; }

        [FromQuery(Name = "trang_thai_id")]
        public int? TrangThaiId { get; set; }

        [FromQuery(Name = "tinh_trang_thiet_bi_id")]
        public int? TinhTrangThietBiId { get; set; }

        [FromQuery(Name = "ngay_nhap_tu")]
        public DateTime? NgayNhapTu { get; set; }

        [FromQuery(Name = "ngay_nhap_den")]
        public DateTime? NgayNhapDen { get; set; }

        [FromQuery(Name = "ngay_dua_vao_su_dung_tu")]
        public DateTime? NgayDuaVaoSuDungTu { get; set; }

        [FromQuery(Name = "ngay_dua_vao_su_dung_den")]
        public DateTime? NgayDuaVaoSuDungDen { get; set; }
    }
}


