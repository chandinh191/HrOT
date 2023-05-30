using FluentValidation;

namespace hrOT.Application.Degrees.Commands.Update
{
    public class UpdateDegreeCommandValidator : AbstractValidator<UpdateDegreeCommand>
    {
        public UpdateDegreeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Degree ID không được bỏ trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên bằng cấp không được bỏ trống.")
                .MaximumLength(100).WithMessage("Tên bằng cấp không được quá 100 ký tự.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái bằng cấp sai.");
        }
    }
}
