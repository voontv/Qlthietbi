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
        Task<bool> XoaDanhMucDungChungAsync(Guid id);
        Task<IEnumerable<NhomThietBiDto>> LayDanhSachNhomThietBiAsync();
        Task<NhomThietBiDto> LuuNhomThietBiAsync(CreateUpdateNhomThietBiRequest request);
        Task<bool> XoaNhomThietBiAsync(Guid id);
        Task<IEnumerable<ThietBiDto>> LayDanhSachThietBiAsync();
        Task<ThietBiDto> LuuThietBiAsync(CreateUpdateThietBiRequest request);
        Task<ThietBiDto?> LayThietBiTheoIdAsync(Guid id);
        Task<bool> XoaThietBiAsync(Guid id);
        Task<IEnumerable<DmThongSoThietBiDto>> LayThongSoTheoNhomThietBiAsync(Guid nhomThietBiId);
        Task<DmThongSoThietBiDto> LuuThongSoThietBiAsync(CreateUpdateDmThongSoThietBiRequest request);
        Task<bool> XoaThongSoThietBiAsync(Guid id);
        Task<PhieuThietBiDto> TaoPhieuThietBiAsync(CreatePhieuThietBiRequest request);
        Task<IEnumerable<LichSuThietBiDto>> LayLichSuThietBiAsync(Guid thietBiId);
    }
}
