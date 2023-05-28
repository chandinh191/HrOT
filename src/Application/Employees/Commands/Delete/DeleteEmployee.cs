using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Commands.Delete;
public record DeleteEmployee : IRequest
{
    public Guid Id { get; set; }
   
  
}

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployee request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
               .Include(e => e.ApplicationUser) // Include the ApplicationUser
               .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

       
        entity.IsDeleted = true;

        // Update ApplicationUser properties
       

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

