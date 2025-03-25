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
    private readonly IUrlHelper _urlHelper;
    private readonly ErrorLocalizer _errorLocalizer;
    private readonly ISubscriberService _subscriberService;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor, IMapper mapper, IEmailService emailService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, ErrorLocalizer errorLocalizer, ISubscriberService subscriberService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _emailService = emailService;
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext ?? new());
        _errorLocalizer = errorLocalizer;
        _subscriberService = subscriberService;
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

        user.EmailConfirmed = true;

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", string.Join(",", result.Errors.Select(x => x.Description)));
            return false;
        }

        await _userManager.AddToRoleAsync(user, IdentityRoles.Member.ToString());

        //await _sendConfirmEmailToken(user);

        await _signInManager.SignInAsync(user, isPersistent: false);

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

        await _signInManager.SignInAsync(user, isPersistent: false);
        await _subscriberService.CreateAsync(new SubscriberCreateDto { Email = dto.Email }, ModelState);

        return true;

    }



    public async Task<List<UserGetDto>> GetAllUserAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        var dtos = _mapper.Map<List<UserGetDto>>(users);

        for (int i = 0; i < dtos.Count; i++)
        {
            dtos[i].Role = (await _userManager.GetRolesAsync(users[i])).FirstOrDefault() ?? "undifiend";
        }

        return dtos;
    }


    public async Task<UserGetDto> GetUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<UserGetDto>(user);

        return dto;
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

    public async Task RemoveBotsAsync()
    {
        var users = await _userManager.Users.Take(1000).ToListAsync();

        foreach (var user in users)
        {
            if (user.UserName!.Count(x => x == '-') > 3)
            {
                await _userManager.DeleteAsync(user);
            }
        }
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


        string emailBody = _getVerifyEmailBody(link ?? "");


        EmailSendDto emailSendDto = new()
        {
            Body = emailBody,
            Subject = "Email Təsdiqləmə",
            ToEmail = user.Email ?? "undifined@undifined.com"
        };

        await _emailService.SendEmailAsync(emailSendDto);
    }
    public async Task<bool> SendForgotPasswordEmailAsync(ForgotPasswordDto dto, ModelStateDictionary ModelState)
    {
        var user = await _userManager.FindByEmailAsync(dto.EmailOrUsername);

        if (user is null)
            user = await _userManager.FindByNameAsync(dto.EmailOrUsername);

        if (user is null)
        {
            ModelState.AddModelError("", _errorLocalizer.GetValue("UserNotFound"));
            return false;
        }

        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        UrlActionContext context = new()
        {
            Action = "ResetPassword",
            Controller = "Account",
            Values = new { token = resetPasswordToken, email = user.Email },
            Protocol = _contextAccessor.HttpContext?.Request.Scheme
        };

        var link = _urlHelper.Action(context);

        string body = _getForgotPasswordBody(link ?? "");

        await _emailService.SendEmailAsync(new()
        {
            Body = body,
            ToEmail = user.Email ?? "",
            Subject = "Forgot Password"
        });


        return true;
    }


    public async Task<bool> CheckResetPasswordTokenAsync(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return false;

        var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", dto.Token);

        return result;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", string.Join(",", result.Errors.Select(x => x.Description)));
            return false;
        }

        await _signInManager.SignInAsync(user, false);
        return true;
    }
    private string _getVerifyEmailBody(string url)
    {

        string result = $@"
<!DOCTYPE html>
<html lang=""az"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Təsdiqləmə</title>
    <style>
        body {{
            margin: 0;
            padding: 0;
            background-color: #ffffff;
            font-family: Arial, sans-serif;
            color: #000000;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            border: 2px solid #000000;
            padding: 20px;
            border-radius: 0;
            background-color: #ffffff;
            text-align: center;
        }}
        .email-header {{
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            text-transform: uppercase;
        }}
        .email-body {{
            font-size: 16px;
            line-height: 1.5;
            margin-bottom: 30px;
        }}
        .email-button {{
            display: inline-block;
            text-decoration: none;
            background-color: #000000;
            color: #ffffff;
            padding: 10px 20px;
            border: 2px solid #000000;
            font-size: 16px;
            font-weight: bold;
            text-transform: uppercase;
        }}
        .email-footer {{
            font-size: 12px;
            color: #555555;
            margin-top: 20px;
        }}
        hr {{
            margin: 30px 0;
            border: none;
            border-top: 1px solid #000;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <!-- Azərbaycan Dili -->
        <div class=""email-header"">Email Təsdiqləmə</div>
        <div class=""email-body"">
            Qeydiyyatdan keçdiyiniz üçün təşəkkür edirik! Hesabınızı aktivləşdirmək üçün e-mail ünvanınızı təsdiqləyin.
        </div>
        <a href=""{url}"" class=""email-button"">E-maili Təsdiqlə</a>
        <div class=""email-footer"">
            Əgər bu hesabı siz yaratmamısınızsa, zəhmət olmasa, bu e-maili nəzərə almayın.
        </div>
        <hr>
        <!-- İngilis Dili -->
        <div class=""email-header"">Email Confirmation</div>
        <div class=""email-body"">
            Thank you for signing up! Please confirm your email address to activate your account.
        </div>
        <a href=""{url}"" class=""email-button"">Confirm Email</a>
        <div class=""email-footer"">
            If you didn’t create this account, please ignore this email.
        </div>
        <hr>
        <!-- Rus Dili -->
        <div class=""email-header"">Подтверждение Email</div>
        <div class=""email-body"">
            Спасибо за регистрацию! Пожалуйста, подтвердите свой адрес электронной почты, чтобы активировать ваш аккаунт.
        </div>
        <a href=""{url}"" class=""email-button"">Подтвердить Email</a>
        <div class=""email-footer"">
            Если вы не создавали эту учетную запись, просто проигнорируйте это письмо.
        </div>
    </div>
</body>
</html>";

        return result;
    }

    private string _getForgotPasswordBody(string url)
    {
        string result = $@"
<!DOCTYPE html>
<html lang=""az"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Şifrəni Sıfırla</title>
    <style>
        body {{
            margin: 0;
            padding: 0;
            background-color: #ffffff;
            font-family: Arial, sans-serif;
            color: #000000;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            border: 2px solid #000000;
            padding: 20px;
            border-radius: 0;
            background-color: #ffffff;
            text-align: center;
        }}
        .email-header {{
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            text-transform: uppercase;
        }}
        .email-body {{
            font-size: 16px;
            line-height: 1.5;
            margin-bottom: 30px;
        }}
        .email-button {{
            display: inline-block;
            text-decoration: none;
            background-color: #000000;
            color: #ffffff;
            padding: 10px 20px;
            border: 2px solid #000000;
            font-size: 16px;
            font-weight: bold;
            text-transform: uppercase;
        }}
        .email-footer {{
            font-size: 12px;
            color: #555555;
            margin-top: 20px;
        }}
        hr {{
            margin: 30px 0;
            border: none;
            border-top: 1px solid #000;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <!-- Azərbaycan Dili -->
        <div class=""email-header"">Şifrəni Sıfırla</div>
        <div class=""email-body"">
            Şifrənizi sıfırlamaq üçün aşağıdakı düyməyə klikləyin. Əgər siz bu sorğunu göndərməmisinizsə, bu e-maili nəzərə almayın.
        </div>
        <a href=""{url}"" class=""email-button"">Şifrəni Sıfırla</a>
        <div class=""email-footer"">
            Bu mesajı siz göndərməmisinizsə, sadəcə onu nəzərə almayın.
        </div>
        <hr>
        <!-- İngilis Dili -->
        <div class=""email-header"">Reset Password</div>
        <div class=""email-body"">
            Click the button below to reset your password. If you did not request this, please ignore this email.
        </div>
        <a href=""{url}"" class=""email-button"">Reset Password</a>
        <div class=""email-footer"">
            If you didn't request this, you can safely ignore this email.
        </div>
        <hr>
        <!-- Rus Dili -->
        <div class=""email-header"">Сброс Пароля</div>
        <div class=""email-body"">
            Нажмите кнопку ниже, чтобы сбросить пароль. Если вы не запрашивали это, просто проигнорируйте это письмо.
        </div>
        <a href=""{url}"" class=""email-button"">Сбросить Пароль</a>
        <div class=""email-footer"">
            Если вы не запрашивали этот сброс, просто проигнорируйте это письмо.
        </div>
    </div>
</body>
</html>";

        return result;
    }


}
