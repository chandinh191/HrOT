using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Application.Holidays;
public class HolidayDTO
{
    public Guid CompanyId { get; set; }
    public string? DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }
}
