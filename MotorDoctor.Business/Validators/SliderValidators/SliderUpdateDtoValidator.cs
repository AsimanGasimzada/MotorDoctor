using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SliderUpdateDtoValidator : AbstractValidator<SliderUpdateDto>
{
    public SliderUpdateDtoValidator()
    {
        RuleFor(x => x.Image).Null();

        RuleForEach(x => x.SliderDetails).SetValidator(new SliderDetailUpdateDtoValidator());
    }
}
