using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.PaySlips.Queries;
public class GetTotalSalaryPayForEmployeeQueryValidator : AbstractValidator<GetTotalSalaryPayForEmployeeQuery>
{
    public GetTotalSalaryPayForEmployeeQueryValidator()
    {
        RuleFor(query => query.FromDate)
            .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.");

        RuleFor(query => query.ToDate)
            .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
            .Must((query, toDate) => BeValidToDate(query.FromDate, toDate)).WithMessage("Ngày kết thúc phải lớn hơn Ngày bắt đầu.");
    }

    private bool BeValidToDate(DateTime fromDate, DateTime? toDate)
    {
        if (toDate == null)
            return false;

        return toDate.Value > fromDate;
    }
}

