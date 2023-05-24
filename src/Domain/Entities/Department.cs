using System.ComponentModel.DataAnnotations;
using LogOT.Domain.Entities;

namespace LogOT.Domain.Entities;

public class Department : BaseAuditableEntity
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public List<Employee>? Employees { get; set; }

    // Relationship
    public ICollection<Position>? Roles { get; set; }

    // Tổng nhân viên hiện có
    public int GetTotalEmployees()
    {
        if (Employees == null)
        {
            return 0;
        }

        return Employees.Count();
    }
}