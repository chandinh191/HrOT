using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Departments.Commands.CreateDepartment;
public record CreateDepartmentCommand : IRequest<Guid>
{
    public Guid PositionId { get; set; }
    public Guid EmployeeId { get; set; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Department();
        //entity.PositionId = request.PositionId;
        //entity.EmployeeId = request.EmployeeId;   
        entity.Name = request.Name;
        entity.Description = request.Description;

        _context.Departments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
