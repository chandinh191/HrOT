using FluentValidation;

namespace hrOT.Application.Degrees.Commands.Create
{
    public class CreateDegreeCommandValidator : AbstractValidator<CreateDegreeCommand>
    {
        public CreateDegreeCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee ID không được bỏ trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên bằng cấp không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên bằng cấp không được quá 100 ký tự.");
        }
    }
}
