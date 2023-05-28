using System.ComponentModel.DataAnnotations.Schema;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Common;
using hrOT.Domain.Entities;

namespace hrOT.Application.Experiences;

public class ExperienceDTO : IMapFrom<Experience>
{
    public Guid Id { get; set; }
    public string NameProject { get; set; }

    public int TeamSize { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Description { get; set; }

    public string TechStack { get; set; }

    public bool Status { get; set; }
}