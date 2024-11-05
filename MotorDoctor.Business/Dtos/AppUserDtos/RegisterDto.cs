using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class RegisterDto : IDto
{
    [Required(ErrorMessage = "E-poçt sahəsi boş ola bilməz.")]
    [EmailAddress(ErrorMessage = "Düzgün e-poçt ünvanı formatı deyil.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "İstifadəçi adı sahəsi boş ola bilməz.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Şifrə sahəsi boş ola bilməz.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Şifrəni təsdiq etmə sahəsi boş ola bilməz.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Təsdiq edilmiş şifrə ilə şifrə eyni olmalıdır.")]
    public string ConfirmPassword { get; set; } = null!;
}