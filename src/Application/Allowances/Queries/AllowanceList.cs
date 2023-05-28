using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.OvertimeLogs.Queries;

namespace hrOT.Application.Allowances.Queries;
public class AllowanceList
{
    public IList<AllowanceDto> Lists { get; set; } = new List<AllowanceDto>();
}
