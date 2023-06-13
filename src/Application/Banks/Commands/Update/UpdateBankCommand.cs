using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Banks.Commands.Update;
public record UpdateBankCommand : IRequest<string>
{
    public Guid Id { get; init; }

    public string BankName { get; init; }
    public string Description { get; init; }
}
public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateBankCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Banks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bank), request.Id);
        }
        else if (entity.IsDeleted == true)
        {
            return "Ngân hàng đã bị xóa!";
        }
        entity.BankName = request.BankName;
        entity.Description = request.Description;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}
