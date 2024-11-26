using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AttendanceCreateDtoValidator : AbstractValidator<AttendanceCreateDto>
{
    public AttendanceCreateDtoValidator()
    {
        RuleFor(x => x.Image).NotEmpty();

        RuleForEach(x => x.AttendanceDetails).SetValidator(new AttendanceDetailCreateDtoValidator());
    }
}