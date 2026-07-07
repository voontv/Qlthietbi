using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlThietBi.Businesses.Auth;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Providers;

namespace QlThietBi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness authBusiness;

        public AuthController(IAuthBusiness authBusiness)
        {
            this.authBusiness = authBusiness;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequest request)
        {
            var result = await authBusiness.LoginAsync(request);
            return Ok(result);
        }

        [HttpGet("check-token")]
        [Authorize]
        [ProducesResponseType(typeof(LoggedInUserDto), 200)]
        public ActionResult<LoggedInUserDto> CheckToken([FromQuery(Name = "ma_nguoi_dung")] string? maNguoiDung = null)
        {
            return Ok(authBusiness.CheckToken(maNguoiDung));
        }
    }
}
