using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Domain.Enums;

namespace hrOT.Domain.Entities;
public class LeaveLog : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double LeaveHours { get; set; }
    public string Reason { get; set; }
    public LeaveLogStatus Status { get; set; }
    public virtual Employee Employee { get; set; }
}
