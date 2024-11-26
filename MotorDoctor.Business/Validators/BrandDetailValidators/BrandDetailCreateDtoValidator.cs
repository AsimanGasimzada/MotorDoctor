using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BrandDetailCreateDtoValidator : AbstractValidator<BrandDetailCreateDto>
{
    public BrandDetailCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
    }
}