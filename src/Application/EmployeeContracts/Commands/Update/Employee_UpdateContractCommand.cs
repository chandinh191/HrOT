using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeContracts.Commands.Update;

public class Employee_UpdateContractCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid ContractId { get; set; }
    public EmployeeContractCommandDTO EmployeeContract { get; set; }

    public Employee_UpdateContractCommand(Guid contractId, Guid employeeId, EmployeeContractCommandDTO employeeContract)
    {
        ContractId = contractId;
        EmployeeId = employeeId;
        EmployeeContract = employeeContract;
    }
}

public class Employee_UpdateContractCommandHandler : IRequestHandler<Employee_UpdateContractCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_UpdateContractCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var employee =  _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefault();

        if (employee != null)
        {
            var contract =  _context.EmployeeContracts
                .Where(c => c.EmployeeId == employee.Id && c.Id == request.ContractId)
                .FirstOrDefault();

            if (contract != null)
            {
                contract.File = request.EmployeeContract.File;
                contract.StartDate = request.EmployeeContract.StartDate;
                contract.EndDate = request.EmployeeContract.EndDate;
                contract.Job = request.EmployeeContract.Job;
                contract.Salary = request.EmployeeContract.Salary;
                contract.CustomSalary = request.EmployeeContract.CustomSalary;
                contract.Number_Of_Dependents = request.EmployeeContract.Number_Of_Dependents;
                contract.Status = request.EmployeeContract.Status;
                contract.InsuranceType = request.EmployeeContract.InsuranceType;
                contract.SalaryType = request.EmployeeContract.SalaryType;
                contract.ContractType = request.EmployeeContract.ContractType;

                _context.EmployeeContracts.Update(contract);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        return false;
    }
}