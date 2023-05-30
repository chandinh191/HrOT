using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Companys.Commands.UpdateCompany;

public record UpdateCompanyCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? AccountEmail { get; set; }
    public string? Phone { get; set; }
    public string? HREmail { get; set; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCompanyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Companies
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Company), request.Id);
        }

        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.AccountEmail = request.AccountEmail;
        entity.Phone = request.Phone;
        entity.HREmail = request.HREmail;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


