using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogOT.Domain.Entities;

public class OvertimeLog : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    // Tổng số giờ làm việc
    public double TotalHours
    {
        get { return EndDate.Subtract(StartDate).TotalHours; }
        set { }
    }

    // Trạng thái kiểm duyệt
    public OvertimeLogStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Employee? Employee { get; set; }
}