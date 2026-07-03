using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;

namespace QlThietBi.Businesses.ThietBi
{
    [ImplementBy(typeof(ThietBiBusiness))]
    public interface IThietBiBusiness
    {
        Task<IEnumerable<DmDungChungDto>> LayDanhMucDungChungAsync(string nhomDanhMuc);
        Task<DmDungChungDto> LuuDanhMucDungChungAsync(CreateUpdateDmDungChungRequest request);
        Task<bool> XoaDanhMucDungChungAsync(int id);
        Task<IEnumerable<NhomThietBiDto>> LayDanhSachNhomThietBiAsync();
        Task<NhomThietBiDto> LuuNhomThietBiAsync(CreateUpdateNhomThietBiRequest request);
        Task<bool> XoaNhomThietBiAsync(int id);
        Task<IEnumerable<ThietBiDto>> LayDanhSachThietBiAsync();
        Task<ThongKeThietBiDto> ThongKeThietBiAsync(QueryThongKeThietBiRequest request);
        Task<ThietBiDto> LuuThietBiAsync(CreateUpdateThietBiRequest request);
        Task<ThietBiDto?> LayThietBiTheoIdAsync(int id);
        Task<bool> XoaThietBiAsync(int id);
        Task<IEnumerable<DmThongSoThietBiDto>> LayThongSoTheoNhomThietBiAsync(int nhomThietBiId);
        Task<DmThongSoThietBiDto> LuuThongSoThietBiAsync(CreateUpdateDmThongSoThietBiRequest request);
        Task<bool> XoaThongSoThietBiAsync(int id);
        Task<PhieuThietBiDto> TaoPhieuThietBiAsync(CreatePhieuThietBiRequest request);
        Task<IEnumerable<LichSuThietBiDto>> LayLichSuThietBiAsync(int thietBiId);
    }
}


