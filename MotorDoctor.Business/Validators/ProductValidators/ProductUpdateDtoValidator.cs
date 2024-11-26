using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().MaximumLength(64);

        RuleForEach(x => x.ProductDetails).SetValidator(new ProductDetailUpdateDtoValidator());
        RuleForEach(x => x.ProductSizes).SetValidator(new ProductSizeUpdateDtoValidator());
    }
}
