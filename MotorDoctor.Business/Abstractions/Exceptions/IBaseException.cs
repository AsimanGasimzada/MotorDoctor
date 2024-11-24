using System.Net;

namespace MotorDoctor.Business;

public interface IBaseException
{
    public HttpStatusCode StatusCode { get; set; }
}
