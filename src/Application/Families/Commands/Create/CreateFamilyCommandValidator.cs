using FluentValidation;

namespace hrOT.Application.Families.Commands.Create
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
               .NotEmpty().WithMessage("Employee ID không được bỏ trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên không quá 100 ký tự.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày sinh không hợp lệ.");

            RuleFor(x => x.Relationship)
                .NotEmpty().WithMessage("Quan hệ không được bỏ trống.");

            RuleFor(x => x.HomeTown)
                .MaximumLength(100).WithMessage("Quê quán không được bỏ trống.");
        }
    }
}
