using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Level : BaseAuditableEntity
{
    [ForeignKey("Position")]
    public Guid PositionId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    // Relationship
    public virtual Position? Position { get; set; }
}