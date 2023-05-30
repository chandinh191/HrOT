﻿using System;
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


namespace hrOT.Application.Degrees.Commands.Update;
public record UpdateDegreeCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public string Name { get; init; }
    public DegreeStatus Status { get; init; }
}

public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDegreeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDegreeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Degrees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Degree), request.Id);
        }
        entity.Name = request.Name;
        entity.Status = request.Status;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
