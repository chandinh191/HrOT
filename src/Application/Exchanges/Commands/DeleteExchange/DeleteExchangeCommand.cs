using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Exchanges.Commands.DeleteExchange;
public record DeleteExchangeCommand(Guid Id) : IRequest;

public class DeleteExchangeCommandHandler : IRequestHandler<DeleteExchangeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExchangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteExchangeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Exchanges
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.IsDeleted = true;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
