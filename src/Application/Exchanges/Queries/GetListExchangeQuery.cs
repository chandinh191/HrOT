using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Exchanges.Queries;
public record GetListExchangeQuery : IRequest<List<Exchange>>;
public class GetListExchangeQueryHandler : IRequestHandler<GetListExchangeQuery, List<Exchange>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListExchangeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Exchange>> Handle(GetListExchangeQuery request, CancellationToken cancellationToken)
    {
        return await _context.Exchanges
                .AsNoTracking()
                .OrderBy(t => t.Muc_Quy_Doi)
                .ToListAsync(cancellationToken);
    }

}
