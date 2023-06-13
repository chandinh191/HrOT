using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Banks.Commands.Create;
public record CreateBankCommand : IRequest<Guid>
{
    public string BankName { get; init; }
    public string Description { get; set; }
}
public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateBankCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var entity = new Bank();
        entity.BankName = request.BankName;
        entity.Description = request.Description;
        entity.CreatedBy = "test";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        _context.Banks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
