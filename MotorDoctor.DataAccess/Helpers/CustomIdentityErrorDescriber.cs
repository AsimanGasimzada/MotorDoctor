using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Helpers;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    private readonly IStringLocalizer<CustomIdentityErrorDescriber> _localizer;

    public CustomIdentityErrorDescriber(IStringLocalizer<CustomIdentityErrorDescriber> localizer)
    {
        _localizer = localizer;
    }
    public override IdentityError ConcurrencyFailure()
    {
        return new IdentityError { Code = nameof(ConcurrencyFailure), Description = _localizer[nameof(ConcurrencyFailure)] };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError { Code = nameof(DefaultError), Description = _localizer[nameof(DefaultError)] };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(_localizer[nameof(DuplicateEmail)], email) };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(_localizer[nameof(DuplicateEmail)], role) };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format(_localizer[nameof(DuplicateEmail)], userName) };
    }

    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(_localizer[nameof(DuplicateEmail)], email) };
    }

    public override IdentityError InvalidRoleName(string? role)
    {
        return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(_localizer[nameof(DuplicateEmail)], role) };
    }

    public override IdentityError InvalidToken()
    {
        return new IdentityError { Code = nameof(InvalidToken), Description = _localizer[nameof(InvalidToken)] };
    }

    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format(_localizer[nameof(DuplicateEmail)], userName) };
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = _localizer[nameof(LoginAlreadyAssociated)] };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError { Code = nameof(PasswordMismatch), Description = _localizer[nameof(PasswordMismatch)] };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _localizer[nameof(PasswordRequiresDigit)] };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError { Code = nameof(PasswordRequiresLower), Description = _localizer[nameof(PasswordRequiresLower)] };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _localizer[nameof(PasswordRequiresUniqueChars)] };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = string.Format(_localizer[nameof(PasswordRequiresUniqueChars)], uniqueChars) };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _localizer[nameof(PasswordRequiresUpper)] };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(_localizer[nameof(PasswordTooShort)], length) };
    }

    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        return new IdentityError { Code = nameof(RecoveryCodeRedemptionFailed), Description = _localizer[nameof(RecoveryCodeRedemptionFailed)] };
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = _localizer[nameof(UserAlreadyHasPassword)] };
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(_localizer[nameof(UserAlreadyInRole)], role) };
    }

    public override IdentityError UserLockoutNotEnabled()
    {
        return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = _localizer[nameof(UserLockoutNotEnabled)] };
    }

    public override IdentityError UserNotInRole(string role)
    {
        return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(_localizer[nameof(UserNotInRole)], role) };
    }
}
