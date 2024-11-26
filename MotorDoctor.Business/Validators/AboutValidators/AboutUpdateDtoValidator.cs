using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AboutUpdateDtoValidator : AbstractValidator<AboutUpdateDto>
{
    public AboutUpdateDtoValidator()
    {
        RuleForEach(x => x.AboutDetails).SetValidator(new AboutDetailUpdateDtoValidator());
    }
}
