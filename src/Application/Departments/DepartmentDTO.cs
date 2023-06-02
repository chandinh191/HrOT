using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Departments;
public class DepartmentDTO:IMapFrom<Department>
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
