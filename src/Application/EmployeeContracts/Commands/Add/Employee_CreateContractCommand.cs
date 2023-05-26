using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeContracts.Commands.Add;

public class Employee_CreateContractCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public EmployeeContractCommandDTO EmployeeContractDTO { get; set; }

    public Employee_CreateContractCommand(Guid employeeId, EmployeeContractCommandDTO employeeContractDTO)
    {
        EmployeeId = employeeId;
        EmployeeContractDTO = employeeContractDTO;
    }
}

public class Employee_CreateContractCommandHandler : IRequestHandler<Employee_CreateContractCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Employee_CreateContractCommand request, CancellationToken cancellationToken)
    {
        var employee =  _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefault();

        if (employee != null)
        {
            var contract = new EmployeeContract
            {
                Id = new Guid(),
                EmployeeId = request.EmployeeId,
                File = request.EmployeeContractDTO.File,
                StartDate = request.EmployeeContractDTO.StartDate,
                EndDate = request.EmployeeContractDTO.EndDate,
                Job = request.EmployeeContractDTO.Job,
                Salary = request.EmployeeContractDTO.Salary,
                Number_Of_Dependents = request.EmployeeContractDTO.Number_Of_Dependents,
                InsuranceType = request.EmployeeContractDTO.InsuranceType,
                CustomSalary = request.EmployeeContractDTO.CustomSalary,
                Status = EmployeeContractStatus.Effective,
                SalaryType = request.EmployeeContractDTO.SalaryType,
                ContractType = request.EmployeeContractDTO.ContractType,
            };
            await _context.EmployeeContracts.AddAsync(contract);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}