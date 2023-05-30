using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid PositionId { get; set; }
    public Guid EmployeeId { get; set; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

