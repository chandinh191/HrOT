using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Companys.Commands.DeleteCompany;

public record DeleteCompanyCommand(Guid Id) : IRequest;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCompanyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Companies
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}


