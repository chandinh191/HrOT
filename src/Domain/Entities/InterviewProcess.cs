using System.ComponentModel.DataAnnotations.Schema;

namespace LogOT.Domain.Entities;

public class InterviewProcess : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [ForeignKey("JobDescription")]
    public Guid JobDescriptionId { get; set; }

    public string Info { get; set; }
    public string DayTime { get; set; }
    public string Place { get; set; }
    public bool Status { get; set; }
    public string FeedBack { get; set; }
    public string Result { get; set; }

    // Relationship
    public virtual JobDescription JobDescription { get; set; }

    public virtual Employee Employee { get; set; }
}