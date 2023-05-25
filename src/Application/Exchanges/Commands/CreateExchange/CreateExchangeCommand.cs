using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Exchanges.Commands.CreateExchange;
public record CreateExchangeCommand : IRequest<Guid>
{
    public double? Muc_quy_doi { get; init; }
    public double? Giam_tru { get; init; }
    public double? Thue_suat { get; init; }
}

public class CreateExchangeCommandHandler : IRequestHandler<CreateExchangeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateExchangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateExchangeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Exchange();

        entity.Muc_Quy_Doi = request.Muc_quy_doi;
        entity.Giam_Tru = request.Giam_tru;
        entity.Thue_Suat = request.Thue_suat;
        entity.CreatedBy = "Staff";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";
        _context.Exchanges.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
