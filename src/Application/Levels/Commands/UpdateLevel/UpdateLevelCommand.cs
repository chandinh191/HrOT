using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Levels.Commands.UpdateLevel;

public record UpdateLevelCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid RoleId { get; set; }
    public string? Name { get; init; }

    public string? Description { get; init; }
}

public class UpdateLevelCommandHandler : IRequestHandler<UpdateLevelCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLevelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Levels
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Level), request.Id);
        }
        entity.RoleId =request.RoleId;
        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

