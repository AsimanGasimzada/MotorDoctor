using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class LoginDto : IDto
{
    [Required(ErrorMessage = "E-poçt və ya istifadəçi adı sahəsi boş ola bilməz.")]
    public string EmailOrUsername { get; set; } = null!;

    [Required(ErrorMessage = "Şifrə sahəsi boş ola bilməz.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = false;

    public string? ReturnUrl { get; set; }
}

