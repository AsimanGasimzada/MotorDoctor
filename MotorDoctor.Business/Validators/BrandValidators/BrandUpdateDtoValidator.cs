using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BrandUpdateDtoValidator : AbstractValidator<BrandUpdateDto>
{
    public BrandUpdateDtoValidator()
    {
        RuleForEach(x => x.BrandDetails).SetValidator(new BrandDetailUpdateDtoValidator());
    }
}
