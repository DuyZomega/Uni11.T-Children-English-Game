using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentHomework
{
    public int StudentHomeworkId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentProcessId { get; set; }

    public int TotalPoint { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual StudentProcess StudentProcess { get; set; } = null!;
}
