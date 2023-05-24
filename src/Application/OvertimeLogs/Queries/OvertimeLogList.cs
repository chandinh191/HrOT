using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.TodoLists.Queries.GetTodos;

namespace hrOT.Application.OvertimeLogs.Queries;
public class OvertimeLogList
{
    public IList<OvertimeLogDto> Lists { get; set; } = new List<OvertimeLogDto>();
}
