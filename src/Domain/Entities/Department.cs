using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Department : BaseAuditableEntity
{
    [ForeignKey("Position")]
    public Guid PositionId { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    // Relationship
    public virtual Position? Position { get; set; }

    public virtual Employee? Employee { get; set; }
}