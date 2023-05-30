using System.ComponentModel.DataAnnotations;

namespace hrOT.Domain.Entities;

public class Position : BaseAuditableEntity
{
    [Required]
    public string Name { get; set; }

    // Relationship
    public ICollection<Level>? Levels { get; set; }

    public ICollection<Department>? Departments { get; set; }
}