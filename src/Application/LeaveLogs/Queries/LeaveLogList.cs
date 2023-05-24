using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.OvertimeLogs.Queries;

namespace hrOT.Application.LeaveLogs.Queries;
public class LeaveLogList
{
    public IList<LeaveLogDto> Lists { get; set; } = new List<LeaveLogDto>();
}
