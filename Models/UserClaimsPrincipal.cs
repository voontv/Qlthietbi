using System.Security.Claims;

namespace QlThietBi.Models
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public UserClaimsPrincipal(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
            MaNguoiDung = FindClaimValue(
                claimsIdentity,
                "ma_nguoi_dung",
                "MaNguoiDung",
                "maNguoiDung",
                "MA_NGUOI_DUNG",
                ClaimTypes.NameIdentifier,
                "nameid",
                "sub",
                "unique_name",
                "preferred_username",
                ClaimTypes.Name,
                "name") ?? string.Empty;
            TenNguoiDung = FindClaimValue(
                claimsIdentity,
                "ten_nguoi_dung",
                "TenNguoiDung",
                "tenNguoiDung",
                "TEN_NGUOI_DUNG",
                ClaimTypes.Name,
                "name",
                "full_name",
                "display_name") ?? MaNguoiDung;
            if (int.TryParse(FindClaimValue(claimsIdentity, "nguoi_su_dung_id", "NguoiSuDungId", "nguoiSuDungId"), out var id))
            {
                NguoiSuDungId = id;
            }
        }

        public int? NguoiSuDungId { get; }
        public string MaNguoiDung { get; }
        public string TenNguoiDung { get; }
        public string Username => MaNguoiDung;

        private static string? FindClaimValue(ClaimsIdentity identity, params string[] claimTypes)
        {
            foreach (var type in claimTypes)
            {
                var value = identity.FindFirst(type)?.Value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }
            }

            return null;
        }
    }
}
