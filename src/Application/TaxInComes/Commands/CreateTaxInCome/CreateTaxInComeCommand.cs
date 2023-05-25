using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.TaxInComes.Commands.CreateTaxInCome;
public record CreateTaxInComeCommand : IRequest<Guid>
{
    public double? Muc_chiu_thue { get; init; }
    public double? Thue_suat { get; init; }
}

public class CreateTaxInComeCommandHandler : IRequestHandler<CreateTaxInComeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTaxInComeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTaxInComeCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaxInCome();

        entity.Muc_chiu_thue = request.Muc_chiu_thue;
        entity.Thue_suat = request.Thue_suat;
        entity.CreatedBy = "Staff";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";
        _context.TaxInComes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
