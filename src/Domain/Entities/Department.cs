using System.ComponentModel.DataAnnotations;

namespace hrOT.Domain.Entities;

public class Department : BaseAuditableEntity
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    // Relationship
    public ICollection<Position>? Positionss { get; set; }
}