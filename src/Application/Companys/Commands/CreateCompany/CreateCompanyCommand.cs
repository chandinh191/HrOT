using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Companys.Commands.CreateCompany;

public record CreateCompanyCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? AccountEmail { get; set; }
    public string? Phone { get; set; }
    public string? HREmail { get; set; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCompanyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Company();

        entity.Name = request.Name;
        entity.Address = request.Address;
        entity.AccountEmail = request.AccountEmail;
        entity.Phone = request.Phone;
        entity.HREmail = request.HREmail;
        _context.Companies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

