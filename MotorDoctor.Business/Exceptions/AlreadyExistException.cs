using System.Net;

namespace MotorDoctor.Business.Exceptions;

public class AlreadyExistException : Exception, IBaseException
{
    public AlreadyExistException(string message) : base(message)
    {
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Conflict;

}
