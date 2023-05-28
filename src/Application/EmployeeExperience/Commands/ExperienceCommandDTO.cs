using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.EmployeeExperience.Commands;
public class ExperienceCommandDTO : IMapFrom<Experience>
{
    public string NameProject { get; set; }

    public int TeamSize { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Description { get; set; }

    public string TechStack { get; set; }

    public bool Status { get; set; }
}
