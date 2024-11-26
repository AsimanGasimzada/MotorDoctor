using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SubscriberCreateDtoValidator : AbstractValidator<SubscriberCreateDto>
{
    public SubscriberCreateDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotNull().MaximumLength(256);
    }
}