using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.OvertimeLogs.Commands.Create;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Degrees.Commands.Create;

public record CreateDegreeCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public string Name { get; init; }
    //public DegreeStatus Status { get; init; }
}

public class CreateDegreeCommandHandler : IRequestHandler<CreateDegreeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateDegreeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDegreeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Degree();
        entity.EmployeeId = request.EmployeeId;
        entity.Name = request.Name;
        entity.CreatedBy = "test";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        _context.Degrees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
