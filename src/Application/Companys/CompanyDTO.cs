using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Companys;
public class CompanyDTO : IMapFrom<Company>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? AccountEmail { get; set; }
    public string? Phone { get; set; }
    public string? HREmail { get; set; }
}
