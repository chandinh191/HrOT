using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.InterviewProcesses.Commands.Update;
public record UpdateInterviewProcessCommand : IRequest
{
    public Guid Id { get; init; }
    public DateTime DayTime { get; set; }
    public string Place { get; set; }
    public string FeedBack { get; set; }
    public InterviewProcessResult Result { get; set; }
}

public class UpdateInterviewProcessCommandHandler : IRequestHandler<UpdateInterviewProcessCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateInterviewProcessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.InterviewProcesses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(InterviewProcess), request.Id);
        }

        entity.DayTime = request.DayTime;
        entity.Place = request.Place;
        entity.FeedBack = request.FeedBack;
        entity.Result = request.Result;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}