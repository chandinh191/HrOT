using FluentValidation;

namespace hrOT.Application.Allowances.Command.Update
{
    public class UpdateAllowanceCommandValidator : AbstractValidator<UpdateAllowanceCommand>
    {
        public UpdateAllowanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required.");

            RuleFor(x => x.EmployeeContractId)
                .NotEmpty().WithMessage("Employee contract ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Eligibility_Criteria)
                .MaximumLength(1000).WithMessage("Eligibility criteria cannot exceed 1000 characters.");

            RuleFor(x => x.Requirements)
                .MaximumLength(1000).WithMessage("Requirements cannot exceed 1000 characters.");
        }
    }
}
