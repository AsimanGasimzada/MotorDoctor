using Microsoft.AspNetCore.Identity;

namespace MotorDoctor.Core.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}
