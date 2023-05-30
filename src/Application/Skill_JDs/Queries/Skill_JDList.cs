using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.OvertimeLogs.Queries;

namespace hrOT.Application.Skill_JDs.Queries;
public class Skill_JDList
{
    public IList<Skill_JDDto> Lists { get; set; } = new List<Skill_JDDto>();
}
