using System.Net;

namespace QlThietBi.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string msg) : base(HttpStatusCode.Forbidden, msg)
        {
        }
    }
}