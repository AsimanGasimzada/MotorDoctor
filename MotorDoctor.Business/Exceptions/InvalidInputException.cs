using System.Net;

namespace MotorDoctor.Business.Exceptions;

public class InvalidInputException:Exception,IBaseException
{
    public InvalidInputException(string message):base(message)  
    {
        
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

}
