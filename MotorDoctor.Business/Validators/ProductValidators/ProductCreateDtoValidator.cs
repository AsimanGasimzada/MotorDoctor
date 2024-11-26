using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(x => x.MainImage).NotNull();

        RuleForEach(x => x.Images).NotNull().NotEmpty();
        RuleForEach(x => x.ProductDetails).SetValidator(new ProductDetailCreateDtoValidator());
        RuleForEach(x => x.ProductSizes).SetValidator(new ProductSizeCreateDtoValidator());
    }
}
