using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class CategoryDetailCreateDtoValidator : AbstractValidator<CategoryDetailCreateDto>
{
    public CategoryDetailCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);

    }
}