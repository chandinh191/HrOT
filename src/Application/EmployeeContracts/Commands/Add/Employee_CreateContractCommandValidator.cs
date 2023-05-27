using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;

namespace hrOT.Application.EmployeeContracts.Commands.Add;
public class Employee_CreateContractCommandValidator : AbstractValidator<Employee_CreateContractCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateContractCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        // Validate File
        RuleFor(v => v.EmployeeContractDTO.File)
            .NotEmpty().WithMessage("File không được để trống.");
            //.MaximumLength(200).WithMessage("File cannot be more than 200 characters long.");

        // Validate StartDate
        RuleFor(v => v.EmployeeContractDTO.StartDate)
            .NotNull().WithMessage("Ngày bắt đầu không được để trống.")
            .LessThan(v => v.EmployeeContractDTO.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn ngày kết thúc.");

        // Validate EndDate
        RuleFor(v => v.EmployeeContractDTO.EndDate)
            .NotNull().WithMessage("Ngày kết thúc không được để trống.")
            .GreaterThan(d => d.EmployeeContractDTO.StartDate).WithMessage("Ngày kết thúc phải trễ hơn ngày bắt đầu");

        // Validate Job
        RuleFor(v => v.EmployeeContractDTO.Job)
            .NotEmpty().WithMessage("Công việc không được để trống");

        // Validate Salary
        RuleFor(v => v.EmployeeContractDTO.Salary)
            .NotNull().WithMessage("Lương không được để trống")
            .GreaterThan(0).WithMessage("Lương không được âm.");

        // Validate CustomSalary
        RuleFor(v => v.EmployeeContractDTO.CustomSalary)
            .NotNull().WithMessage("CustomSalary không được để trống.")
            .GreaterThan(0).WithMessage("CustomSalary không được âm.");

        // Validate Number_Of_Dependents
        RuleFor(v => v.EmployeeContractDTO.Number_Of_Dependents)
            .NotNull().WithMessage("Số người phụ thuộc không được để trống.");

        // Validate Status
        RuleFor(v => v.EmployeeContractDTO.Status)
            .NotNull().WithMessage("Trạng thái hợp đồng không được để trống.");
    }
}
