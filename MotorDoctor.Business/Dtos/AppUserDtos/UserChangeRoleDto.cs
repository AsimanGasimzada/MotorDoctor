using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class UserChangeRoleDto : IDto
{
    [Required(ErrorMessage = "İstifadəçi ID sahəsi boş ola bilməz.")]
    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "Rol adı sahəsi boş ola bilməz.")]
    public string RoleName { get; set; } = null!;
}

