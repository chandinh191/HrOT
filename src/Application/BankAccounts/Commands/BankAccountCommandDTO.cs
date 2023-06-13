using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.BankAccounts.Commands;
public class BankAccountCommandDTO : IMapFrom<BankAccount>
{
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
}
