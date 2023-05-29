
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Holidays.Commands.DeleteHoliday;

public record DeleteHolidayCommand(Guid Id) : IRequest;

public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Holidays
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Holiday), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}


