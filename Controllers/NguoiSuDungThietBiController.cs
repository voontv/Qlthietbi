using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QlThietBi.Businesses.NguoiSuDungThietBi;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;

namespace QlThietBi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NguoiSuDungThietBiController : ControllerBase
    {
        private readonly INguoiSuDungThietBiBusiness business;

        public NguoiSuDungThietBiController(INguoiSuDungThietBiBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Lấy danh sách người sử dụng thiết bị.
        /// </summary>
        /// <response code="200">Danh sách người sử dụng trả về.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NguoiSuDungThietBiDto>), 200)]
        public async Task<ActionResult<IEnumerable<NguoiSuDungThietBiDto>>> GetUsers()
        {
            var result = await business.GetUsersAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy người sử dụng theo Id.
        /// </summary>
        /// <param name="id">Id của người sử dụng.</param>
        /// <response code="200">Thông tin người sử dụng.</response>
        /// <response code="404">Không tìm thấy người sử dụng.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NguoiSuDungThietBiDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<NguoiSuDungThietBiDto>> GetUser(int id)
        {
            var result = await business.GetUserByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật người sử dụng thiết bị.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "maNguoiDung": "ND001",
        ///   "tenNguoiDung": "Nguyễn Văn A",
        ///   "donViBoPhanId": "int?",
        ///   "chucVu": "Kỹ sư",
        ///   "soDienThoai": "0123456789",
        ///   "email": "a@example.com",
        ///   "ghiChu": "",
        ///   "isActive": true
        /// }
        /// </remarks>
        /// <response code="200">Người sử dụng được lưu.</response>
        [HttpPost]
        [ProducesResponseType(typeof(NguoiSuDungThietBiDto), 200)]
        public async Task<ActionResult<NguoiSuDungThietBiDto>> SaveUser(CreateUpdateNguoiSuDungThietBiRequest request)
        {
            var result = await business.SaveUserAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa mềm người sử dụng thiết bị.
        /// </summary>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy người sử dụng.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var removed = await business.DeleteUserAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


