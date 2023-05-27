using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeContracts.Commands.Delete;

public class Employee_DeleteContractCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid ContractId { get; set; }

    public Employee_DeleteContractCommand(Guid contractId, Guid employeeId)
    {
        EmployeeId = employeeId;
        ContractId = contractId;
    }
}

public class Employee_DeleteContractCommandHandler : IRequestHandler<Employee_DeleteContractCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public Employee_DeleteContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Employee_DeleteContractCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefault();

        if (employee != null)
        {
            var contract = await _context.EmployeeContracts
                .Where(c => c.EmployeeId == employee.Id && c.Id == request.ContractId)
                .FirstOrDefaultAsync();

            if (contract != null)
            {
                contract.IsDeleted = true;

                _context.EmployeeContracts.Update(contract);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
        return false;
    }
}