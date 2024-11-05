namespace MotorDoctor.Business.Exceptions;

public class InvalidInputException:Exception,IBaseException
{
    public InvalidInputException(string message):base(message)  
    {
        
    }
}
