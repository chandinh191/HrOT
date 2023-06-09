using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Holidays.Commands.UpdateHoliday;

public record UpdateHolidayCommand : IRequest
{
    public Guid Id { get; init; }
    public string? DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }
}

public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Holidays
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Holiday), request.Id);
        }

        entity.DateName = request.DateName;
        entity.Day = request.Day;
        entity.HourlyPay = request.HourlyPay;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


