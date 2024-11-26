using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleForEach(x => x.CategoryDetails).SetValidator(new CategoryDetailUpdateDtoValidator());
    }
}
