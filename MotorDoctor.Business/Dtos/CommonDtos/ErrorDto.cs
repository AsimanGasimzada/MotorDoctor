namespace MotorDoctor.Business.Dtos;

public class ErrorDto : IDto
{
    public string Name { get; set; } = "Xəta baş verdi";
    public string Message { get; set; } = null!;
    public int StatusCode { get; set; }
}

