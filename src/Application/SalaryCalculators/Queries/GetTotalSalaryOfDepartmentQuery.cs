using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using LogOT.Application.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.SalaryCalculators.Queries;
public record GetTotalSalaryOfDepartmentQuery(Guid DepartmentID) : IRequest<double>;

public class GetTotalSalaryOfDepartmentQueryHandler : IRequestHandler<GetTotalSalaryOfDepartmentQuery, double>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetTotalSalaryOfDepartmentQueryHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<double> Handle(GetTotalSalaryOfDepartmentQuery request, CancellationToken cancellationToken)
    {
        EmployeeContractStatus contractStatus = EmployeeContractStatus.Effective;

        bool departmentExists = await _context.Departments.AnyAsync(d => d.Id == request.DepartmentID);
        if (!departmentExists)
        {
            throw new NotFoundException("Department not found.");
        }

        // get list employee
        var listEmployee = await _context.Employees
            .Include(e => e.Position)
            .ThenInclude(e => e.Department)
            .Where(e => e.IsDeleted == false && e.Position.Department.Id == request.DepartmentID)
            .ToListAsync();

        double total = 0;

        foreach (var employee in listEmployee)
        {
            Guid employeeId = employee.Id;
            // get list Employee Contract
            var listEmployeeContract = await _context.EmployeeContracts
                .Where(contract => contract.EmployeeId == employeeId && contract.Status == contractStatus)
                .ToListAsync();

            // Get the payslip with the nearest paid_date
            PaySlip result = null;

            foreach (var employeeContract in listEmployeeContract)
            {
                var newestPaySlip = await _context.PaySlips
                    .Where(p => p.EmployeeContractId == employeeContract.Id)
                    .OrderByDescending(p => p.Paid_date)
                    .FirstOrDefaultAsync();
                
                if (newestPaySlip != null)
                {
                    if (result == null || newestPaySlip.Paid_date > result.Paid_date)
                    {
                        result = newestPaySlip;
                    }
                }
            }

            if (result != null)
            {
                total += (double)result.Company_Paid;
            }
        }

        return total;
    }

}
