using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.InterviewProcesses.Commands.Create;
public record CreateInterviewProcessCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public Guid JobDescriptionId { get; init; }
    public DateTime DayTime { get; set; }
    public string Place { get; set; }
    public string FeedBack { get; set; }
}


public class CreateInterviewProcessCommandHandler : IRequestHandler<CreateInterviewProcessCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateInterviewProcessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var entity = new InterviewProcess();
        entity.EmployeeId = request.EmployeeId;
        entity.JobDescriptionId = request.JobDescriptionId;
        entity.DayTime = request.DayTime;
        entity.Place = request.Place;
        entity.FeedBack = request.FeedBack;
        entity.Result = InterviewProcessResult.Await;
        entity.CreatedBy = "Admin";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        _context.InterviewProcesses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
