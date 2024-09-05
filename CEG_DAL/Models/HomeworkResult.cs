using System;
using System.Collections.Generic;

namespace CEG_DAL.Models;

public partial class HomeworkResult
{
    public int HomeworkResultId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentProcessId { get; set; }

    public int? TotalPoint { get; set; }

    public int? WordAmount { get; set; }

    public TimeOnly? Playtime { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual StudentProcess StudentProcess { get; set; } = null!;
}
