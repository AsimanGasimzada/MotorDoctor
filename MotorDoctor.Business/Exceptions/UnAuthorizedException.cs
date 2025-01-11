using System.Net;

namespace MotorDoctor.Business.Exceptions;

public class UnAuthorizedException : Exception, IBaseException
{
    public UnAuthorizedException(string message = "Qeydiyyatdan keçməyən istifadəçi") : base(message)
    {

    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;
}
