using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace hrOT.Domain.Entities;
public class Family: BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }
    public int? NumberOfDependents { get; set; }
    public string? HomeTown { get; set; }
    public virtual Employee Employee { get; set; }
}
