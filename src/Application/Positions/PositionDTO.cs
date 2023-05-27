using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Application.Positions;
public class PositionDTO
{
   
    public Guid DepartmentId { get; set; }

    public Guid EmployeeId { get; set; }

    public string Name { get; set; }
}
