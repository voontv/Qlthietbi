using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QlThietBi.Businesses.DonViBoPhan
{
    [ImplementBy(typeof(DonViBoPhanBusiness))]
    public interface IDonViBoPhanBusiness
    {
        Task<IEnumerable<DonViBoPhanDto>> GetUnitsAsync();
        Task<DonViBoPhanDto?> GetUnitByIdAsync(Guid id);
        Task<DonViBoPhanDto> SaveUnitAsync(CreateUpdateDonViBoPhanRequest request);
        Task<bool> DeleteUnitAsync(Guid id);
    }
}
