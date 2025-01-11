using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int SessionId { get; set; }

    public int ClassId { get; set; }

    public DateTime? ScheduleDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Class Class { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
