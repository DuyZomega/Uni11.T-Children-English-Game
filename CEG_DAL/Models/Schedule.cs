using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int SessionId { get; set; }

    public int ClassId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
