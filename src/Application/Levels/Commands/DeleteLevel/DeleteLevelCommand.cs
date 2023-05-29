
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Levels.Commands.DeleteLevel;

public record DeleteLevelCommand(Guid Id) : IRequest;

public class DeleteLevelCommandHandler : IRequestHandler<DeleteLevelCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLevelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Levels
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Level), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}

