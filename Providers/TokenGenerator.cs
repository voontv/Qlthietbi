using QlThietBi.AutoConfig;
using Microsoft.IdentityModel.Tokens;
using QlThietBi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QlThietBi.Providers
{
    [ImplementBy(typeof(TokenGenerator))]
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(string username);
        TokenResponse GenerateToken(LoggedInUser user);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly SecuritySettings securitySettings;

        public readonly SigningCredentials SigningCredentials;

        public TokenGenerator(SecuritySettings securitySettings)
        {
            this.securitySettings = securitySettings;

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.securitySettings.Secret));
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, null, ClaimsIdentity.DefaultIssuer, "Provider");
        }

        public TokenResponse GenerateToken(string username)
        {
            return GenerateToken(new LoggedInUser
            {
                MaNguoiDung = username,
                TenNguoiDung = username
            });
        }

        public TokenResponse GenerateToken(LoggedInUser user)
        {
            var claims = new List<Claim>
            {
                CreateClaim(ClaimTypes.NameIdentifier, user.MaNguoiDung),
                CreateClaim(ClaimTypes.Name, user.TenNguoiDung),
                CreateClaim("ma_nguoi_dung", user.MaNguoiDung),
                CreateClaim("ten_nguoi_dung", user.TenNguoiDung)
            };
            if (user.NguoiSuDungId.HasValue)
            {
                claims.Add(CreateClaim("nguoi_su_dung_id", user.NguoiSuDungId.Value.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "Bearer");
            var now = DateTime.UtcNow;
            var expiresAt = now.AddSeconds(securitySettings.Expiration);

            var jwt = new JwtSecurityToken(
                claims: identity.Claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse(token)
            {
                ExpiresAt = expiresAt,
                NguoiSuDungId = user.NguoiSuDungId,
                MaNguoiDung = user.MaNguoiDung,
                TenNguoiDung = user.TenNguoiDung
            };
        }
    }
}
