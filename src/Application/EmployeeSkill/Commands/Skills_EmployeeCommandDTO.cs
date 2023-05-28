using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.EmployeeSkill.Commands;
public class Skills_EmployeeCommandDTO :IMapFrom<Skill_Employee>
{
    public string Level { get; set; }
}
