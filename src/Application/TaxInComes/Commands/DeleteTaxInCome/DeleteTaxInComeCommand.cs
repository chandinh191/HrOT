using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.TaxInComes.Commands.DeleteTaxInCome;
public record DeleteTaxInComeCommand(Guid Id) : IRequest;

public class DeleteTaxInComeCommandHandler : IRequestHandler<DeleteTaxInComeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTaxInComeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTaxInComeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TaxInComes
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