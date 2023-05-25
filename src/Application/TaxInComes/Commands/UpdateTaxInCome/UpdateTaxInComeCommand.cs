using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.TaxInComes.Commands.UpdateTaxInCome;
public record UpdateTaxInComeCommand : IRequest
{
    public Guid Id { get; init; }
    public double? Muc_chiu_thue { get; init; }
    public double? Thue_suat { get; init; }
}

public class UpdateTaxInComeCommandHandler : IRequestHandler<UpdateTaxInComeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaxInComeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTaxInComeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TaxInComes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Muc_chiu_thue = request.Muc_chiu_thue;
        entity.Thue_suat = request.Thue_suat;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Staff";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
