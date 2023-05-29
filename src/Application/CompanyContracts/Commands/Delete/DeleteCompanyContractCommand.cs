using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.CompanyContracts.Commands.Delete;
public record DeleteCompanyContractCommand(Guid Id) : IRequest;

public class DeleteCompanyContractCommandHandler : IRequestHandler<DeleteCompanyContractCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCompanyContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCompanyContractCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyContracts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CompanyContract), request.Id);
        }

        entity.IsDeleted = true;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}