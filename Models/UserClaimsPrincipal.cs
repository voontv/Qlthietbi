using System.Security.Claims;

namespace QlThietBi.Models
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public UserClaimsPrincipal(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
            Username = claimsIdentity.Name ?? string.Empty;
        }

        public string Username { get; }
    }
}
