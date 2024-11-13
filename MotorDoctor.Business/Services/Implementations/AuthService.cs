using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;

namespace MotorDoctor.Business.Services.Implementations;

internal class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly string _staticFilesPath;
    private readonly IUrlHelper _urlHelper;
    private readonly ErrorLocalizer _errorLocalizer;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor, IMapper mapper, IEmailService emailService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, ErrorLocalizer errorLocalizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _emailService = emailService;
        _staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Connex.Business", "StaticFiles");
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext ?? new());
        _errorLocalizer = errorLocalizer;
    }

    public async Task<bool> LoginAsync(LoginDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var user = await _userManager.FindByEmailAsync(dto.EmailOrUsername);

        if (user is null)
            user = await _userManager.FindByNameAsync(dto.EmailOrUsername);

        if (user is null)
        {
            ModelState.AddModelError("", _errorLocalizer.GetValue("InvalidCredentials"));
            return false;
        }

        if (user.EmailConfirmed is false)
        {
            ModelState.AddModelError("", _errorLocalizer.GetValue("UnconfirmedEmail"));
            await _sendConfirmEmailToken(user);
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", _errorLocalizer.GetValue("BlockedUser"));
                return false;
            }

            ModelState.AddModelError("", _errorLocalizer.GetValue("InvalidCredentials"));
            return false;
        }

        return true;
    }

    public async Task<bool> LogoutAsync()
    {
        if (!_contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false)
            return false;

        await _signInManager.SignOutAsync();

        return true;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;


        var isExist = await _userManager.Users.AnyAsync(x => x.NormalizedEmail == dto.Email.ToUpper());

        if (isExist)
        {
            ModelState.AddModelError("", _errorLocalizer.GetValue("DuplicateEmail"));
            return false;
        }

        isExist = await _userManager.Users.AnyAsync(x => x.NormalizedUserName == dto.Username.ToUpper());

        if (isExist)
        {
            ModelState.AddModelError("", _errorLocalizer.GetValue("DuplicateUserName"));
            return false;
        }

        var user = _mapper.Map<AppUser>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", string.Join(",", result.Errors.Select(x => x.Description)));
            return false;
        }

        await _userManager.AddToRoleAsync(user, IdentityRoles.Member.ToString());

        await _sendConfirmEmailToken(user);

        return true;
    }



    public async Task<bool> VerifyEmailAsync(VerifyEmailDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            throw new InvalidInputException(_errorLocalizer.GetValue(nameof(InvalidInputException)));


        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            throw new InvalidInputException(_errorLocalizer.GetValue(nameof(InvalidInputException)));

        var result = await _userManager.ConfirmEmailAsync(user, dto.Token);


        if (!result.Succeeded)
        {
            await _sendConfirmEmailToken(user);

            throw new InvalidInputException(_errorLocalizer.GetValue("UnconfirmedEmail"));
        }


        return true;

    }



    public async Task<List<UserGetDto>> GetAllUserAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var dtos = _mapper.Map<List<UserGetDto>>(users);

        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault() ?? "undified";
        }

        return dtos;
    }

    public async Task<bool> ChangeUserRoleAsync(UserChangeRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);

        if (user is null)
            throw new InvalidInputException("Gözlənilməz xəta baş verdi yenidən sınayın.Təsdiqləmə linki yenidən göndərilmişdir.");

        var role = Enum.GetNames(typeof(IdentityRoles)).ToList().FirstOrDefault(x => x.ToLower() == dto.RoleName.ToLower());

        if (string.IsNullOrWhiteSpace(role))
            throw new InvalidInputException("Gözlənilməz xəta baş verdi yenidən sınayın.Təsdiqləmə linki yenidən göndərilmişdir.");

        await _userManager.AddToRoleAsync(user, role);

        return true;
    }

    private async Task _sendConfirmEmailToken(AppUser user)
    {
        string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        UrlActionContext context = new()
        {
            Action = "VerifyEmail",
            Controller = "Account",
            Values = new { token = confirmEmailToken, email = user.Email },
            Protocol = _contextAccessor.HttpContext?.Request.Scheme
        };

        var link = _urlHelper.Action(context);


        string emailBody = await _getTemplateContentAsync(link ?? "", user.UserName ?? "", "ConfirmEmailBody.html");


        EmailSendDto emailSendDto = new()
        {
            Body = emailBody,
            Subject = "Email Təsdiqləmə",
            ToEmail = user.Email ?? "undifined@undifined.com"
        };

        await _emailService.SendEmailAsync(emailSendDto);
    }

    private async Task<string> _getTemplateContentAsync(string url, string username, string filename)
    {
        string path = Path.Combine(_staticFilesPath, filename);

        using StreamReader streamReader = new StreamReader(path);
        string result = await streamReader.ReadToEndAsync();

        result = result.Replace("[REPLACE_URL]", url);
        result = result.Replace("[REPLACE_USERNAME]", username);

        streamReader.Close();
        return result;
    }
}
