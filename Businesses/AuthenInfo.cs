using Microsoft.EntityFrameworkCore;
using QlThietBi.AutoConfig;
using QlThietBi.Models;

namespace QlThietBi.Businesses
{
    [ImplementBy(typeof(AuthenInfo))]
    public interface IAuthenInfo
    {
        LoggedInUser? Get();
    }

    public class AuthenInfo : IAuthenInfo
    {
        private readonly IHttpContextAccessor context;
        private readonly QlThietBiContext dbContext;

        public AuthenInfo(IHttpContextAccessor context, QlThietBiContext dbContext)
        {
            this.context = context;
            this.dbContext = dbContext;
        }

        public LoggedInUser? Get()
        {
            if (context.HttpContext?.User is not UserClaimsPrincipal user || string.IsNullOrWhiteSpace(user.MaNguoiDung))
            {
                return null;
            }

            var dbUser = dbContext.NguoiSuDungThietBis
                .AsNoTracking()
                .FirstOrDefault(x => x.IsActive && x.MaNguoiDung == user.MaNguoiDung);
            if (dbUser == null)
            {
                return null;
            }

            return new LoggedInUser
            {
                NguoiSuDungId = dbUser.Id,
                MaNguoiDung = dbUser.MaNguoiDung,
                TenNguoiDung = dbUser.TenNguoiDung
            };
        }
    }
}
