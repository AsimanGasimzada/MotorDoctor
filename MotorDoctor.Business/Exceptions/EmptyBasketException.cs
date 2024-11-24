using System.Net;

namespace MotorDoctor.Business.Exceptions;

public class EmptyBasketException : Exception, IBaseException
{
    public EmptyBasketException(string message = "Səbətiniz boşdur") : base(message)
    {

    }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

}
