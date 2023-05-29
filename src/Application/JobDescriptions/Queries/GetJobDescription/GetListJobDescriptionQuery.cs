using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.JobDescriptions.Queries.GetJobDescription;

public record GetListJobDescriptionQuery : IRequest<List<JobDescriptionDTO>>;

public class GetListJobDescriptionQueryHandler : IRequestHandler<GetListJobDescriptionQuery, List<JobDescriptionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListJobDescriptionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<JobDescriptionDTO>> Handle(GetListJobDescriptionQuery request, CancellationToken cancellationToken)
    {
        return await _context.JobDescriptions
                 .ProjectTo<JobDescriptionDTO>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
    }
}


