using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Levels.Commands.CreateLevel;

public record CreateLevelCommand : IRequest<Guid>
{
    public Guid RoleId { get; set; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class CreateLevelCommandHandler : IRequestHandler<CreateLevelCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLevelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = new Level();
        entity.RoleId = request.RoleId;
        entity.Name = request.Name;
        entity.Description = request.Description;

        _context.Levels.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
