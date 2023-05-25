using hrOT.Application.Common.Interfaces;

namespace hrOT.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
