using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SliderDetailUpdateDtoValidator : AbstractValidator<SliderDetailUpdateDto>
{
    public SliderDetailUpdateDtoValidator()
    {
        RuleFor(x => x.ButtonTitle).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Description).MaximumLength(128);
    }
}
