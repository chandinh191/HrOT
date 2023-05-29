using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.CompanyContracts.Queries;
public class CompanyContractDto : IMapFrom<CompanyContract>
{
    public string? File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CompanyContractStatus? Status { get; set; }
}
