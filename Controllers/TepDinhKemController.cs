using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlThietBi.Businesses.TepDinhKem;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;

namespace QlThietBi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TepDinhKemController : ControllerBase
    {
        private readonly ITepDinhKemBusiness business;

        public TepDinhKemController(ITepDinhKemBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Lấy danh sách tệp đính kèm theo đối tượng.
        /// </summary>
        /// <param name="doiTuongLoai">Loại đối tượng, ví dụ: ThietBi.</param>
        /// <param name="doiTuongId">Id đối tượng cần lấy tệp đính kèm.</param>
        /// <response code="200">Danh sách tệp đính kèm trả về.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TepDinhKemDto>), 200)]
        public async Task<ActionResult<IEnumerable<TepDinhKemDto>>> GetAttachments([FromQuery] string doiTuongLoai, [FromQuery] int doiTuongId)
        {
            var result = await business.GetAttachmentsAsync(doiTuongLoai, doiTuongId);
            return Ok(result);
        }

        /// <summary>
        /// Lấy tệp đính kèm theo Id.
        /// </summary>
        /// <param name="id">Id tệp đính kèm.</param>
        /// <response code="200">Thông tin tệp đính kèm.</response>
        /// <response code="404">Không tìm thấy tệp đính kèm.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TepDinhKemDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TepDinhKemDto>> GetAttachment(int id)
        {
            var result = await business.GetAttachmentByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật tệp đính kèm.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "doiTuongLoai": "ThietBi",
        ///   "doiTuongId": "int",
        ///   "tenFile": "file.pdf",
        ///   "duongDan": "/uploads/file.pdf",
        ///   "loaiFile": "application/pdf",
        ///   "dungLuong": 12345,
        ///   "ghiChu": ""
        /// }
        /// </remarks>
        /// <response code="200">Tệp đính kèm được lưu và trả về dữ liệu.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(TepDinhKemDto), 200)]
        public async Task<ActionResult<TepDinhKemDto>> SaveAttachment(CreateUpdateTepDinhKemRequest request)
        {
            var result = await business.SaveAttachmentAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa tệp đính kèm.
        /// </summary>
        /// <param name="id">Id tệp đính kèm cần xóa.</param>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy tệp đính kèm.</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAttachment(int id)
        {
            var removed = await business.DeleteAttachmentAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


