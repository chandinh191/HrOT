using FluentValidation;

namespace hrOT.Application.Allowances.Command.Update
{
    public class UpdateAllowanceCommandValidator : AbstractValidator<UpdateAllowanceCommand>
    {
        public UpdateAllowanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID không được để trống");

            RuleFor(x => x.EmployeeContractId)
                 .NotEmpty().WithMessage("ID hợp đồng của nhân viên không được để trống");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(50).WithMessage("Tên không được vượt quá 50 kí tự");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Loại không được để trống");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Số lượng không được để trống.")
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");

            RuleFor(x => x.Eligibility_Criteria)
                .MaximumLength(1000).WithMessage("Điều kiện để áp đụng phụ cấp không được quá 1000 chữ");

            RuleFor(x => x.Requirements)
                .MaximumLength(1000).WithMessage("Yêu cầu không được quá 1000 chữ");
        }
    }
}