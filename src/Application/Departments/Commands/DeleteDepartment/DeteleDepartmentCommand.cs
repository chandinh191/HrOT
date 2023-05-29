
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;


namespace hrOT.Application.Departments.Commands.DeleteDepartment;

public record DeteleDepartmentCommand(Guid Id) : IRequest;

public class DeteleDepartmentCommandHandler : IRequestHandler<DeteleDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeteleDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeteleDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        entity.IsDeleted = true;
       
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
      
    }
}

