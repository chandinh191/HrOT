using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Positions.Commands.CreatePosition;

public record CreatePositionCommand : IRequest<Guid>
{
    
    public string? Name { get; set; }
}

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Position();
      
        entity.Name = request.Name;

        _context.Positions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

