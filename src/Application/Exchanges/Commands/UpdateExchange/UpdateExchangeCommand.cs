using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Exchanges.Commands.UpdateExchange;
public record UpdateExchangeCommand : IRequest<string>
{
    public Guid Id { get; init; }
    public double? Muc_quy_doi { get; init; }
    public double? Giam_tru { get; init; }
    public double? Thue_suat { get; init; }
}

public class UpdateExchangeCommandHandler : IRequestHandler<UpdateExchangeCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateExchangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateExchangeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Exchanges
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        } else if (entity.IsDeleted == true)
        {
            return "Bảng quy đổi này đã bị xóa";
        }

        entity.Muc_Quy_Doi = request.Muc_quy_doi;
        entity.Giam_Tru = request.Giam_tru;
        entity.Thue_Suat = request.Thue_suat;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";

        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}
