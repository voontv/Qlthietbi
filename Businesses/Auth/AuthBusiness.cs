using Microsoft.EntityFrameworkCore;
using QlThietBi.Businesses;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;
using QlThietBi.Providers;

namespace QlThietBi.Businesses.Auth
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly QlThietBiContext context;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthenInfo authenInfo;

        public AuthBusiness(QlThietBiContext context, ITokenGenerator tokenGenerator, IAuthenInfo authenInfo)
        {
            this.context = context;
            this.tokenGenerator = tokenGenerator;
            this.authenInfo = authenInfo;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var maNguoiDung = request.MaNguoiDung?.Trim();
            if (string.IsNullOrWhiteSpace(maNguoiDung))
            {
                throw new InvalidOperationException("Thiếu mã người dùng.");
            }

            var user = await context.NguoiSuDungThietBis
                .Where(x => x.IsActive && x.MaNguoiDung == maNguoiDung)
                .Select(x => new LoggedInUser
                {
                    NguoiSuDungId = x.Id,
                    MaNguoiDung = x.MaNguoiDung,
                    TenNguoiDung = x.TenNguoiDung
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy người dùng '{maNguoiDung}'.");
            }

            return tokenGenerator.GenerateToken(user);
        }

        public LoggedInUserDto CheckToken(string? maNguoiDung = null)
        {
            var user = authenInfo.Get() ?? throw new UnauthorizedAccessException("Token không hợp lệ, đã hết hạn, hoặc mã người dùng không tồn tại trong hệ thống.");
            return new LoggedInUserDto
            {
                NguoiSuDungId = user.NguoiSuDungId,
                MaNguoiDung = user.MaNguoiDung,
                TenNguoiDung = user.TenNguoiDung,
                KhopMaNguoiDung = string.IsNullOrWhiteSpace(maNguoiDung)
                    ? null
                    : string.Equals(user.MaNguoiDung, maNguoiDung.Trim(), StringComparison.OrdinalIgnoreCase)
            };
        }
    }
}
