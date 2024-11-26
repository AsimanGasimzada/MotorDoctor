using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(64);
        RuleFor(x => x.City).NotEmpty().MaximumLength(64);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Region).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Street).NotEmpty().MaximumLength(128);
    }
}
