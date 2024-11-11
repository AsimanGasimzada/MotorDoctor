namespace MotorDoctor.Business.Exceptions;

public class EmptyBasketException : Exception, IBaseException
{
    public EmptyBasketException(string message = "Səbətiniz boşdur") : base(message)
    {

    }
}
