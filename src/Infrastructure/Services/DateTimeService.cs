using LogOT.Application.Common.Interfaces;

namespace LogOT.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
