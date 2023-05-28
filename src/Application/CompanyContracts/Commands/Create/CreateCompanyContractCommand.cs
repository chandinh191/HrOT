using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.CompanyContracts.Commands.Create;
public record CreateCompanyContractCommand : IRequest<Guid>
{
    public string? File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class CreateCompanyContractCommandHandler : IRequestHandler<CreateCompanyContractCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCompanyContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCompanyContractCommand request, CancellationToken cancellationToken)
    {
        var entity = new CompanyContract();
        entity.File = request.File;
        entity.Salary = request.Salary;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.CreatedBy = "Admin";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        _context.CompanyContracts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
