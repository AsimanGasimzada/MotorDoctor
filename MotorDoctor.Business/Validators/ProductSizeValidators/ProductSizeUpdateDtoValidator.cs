using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductSizeUpdateDtoValidator : AbstractValidator<ProductSizeUpdateDto>
{
    public ProductSizeUpdateDtoValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Count).GreaterThan(0);
        RuleFor(x => x.Size).NotEmpty().MaximumLength(256);
    }
}
