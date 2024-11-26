using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SliderCreateDtoValidator : AbstractValidator<SliderCreateDto>
{
    public SliderCreateDtoValidator()
    {
        RuleFor(x => x.Image).NotNull();

        RuleForEach(x => x.SliderDetails).SetValidator(new SliderDetailCreateDtoValidator());
    }
}
