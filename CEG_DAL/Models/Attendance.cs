using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int ScheduleId { get; set; }

    public int StudentId { get; set; }

    public string HasAttended { get; set; } = null!;

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
