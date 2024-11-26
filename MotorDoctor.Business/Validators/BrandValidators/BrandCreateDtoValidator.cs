using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BrandCreateDtoValidator : AbstractValidator<BrandCreateDto>
{
    public BrandCreateDtoValidator()
    {
        RuleFor(x => x.Image).NotNull();

        RuleForEach(x => x.BrandDetails).SetValidator(new BrandDetailCreateDtoValidator());
    }
}
