using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SettingCreateDtoValidator : AbstractValidator<SettingCreateDto>
{
    public SettingCreateDtoValidator()
    {
        RuleFor(x => x.Key).NotEmpty().MaximumLength(64);

        RuleForEach(x => x.SettingDetails).SetValidator(new SettingDetailCreateDtoValidator());
    }
}