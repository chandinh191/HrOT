using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.InterviewProcesses.Queries;
public record GetListInterviewProcessByJobIdQuery(Guid JobId) : IRequest<List<InterviewProcessDto>>;

public class GetListInterviewProcessByJobIdQueryHandler : IRequestHandler<GetListInterviewProcessByJobIdQuery, List<InterviewProcessDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListInterviewProcessByJobIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<InterviewProcessDto>> Handle(GetListInterviewProcessByJobIdQuery request, CancellationToken cancellationToken)
    {
        var interviewProcesses = await _context.InterviewProcesses
            .Where(x => x.JobDescriptionId == request.JobId)
            .ToListAsync(cancellationToken);
        if (interviewProcesses == null)
        {
            return null;
        }
        var employeeIds = interviewProcesses.Select(x => x.EmployeeId).ToList();
        var employees = await _context.Employees
            .Include(e => e.ApplicationUser)
            .Where(x => employeeIds.Contains(x.Id))
        .ToListAsync(cancellationToken);

        var interviewProcessDtos = _mapper.Map<List<InterviewProcessDto>>(interviewProcesses);

        // Map thông tin name vào từ ApplicationUser
        foreach (var InterviewProcessDto in interviewProcessDtos)
        {
            var employee = employees.FirstOrDefault(x => x.Id == InterviewProcessDto.EmployeeId);
            if (employee != null)
            {
                InterviewProcessDto.EmployeeName = employee.ApplicationUser.Fullname;
            }
        }
        return interviewProcessDtos;
    }
}
