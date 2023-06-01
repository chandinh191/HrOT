using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.Families;
public class FamilyDto : IMapFrom<Family>
{
    public Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public string? HomeTown { get; set; }
}
