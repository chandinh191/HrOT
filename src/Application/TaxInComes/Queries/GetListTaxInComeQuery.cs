using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Queries.GetTodos;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.TaxInComes.Queries;
public record GetListTaxInComeQuery : IRequest<List<TaxInCome>>;
public class GetListTaxInComeQueryHandler : IRequestHandler<GetListTaxInComeQuery, List<TaxInCome>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListTaxInComeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TaxInCome>> Handle(GetListTaxInComeQuery request, CancellationToken cancellationToken)
    {
        return await _context.TaxInComes
                .AsNoTracking()
                .OrderBy(t => t.Muc_chiu_thue)
                .ToListAsync(cancellationToken);
    }

}
