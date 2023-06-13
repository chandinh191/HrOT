using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.BankAccounts.Commands.Delete;
public record DeleteBankAccountCommand : IRequest
{
    public Guid Id { get; init; }
}
public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand>
{
    private readonly IApplicationDbContext _context;


    public DeleteBankAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<Unit> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BankAccounts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(BankAccount), request.Id);
        }

        entity.IsDeleted = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
