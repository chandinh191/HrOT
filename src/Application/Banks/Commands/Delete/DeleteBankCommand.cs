using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Banks.Commands.Delete;
public record DeleteBankCommand(Guid Id) : IRequest;
public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand>
{
    private readonly IApplicationDbContext _context;


    public DeleteBankCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Banks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bank), request.Id);
        }

        entity.IsDeleted = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
