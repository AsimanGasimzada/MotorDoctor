using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SettingUpdateDtoValidator : AbstractValidator<SettingUpdateDto>
{
    public SettingUpdateDtoValidator()
    {
        RuleFor(x => x.Key).Empty();

        RuleForEach(x => x.SettingDetails).SetValidator(new SettingDetailUpdateDtoValidator());
    }
}
