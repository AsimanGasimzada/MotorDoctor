namespace MotorDoctor.Business.Dtos;

public class ResetPasswordDto : IDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}