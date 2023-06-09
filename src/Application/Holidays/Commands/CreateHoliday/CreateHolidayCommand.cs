using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Holidays.Commands.CreateHoliday;

public record CreateHolidayCommand : IRequest<Guid>
{
    public Guid CompanyId { get; set; }
    public string? DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }
}

public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        var entity = new Holiday();

        entity.CompanyId = request.CompanyId;
        entity.DateName = request.DateName;
        entity.Day = request.Day;
        entity.HourlyPay = request.HourlyPay;

        _context.Holidays.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

