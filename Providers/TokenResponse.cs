using System;

namespace QlThietBi.Providers
{
    public class TokenResponse
    {
        public TokenResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int? NguoiSuDungId { get; set; }
        public string? MaNguoiDung { get; set; }
        public string? TenNguoiDung { get; set; }
    }
}
