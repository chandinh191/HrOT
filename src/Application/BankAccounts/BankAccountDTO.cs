using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.BankAccounts;
public class BankAccountDTO : IMapFrom<BankAccount>
{
    [ForeignKey("Bank")]
    public Guid BankID { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }

    //Relationship
    public virtual Bank? Bank { get; set; }
}
