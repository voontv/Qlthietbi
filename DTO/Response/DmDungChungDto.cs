using System;

namespace QlThietBi.DTO.Response
{
    public class DmDungChungDto
    {
        public int Id { get; set; }
        public string NhomDanhMuc { get; set; } = null!;
        public string Ma { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public string? GhiChu { get; set; }
        public int? SapXep { get; set; }
        public bool IsActive { get; set; }
    }
}


