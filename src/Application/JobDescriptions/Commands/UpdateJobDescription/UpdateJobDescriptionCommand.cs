using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.JobDescriptions.Commands.UpdateJobDescription;

public record UpdateJobDescriptionCommand : IRequest
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
}

public class UpdateJobDescriptionCommandHandler : IRequestHandler<UpdateJobDescriptionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateJobDescriptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateJobDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.JobDescriptions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(JobDescription), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


