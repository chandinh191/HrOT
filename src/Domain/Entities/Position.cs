﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hrOT.Domain.Entities;

public class Position : BaseAuditableEntity
{
    [ForeignKey("Department")]
    public Guid DepartmentId { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    [Required]
    public string Name { get; set; }

    // Relationship
    public ICollection<Level>? Levels { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? Employee { get; set; }

}