using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductDetailUpdateDtoValidator : AbstractValidator<ProductDetailUpdateDto>
{
    public ProductDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2048).MinimumLength(1);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(128).MinimumLength(2);
    }
}
