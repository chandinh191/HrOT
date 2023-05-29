using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.CompanyContracts.Commands.Create;
public class CreateCompanyContractCommandValidator : AbstractValidator<CreateCompanyContractCommand>
{
    public CreateCompanyContractCommandValidator()
    {
        RuleFor(command => command.File)
           .NotEmpty().WithMessage("File không được để trống.");

        RuleFor(command => command.Salary)
            .NotEmpty().WithMessage("Lương không được để trống.");

        RuleFor(command => command.StartDate)
            .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.")
            .Must(BeValidStartDate).WithMessage("Ngày bắt đầu phải lớn hơn hoặc bằng ngày hiện tại.");

        RuleFor(command => command.EndDate)
            .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
            .Must((command, endDate) => BeValidEndDate(command.StartDate, endDate)).WithMessage("Ngày kết thúc phải lớn hơn Ngày bắt đầu.");
    }

    private bool BeValidStartDate(DateTime? startDate)
    {
        if (startDate == null)
            return false;

        return startDate.Value >= DateTime.Today;
    }

    private bool BeValidEndDate(DateTime? startDate, DateTime? endDate)
    {
        if (startDate == null || endDate == null)
            return false;

        return endDate.Value > startDate.Value;
    }
}
