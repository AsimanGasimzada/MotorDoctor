using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AboutDetailUpdateDtoValidator : AbstractValidator<AboutDetailUpdateDto>
{
    public AboutDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(9196);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
    }
}
