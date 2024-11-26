using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SettingDetailCreateDtoValidator : AbstractValidator<SettingDetailCreateDto>
{
    public SettingDetailCreateDtoValidator()
    {
        RuleFor(x => x.Value).NotEmpty().MaximumLength(1024);
    }
}
