using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleForEach(x => x.CategoryDetails).SetValidator(new CategoryDetailCreateDtoValidator());
    }
}
