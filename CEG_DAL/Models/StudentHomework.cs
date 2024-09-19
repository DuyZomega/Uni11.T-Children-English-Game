using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class StudentHomework
{
    public int StudentHomeworkId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentProgressId { get; set; }

    public int TotalPoint { get; set; }

    public string? Status { get; set; }

    public int? Hours { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual StudentProgress StudentProgress { get; set; } = null!;
}
