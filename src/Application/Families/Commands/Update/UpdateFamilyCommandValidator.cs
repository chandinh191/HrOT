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

            RuleFor(x => x.FatherName)
                .MaximumLength(100).WithMessage("Tên của cha không được quá 100 ký tự.");

            RuleFor(x => x.MotherName)
                .MaximumLength(100).WithMessage("Tên của mẹ không được quá 100 ký tự.");

            RuleFor(x => x.NumberOfDependents)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng người phụ thuộc phải >=0.");

            RuleFor(x => x.HomeTown)
                .MaximumLength(100).WithMessage("Quê quán không được quá 100 ký tự.");
        }
    }
}
