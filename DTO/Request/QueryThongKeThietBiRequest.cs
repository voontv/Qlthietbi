using System;
using Microsoft.AspNetCore.Mvc;

namespace QlThietBi.DTO.Request
{
    public class QueryThongKeThietBiRequest
    {
        [FromQuery(Name = "phong_ban_id")]
        public int? PhongBanId { get; set; }

        [FromQuery(Name = "bo_phan_id")]
        public int? BoPhanId { get; set; }

        [FromQuery(Name = "nhom_thiet_bi_id")]
        public int? NhomThietBiId { get; set; }
    }
}


