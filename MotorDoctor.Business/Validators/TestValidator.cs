using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class TestValidator : AbstractValidator<LoginDto>
{
    public TestValidator()
    {
        //RuleFor(x => x.EmailOrUsername).NotEmpty().MaximumLength(10);
        //RuleFor(x => x.Password).NotEmpty().MaximumLength(10);
    }
}

public class Test2Validator : AbstractValidator<SubscriberCreateDto>
{
    public Test2Validator()
    {
        //RuleFor(x=>x.Email).NotEmpty().MaximumLength(10).WithMessage("max 10").MinimumLength(2).WithMessage("min 2");
    }
}