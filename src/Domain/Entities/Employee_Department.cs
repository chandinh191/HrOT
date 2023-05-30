using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Employee_Department : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [ForeignKey("Department")]
    public Guid DepartmentId { get; set; }

    // Relationship
    public virtual Employee Employee { get; set; }

    public virtual Department Department { get; set; }
}