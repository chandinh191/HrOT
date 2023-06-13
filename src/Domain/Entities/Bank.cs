using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class Bank : BaseAuditableEntity
{
    [Required]
    public string BankName { get; set; }
    public string Description { get; set; }

    // Relationship
    public IList<BankAccount> BankAccounts { get; private set; }
}
