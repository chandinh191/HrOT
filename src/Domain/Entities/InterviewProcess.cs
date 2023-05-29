using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class InterviewProcess : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [ForeignKey("JobDescription")]
    public Guid JobDescriptionId { get; set; }

    public DateTime DayTime { get; set; }
    public string Place { get; set; }
    public string FeedBack { get; set; }
    public InterviewProcessResult Result { get; set; }

    // Relationship
    public virtual JobDescription JobDescription { get; set; }

    public virtual Employee Employee { get; set; }
}