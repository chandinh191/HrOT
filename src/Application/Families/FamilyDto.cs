using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Families;
public class FamilyDto : IMapFrom<Family>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }
    public int? NumberOfDependents { get; set; }
    public string? HomeTown { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Employee Employee { get; set; }
}
