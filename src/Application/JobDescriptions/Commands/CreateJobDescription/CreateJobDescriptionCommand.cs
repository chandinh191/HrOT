using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.JobDescriptions.Commands.CreateJobDescription;

public record CreateJobDescriptionCommand : IRequest<Guid>
{
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
}

public class CreateJobDescriptionCommandHandler : IRequestHandler<CreateJobDescriptionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateJobDescriptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateJobDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = new JobDescription();

        entity.CompanyId = request.CompanyId;
        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Status = request.Status;

        _context.JobDescriptions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

