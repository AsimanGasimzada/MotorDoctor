using System.Net;

namespace MotorDoctor.Business.Exceptions;

public class NotFoundException : Exception, IBaseException
{
    public NotFoundException(string message = "Not found") : base(message)
    {

    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

}
