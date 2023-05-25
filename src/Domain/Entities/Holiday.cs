using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Holiday : BaseAuditableEntity
{
    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public string DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }

    //Relationship
    public virtual Company Company { get; set; }
}