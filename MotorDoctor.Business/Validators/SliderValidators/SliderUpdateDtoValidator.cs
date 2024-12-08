using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SliderUpdateDtoValidator : AbstractValidator<SliderUpdateDto>
{
    public SliderUpdateDtoValidator()
    {
        RuleFor(x => x.ButtonPath).MaximumLength(256);

        RuleForEach(x => x.SliderDetails).SetValidator(new SliderDetailUpdateDtoValidator());
    }
}
