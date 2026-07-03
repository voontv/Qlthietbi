using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QlThietBi.Businesses.NguoiSuDungThietBi
{
    [ImplementBy(typeof(NguoiSuDungThietBiBusiness))]
    public interface INguoiSuDungThietBiBusiness
    {
        Task<IEnumerable<NguoiSuDungThietBiDto>> GetUsersAsync();
        Task<NguoiSuDungThietBiDto?> GetUserByIdAsync(int id);
        Task<NguoiSuDungThietBiDto> SaveUserAsync(CreateUpdateNguoiSuDungThietBiRequest request);
        Task<bool> DeleteUserAsync(int id);
    }
}


