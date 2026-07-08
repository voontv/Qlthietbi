using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlThietBi.Businesses.DonViBoPhan;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;

namespace QlThietBi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonViBoPhanController : ControllerBase
    {
        private readonly IDonViBoPhanBusiness business;

        public DonViBoPhanController(IDonViBoPhanBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Lấy danh sách đơn vị / bộ phận đang kích hoạt.
        /// </summary>
        /// <response code="200">Danh sách đơn vị / bộ phận trả về.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DonViBoPhanDto>), 200)]
        public async Task<ActionResult<IEnumerable<DonViBoPhanDto>>> GetUnits()
        {
            var result = await business.GetUnitsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách bộ phận con theo mã phòng ban cha.
        /// </summary>
        /// <param name="maPhongBan">Mã phòng ban cha, là đơn vị có ParentId null.</param>
        /// <response code="200">Danh sách bộ phận con. Nếu phòng ban không có bộ phận thì trả mảng rỗng.</response>
        [HttpGet("phong-ban/{maPhongBan}/bo-phan")]
        [ProducesResponseType(typeof(IEnumerable<DonViBoPhanDto>), 200)]
        public async Task<ActionResult<IEnumerable<DonViBoPhanDto>>> GetPartsByDepartmentCode(string maPhongBan)
        {
            var result = await business.GetPartsByDepartmentCodeAsync(maPhongBan);
            return Ok(result);
        }

        /// <summary>
        /// Lấy chi tiết đơn vị / bộ phận theo Id.
        /// </summary>
        /// <param name="id">Id của đơn vị / bộ phận.</param>
        /// <response code="200">Thông tin đơn vị / bộ phận.</response>
        /// <response code="404">Không tìm thấy đơn vị / bộ phận.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DonViBoPhanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DonViBoPhanDto>> GetUnit(int id)
        {
            var result = await business.GetUnitByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật đơn vị / bộ phận.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "maDonVi": "DV001",
        ///   "tenDonVi": "Phòng Kỹ thuật",
        ///   "parentId": "int?",
        ///   "loaiDonVi": "PHONG_BAN",
        ///   "ghiChu": "",
        ///   "sapXep": 1,
        ///   "isActive": true
        /// }
        /// </remarks>
        /// <response code="200">Đơn vị / bộ phận đã được lưu.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(DonViBoPhanDto), 200)]
        public async Task<ActionResult<DonViBoPhanDto>> SaveUnit(CreateUpdateDonViBoPhanRequest request)
        {
            var result = await business.SaveUnitAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa mềm đơn vị / bộ phận.
        /// </summary>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy đơn vị / bộ phận.</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteUnit(int id)
        {
            var removed = await business.DeleteUnitAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


