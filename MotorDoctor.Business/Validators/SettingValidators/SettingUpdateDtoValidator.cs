using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SettingUpdateDtoValidator : AbstractValidator<SettingUpdateDto>
{
    public SettingUpdateDtoValidator()
    {
        RuleForEach(x => x.SettingDetails).SetValidator(new SettingDetailUpdateDtoValidator());
    }
}
