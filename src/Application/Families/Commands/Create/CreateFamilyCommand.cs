﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Families.Commands.Create;
public record CreateFamilyCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public string? Name { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public Relationship Relationship { get; init; }
    public string? HomeTown { get; init; }
}

public class CreateFamilyCommandHandler : IRequestHandler<CreateFamilyCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Family();
        entity.EmployeeId = request.EmployeeId;
        entity.DateOfBirth = request.DateOfBirth;
        entity.Name = request.Name;
        entity.Relationship = request.Relationship;
        entity.HomeTown = request.HomeTown;
        entity.CreatedBy = "test";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "test";

        _context.Families.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
