using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SettingDetailUpdateDtoValidator : AbstractValidator<SettingDetailUpdateDto>
{
    public SettingDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Value).NotEmpty().MaximumLength(1024);
    }
}
