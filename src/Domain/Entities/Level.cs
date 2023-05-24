using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Level : BaseAuditableEntity
{
    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    // Relationship
    public virtual Position? Role { get; set; }
}