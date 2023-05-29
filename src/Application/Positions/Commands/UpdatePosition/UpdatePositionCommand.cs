using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Positions.Commands.UpdatePosition;

public record UpdatePositionCommand : IRequest
{
    public Guid Id { get; init; }
    public string? Name { get; init; }

}

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Positions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Level), request.Id);
        }
        
        entity.Name = request.Name;
       

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
