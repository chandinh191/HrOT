using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.CompanyContracts.Commands.Update;
public record UpdateCompanyContractCommand : IRequest
{
    public Guid Id { get; set; }
    public string? File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class UpdateCompanyContractCommandHandler : IRequestHandler<UpdateCompanyContractCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCompanyContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCompanyContractCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyContracts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CompanyContract), request.Id);
        }

        entity.File = request.File;
        entity.Salary = request.Salary;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
