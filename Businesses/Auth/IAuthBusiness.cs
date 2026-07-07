using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;
using QlThietBi.Providers;

namespace QlThietBi.Businesses.Auth
{
    [ImplementBy(typeof(AuthBusiness))]
    public interface IAuthBusiness
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        LoggedInUserDto CheckToken(string? maNguoiDung = null);
    }
}
