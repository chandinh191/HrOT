using System.ComponentModel.DataAnnotations;

namespace hrOT.Domain.Entities;

public class Skill : BaseAuditableEntity
{
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Skill name must contain only letters, numbers, and underscores")]
    public string SkillName { get; set; }

    [RegularExpression(@"^.{10,255}$", ErrorMessage = "Skill description must be between 10 and 255 characters")]
    public string Skill_Description { get; set; }

    // Relationship
    public ICollection<Skill_Employee>? Skill_Employees { get; set; }

    public ICollection<Skill_JD>? Skill_JDs { get; set; }
}