using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductDetailCreateDtoValidator : AbstractValidator<ProductDetailCreateDto>
{
    public ProductDetailCreateDtoValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).MinimumLength(1);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(128).MinimumLength(2);
    }
}


