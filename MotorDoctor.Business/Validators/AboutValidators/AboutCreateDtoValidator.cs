using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AboutCreateDtoValidator : AbstractValidator<AboutCreateDto>
{
    public AboutCreateDtoValidator()
    {
        RuleFor(x => x.Image).NotEmpty();

        RuleForEach(x => x.AboutDetails).SetValidator(new AboutDetailCreateDtoValidator());
    }
}