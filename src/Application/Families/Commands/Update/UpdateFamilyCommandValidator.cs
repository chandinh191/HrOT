using FluentValidation;

namespace hrOT.Application.Families.Commands.Update
{
    public class UpdateFamilyCommandValidator : AbstractValidator<UpdateFamilyCommand>
    {
        public UpdateFamilyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Family ID không được bỏ trống.");

            RuleFor(x => x.EmployeeId)
               .NotEmpty().WithMessage("Employee ID không được bỏ trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên không được quá 100 ký tự.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Ngày sinh không hợp lệ.");

            RuleFor(x => x.Relationship)
                .NotEmpty().WithMessage("Quan hệ không được bỏ trống.");

            RuleFor(x => x.HomeTown)
                .MaximumLength(100).WithMessage("Quê quán không được quá 100 ký tự.");
        }
    }
}

