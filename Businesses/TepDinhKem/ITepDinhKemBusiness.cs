using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QlThietBi.Businesses.TepDinhKem
{
    [ImplementBy(typeof(TepDinhKemBusiness))]
    public interface ITepDinhKemBusiness
    {
        Task<IEnumerable<TepDinhKemDto>> GetAttachmentsAsync(string doiTuongLoai, int doiTuongId);
        Task<TepDinhKemDto?> GetAttachmentByIdAsync(int id);
        Task<TepDinhKemDto> SaveAttachmentAsync(CreateUpdateTepDinhKemRequest request);
        Task<bool> DeleteAttachmentAsync(int id);
    }
}


