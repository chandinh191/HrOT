using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Allowances.Command.Update;

public record UpdateAllowanceCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public Guid EmployeeContractId { get; init; }
    public string Name { get; init; }
    public AllowanceType Type { get; init; }
    public double Amount { get; init; }
    public string Eligibility_Criteria { get; init; }
    public string Requirements { get; init; }
    //public bool IsDeleted { get; init; }
}
public class UpdateAllowanceCommandHandler : IRequestHandler<UpdateAllowanceCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateAllowanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateAllowanceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Allowances
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Allowance), request.Id);
        } else if (entity.IsDeleted == true)
        {
            return "Khoảng trợ cấp này đã bị xóa!";
        }

        entity.EmployeeContractId = request.EmployeeContractId;
        entity.Name = request.Name;
        entity.Type = request.Type;
        entity.Amount = request.Amount;
        entity.Eligibility_Criteria = request.Eligibility_Criteria;
        entity.Requirements = request.Requirements;
        //entity.IsDeleted = request.IsDeleted;
        
        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}