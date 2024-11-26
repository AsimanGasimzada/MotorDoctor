using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class SubscriberUpdateDtoValidator : AbstractValidator<SubscriberUpdateDto>
{
    public SubscriberUpdateDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotNull().MaximumLength(256);
    }
}
