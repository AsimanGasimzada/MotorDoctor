using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class CategoryDetailUpdateDtoValidator : AbstractValidator<CategoryDetailUpdateDto>
{
    public CategoryDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);

    }
}
