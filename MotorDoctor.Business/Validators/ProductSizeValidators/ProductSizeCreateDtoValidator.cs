using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class ProductSizeCreateDtoValidator : AbstractValidator<ProductSizeCreateDto>
{
    public ProductSizeCreateDtoValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Count).GreaterThan(0);
        RuleFor(x => x.Size).NotEmpty().MaximumLength(256);
    }
}