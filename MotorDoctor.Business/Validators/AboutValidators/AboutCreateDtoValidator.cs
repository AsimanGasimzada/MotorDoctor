using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AboutCreateDtoValidator : AbstractValidator<AboutCreateDto>
{
    public AboutCreateDtoValidator()
    {
        RuleForEach(x => x.AboutDetails).SetValidator(new AboutDetailCreateDtoValidator());
    }
}