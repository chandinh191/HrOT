using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Commands.CreateTodoList;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.OvertimeLogs.Commands.Create;
public record Employee_CreateOvertimeLogCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double TotalHours { get; init; }
}


public class Employee_CreateOvertimeLogCommandHandler : IRequestHandler<Employee_CreateOvertimeLogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(Employee_CreateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = new OvertimeLog();
        entity.EmployeeId = request.EmployeeId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.TotalHours = request.TotalHours;
        entity.Status = OvertimeLogStatus.Pending;
        entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Employee";

        _context.OvertimeLogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}