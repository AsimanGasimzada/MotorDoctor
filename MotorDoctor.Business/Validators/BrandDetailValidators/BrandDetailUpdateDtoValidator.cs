using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BrandDetailUpdateDtoValidator : AbstractValidator<BrandDetailUpdateDto>
{
    public BrandDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
    }
}
