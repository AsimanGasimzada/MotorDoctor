namespace MotorDoctor.Business.Dtos;

public class ForgotPasswordDto : IDto
{
    public string EmailOrUsername { get; set; } = null!;
}
