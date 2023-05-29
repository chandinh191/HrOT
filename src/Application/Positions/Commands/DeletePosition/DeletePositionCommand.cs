
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Positions.Commands.DeletePosition;

public record DeletePositionCommand(Guid Id) : IRequest;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Positions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Position), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}


