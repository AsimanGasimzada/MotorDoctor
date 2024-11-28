using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SliderUpdateDtoValidator : AbstractValidator<SliderUpdateDto>
{
    public SliderUpdateDtoValidator()
    {
        RuleForEach(x => x.SliderDetails).SetValidator(new SliderDetailUpdateDtoValidator());
    }
}
