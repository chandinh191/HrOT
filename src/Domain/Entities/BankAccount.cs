using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class BankAccount : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    [ForeignKey("Bank")]
    public Guid BankId { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }

    // Relationship
    public virtual Employee Employee { get; set; }
    public virtual Bank Bank { get; set; }
}
