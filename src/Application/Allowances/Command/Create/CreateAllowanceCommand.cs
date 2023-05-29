using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Allowances.Command.Create;

public record CreateAllowanceCommand : IRequest<Guid>
{
    //public Guid Id { get; init; }
    public Guid EmployeeContractId { get; init; }
    public string Name { get; init; }
    public AllowanceType Type { get; init; }
    public double Amount { get; init; }
    public string Eligibility_Criteria { get; init; }
    public string Requirements { get; init; }
}

    public class CreateAllowanceCommandHandler : IRequestHandler<CreateAllowanceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAllowanceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Allowance();
        entity.EmployeeContractId = request.EmployeeContractId;
        entity.Name = request.Name;
        entity.Type = request.Type;
        entity.Amount = request.Amount;
        entity.Eligibility_Criteria = request.Eligibility_Criteria;
        entity.Requirements = request.Requirements;
        //entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        //entity.LastModifiedBy = "Employee";

        _context.Allowances.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
