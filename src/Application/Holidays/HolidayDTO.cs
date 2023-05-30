using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Holidays;
public class HolidayDTO: IMapFrom<Holiday>
{
    public Guid CompanyId { get; set; }
    public string? DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }
}
