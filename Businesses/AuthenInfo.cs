using QlThietBi.AutoConfig;
using QlThietBi.Models;


namespace QlThietBi.Businesses
{
    [ImplementBy(typeof(AuthenInfo))]
    public interface IAuthenInfo
    {
        LoggedInUser? Get();
    }
    public class AuthenInfo: IAuthenInfo
    {
        private readonly IHttpContextAccessor context;

        public AuthenInfo(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public LoggedInUser? Get()
        {
            if (context.HttpContext?.User is UserClaimsPrincipal user)
            {
                return new LoggedInUser
                {
                    Username = user.Username,
                };
            }

            return null;
        }
    }
}