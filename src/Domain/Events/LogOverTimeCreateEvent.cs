using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Events;
public class LogOverTimeCreateEvent
{
    public LogOverTimeCreateEvent(OvertimeLog item)
    {
        Item = item;
    }

    public OvertimeLog Item { get; }
}
