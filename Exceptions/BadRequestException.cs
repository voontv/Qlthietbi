using QlThietBi.Exceptions;
using System.Net;

namespace QlThietBi.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string msg) : base(HttpStatusCode.BadRequest, msg)
        {
        }
    }
}