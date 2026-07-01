using System.Net;

namespace QlThietBi.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string msg) : base(HttpStatusCode.NotFound, msg)
        {
        }
    }
}