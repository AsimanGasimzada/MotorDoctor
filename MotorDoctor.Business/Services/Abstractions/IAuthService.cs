namespace MotorDoctor.Business.Services.Abstractions;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto, ModelStateDictionary ModelState);
    Task<bool> LoginAsync(LoginDto dto, ModelStateDictionary ModelState);
    Task<bool> LogoutAsync();
    Task<bool> VerifyEmailAsync(VerifyEmailDto dto, ModelStateDictionary ModelState);
    Task<List<UserGetDto>> GetAllUserAsync();
    Task<UserGetDto> GetUserAsync(string id);
    Task<bool> ChangeUserRoleAsync(UserChangeRoleDto dto);
}
