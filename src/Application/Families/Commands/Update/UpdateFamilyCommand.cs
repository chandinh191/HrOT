using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Update;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Families.Commands.Update;
public record UpdateFamilyCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public string? FatherName { get; init; }
    public string? MotherName { get; init; }
    public int? NumberOfDependents { get; init; }
    public string? HomeTown { get; init; }
}

public class UpdateFamilyCommandHandler : IRequestHandler<UpdateFamilyCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Families
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Family), request.Id);
        }
        entity.EmployeeId = request.EmployeeId;
        entity.FatherName = request.FatherName;
        entity.MotherName = request.MotherName;
        entity.NumberOfDependents = request.NumberOfDependents;
        entity.HomeTown = request.HomeTown;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
